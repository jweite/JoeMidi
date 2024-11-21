using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace JoeMidi1
{
    internal class FlattenedMapping
    {
        // These must be properties to work with WinForms Data Binding.  Cannot be simple public attributes.
        public String logicalInputDeviceName { get; set; }
        public int inputDeviceChannel { get; set; }
        public String soundGeneratorName { get; set; }
        public int soundGeneratorRelativeChannel { get; set; }
        public String patchName { get; set; }
        public double? volume { get; set; }
        public int lowestNote { get; set; }
        public int highestNote { get; set; }
        public int pitchOffset { get; set; }
        public double pbScale { get; set; }
        public int? damperRemap { get; set; }
        public int? modRemap { get; set; }
        public String additionalCCs { get; set; }

        public FlattenedMapping()
        {
            this.logicalInputDeviceName = "";
            this.inputDeviceChannel = 0;
            this.soundGeneratorName = "";
            this.soundGeneratorRelativeChannel = 0;
            this.patchName = "";
            this.volume = 0.0;
            this.lowestNote = 0;
            this.highestNote = 127;
            this.pitchOffset = 0;
            this.pbScale = 1.0;
            this.damperRemap = 64;
            this.modRemap = 1;
            this.additionalCCs = "";
        }

        public FlattenedMapping(Mapping.PerDeviceChannelMapping pdcm, String soundGeneratorName, int soundGeneratorRelativeChannel)
        {
            this.logicalInputDeviceName = pdcm.logicalInputDeviceName;
            this.inputDeviceChannel = pdcm.inputDeviceChannel;
            this.soundGeneratorName = soundGeneratorName;
            this.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
            this.patchName = "";
            this.volume = 0.0;
            this.lowestNote = 0;
            this.highestNote = 127;
            this.pitchOffset = 0;
            this.pbScale = 1.0;
            this.damperRemap = 64;
            this.modRemap = 1;
            this.additionalCCs = "";
        }

        public static Dictionary<String, FlattenedMapping> Flatten(Mapping mapping)
        {
            String keyPattern = "{0}\t{1}\t{2}\t{3}";

            Dictionary<String, FlattenedMapping> flattenedMappings = new Dictionary<string, FlattenedMapping>();

            foreach (Mapping.PerDeviceChannelMapping pdcm in mapping.perDeviceChannelMappings.Values)
            {
                foreach (MappingPatch mappingPatch in pdcm.mappingPatches)
                {
                    String key = String.Format(keyPattern, pdcm.logicalInputDeviceName, pdcm.inputDeviceChannel, mappingPatch.soundGeneratorName, mappingPatch.soundGeneratorRelativeChannel);
                    FlattenedMapping flattenedMapping = null;
                    if (flattenedMappings.ContainsKey(key))
                    {
                        flattenedMapping = flattenedMappings[key];
                    }
                    else
                    {
                        flattenedMapping = new FlattenedMapping(pdcm, mappingPatch.soundGeneratorName, mappingPatch.soundGeneratorRelativeChannel);
                        flattenedMappings.Add(key, flattenedMapping);
                    }
                    flattenedMapping.patchName = mappingPatch.patchName;
                    flattenedMapping.volume = mappingPatch.volume;
                }
                foreach (NoteMapping noteMapping in pdcm.noteMappings)
                {
                    String key = String.Format(keyPattern, pdcm.logicalInputDeviceName, pdcm.inputDeviceChannel, noteMapping.soundGeneratorName, noteMapping.soundGeneratorRelativeChannel);
                    FlattenedMapping flattenedMapping = null;
                    if (flattenedMappings.ContainsKey(key))
                    {
                        flattenedMapping = flattenedMappings[key];
                    }
                    else
                    {
                        flattenedMapping = new FlattenedMapping(pdcm, noteMapping.soundGeneratorName, noteMapping.soundGeneratorRelativeChannel);
                        flattenedMappings.Add(key, flattenedMapping);
                    }
                    flattenedMapping.lowestNote = noteMapping.lowestNote;
                    flattenedMapping.highestNote = noteMapping.highestNote;
                    flattenedMapping.pitchOffset = noteMapping.pitchOffset;
                }
                foreach (PitchBendMapping pbMapping in pdcm.pitchBendMappings)
                {
                    String key = String.Format(keyPattern, pdcm.logicalInputDeviceName, pdcm.inputDeviceChannel, pbMapping.soundGeneratorName, pbMapping.soundGeneratorRelativeChannel);
                    FlattenedMapping flattenedMapping = null;
                    if (flattenedMappings.ContainsKey(key))
                    {
                        flattenedMapping = flattenedMappings[key];
                    }
                    else
                    {
                        flattenedMapping = new FlattenedMapping(pdcm, pbMapping.soundGeneratorName, pbMapping.soundGeneratorRelativeChannel);
                        flattenedMappings.Add(key, flattenedMapping);
                    }
                    flattenedMapping.pbScale = pbMapping.scale;
                }
                foreach (ControlMapping ccMapping in pdcm.controlMappings)
                {
                    int DAMPER_CC = 64;
                    int MOD_CC = 1;
                    String key = String.Format(keyPattern, pdcm.logicalInputDeviceName, pdcm.inputDeviceChannel, ccMapping.soundGeneratorName, ccMapping.soundGeneratorRelativeChannel);
                    String formattedCcMapping = String.Format("{0},{1},{2},{3},{4}", ccMapping.sourceControlNumber, ccMapping.mappedControlNumber, ccMapping.min, ccMapping.max, ccMapping.initialValue);
                    FlattenedMapping flattenedMapping = null;
                    if (flattenedMappings.ContainsKey(key))
                    {
                        flattenedMapping = flattenedMappings[key];
                    }
                    else
                    {
                        flattenedMapping = new FlattenedMapping(pdcm, ccMapping.soundGeneratorName, ccMapping.soundGeneratorRelativeChannel);
                        flattenedMappings.Add(key, flattenedMapping);
                    }
                    if (ccMapping.sourceControlNumber == DAMPER_CC && ccMapping.min == 0 && ccMapping.max == 127 && ccMapping.initialValue == 0)
                    {
                        // Simplified handling of this common case
                        flattenedMapping.damperRemap = ccMapping.mappedControlNumber;
                    }
                    else if (ccMapping.sourceControlNumber == MOD_CC && ccMapping.min == 0 && ccMapping.max == 127 && ccMapping.initialValue == 0)
                    {
                        // Simplified handling of this common case
                        flattenedMapping.modRemap = ccMapping.mappedControlNumber;
                    }
                    else
                    {
                        if (flattenedMapping.additionalCCs.Length > 0)
                        {
                            flattenedMapping.additionalCCs += ("; ");
                        }
                        flattenedMapping.additionalCCs += formattedCcMapping;
                    }
                }
            }
            return flattenedMappings;
        }
    }
}

