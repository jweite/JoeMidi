using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Midi;
using Newtonsoft.Json;
using NLua;

namespace JoeMidi1
{
    public class NoteMapping : SoundGeneratorChannel
    {
        // Class for filtering and remapping notes.  Defines the range of notes that it passes through and a pitch offset added to each.

        public int lowestNote;
        public int highestNote;

        public int pitchOffset;

        public int? secondaryPC = null;     // When set, this Note Mapping only active if current PC for this device/chan matches this value.

        [JsonIgnore]
        public InputDevice sourceDevice;

        [JsonIgnore]
        public LuaFunction noteOnLuaFunction;
        public LuaFunction noteOffLuaFunction;

        public void bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators, Mapping mapping)
        {
            String luaFunctionBaseName = this.soundGeneratorName.ToLower().Replace(" ", "_");
            noteOnLuaFunction = (LuaFunction)mapping.luaState[luaFunctionBaseName + "__noteon"];
            noteOffLuaFunction = (LuaFunction)mapping.luaState[luaFunctionBaseName + "__noteoff"];
            base.bind(soundGenerators);
        }

        public static void createTrialConfiguration(int whichMappingToCreate, List<NoteMapping> noteMappings)
        {
            switch (whichMappingToCreate)
            {
                case 0:
                    NoteMapping noteMapping = new NoteMapping();
                    noteMapping.soundGeneratorName = "Proteus VX";
                    noteMapping.soundGeneratorRelativeChannel = 0;
                    noteMapping.lowestNote = 0;
                    noteMapping.highestNote = 127;
                    noteMapping.pitchOffset = 0;
                    noteMappings.Add(noteMapping);
                    break;
                case 1:
                    noteMapping = new NoteMapping();
                    noteMapping.soundGeneratorName = "Proteus VX";
                    noteMapping.soundGeneratorRelativeChannel = 0;
                    noteMapping.lowestNote = 0;
                    noteMapping.highestNote = 127;
                    noteMapping.pitchOffset = 0;
                    noteMappings.Add(noteMapping);
                    break;
                case 2:
                    noteMapping = new NoteMapping();
                    noteMapping.soundGeneratorName = "VB3";
                    noteMapping.soundGeneratorRelativeChannel = 0;
                    noteMapping.lowestNote = 0;
                    noteMapping.highestNote = 127;
                    noteMapping.pitchOffset = 0;
                    noteMappings.Add(noteMapping);
                    break;
                case 3:
                    noteMapping = new NoteMapping();
                    noteMapping.soundGeneratorName = "SuperWave P8";
                    noteMapping.soundGeneratorRelativeChannel = 0;
                    noteMapping.lowestNote = 0;
                    noteMapping.highestNote = 127;
                    noteMapping.pitchOffset = 0;
                    noteMappings.Add(noteMapping);
                    break;
                default:
                    MessageBox.Show("Unknown trial configuration mapping requested: " + whichMappingToCreate);
                    break;
            }
        }
    }
}
