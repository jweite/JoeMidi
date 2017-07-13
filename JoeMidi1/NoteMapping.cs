using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Midi;
using Newtonsoft.Json;

namespace JoeMidi1
{
    public class NoteMapping : SoundGeneratorChannel
    {
        public int lowestNote;
        public int highestNote;

        public int pitchOffset;

        [JsonIgnore]
        public InputDevice sourceDevice;

        public bool  bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
        {
            return base.bind(soundGenerators);
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
