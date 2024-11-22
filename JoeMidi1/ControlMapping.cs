using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Midi;

namespace JoeMidi1
{
    public class ControlMapping : SoundGeneratorChannel
    {
        // Class for filtering and remapping a CC.  Remaps a particular CC# to a different one.  Scales CC values between declared min and max.  
        //  Captures an initial value for the CC, to be sent out when the mapping containing it is activated.

        public int sourceControlNumber;
        public int mappedControlNumber;
        public int min = 0;
        public int max = 127;
        public bool bToggle = false;
        public int initialValue = -1;

        [JsonIgnore]
        public InputDevice sourceDevice;

        [JsonIgnore]
        public double scale;

        [JsonIgnore]
        public bool currentToggleState = false;

        public bool Equals(ControlMapping other)
        {
            return (base.Equals(other)) && (sourceControlNumber == other.sourceControlNumber) && (mappedControlNumber == other.mappedControlNumber);
        }

        public void bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
        {
            scale = ((double)max - (double)min) / (double)127;
            currentToggleState = (bToggle == true && initialValue >= 1);
            if (mappedControlNumber == 7 && initialValue == -1)                 // Force sound gens to max vol if not defined in the mapping.  
                initialValue = 127;
            base.bind(soundGenerators);
        }

        public static void createTrialConfiguration(int whichMappingToCreate, List<ControlMapping> controlMappings)
        {
            ControlMapping controlMapping = new ControlMapping();

            switch (whichMappingToCreate)
            {
                case 0:
                    controlMapping.soundGeneratorName = "Proteus VX";
                    controlMapping.soundGeneratorRelativeChannel = 0;
                    controlMapping.sourceControlNumber = 64;     // Damper
                    controlMapping.mappedControlNumber = 64;
                    controlMapping.min = 0;
                    controlMapping.max = 127;
                    controlMapping.initialValue = -1;
                    controlMappings.Add(controlMapping);
                    break;
                case 1:
                    controlMapping.soundGeneratorName = "Proteus VX";
                    controlMapping.soundGeneratorRelativeChannel = 0;
                    controlMapping.sourceControlNumber = 64;     // Damper
                    controlMapping.mappedControlNumber = 64;
                    controlMapping.min = 0;
                    controlMapping.max = 127;
                    controlMapping.initialValue = -1;
                    controlMappings.Add(controlMapping);
                    break;
                case 2:
                    controlMapping.soundGeneratorName = "VB3";
                    controlMapping.soundGeneratorRelativeChannel = 0;
                    controlMapping.sourceControlNumber = 1;     // Mod Wheel
                    controlMapping.mappedControlNumber = 1;
                    controlMapping.min = 30;
                    controlMapping.max = 90;
                    controlMapping.initialValue = -1;
                    controlMappings.Add(controlMapping);
                    break;
                case 3:
                    controlMapping.soundGeneratorName = "SuperWave P8";
                    controlMapping.soundGeneratorRelativeChannel = 0;
                    controlMapping.sourceControlNumber = 1;     // Mod Wheel
                    controlMapping.mappedControlNumber = 1;
                    controlMapping.min = 0;
                    controlMapping.max = 64;
                    controlMapping.initialValue = 32;
                    controlMappings.Add(controlMapping);
                    break;
                default:
                    MessageBox.Show("Unknown trial configuration mapping requested: " + whichMappingToCreate);
                    break;
            }
        }
    }
}
