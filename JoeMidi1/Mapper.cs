using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Midi;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Threading;
using SharpOSC;
using NLua;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Xml.Serialization;

namespace JoeMidi1
{
    public class Mapper
    {
        // Class that actually does all the mide re-mapping defined by Mapping (and destroys any sense of abstraction implied by the other 
        //  Mapping and SoundGenerator classes).
        //  Really needs refactoring, now that I know what this does.

        public Configuration configuration = new Configuration();

        // Contains the current set of mappings to be done for each midi stream (aka Device-Channel), keyed by DeviceName_Channel.
        public Dictionary<String, Mapping.PerDeviceChannelMapping> m_perDeviceChannelMappings = new Dictionary<String, Mapping.PerDeviceChannelMapping>();

        // Stores the current set of notes sounding, both their original and mapped values.
        List<MappedNote> m_mappedNotesList = new List<MappedNote>();

        // Associates a set of midi program changes, sent by the controller's single-press program change buttons, with 8 "logical" button IDs (implied by array index).
        const int NUM_PROGRAM_BUTTONS = 8;
        int[] m_programButtonLastProgramNo = new int[NUM_PROGRAM_BUTTONS];
        int m_lastRequestedProgramNo = -1;

        public int masterTranspose = 0;

        // Notify the interested (UI) of midi program changes requested of the mapper
        public delegate void MidiProgramChange(int programNumber);
        public MidiProgramChange midiProgramChangeNotification = null;

        // Notify the interested (UI) of midi programs activated by the mapper
        public delegate void MidiProgramActivated(MidiProgram midiProgram);
        public MidiProgramActivated midiProgramActivatedNotification = null;

        SharpOSC.UDPSender oscSender = null;
        SharpOSC.UDPSender oscSenderLocalhost = null;

        const int MAX_SENDS = 4;

        public Mapper()
        {
            ConfigurationSubDirectory = "JoeMidi";
            for (int i = 0; i < NUM_PROGRAM_BUTTONS; ++i)
            {
                m_programButtonLastProgramNo[i] = i;
            }
        }

        IList<ChannelMessage> parseLuaReturn(Object luaReturn, ChannelMessage originalMsg)
        {
            // Expecting the luaReturn to be a table of tables.  Each outer table value is a midi message.
            //  Inner tables contain 3 or 4 longs each, representing an actual message: type, ch, d0 and optionally d1.

            IList<ChannelMessage> returnMessages = new List<ChannelMessage>();

            if (luaReturn is Object[] && ((Object[])luaReturn).Length >= 1 && ((Object[])luaReturn)[0] is LuaTable)
            {
                LuaTable luaReturnOuter = ((LuaTable)((Object[])luaReturn)[0]);
                foreach (var inner in luaReturnOuter.Values)
                {
                    if (inner is LuaTable && ((LuaTable)inner).Values.Count >= 3)   // midiType, ch, d0
                    {
                        var luaReturnInner = ((IEnumerable<object>)((LuaTable)inner).Values).ToList();
                        if (luaReturnInner[0] is long && luaReturnInner[1] is long && luaReturnInner[2] is long)
                        {
                            long midiType = (long)luaReturnInner[0];
                            long ch = (long)luaReturnInner[1];
                            long d0 = (long)luaReturnInner[2];
                            if (ch >= 0 && ch < 16 && d0 >= 0 && d0 < 128) 
                            {
                                if ((midiType == 0x90 || midiType == 0x80 || midiType == 0xB0)
                                    && luaReturnInner.Count >= 4    // midiType, ch, d0, d1  
                                    && luaReturnInner[3] is long)
                                {
                                    long d1 = (long)luaReturnInner[3];
                                    if (d1 >= 0 && d1 < 128)
                                    {
                                        if (midiType == 0x90)
                                        {
                                            returnMessages.Add(new NoteOnMessage(originalMsg.Device, (Channel)ch, (Pitch)d0, (int)d1, originalMsg.Time));
                                        }
                                        else if (midiType == 0x80)
                                        {
                                            returnMessages.Add(new NoteOffMessage(originalMsg.Device, (Channel)ch, (Pitch)d0, (int)d1, originalMsg.Time));
                                        }
                                        else if (midiType == 0xB0)
                                        {
                                            returnMessages.Add(new ControlChangeMessage(originalMsg.Device, (Channel)ch, (Midi.Control)d0, (int)d1, originalMsg.Time));
                                        }
                                    }
                                }
                                else if (midiType == 0xC0)
                                {
                                    returnMessages.Add(new ProgramChangeMessage(originalMsg.Device, (Channel)ch, (Instrument)d0, originalMsg.Time));
                                }
                            }
                        }
                    }
                }
            }
            return returnMessages;
        }

        public void NoteOn(NoteOnMessage msg)
        {
            // Find the per-device/channel mappings for the message's Device/Channel.  If there's none, nothing to do.
            String deviceKey = Mapping.PerDeviceChannelMapping.createKey(msg.Device.Name, (int)msg.Channel);
            if (!m_perDeviceChannelMappings.ContainsKey(deviceKey))
            {
                return;
            }
            Mapping.PerDeviceChannelMapping perDeviceChannelMapping = m_perDeviceChannelMappings[deviceKey];

            if (perDeviceChannelMapping.noteOnLuaFunction != null)
            {
                var luaReturnRaw = perDeviceChannelMapping.noteOnLuaFunction.Call((int)msg.Channel, (int)msg.Pitch, msg.Velocity);
                IList<ChannelMessage> luaReturnMidiMessages = parseLuaReturn(luaReturnRaw, msg);
                handleChannelMessages(luaReturnMidiMessages, perDeviceChannelMapping);
            }
            else
            {
                HandleNoteOn(msg, perDeviceChannelMapping);
            }
        }

        void handleChannelMessages(IList<ChannelMessage> msgs, Mapping.PerDeviceChannelMapping perDeviceChannelMapping)
        {
            foreach (ChannelMessage msg in msgs)
            {
                if (msg is NoteOnMessage)
                {
                    HandleNoteOn((NoteOnMessage)msg, perDeviceChannelMapping);
                }
                else if (msg is NoteOffMessage)
                {
                    HandleNoteOff((NoteOffMessage)msg);
                }
                else if (msg is ControlChangeMessage)
                {
                    HandleCC((ControlChangeMessage)msg, perDeviceChannelMapping);
                }
                else if (msg is PitchBendMessage)
                {
                    HandlePB((PitchBendMessage)msg, perDeviceChannelMapping);
                }
                else if (msg is ProgramChangeMessage)
                {
                    HandlePC((ProgramChangeMessage)msg);
                }
            }
        }

        void HandleNoteOn(NoteOnMessage msg, Mapping.PerDeviceChannelMapping perDeviceChannelMapping)
        {
            // Iterate over the NoteMappings for this device/channel
            foreach (NoteMapping mapping in perDeviceChannelMapping.noteMappings)
            {
                // See if the note received is in range for the NoteMapping currently under consideration
                if (msg.Pitch >= (Pitch)mapping.lowestNote && msg.Pitch <= (Pitch)mapping.highestNote)
                {
                    // It is.
                    // Create mapped note record 
                    MappedNote mappedNoteRecord = new MappedNote();
                    mappedNoteRecord.sourceDeviceName = msg.Device.Name;
                    mappedNoteRecord.sourceChannel = msg.Channel;
                    mappedNoteRecord.origNote = msg.Pitch;
                    SoundGenerator soundGenerator = mapping.soundGenerator;
                    mappedNoteRecord.mappedDevice = soundGenerator.device;
                    mappedNoteRecord.mappedChannel = (Channel)mapping.soundGeneratorPhysicalChannel;
                    mappedNoteRecord.mappedNote = msg.Pitch + mapping.pitchOffset + masterTranspose;

                    if (mappedNoteRecord.mappedNote < 0 || mappedNoteRecord.mappedNote > (Pitch)127)
                    {
                        continue;
                    }

                    // See if this note is already sounding.  Look it up based on it's unmapped device, channel and note#.
                    MappedNote matchingMappedNoteAlreadySounding = FindMappedNote(mappedNoteRecord);
                    if (matchingMappedNoteAlreadySounding != null)
                    {
                        matchingMappedNoteAlreadySounding.mappedDevice.SendNoteOff(matchingMappedNoteAlreadySounding.mappedChannel, matchingMappedNoteAlreadySounding.mappedNote, 127);
                        m_mappedNotesList.Remove(matchingMappedNoteAlreadySounding);
                    }

                    // Now, play the new mapping of the source note.
                    mappedNoteRecord.mappedDevice.SendNoteOn(mappedNoteRecord.mappedChannel, mappedNoteRecord.mappedNote, msg.Velocity);

                    // And add it to the dictionary of sounding notes.
                    m_mappedNotesList.Add(mappedNoteRecord);

                }
            }
        }

        public void NoteOff(NoteOffMessage msg)
        {
            // Find the per-device/channel mappings for the message's Device/Channel.  If there's none, nothing to do.
            String deviceKey = Mapping.PerDeviceChannelMapping.createKey(msg.Device.Name, (int)msg.Channel);
            if (!m_perDeviceChannelMappings.ContainsKey(deviceKey))
            {
                return;
            }
            Mapping.PerDeviceChannelMapping perDeviceChannelMapping = m_perDeviceChannelMappings[deviceKey];

            if (perDeviceChannelMapping.noteOffLuaFunction != null)
            {
                var luaReturnRaw = perDeviceChannelMapping.noteOffLuaFunction.Call((int)msg.Channel, (int)msg.Pitch, msg.Velocity);
                IList<ChannelMessage> luaReturnMidiMessages = parseLuaReturn(luaReturnRaw, msg);
                handleChannelMessages(luaReturnMidiMessages, perDeviceChannelMapping);
            }
            else
            {
                HandleNoteOff(msg);
            }
        }

        public void HandleNoteOff(NoteOffMessage msg)
        {
            // This note should be in the mapped notes dict from it's note-on.  Build the key to find what it was mapped to.
            String sourceDeviceName = msg.Device.Name;
            Channel sourceChannel = msg.Channel;
            Pitch origNote = msg.Pitch;

            // Look up this note in the mapped notes dict to find what it was mapped to
            List<MappedNote> mappedNotesAlreadySounding = FindMappedNote(sourceDeviceName, sourceChannel, origNote);
            if (mappedNotesAlreadySounding.Count > 0)
            {
                foreach(MappedNote noteToSilence in mappedNotesAlreadySounding)
                {
                    // Send a note off for what it was mapped to, and remove this entry from the mapped notes dict.
                    noteToSilence.mappedDevice.SendNoteOff(noteToSilence.mappedChannel, noteToSilence.mappedNote, msg.Velocity);
                    m_mappedNotesList.Remove(noteToSilence);
                }
            }
            else
            {
                // No mapping in the mapped notes dict...  worth logging, but I'm not sure about the overhead of doing that in a midi event handler.
            }
        }

        public void ProgramChange(ProgramChangeMessage msg)
        {
            // Find the per-device/channel mappings for the message's Device/Channel.  If there's none, nothing to do.
            String deviceKey = Mapping.PerDeviceChannelMapping.createKey(msg.Device.Name, (int)msg.Channel);
            if (!m_perDeviceChannelMappings.ContainsKey(deviceKey))
            {
                return;
            }
            Mapping.PerDeviceChannelMapping perDeviceChannelMapping = m_perDeviceChannelMappings[deviceKey];

            if (perDeviceChannelMapping.pcLuaFunction != null)
            {
                var luaReturnRaw = perDeviceChannelMapping.pcLuaFunction.Call((int)msg.Channel, (int)msg.Instrument);
                IList<ChannelMessage> luaReturnMidiMessages = parseLuaReturn(luaReturnRaw, msg);
                handleChannelMessages(luaReturnMidiMessages, perDeviceChannelMapping);
            }
            else
            {
                HandlePC(msg);
            }
        }

        void HandlePC(ProgramChangeMessage msg) { 
            int requestedProgramNo = (int)msg.Instrument;
            if (midiProgramChangeNotification != null)
            {
                midiProgramChangeNotification(requestedProgramNo);
            }
        }

        public void RotatingProgramChange(int requestedProgramNo)
        {
            // This simulates my PX3 controllers ability to cycle through a set of related programs with subsequent pushes of a single-push program 
            //  select button, which I always though was convenient.  It will cycle through a column of program in the current Random Access grid.

            int programNoToSwitchTo = -1;

            // Figure out what programNoToSwitchTo:

            
            if (requestedProgramNo >= 0 && requestedProgramNo < NUM_PROGRAM_BUTTONS)
            {
                // Program Change 0 - NUM_PROGRAM_BUTTONS-1 (typically 8) are handled specially to implement a friendly live-perf model
                //  similar to that of my Casio PX3.

                if (m_lastRequestedProgramNo != requestedProgramNo)     // Detect consecutive presses of the same button
                {
                    // First consecutive press of this button.  Switch to whatever sound was last selected with this button.
                    programNoToSwitchTo = m_programButtonLastProgramNo[requestedProgramNo];

                    // And remember that this button was pressed last, for next time's consecutive button press detection
                    m_lastRequestedProgramNo = requestedProgramNo;

                }
                else {
                    // Second or higher consecutive press.  Rotate to the next sound for this button.
                    int prospectiveProgramNo = m_programButtonLastProgramNo[requestedProgramNo] + NUM_PROGRAM_BUTTONS;
                    if (configuration.midiPrograms.ContainsKey(prospectiveProgramNo))
                    {
                        // Good: there's another program no for us to rotate to with this button.  Let's use it... 
                        programNoToSwitchTo = prospectiveProgramNo;

                        // ...and remember it as last program for this button.
                        m_programButtonLastProgramNo[requestedProgramNo] = prospectiveProgramNo;
                    }
                    else
                    {
                        // No more programs defined for us to rotate to with this button.  Rotate back to the beginning.
                        programNoToSwitchTo = requestedProgramNo;

                        // ...and remember it as last program for this button.
                        m_programButtonLastProgramNo[requestedProgramNo] = requestedProgramNo;
                    }
                }
            }
            else
            {
                // Anything above the buttons (typically 1-8) just use verbatim.
                programNoToSwitchTo = requestedProgramNo;
            }

            ProgramChange(programNoToSwitchTo);

        }


        public void ProgramChange(int programNoToSwitchTo) 
        {
            // Select the mapping that matches the requested bank/program#
            if (configuration.midiPrograms.ContainsKey(programNoToSwitchTo))
            {
                MidiProgram midiProgramToActivate = configuration.midiPrograms[programNoToSwitchTo];
                
                SetMapping(midiProgramToActivate.mapping);

                if (midiProgramActivatedNotification != null)
                {
                    midiProgramActivatedNotification(midiProgramToActivate);
                }
            }
        }

        public void SetMapping(Mapping mappingToActivate, double volumeTweak = 0.0) 
        {
            // Sets what Mapping this Mapper will use to do remapping.

            // Step through the per-device/channel mappings contained in this mapping and add/replace them into the mapper's dict.
            //  This effectively merges the new mapping into any existing mappings on a per-device/channel basis. 
            //   ie, if the new mapping only has a mapping for one device-channel in it then the other current mappings
            //   in the mapper for other device/channels remain in effect.
            foreach (Mapping.PerDeviceChannelMapping perDeviceChannelMapping in mappingToActivate.perDeviceChannelMappings.Values)
            {
                if (m_perDeviceChannelMappings.ContainsKey(perDeviceChannelMapping.key))
                {
                    var mappingToBeReplaced = m_perDeviceChannelMappings[perDeviceChannelMapping.key];

                    if (configuration.disableUnusedVSTIs == true)
                    {
                        foreach (NoteMapping noteMapping in mappingToBeReplaced.noteMappings)
                        {
                                DeferredDisableSoundGenerator(ref noteMapping.soundGenerator, 10);
                        }
                    }
                    m_perDeviceChannelMappings[perDeviceChannelMapping.key] = perDeviceChannelMapping;
                }
                else
                {
                    m_perDeviceChannelMappings.Add(perDeviceChannelMapping.key, perDeviceChannelMapping);
                }
            }

            // Step through the new per-device/channel mappings and send any program/bank/OSC/control changes required to effect them on the synths.
            foreach (Mapping.PerDeviceChannelMapping perDeviceChannelMapping in mappingToActivate.perDeviceChannelMappings.Values)
            {
                // Enable the VSTis needed
                foreach (NoteMapping noteMapping in perDeviceChannelMapping.noteMappings)
                {
                    cancelAnyPendingDisablesOfThisSoundGenerator(ref noteMapping.soundGenerator);
                    noteMapping.soundGenerator.EnableVSTi(this);
                }
                Thread.Sleep(100);   // Give OSC/Reaper a chance to actually enable the VSTI before moving on.  Empirically sufficient on current HP Envy.

                // Send out the mapping's registered bank/program change/OSC messages to each of that Mapping's Sound Generators.
                foreach (MappingPatch mappingPatch in perDeviceChannelMapping.mappingPatches)
                {
                    if (mappingPatch.bank >= 0)
                    {
                        int msb = mappingPatch.bank / 128;
                        mappingPatch.soundGenerator.device.SendControlChange((Channel)mappingPatch.soundGeneratorPhysicalChannel, (Midi.Control)0, msb);

                        int lsb = mappingPatch.bank % 128;
                        mappingPatch.soundGenerator.device.SendControlChange((Channel)mappingPatch.soundGeneratorPhysicalChannel, (Midi.Control)32, lsb);
                    }

                    if (mappingPatch.patchNumber >= 0 && mappingPatch.patchNumber < 128)
                    {
                        mappingPatch.soundGenerator.device.SendProgramChange((Channel)mappingPatch.soundGeneratorPhysicalChannel, (Instrument)mappingPatch.patchNumber);
                    }

                    if (mappingPatch.track != null && mappingPatch.track.Length > 0)
                    {
                        // This silently requires numeric track references prefixed with #, and fxPresets of the form fxSlotNum:presetName.  Preset name "disabled" reserved to bypass the FX.
                        if (mappingPatch.track.StartsWith("#"))
                        {
                            int trackNum = 0;
                            if (int.TryParse(mappingPatch.track.Substring(1), out trackNum) && trackNum > 0)
                            {
                                // Enable/Disable the track's FX, and select the mapping-specified preset 
                                foreach (int fxSlotNum in mappingPatch.fxPresets.Keys)
                                {
                                    String presetName = mappingPatch.fxPresets[fxSlotNum];
                                    if (presetName.ToLower() == "disabled")
                                    {
                                        // Disable FX
                                        var oscMessage = new SharpOSC.OscMessage(String.Format("/track/{0}/fx/{1}/bypass", trackNum, fxSlotNum), 0);
                                        SendOSC(oscMessage);
                                    }
                                    else
                                    {
                                        // Enable FX and select specified preset
                                        var oscMessage = new SharpOSC.OscMessage(String.Format("/track/{0}/fx/{1}/bypass", trackNum, fxSlotNum), 1);
                                        SendOSC(oscMessage);
                                        // Select Preset
                                        oscMessage = new SharpOSC.OscMessage(String.Format("/track/{0}/fx/{1}/preset", trackNum, fxSlotNum), presetName);
                                        SendOSC(oscMessage);
                                    }
                                }

                                // Set the track volume with the mapping patch volume.  (MappingPatch volume is the SG
                                // default volume possibly overridden by the SG Patch vol. VolumeTweak to support
                                // SongRelativeVolume adjustment.)
                                if (mappingPatch.volume != null)
                                {
                                    var volume = (System.Single)(mappingPatch.volume + volumeTweak);
                                    var oscMessage = new SharpOSC.OscMessage(String.Format("/track/{0}/volume/db", trackNum), volume);
                                    SendOSC(oscMessage);
                                }

                                // Set the track send volumes to send the track to its mapping-specified submix send.
                                for (int i = 1; i <= MAX_SENDS; ++i)
                                {
                                    var sendVolume =
                                        (System.Single)((i == mappingPatch.send || (mappingPatch.send == null && i == 1)) ? 0.715 : 0.0);
                                    var oscMessage = new SharpOSC.OscMessage(String.Format("/track/{0}/send/{1}/volume", trackNum, i), sendVolume);
                                    SendOSC(oscMessage);
                                }
                            }
                        }
                    }
                }

                // Send out initial values for all mapped controls that have them
                foreach (ControlMapping controlMapping in perDeviceChannelMapping.controlMappings)
                {
                    Thread.Sleep(5);       // Some weird race condition exists.  Without a pause vol is always 100%

                    if (controlMapping.mappedControlNumber >= 0 && controlMapping.mappedControlNumber < 128 && controlMapping.initialValue >= 0 && controlMapping.initialValue < 128)
                    {
                        int scaledInitialValue = controlMapping.initialValue;
                        if (controlMapping.mappedControlNumber == 7)
                        {
                            // Diff sound gens have diff cc7 response.  We re-scale cc7 for each based on its cc7 min and max settings.
                            scaledInitialValue = (int)(scaledInitialValue * controlMapping.soundGenerator.cc7Scale) + controlMapping.soundGenerator.cc7Min;
                        }
                        controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, scaledInitialValue);
                    }
                }
            }
        }

        public void SendOSC(OscPacket msg)
        {
            // This func to support continued OSC integration with Reaper even if the configured oscSender address goes away during a show.
            //  When a true NIC-backed IP address is found, Reaper only binds to it for OSC.  When it's not available, Reaper binds to 0.0.0.0, which includes localhost.
            //  So, if the configured, true NIC-backed IP address exists, Reaper doesn't listen to localhost.
            //  This code prefers the the true NIC-backed IP address if its bound, else it falls back to localhost.
            //  TBD whether I should try localhost first, since I usually gig with Wifi off.
            if (oscSender != null)
            {
                // If there's an oscSender bound to the address specified in the config, try it first.  If it fails then use the one bound to localhost if it was successfully created.
                try
                {
                    oscSender.Send(msg);
                    return;
                }
                catch (System.Net.Sockets.SocketException)
                {
                    // Fall through and try localhost
                }
                try
                {
                    if (oscSenderLocalhost != null)
                    {
                        oscSenderLocalhost.Send(msg);
                        return;
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    return;
                }
            }
            else if (oscSenderLocalhost != null)
            {
                // If there's no oscSender bound to the address specified in the config, then use the one bound to localhost if it was successfully created.
                try
                {
                    oscSenderLocalhost.Send(msg);
                    return;
                }
                catch (System.Net.Sockets.SocketException)
                {
                    return;
                }
            }
            else
            {
                // If neither oscSender exists, we do nothing.
                return;
            }
        }

        public void PitchBend(PitchBendMessage msg)
        {
            // Find the per-device/channel mappings for the message's Device/Channel.  If there's none, nothing to do.
            String deviceKey = Mapping.PerDeviceChannelMapping.createKey(msg.Device.Name, (int)msg.Channel);
            if (!m_perDeviceChannelMappings.ContainsKey(deviceKey)) {
                return;
            }
            Mapping.PerDeviceChannelMapping perDeviceChannelMapping = m_perDeviceChannelMappings[deviceKey];

            if (perDeviceChannelMapping.pbLuaFunction != null)
            {
                var luaReturnRaw = perDeviceChannelMapping.pbLuaFunction.Call((int)msg.Channel, (int)msg.Value);
                IList<ChannelMessage> luaReturnMidiMessages = parseLuaReturn(luaReturnRaw, msg);
                handleChannelMessages(luaReturnMidiMessages, perDeviceChannelMapping);
            }
            else
            {
                HandlePB(msg, perDeviceChannelMapping);
            }
        }

        void HandlePB(PitchBendMessage msg, Mapping.PerDeviceChannelMapping perDeviceChannelMapping)
        {
            // Iterate over the pitch-bend mappings for this device/channel and send the pitch bend, scaled per the mapping, to the target device
            foreach (PitchBendMapping pitchBendMapping in perDeviceChannelMapping.pitchBendMappings)
            {
                if (pitchBendMapping.scale != 0.0)
                {
                    pitchBendMapping.soundGenerator.device.SendPitchBend((Channel)pitchBendMapping.soundGeneratorPhysicalChannel, (int)(msg.Value * pitchBendMapping.scale));
                }
            }
        }


        public void ControlChange(InputDevice fauxSourceDevice, int channel, int controlNumber, int value)
        {
            ControlChangeMessage msg = new ControlChangeMessage(fauxSourceDevice, (Channel)channel, (Midi.Control)controlNumber, value, 0);
            ControlChange(msg);
        }


        public void ControlChange(ControlChangeMessage msg)
        {
            // First we see if this is a "global" control, ie, one registered for JoeMidi as a whole, not for particular mappings.
            //  This might be used to create a master-volume control that's always in effect.

            bool globalControlChangeSent = false;

            // Hard-mapping of sostenuto pedal down to next patch
            if (msg.Device == this.configuration.primaryInputDevice.device &&
                // msg.Channel == 0 &&
                (int)msg.Control == 0x42 &&
                msg.Value > 0x40)
            {
                ProgramChangeMessage pcMsg = new ProgramChangeMessage(msg.Device, msg.Channel, (Instrument)configuration.currentPrimaryControllerButtonProgramNumbers[7], msg.Time);  // Button 8's (NextProgram) program change number.
                this.ProgramChange(pcMsg);
                return;
            }

            foreach (Mapping.PerDeviceChannelMapping globalPerDeviceChannelMappings in configuration.globalControlMappings)
            {
                if (globalPerDeviceChannelMappings.inputDevice != null && globalPerDeviceChannelMappings.inputDevice.Equals(msg.Device) && globalPerDeviceChannelMappings.inputDeviceChannel == (int)msg.Channel)
                {
                    foreach (ControlMapping controlMapping in globalPerDeviceChannelMappings.controlMappings)
                    {
                        if (controlMapping.sourceControlNumber == (int)msg.Control)
                        {
                            int scaledValue = (int)(msg.Value * controlMapping.scale) + controlMapping.min;
                            controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, scaledValue);
                            globalControlChangeSent = true;
                        }
                    }
                }
            }
            if (globalControlChangeSent == true)
            {
                // When a control mapping is defined globally any per-mapping definitions of it are ignored.  Debatable one day...
                return;
            }


            // Find the per-device/channel mappings for the message's Device/Channel.  If there's none, nothing else to do.
            String deviceKey = Mapping.PerDeviceChannelMapping.createKey(msg.Device.Name, (int)msg.Channel);
            if (!m_perDeviceChannelMappings.ContainsKey(deviceKey)) {
                return;
            }
            Mapping.PerDeviceChannelMapping perDeviceChannelMapping = m_perDeviceChannelMappings[deviceKey];

            if (perDeviceChannelMapping.ccLuaFunction != null)
            {
                var luaReturnRaw = perDeviceChannelMapping.ccLuaFunction.Call((int)msg.Channel, (int)msg.Control, msg.Value);
                IList<ChannelMessage> luaReturnMidiMessages = parseLuaReturn(luaReturnRaw, msg);
                handleChannelMessages(luaReturnMidiMessages, perDeviceChannelMapping);
            }
            else
            {
                HandleCC(msg, perDeviceChannelMapping);
            }
        }

        void HandleCC(ControlChangeMessage msg, Mapping.PerDeviceChannelMapping perDeviceChannelMapping)
        {
            // Iterate over the control mappings ...
            foreach (ControlMapping controlMapping in perDeviceChannelMapping.controlMappings)
            {
                // If we've received a control change message that the current mapping remaps process it.
                if (controlMapping.sourceControlNumber == (int)msg.Control && controlMapping.mappedControlNumber >= 0)
                {
                    // Two control mapping modes: toggle and regular.  
                    if (controlMapping.bToggle == false) {
                        // Regular remap of incoming control message to a different outgoing control#, with value scaled from the incoming range of 
                        //  0-127 to an outgoing range of min-max.
                        int scaledValue = (int)(msg.Value * controlMapping.scale) + controlMapping.min;

                        // Special processing for sustain pedal
                        if (controlMapping.mappedControlNumber == (int)Midi.Control.SustainPedal) {
                            if (msg.Value > 32)     // This value to turn continuous sus pedal into switching sus pedal.
                            {
                                // Down: Record what physical device/channels are receiving it from the source device.
                                if (recordSustainPedalDown(msg.Device, controlMapping.soundGenerator.device, (Channel)controlMapping.soundGeneratorPhysicalChannel) == false) {
                                    // Only send the CC if the damper wasn't already down.
                                    controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, 127 /* scaledValue*/ );
                                }
                            }
                            else
                            {
                                // Up: un-sustain any soundGen that was previously sustained by a message originating from the source input device
                                sendSustainPedalUpToAllDeviceChannelsWithSustainPedalDown(msg.Device);
                            }
                        }
                        // Special Processing for cc7: Scaling again, on a per-sound-generator basis, to accomodate diff sound generator cc7 ranges.
                        else if (controlMapping.mappedControlNumber == (int)Midi.Control.Volume)
                        {
                            scaledValue = (int)(scaledValue * controlMapping.soundGenerator.cc7Scale) + controlMapping.soundGenerator.cc7Min;
                            controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, scaledValue);
                        }
                        else {
                            controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, scaledValue);
                        }
                    }
                    else 
                    {
                        // Toggle mode alternately sends min and max remapped values to the remap control# on receipt of remappable control messages with values > 0x40.
                        //  This is typically used with momentary switch controls which send 7F on press and 0 on release.
                        //  I created this to allow my sustain pedal to toggle Rotary Speaker speed between high and low on each press.
                        if (msg.Value >= 0x40)
                        {
                            if (controlMapping.currentToggleState == true)
                            {
                                controlMapping.currentToggleState = false;
                                controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, controlMapping.max);
                            }
                            else
                            {
                                controlMapping.currentToggleState = true;
                                controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, controlMapping.min);
                            }
                        }
                    }
                }
            }
        }

        // While named generically at this point in time we only record control messages about activation of the Sustain pedal.
        IList<MappedMidiControl> mappedMidiControls = new List<MappedMidiControl>();

        private bool recordSustainPedalDown(DeviceBase damperMsgSourceDevice, OutputDevice physicalDeviceWithDamperDown, Channel physicalChannelWithDamperDown)
        {
            // Create a control mapping entry for this damper-down
            MappedMidiControl mappedControl = new MappedMidiControl();
            mappedControl.sourceDeviceName = damperMsgSourceDevice.Name;
            mappedControl.mappedDevice = physicalDeviceWithDamperDown;
            mappedControl.mappedChannel = physicalChannelWithDamperDown;
            mappedControl.mappedControl = Midi.Control.SustainPedal;  // Damper
            mappedControl.mappedValue = 127;
            bool rc = false;

            // If, for some reason, there's an existing mapping of this control in the list delete it.
            for (int i = mappedMidiControls.Count - 1; i >= 0; --i)
            {
                MappedMidiControl ctl = mappedMidiControls[i];
                if (ctl.sourceDeviceName == damperMsgSourceDevice.Name && ctl.mappedDevice == physicalDeviceWithDamperDown && ctl.mappedChannel == physicalChannelWithDamperDown && ctl.mappedControl == Midi.Control.SustainPedal)
                {
                    mappedMidiControls.Remove(ctl);
                    rc = true;
                }
            }

            // Add the newly created control to the list
            mappedMidiControls.Add(mappedControl);
            return rc;
        }

        private void sendSustainPedalUpToAllDeviceChannelsWithSustainPedalDown(DeviceBase damperMsgSourceDevice)
        {
            // The idea being that lifting the sustain pedal un-sustains every soundGenerator previously 
            for (int i = mappedMidiControls.Count-1; i >= 0 ; --i) { 
                MappedMidiControl ctl = mappedMidiControls[i];
                if (ctl.sourceDeviceName == damperMsgSourceDevice.Name && ctl.mappedControl == Midi.Control.SustainPedal)
                {
                    ctl.mappedDevice.SendControlChange(ctl.mappedChannel, Midi.Control.SustainPedal, 0);
                    mappedMidiControls.Remove(ctl);
                }
            }

        }

        OutputDevice findOutputDeviceByName(String name, bool showErrorPopup)
        {
            foreach (OutputDevice device in OutputDevice.InstalledDevices)
            {
                if (device.Name.Equals(name))
                {
                    return device;
                }
            }
            if (showErrorPopup == true)
            {
                MessageBox.Show("Ouput device " + name + " in mapping not present on system.");
            }
            return null;
        }

        InputDevice findInputDeviceByName(String name, bool showErrorPopup)
        {
            foreach (InputDevice device in InputDevice.InstalledDevices)
            {
                if (device.Name.Equals(name))
                {
                    return device;
                }
            }
            if (showErrorPopup == true)
            {
                MessageBox.Show("Input device " + name + " in mapping not present on system.");
            }
            return null;
        }

        //public String getCurrentMappingName()
        //{
        //    if (m_currentMapping != null)
        //    {
        //        return m_currentPatchNo + ": " + m_currentMapping.name;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        public void selectFirstMidiProgram()
        {
            if (configuration.midiPrograms.Count > 0)
            {
                int firstMidiProgramKey = configuration.midiPrograms.Keys.Min();
                MidiProgram firstMidiProgram = configuration.midiPrograms[firstMidiProgramKey];

                this.ProgramChange(firstMidiProgram.myPatchNumber);
            }
        }


        public void startMapper()
        {
            loadConfiguration();
            configuration.portraitMode = (SystemInformation.ScreenOrientation == ScreenOrientation.Angle90 || SystemInformation.ScreenOrientation == ScreenOrientation.Angle270);
            configuration.AutoGeneratePatchesFromReaperPresets();
            configuration.bind(JoeMidiDirectory);
            openSourceDevices();
            var testMessage = new SharpOSC.OscMessage("/gleep");
            if (configuration.oscAddress != "" && configuration.oscPort > 0)
            {
                oscSender = new SharpOSC.UDPSender(configuration.oscAddress, configuration.oscPort);
                try
                {
                    oscSender.Send(testMessage);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    oscSender = null;
                }
            }
            oscSenderLocalhost = new SharpOSC.UDPSender("127.0.0.1", configuration.oscPort);
            try
            {
                oscSenderLocalhost.Send(testMessage);
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Cannot open OSC address.  Proceeding without OSC.");
                oscSenderLocalhost = null;
            }

            disableAllVSTIs();

            // Send global CC initial values
            foreach (Mapping.PerDeviceChannelMapping globalPerDeviceChannelMappings in configuration.globalControlMappings)
            {
                foreach (ControlMapping controlMapping in globalPerDeviceChannelMappings.controlMappings)
                {
                    if (controlMapping.initialValue >= 0)
                    {
                        controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, controlMapping.initialValue);
                    }
                }
            }
        }

        private void disableAllVSTIs()
        {
            foreach (var soundGeneratoDictEntry in this.configuration.soundGenerators)
            {
                soundGeneratoDictEntry.Value.DisableVSTi(this);
            }
        }

        private void loadConfiguration()
        {
            String filePath = JoeMidiDirectory + @"\JoeMidi.json";
            String localFilePath = JoeMidiDirectory + @"\JoeMidiLocal.json";

            if (File.Exists(filePath) == false)
            {
                MessageBox.Show(filePath + " doesn't exist.  Creating trial configuration");
                configuration.createTrialConfiguration();
            }
            else
            {
                configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(filePath));

                try
                {
                    LocalConfiguration localConfiguration = JsonConvert.DeserializeObject<LocalConfiguration>(File.ReadAllText(localFilePath));
                    configuration.MergeLocalConfigurationOverrides(localConfiguration);
                }
                catch { };  // Local configuration file is not mandatory

                addAllPseudoSetlist();
            }
        }

        private void addAllPseudoSetlist()
        {
            Setlist setList = new Setlist();
            setList.name = "(All)";
            foreach(Song song in configuration.getSortedSongList())
            {
                setList.songTitles.Add(song.name);
            }
            setList.bind(configuration.songDict, configuration.logicalInputDeviceDict, configuration.soundGenerators, configuration.mappings, configuration.primaryInputDevice);
            configuration.setlists.Insert(0, setList);
        }

        public void refreshAllPseudoSetlist()
        {
            Setlist setList = configuration.setlists.Find(sl => sl.name == "(All)");
            if (setList != null)
            {
                setList.songs.Clear();
                setList.songTitles.Clear();

                foreach (Song song in configuration.getSortedSongList())
                {
                    setList.songTitles.Add(song.name);
                }
                setList.bind(configuration.songDict, configuration.logicalInputDeviceDict, configuration.soundGenerators, configuration.mappings, configuration.primaryInputDevice);
            }
        }

        public void stopMapper()
        {
            if (configuration.dirty == true)
            {
                saveConfiguration();
            }

            if (configuration != null && configuration.logicalInputDeviceDict != null)
            {
                foreach (LogicalInputDevice inputDevice in configuration.logicalInputDeviceDict.Values) 
                {
                    if (inputDevice.device != null)
                    {
                        if (inputDevice.device.IsReceiving == true)
                        {
                            inputDevice.device.StopReceiving();
                        }
                        inputDevice.device.RemoveAllEventHandlers();
                    }
                }
            }
        }

        public void saveConfiguration()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.Objects;

            // Remove (All) pseudo-setlist
            Setlist setlistToRemove = configuration.setlists.Find(setlist => setlist.name == "(All)");
            configuration.setlists.Remove(setlistToRemove);

            String json = JsonConvert.SerializeObject(configuration, Formatting.Indented, serializerSettings);

            if (!Directory.Exists(JoeMidiDirectory))
            {
                Directory.CreateDirectory(JoeMidiDirectory);
            }

            String filePath = JoeMidiDirectory + @"\JoeMidi.json";

            String backupFilePath = filePath + String.Format(".{0:yyyyMMddHHmmss}", DateTime.Now);
            if (File.Exists(filePath))
            {
                File.Move(filePath, backupFilePath);
            }

            File.WriteAllText(filePath, json);

            configuration.dirty = false;
        }

        void openSourceDevices()
        {
            foreach (String sourceDeviceLogicalName in configuration.logicalInputDeviceDict.Keys)
            {
                InputDevice inputDevice = configuration.logicalInputDeviceDict[sourceDeviceLogicalName].device;
                if (inputDevice != null)
                {
                    inputDevice.NoteOn += new InputDevice.NoteOnHandler(this.NoteOn);
                    inputDevice.NoteOff += new InputDevice.NoteOffHandler(this.NoteOff);
                    inputDevice.PitchBend += new InputDevice.PitchBendHandler(this.PitchBend);
                    inputDevice.ProgramChange += new InputDevice.ProgramChangeHandler(this.ProgramChange);
                    inputDevice.ControlChange += new InputDevice.ControlChangeHandler(this.ControlChange);
                    try
                    {
                        inputDevice.Open();
                        inputDevice.StartReceiving(null);
                    }
                    catch (DeviceException)
                    {
                        MessageBox.Show("Could open physical input device " + inputDevice.Name + " configured for logical input device " + sourceDeviceLogicalName);
                        inputDevice.RemoveAllEventHandlers();
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show("Physical input device " + inputDevice.Name + ", for logical input device " + sourceDeviceLogicalName + " already open.  Leaving it open.");
                    }
                }
            }
        }

        public void changeMasterVol(int vol)
        {
            LogicalInputDevice logicalInputDevice = configuration.logicalInputDeviceDict[configuration.volSliderInputDeviceName];
            if (logicalInputDevice != null)
            {
                ControlChange(logicalInputDevice.device, configuration.volSliderMidiChannel, configuration.volSliderControlNum, vol); // Ctl 75 is assigned in the 
            }
        }

        List<MappedNote> FindMappedNote(String sourceDeviceName, Channel sourceChannel, Pitch origNote )
        {
            List<MappedNote> foundMappedNotes = new List<MappedNote>();

            foreach (MappedNote mappedNote in m_mappedNotesList)
            {
                if (mappedNote.sourceDeviceName == sourceDeviceName && mappedNote.sourceChannel == sourceChannel && mappedNote.origNote == origNote)
                {
                    foundMappedNotes.Add(mappedNote);
                }
            }

            return foundMappedNotes;
        }

        MappedNote FindMappedNote(MappedNote noteToRemove)
        {
            foreach (MappedNote note in m_mappedNotesList)
            {
                if (note.sourceDeviceName == noteToRemove.sourceDeviceName && note.sourceChannel == noteToRemove.sourceChannel && note.origNote == noteToRemove.origNote &&
                        note.mappedDevice.Name == noteToRemove.mappedDevice.Name && note.mappedChannel == noteToRemove.mappedChannel && note.mappedNote == noteToRemove.mappedNote)
                {
                    return note;
                }
            }
            return null;
        }

        void RemoveMappedNote(MappedNote noteToRemove)
        {
            foreach(MappedNote note in m_mappedNotesList)
            {
                if (note.sourceDeviceName == noteToRemove.sourceDeviceName && note.sourceChannel == noteToRemove.sourceChannel && note.origNote == noteToRemove.origNote &&
                        note.mappedDevice.Name == noteToRemove.mappedDevice.Name && note.mappedChannel == noteToRemove.mappedChannel && note.mappedNote == noteToRemove.mappedNote)
                {
                    m_mappedNotesList.Remove(note);
                    break;
                }
            }
        }

        public string ConfigurationSubDirectory { get; set; }

        public string JoeMidiDirectory
        {
            get
            {
                String myDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                if (ConfigurationSubDirectory == "")
                {
                    return myDocsFolder;
                }

                return myDocsFolder + @"\" + ConfigurationSubDirectory;
            }
        }

        List<Tuple<SoundGenerator, long>> soundGeneratorsToEventuallyDeactivate = new List<Tuple<SoundGenerator, long>>();

        void DeferredDisableSoundGenerator(ref SoundGenerator sg, long delayInSeconds)
        {
            soundGeneratorsToEventuallyDeactivate.Add(Tuple.Create(sg, this.tickCount + delayInSeconds));
        }
        
        void cancelAnyPendingDisablesOfThisSoundGenerator(ref SoundGenerator sg)
        {
            if (configuration.disableUnusedVSTIs == true)
            {
                var sgName = sg.name;
                soundGeneratorsToEventuallyDeactivate.RemoveAll(tuple => tuple.Item1.name == sgName);
            }
        }


        private long tickCount = 0;

        public void tick()
        {
            tickCount = tickCount + 1;

            if (configuration.disableUnusedVSTIs == true)
            {
                foreach (var tuple in soundGeneratorsToEventuallyDeactivate)
                {
                    if (tuple.Item2 <= tickCount)
                    {
                        tuple.Item1.DisableVSTi(this);
                    }
                }
                soundGeneratorsToEventuallyDeactivate.RemoveAll(tuple => tuple.Item2 <= tickCount);
            }
        }
    }
}
