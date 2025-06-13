using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Midi;
using NLua;

namespace JoeMidi1
{
    public class Mapping
    {
        // A Mapping represents a named set of of midi transformations, one per midi "stream" (a "stream" being a single channel originating from
        //  a particular input device, awkardly referred to below as a DeviceChannel) and defined in this below inner class PerDeviceChannelMapping.
        //  The set of PerDeviceChannelMappings in a Mapping is kept in a Dict, keyed by the PerDeviceChannelMapping's key (device name + channel).

        public class PerDeviceChannelMapping
        {
            // PerDeviceChannelMapping represents a transformation of a single midi stream.  Transformation includes the remapping of notes, 
            //  program changes (patches), rescaling pitch-bend, remapping and rescaling cc's.  Each of these "sub-transformations" is 
            //  represented by a XXXXMapping class (i.e. NoteMapping, PitchBendMapping) appropriate for the remapping of kind of midi message.
            //  Being "per device channel", each PerDeviceChannelMapping has both an input device and a channel# which defines the midi stream 
            //  it services.  PerDeviceChannelMappings are uniquely identified with their "key", a concatenation of their input device name and channel.

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

            public List<MappingPatch> mappingPatches = new List<MappingPatch>();                // Program changes, to be sent on mapping activation
            public List<NoteMapping> noteMappings = new List<NoteMapping>();                    // Note filtering/transpostion
            public List<PitchBendMapping> pitchBendMappings = new List<PitchBendMapping>();     // Pitch bend scalings
            public List<ControlMapping> controlMappings = new List<ControlMapping>();           // CC remapping/scaling/initial values to be sent on mapping activation.

            [JsonIgnore]
            public InputDevice inputDevice;

            [JsonIgnore]
            public LuaFunction noteOnLuaFunction;

            [JsonIgnore]
            public LuaFunction noteOffLuaFunction;

            [JsonIgnore]
            public LuaFunction ccLuaFunction;

            [JsonIgnore]
            public LuaFunction pbLuaFunction;

            [JsonIgnore]
            public LuaFunction pcLuaFunction;

            public virtual void bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators, Mapping mapping)
            {
                if (mapping != null)
                {
                    noteOnLuaFunction = (LuaFunction)mapping.luaState["noteon"];
                    noteOffLuaFunction = (LuaFunction)mapping.luaState["noteoff"];
                    ccLuaFunction = (LuaFunction)mapping.luaState["cc"];
                    pbLuaFunction = (LuaFunction)mapping.luaState["pitchbend"];
                    pcLuaFunction = (LuaFunction)mapping.luaState["programchange"];
                }

                if (!logicalInputDeviceDict.ContainsKey(logicalInputDeviceName))
                {
                    throw new ConfigurationException("Exception binding PerDeviceChannelMapping for " + key + ": unknown logical input device " + logicalInputDeviceName);
                }
                inputDevice = logicalInputDeviceDict[logicalInputDeviceName].device;

                foreach (MappingPatch mappingPatch in mappingPatches)
                {
                    mappingPatch.bind(soundGenerators);
                }

                foreach (NoteMapping noteMapping in noteMappings)
                {
                    noteMapping.bind(logicalInputDeviceDict, soundGenerators, mapping);
                }

                foreach (PitchBendMapping pitchBendMapping in pitchBendMappings)
                {
                    pitchBendMapping.bind(logicalInputDeviceDict, soundGenerators);
                }

                foreach (ControlMapping controlMapping in controlMappings)
                {
                    controlMapping.bind(logicalInputDeviceDict, soundGenerators, mapping);
                }
            }
        }
        
        public String name;

        public Dictionary<String, PerDeviceChannelMapping> perDeviceChannelMappings = new Dictionary<string, PerDeviceChannelMapping>();

        [JsonIgnore]
        public Lua luaState = new Lua();

        [JsonIgnore]
        public LuaFunction initLuaFunction;

        public virtual void bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators, String JoeMidiDirectory)
        {
            // If there's a lua script defined for this mapping, load it.
            String luaFilename = JoeMidiDirectory + @"\" + ReplaceInvalidFilenameChars(name) + ".lua";
            if (File.Exists(luaFilename)) {
                luaState.DoFile(luaFilename);
                initLuaFunction = (LuaFunction)luaState["init"];
            }

            foreach (PerDeviceChannelMapping perDeviceChannelMapping in perDeviceChannelMappings.Values)
            {
                try
                {
                    perDeviceChannelMapping.bind(logicalInputDeviceDict, soundGenerators, this);
                }
                catch (ConfigurationException ex)
                {
                    throw new ConfigurationException("Nested exception binding Mapping " + name + ": " + ex.Message);
                }
            }
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

        public static String ReplaceInvalidFilenameChars(String filename)
        {
            return String.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

    }
}
