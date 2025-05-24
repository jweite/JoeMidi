using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Midi;

namespace JoeMidi1
{
    public class PitchBendMapping : SoundGeneratorChannel
    {
        // Class for remapping pitch bends: simply scales them.
        public double scale;

        [JsonIgnore]
        public InputDevice sourceDevice;

        public void bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
        {
            base.bind(soundGenerators);
        }

        public static void createTrialConfiguration(int whichMappingToCreate, List<PitchBendMapping> pitchBendMappings)
        {
            PitchBendMapping pitchBendMapping = new PitchBendMapping();

            switch (whichMappingToCreate)
            {
                case 0:
                    pitchBendMapping.soundGeneratorName = "Proteus VX";
                    pitchBendMapping.soundGeneratorRelativeChannel = 0;
                    pitchBendMapping.scale = 0.0;
                    pitchBendMappings.Add(pitchBendMapping);
                    break;
                case 1:
                    pitchBendMapping.soundGeneratorName = "Proteus VX";
                    pitchBendMapping.soundGeneratorRelativeChannel = 0;
                    pitchBendMapping.scale = 1.0;
                    pitchBendMappings.Add(pitchBendMapping);
                    break;
                case 2:
                    pitchBendMapping.soundGeneratorName = "VB3";
                    pitchBendMapping.soundGeneratorRelativeChannel = 0;
                    pitchBendMapping.scale = 1.0;
                    pitchBendMappings.Add(pitchBendMapping);
                    break;
                case 3:
                    pitchBendMapping.soundGeneratorName = "SuperWave P8";
                    pitchBendMapping.soundGeneratorRelativeChannel = 0;
                    pitchBendMapping.scale = 1.0;
                    pitchBendMappings.Add(pitchBendMapping);
                    break;
                default:
                    MessageBox.Show("Unknown trial configuration mapping requested: " + whichMappingToCreate);
                    break;
            }
        }

    }
}
