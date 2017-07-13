using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Midi;

namespace JoeMidi1
{
    public class Mapping
    {

        public class PerDeviceChannelMapping
        {
            public String logicalInputDeviceName;
            public int inputDeviceChannel = 0;

            public PerDeviceChannelMapping() { }
            
            public PerDeviceChannelMapping(String _logicalInputDeviceName, int _inputDeviceChannel)
            {
                logicalInputDeviceName = _logicalInputDeviceName;
                inputDeviceChannel = _inputDeviceChannel;
            }

            [JsonIgnore]
            public String key
            {
                get
                {
                    return createKey((inputDevice != null) ? inputDevice.Name : "Unknown input device for " + logicalInputDeviceName, inputDeviceChannel);
                }
            }

            public static String createKey(String deviceName, int deviceChannel)
            {
                return deviceName + "\t" + deviceChannel;
            }

            public List<MappingPatch> mappingPatches = new List<MappingPatch>();
            public List<NoteMapping> noteMappings = new List<NoteMapping>();
            public List<PitchBendMapping> pitchBendMappings = new List<PitchBendMapping>();
            public List<ControlMapping> controlMappings = new List<ControlMapping>();

            [JsonIgnore]
            public InputDevice inputDevice;

            public virtual bool bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
            {
                if (!logicalInputDeviceDict.ContainsKey(logicalInputDeviceName))
                {
                    return false;
                }
                inputDevice = logicalInputDeviceDict[logicalInputDeviceName].device;


                foreach (MappingPatch mappingPatch in mappingPatches)
                {
                    mappingPatch.bind(soundGenerators);
                }

                foreach (NoteMapping noteMapping in noteMappings)
                {
                    if (noteMapping.bind(logicalInputDeviceDict, soundGenerators) == false)
                    {
                        return false;
                    }
                }

                foreach (PitchBendMapping pitchBendMapping in pitchBendMappings)
                {
                    pitchBendMapping.bind(logicalInputDeviceDict, soundGenerators);
                }

                foreach (ControlMapping controlMapping in controlMappings)
                {
                    controlMapping.bind(logicalInputDeviceDict, soundGenerators);
                }

                return true;
            }


        }
        
        public String name;

        public Dictionary<String, PerDeviceChannelMapping> perDeviceChannelMappings = new Dictionary<string, PerDeviceChannelMapping>();

        public virtual bool bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
        {
            foreach (PerDeviceChannelMapping perDeviceChannelMapping in perDeviceChannelMappings.Values)
            {
                perDeviceChannelMapping.bind(logicalInputDeviceDict, soundGenerators);
            }
            return true;
        }

        public static void createTrialConfiguration(Dictionary<String, Mapping> mappings)
        {
            Mapping mapping = new Mapping();
            mapping.name = "Rock Organ 1";

            PerDeviceChannelMapping perDeviceChannelMapping = new PerDeviceChannelMapping();
            perDeviceChannelMapping.logicalInputDeviceName = "Input Device 1";
            perDeviceChannelMapping.inputDeviceChannel = 0;

            NoteMapping.createTrialConfiguration(2, perDeviceChannelMapping.noteMappings);
            MappingPatch.createTrialConfiguration(2, perDeviceChannelMapping.mappingPatches);
            PitchBendMapping.createTrialConfiguration(2, perDeviceChannelMapping.pitchBendMappings);
            ControlMapping.createTrialConfiguration(2, perDeviceChannelMapping.controlMappings);
            mapping.perDeviceChannelMappings.Add(perDeviceChannelMapping.key, perDeviceChannelMapping);
            mappings.Add(mapping.name, mapping);


            mapping = new Mapping();
            mapping.name = "Vintage Vince";

            perDeviceChannelMapping = new PerDeviceChannelMapping();
            perDeviceChannelMapping.logicalInputDeviceName = "Input Device 1";
            perDeviceChannelMapping.inputDeviceChannel = 0;

            NoteMapping.createTrialConfiguration(3, perDeviceChannelMapping.noteMappings);
            MappingPatch.createTrialConfiguration(3, perDeviceChannelMapping.mappingPatches);
            PitchBendMapping.createTrialConfiguration(3, perDeviceChannelMapping.pitchBendMappings);
            ControlMapping.createTrialConfiguration(3, perDeviceChannelMapping.controlMappings);
            mapping.perDeviceChannelMappings.Add(perDeviceChannelMapping.key, perDeviceChannelMapping);
            mappings.Add(mapping.name, mapping);
        }
    }
}
