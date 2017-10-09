using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JoeMidi1
{
    public class MidiProgram
    {
        // Represents a sound (or mapping of multiple sounds) that can be requested of the JoeMidi managed SoundGenerators 
        //  by sending a Program Change msg to JoeMidi (by a controller or via the JoeMidi Random Access buttons)

        public int myBankNumber = -1;                        // To activate me
        public int myPatchNumber = -1;                       // To activate me
        public bool bSingle = false;
        public String SingleSoundGeneratorName;
        public String SinglePatchName;
        public String MappingName;

        [JsonIgnore]
        public Mapping mapping;

        [JsonIgnore]
        public Button activationButton = null;
        
        public MidiProgram() { }
        
        public MidiProgram(MidiProgram orig)
        {
            myBankNumber = orig.myBankNumber;
            myPatchNumber = orig.myPatchNumber;
            bSingle = orig.bSingle;
            SingleSoundGeneratorName = orig.SingleSoundGeneratorName;
            SinglePatchName = orig.SinglePatchName;
            MappingName = orig.MappingName;
        }

        public MidiProgram(SoundGeneratorPatch patch)
        {
            bSingle = true;
            SinglePatchName = patch.name;
            SingleSoundGeneratorName = patch.soundGenerator.name;
        }

        public MidiProgram(Mapping mapping)
        {
            bSingle = false;
            MappingName = mapping.name;

        }

        public static void createTrialConfiguration(Dictionary<int, MidiProgram> midiProgramDict)
        {
            MidiProgram midiProgram = new MidiProgram();
            midiProgram.myBankNumber = 0;
            midiProgram.myPatchNumber = 0;
            midiProgram.bSingle = true;
            midiProgram.SingleSoundGeneratorName = "Proteus VX";
            midiProgram.SinglePatchName = "Dynamic Grand";
            midiProgramDict.Add((midiProgram.myBankNumber * 128) + midiProgram.myPatchNumber, midiProgram);

            midiProgram = new MidiProgram();
            midiProgram.myBankNumber = 0;
            midiProgram.myPatchNumber = 1;
            midiProgram.bSingle = true;
            midiProgram.SingleSoundGeneratorName = "Proteus VX";
            midiProgram.SinglePatchName = "EP-3";
            midiProgramDict.Add((midiProgram.myBankNumber * 128) + midiProgram.myPatchNumber, midiProgram);

            midiProgram = new MidiProgram();
            midiProgram.myBankNumber = 0;
            midiProgram.myPatchNumber = 9;
            midiProgram.bSingle = true;
            midiProgram.SingleSoundGeneratorName = "Proteus VX";
            midiProgram.SinglePatchName = "EP-2";
            midiProgramDict.Add((midiProgram.myBankNumber * 128) + midiProgram.myPatchNumber, midiProgram);

            midiProgram = new MidiProgram();
            midiProgram.myBankNumber = 0;
            midiProgram.myPatchNumber = 17;
            midiProgram.bSingle = true;
            midiProgram.SingleSoundGeneratorName = "Proteus VX";
            midiProgram.SinglePatchName = "Clavinetti";
            midiProgramDict.Add((midiProgram.myBankNumber * 128) + midiProgram.myPatchNumber, midiProgram);

            midiProgram = new MidiProgram();
            midiProgram.myBankNumber = 0;
            midiProgram.myPatchNumber = 2;
            midiProgram.bSingle = false;
            midiProgram.MappingName = "Rock Organ 1";
            midiProgramDict.Add((midiProgram.myBankNumber * 128) + midiProgram.myPatchNumber, midiProgram);

            midiProgram = new MidiProgram();
            midiProgram.myBankNumber = 0;
            midiProgram.myPatchNumber = 3;
            midiProgram.bSingle = false;
            midiProgram.MappingName = "Vintage Vince";
            midiProgramDict.Add((midiProgram.myBankNumber * 128) + midiProgram.myPatchNumber, midiProgram);

        }

        virtual public bool bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators, Dictionary<String, Mapping> mappings, LogicalInputDevice primaryInputDevice)
        {
            // Find or create a Mapping for this MidiProgram that it will point to

            if (bSingle == false)       // If it's not a single, then it's a mapping, so just find the one named.
            {
                if (mappings.ContainsKey(this.MappingName))
                {
                    mapping = mappings[this.MappingName];
                    return true;
                }
                else
                {
                    MessageBox.Show("MidiProgram Mapping " + this.MappingName + " not defined in this configuration.");
                    return false;
                }
            }
            else
            {
                // If it's a single, then we create a mapping on the fly for it.

                mapping = new Mapping();
                
                String candidateName = "(S) " + this.SinglePatchName;
                if (mappings.ContainsKey(candidateName)) {
                    // Patch Name alone isn't a unique mapping name.  Fully qualify it with the SG name too.
                    candidateName = "(S) " + this.SingleSoundGeneratorName + " - " + this.SinglePatchName;
                }
                mapping.name = candidateName;

                // Create a single mapping with some settings values on the fly
                Mapping.PerDeviceChannelMapping perDeviceChannelMapping = new Mapping.PerDeviceChannelMapping();
                perDeviceChannelMapping.logicalInputDeviceName = primaryInputDevice.logicalDeviceName;
                perDeviceChannelMapping.inputDeviceChannel = 0;

                NoteMapping noteMapping = new NoteMapping();
                noteMapping.soundGeneratorName = this.SingleSoundGeneratorName;
                noteMapping.soundGeneratorRelativeChannel = 0;
                noteMapping.lowestNote = 0;
                noteMapping.highestNote = 127;
                noteMapping.pitchOffset = 0;
                perDeviceChannelMapping.noteMappings.Add(noteMapping);

                MappingPatch mappingPatch = new MappingPatch();
                mappingPatch.soundGeneratorName = this.SingleSoundGeneratorName;
                mappingPatch.soundGeneratorRelativeChannel = 0;
                mappingPatch.patchName = this.SinglePatchName;
                perDeviceChannelMapping.mappingPatches.Add(mappingPatch);

                PitchBendMapping pitchBendMapping = new PitchBendMapping();
                pitchBendMapping.soundGeneratorName = this.SingleSoundGeneratorName;
                pitchBendMapping.soundGeneratorRelativeChannel = 0;
                pitchBendMapping.scale = 1.0;
                perDeviceChannelMapping.pitchBendMappings.Add(pitchBendMapping);

                ControlMapping controlMapping = new ControlMapping();
                controlMapping.soundGeneratorName = this.SingleSoundGeneratorName;
                controlMapping.soundGeneratorRelativeChannel = 0;
                controlMapping.sourceControlNumber = 1;     // Mod Wheel
                controlMapping.mappedControlNumber = 1;
                controlMapping.min = 0;
                controlMapping.max = 127;
                controlMapping.initialValue = -1;
                perDeviceChannelMapping.controlMappings.Add(controlMapping);

                //controlMapping = new ControlMapping();
                //controlMapping.soundGeneratorName = this.SingleSoundGeneratorName;
                //controlMapping.soundGeneratorRelativeChannel = 0;
                //controlMapping.sourceControlNumber = 7;     // Vol
                //controlMapping.mappedControlNumber = 7;
                //controlMapping.min = 0;
                //controlMapping.max = 127;
                //controlMapping.initialValue = 127;
                //perDeviceChannelMapping.controlMappings.Add(controlMapping);

                controlMapping = new ControlMapping();
                controlMapping.soundGeneratorName = this.SingleSoundGeneratorName;
                controlMapping.soundGeneratorRelativeChannel = 0;
                controlMapping.sourceControlNumber = 64;     // Damper
                controlMapping.mappedControlNumber = 64;
                controlMapping.min = 0;
                controlMapping.max = 127;
                controlMapping.initialValue = -1;
                perDeviceChannelMapping.controlMappings.Add(controlMapping);

                mapping.perDeviceChannelMappings.Add(perDeviceChannelMapping.key, perDeviceChannelMapping);

                return mapping.bind(logicalInputDeviceDict, soundGenerators);
            }
        }

        [JsonIgnore]
        public String name
        {
            get
            {
                return (MappingName != null) ? MappingName : SinglePatchName;
            }
        }

        [JsonIgnore]
        public int key { get { return (myBankNumber * 128) + myPatchNumber; } }

    }
}
