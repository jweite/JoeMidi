using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Midi;

namespace JoeMidi1
{
    public class ChannelPressureMapping : SoundGeneratorChannel
    {
        // Class for remapping channel pressure to CC.  Pseudo cc 128 maps to normal channel pressure.  -1 = none
        public int CC = -1;

        [JsonIgnore]
        public InputDevice sourceDevice;

        public void bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
        {
            base.bind(soundGenerators);
        }

        public static void createTrialConfiguration(int whichMappingToCreate, List<ChannelPressureMapping> channelPressureMappings)
        {
            ChannelPressureMapping channelPressureMapping = new ChannelPressureMapping();

            switch (whichMappingToCreate)
            {
                case 0:
                    // None
                    break;
                case 1:
                    channelPressureMapping.CC = 0;
                    channelPressureMappings.Add(channelPressureMapping);
                    break;
                case 2:
                    channelPressureMapping.CC = 128;
                    channelPressureMappings.Add(channelPressureMapping);
                    break;
                case 3:
                    channelPressureMapping.CC = 1;
                    channelPressureMappings.Add(channelPressureMapping);
                    break;
                default:
                    MessageBox.Show("Unknown trial configuration mapping requested: " + whichMappingToCreate);
                    break;
            }
        }
    }
}
