﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Midi;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace JoeMidi1
{
    class Mapper
    {
        public Configuration configuration = new Configuration();
        public Dictionary<String, Mapping.PerDeviceChannelMapping> m_perDeviceChannelMappings = new Dictionary<String, Mapping.PerDeviceChannelMapping>();

        Dictionary<String, MappedNote> m_mappedNotesDict = new Dictionary<string, MappedNote>();

        const int NUM_PROGRAM_BUTTONS = 8;
        int[] m_programButtonLastProgramNo = new int[NUM_PROGRAM_BUTTONS];
        int m_lastRequestedProgramNo = -1;

        public int masterTranspose = 0;

        public delegate void MidiProgramChange(int programNumber);
        public MidiProgramChange midiProgramChangeNotification = null;

        public delegate void MidiProgramActivated(MidiProgram midiProgram);
        public MidiProgramActivated midiProgramActivatedNotification = null;

        public Mapper()
        {
            for (int i = 0; i < NUM_PROGRAM_BUTTONS; ++i)
            {
                m_programButtonLastProgramNo[i] = i;
            }
        }

        public void NoteOn(NoteOnMessage msg)
        {
            // Find the per-device/channel mappings for the message's Device/Channel.  If there's none, nothing to do.
            String deviceKey = Mapping.PerDeviceChannelMapping.createKey(msg.Device.Name, (int)msg.Channel);
            if (!m_perDeviceChannelMappings.ContainsKey(deviceKey)) {
                return;
            }
            Mapping.PerDeviceChannelMapping perDeviceChannelMapping = m_perDeviceChannelMappings[deviceKey];

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
                    String mappingKey = getMappingKey(mappedNoteRecord.sourceDeviceName, mappedNoteRecord.sourceChannel, mappedNoteRecord.origNote);
                    if (m_mappedNotesDict.ContainsKey(mappingKey))
                    {
                        // It is: shut off the note it was (previously) mapped to and remove the mapping record from the dict of sounding notes.
                        mappedNoteRecord.mappedDevice.SendNoteOff(mappedNoteRecord.mappedChannel, mappedNoteRecord.mappedNote, 127);
                        m_mappedNotesDict.Remove(mappingKey);
                    }

                    // Now, play the new mapping of the source note.
                    mappedNoteRecord.mappedDevice.SendNoteOn(mappedNoteRecord.mappedChannel, mappedNoteRecord.mappedNote, msg.Velocity);

                    // And add it to the dictionary of sounding notes.
                    m_mappedNotesDict.Add(mappingKey, mappedNoteRecord);

                }
            }
        }
        

        public void NoteOff(NoteOffMessage msg)
        {
            // This note should be in the mapped notes dict from it's note-on.  Build the key to find what it was mapped to.
            String sourceDeviceName = msg.Device.Name;
            Channel sourceChannel = msg.Channel;
            Pitch origNote = msg.Pitch;
            String mappingKey = getMappingKey(sourceDeviceName, sourceChannel, origNote);
                    
            // Look up this note in the mapped notes dict to find what it was mapped to
            if (m_mappedNotesDict.ContainsKey(mappingKey))
            {
                // Send a note off for what it was mapped to, and remove this entry from the mapped notes dict.
                MappedNote mappedNoteRecord = m_mappedNotesDict[mappingKey];
                mappedNoteRecord.mappedDevice.SendNoteOff(mappedNoteRecord.mappedChannel, mappedNoteRecord.mappedNote, msg.Velocity);
                m_mappedNotesDict.Remove(mappingKey);
            }
            else
            {
                // No mapping in the mapped notes dict...  worth logging, but I'm not sure about the overhead of doing that in a midi event handler.
            }
        }

        public void ProgramChange(ProgramChangeMessage msg)
        {
            int requestedProgramNo = (int)msg.Instrument;
            if (midiProgramChangeNotification != null)
            {
                midiProgramChangeNotification(requestedProgramNo);
            }
        }

        public void RotatingProgramChange(int requestedProgramNo)
        {
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

        public void SetMapping(Mapping mappingToActivate) 
        {
            // Step through the per-device/channel mappings contained in this mapping and add/replace them into the mapper's dict.
            //  This effectively merges the new mapping into any existing mappings on a per-device/channel basis. 
            //   ie, if the new mapping only has one perDeviceMapping in it for a specific device/channel then any pre-existing mappings
            //   in the mapper for other device/channels remain in effect.
            foreach (Mapping.PerDeviceChannelMapping perDeviceChannelMapping in mappingToActivate.perDeviceChannelMappings.Values)
            {
                if (m_perDeviceChannelMappings.ContainsKey(perDeviceChannelMapping.key))
                {
                    m_perDeviceChannelMappings[perDeviceChannelMapping.key] = perDeviceChannelMapping;
                }
                else
                {
                    m_perDeviceChannelMappings.Add(perDeviceChannelMapping.key, perDeviceChannelMapping);
                }
            }

            // Step through the new per-device/channel mappings and send any program/bank/control changes required to effect them on the synths.
            foreach (Mapping.PerDeviceChannelMapping perDeviceChannelMapping in mappingToActivate.perDeviceChannelMappings.Values)
            {
                // Send out the mapping's registered bank/program change to each of that Mapping's Sound Generators.
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
                }

                // Send out initial values for all mapped controls that have them
                foreach (ControlMapping controlMapping in perDeviceChannelMapping.controlMappings)
                {
                    if (controlMapping.mappedControlNumber >= 0 && controlMapping.mappedControlNumber < 127 && controlMapping.initialValue >= 0 && controlMapping.initialValue < 128)
                    {
                        controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, controlMapping.initialValue);
                    }
                }
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
                        controlMapping.soundGenerator.device.SendControlChange((Channel)controlMapping.soundGeneratorPhysicalChannel, (Midi.Control)controlMapping.mappedControlNumber, scaledValue);
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
            try
            {
                loadConfiguration();
                configuration.bind();
                openSourceDevices();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception starting mapper: " + e);
                return;
            }
        }

        private void loadConfiguration()
        {
            String myDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String directoryPath = myDocsFolder + @"\JoeMidi";
            String filePath = directoryPath + @"\JoeMidi.json";
            if (File.Exists(filePath) == false)
            {
                MessageBox.Show("JoeMidi.json doesn't exist.  Creating trial configuration");
                configuration.createTrialConfiguration();
            }
            else
            {
                configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(filePath));
            }
        }

        public void stopMapper()
        {
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

            if (configuration.dirty == true)
            {
                saveConfiguration();
            }
        }

        private void saveConfiguration()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.Objects;

            String json = JsonConvert.SerializeObject(configuration, Formatting.Indented, serializerSettings);

            String myDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String directoryPath = myDocsFolder + @"\JoeMidi";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            String filePath = directoryPath + @"\JoeMidi.json";

            String backupFilePath = filePath + String.Format(".{0:yyyyMMddHHmmss}", DateTime.Now);
            if (File.Exists(filePath))
            {
                File.Move(filePath, backupFilePath);
            }

            File.WriteAllText(filePath, json);
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

        private String getMappingKey(String deviceName, Channel channel, Pitch pitch) 
        {
            return "_" + (int)channel + "_" + (int)pitch + "_" + deviceName;
        }

        public void changeMasterVol(int vol)
        {
            LogicalInputDevice logicalInputDevice = configuration.logicalInputDeviceDict[configuration.volSliderInputDeviceName];
            if (logicalInputDevice != null)
            {
                ControlChange(logicalInputDevice.device, configuration.volSliderMidiChannel, configuration.volSliderControlNum, vol); // Ctl 75 is assigned in the 
            }
        }
    }
}