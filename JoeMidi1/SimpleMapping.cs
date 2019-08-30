using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoeMidi1
{
    public class SimpleMapping : Mapping
    {
        public class SimpleMappingDefinition
        {
            public bool bLower = false;
            String _programName;
            public String soundGeneratorName;
            public int transpose = 0;
            public float pbScale = 1.0F;
            public bool bEnaVolControl = true;
            public bool bEnaModControl = true;
            public bool bEnaDamperControl = true;
            public int initialVolume = -1;
            public int damperRemap = -1;
            public bool bDamplerToggle = false;

            public String programName
            {
                get
                {
                    return _programName;
                }
                set
                {
                    _programName = value;
                }
            }
        }

        public class PerDeviceSimpleMapping
        {
            public String inputDeviceName;
            public int inputDeviceChannel = 0;
            public int splitPoint = -1;
            public List<SimpleMappingDefinition> simpleMappingDefinitions = new List<SimpleMappingDefinition>();
        }

        public List<PerDeviceSimpleMapping> perDeviceSimpleMappings = new List<PerDeviceSimpleMapping>();

        override public bool bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
        {
            Dictionary<String, int> soundGeneratorChannelAssignments = new Dictionary<string, int>();

            perDeviceChannelMappings.Clear();

            foreach (PerDeviceSimpleMapping perDeviceSimpleMapping in perDeviceSimpleMappings)
            {
                Mapping.PerDeviceChannelMapping perDeviceChannelMapping = new Mapping.PerDeviceChannelMapping(perDeviceSimpleMapping.inputDeviceName, perDeviceSimpleMapping.inputDeviceChannel);

                foreach (SimpleMappingDefinition simpleMappingDefinition in perDeviceSimpleMapping.simpleMappingDefinitions)
                {
                    
                    NoteMapping noteMapping = new NoteMapping();
                    String soundGeneratorName = simpleMappingDefinition.soundGeneratorName;
                    noteMapping.soundGeneratorName = soundGeneratorName;

                    int soundGeneratorRelativeChannel = 0;
                    if (soundGeneratorChannelAssignments.ContainsKey(soundGeneratorName)) {
                        soundGeneratorRelativeChannel = soundGeneratorChannelAssignments[soundGeneratorName] + 1;
                        soundGeneratorChannelAssignments[soundGeneratorName] = soundGeneratorRelativeChannel;
                    }
                    else {
                        soundGeneratorRelativeChannel = 0;
                        soundGeneratorChannelAssignments.Add(soundGeneratorName, 0);
                    }
                    noteMapping.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;

                    if (perDeviceSimpleMapping.splitPoint == -1)
                    {
                        noteMapping.highestNote = 127;
                        noteMapping.lowestNote = 0;
                    }
                    else if (simpleMappingDefinition.bLower == true)
                    {
                        noteMapping.highestNote = perDeviceSimpleMapping.splitPoint - 1;
                        noteMapping.lowestNote = 0;
                    }
                    else
                    {
                        noteMapping.highestNote = 127;
                        noteMapping.lowestNote = perDeviceSimpleMapping.splitPoint;
                    }
                    noteMapping.pitchOffset = simpleMappingDefinition.transpose;
                    perDeviceChannelMapping.noteMappings.Add(noteMapping);

                    MappingPatch mappingPatch = new MappingPatch();
                    mappingPatch.patchName = simpleMappingDefinition.programName;
                    mappingPatch.soundGeneratorName = soundGeneratorName;
                    mappingPatch.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
                    perDeviceChannelMapping.mappingPatches.Add(mappingPatch);

                    if (simpleMappingDefinition.pbScale != 0) {
                        PitchBendMapping pitchBendMapping = new PitchBendMapping();
                        pitchBendMapping.soundGeneratorName = soundGeneratorName;
                        pitchBendMapping.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
                        pitchBendMapping.scale = simpleMappingDefinition.pbScale;
                        perDeviceChannelMapping.pitchBendMappings.Add(pitchBendMapping);
                    }

                    if (simpleMappingDefinition.bEnaVolControl == true || simpleMappingDefinition.initialVolume >= 0)
                    {
                        // This may be a mapping only, it may be an initial value setting only, or it may be both
                        ControlMapping ctlMapping = new ControlMapping();
                        ctlMapping.soundGeneratorName = soundGeneratorName;
                        ctlMapping.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
                        ctlMapping.sourceControlNumber = (simpleMappingDefinition.bEnaVolControl == true) ? 7 : -1;
                        ctlMapping.mappedControlNumber = 7;
                        ctlMapping.initialValue = (simpleMappingDefinition.initialVolume >= 0) ? simpleMappingDefinition.initialVolume : -1;
                        perDeviceChannelMapping.controlMappings.Add(ctlMapping);
                    }
                    if (simpleMappingDefinition.bEnaModControl == true)
                    {
                        ControlMapping ctlMapping = new ControlMapping();
                        ctlMapping.soundGeneratorName = soundGeneratorName;
                        ctlMapping.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
                        ctlMapping.sourceControlNumber = 1;
                        ctlMapping.mappedControlNumber = 1;
                        ctlMapping.initialValue = 0;
                        perDeviceChannelMapping.controlMappings.Add(ctlMapping);
                    }
                    if (simpleMappingDefinition.bEnaDamperControl == true)
                    {
                        ControlMapping ctlMapping = new ControlMapping();
                        ctlMapping.soundGeneratorName = soundGeneratorName;
                        ctlMapping.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
                        ctlMapping.sourceControlNumber = 64;
                        ctlMapping.mappedControlNumber = simpleMappingDefinition.damperRemap;
                        ctlMapping.bToggle = simpleMappingDefinition.bDamplerToggle;                    // Special option for damper: toggle mode: ie for Leslie ctl.
                        ctlMapping.initialValue = 0;
                        perDeviceChannelMapping.controlMappings.Add(ctlMapping);
                    }

                    {
                        // Kurzweil Artis Variation Button
                        ControlMapping ctlMapping = new ControlMapping();
                        ctlMapping.soundGeneratorName = soundGeneratorName;
                        ctlMapping.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
                        ctlMapping.sourceControlNumber = 0x1D;
                        ctlMapping.mappedControlNumber = 0x1D;
                        ctlMapping.initialValue = 0;
                        perDeviceChannelMapping.controlMappings.Add(ctlMapping);
                    }
                }

                perDeviceChannelMappings.Add(perDeviceChannelMapping.key, perDeviceChannelMapping);

            }

            return base.bind(logicalInputDeviceDict, soundGenerators);

        }



    }
}
