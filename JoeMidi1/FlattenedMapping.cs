using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using static JoeMidi1.Mapping;

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

        const int DAMPER_CC = 64;

        const int MOD_CC = 1;

        public FlattenedMapping(String defaultLogicalInputDeviceName = "")
        {
            this.logicalInputDeviceName = defaultLogicalInputDeviceName;
            this.inputDeviceChannel = 0;
            this.soundGeneratorName = "";
            this.soundGeneratorRelativeChannel = 0;
            this.patchName = "";
            this.volume = null;
            this.lowestNote = 0;
            this.highestNote = 127;
            this.pitchOffset = 0;
            this.pbScale = 1.0;
            this.damperRemap = DAMPER_CC;
            this.modRemap = MOD_CC;
            this.additionalCCs = "";
        }

        public FlattenedMapping(String logicalInputDeviceName, int inputDeviceChannel, String soundGeneratorName, int soundGeneratorRelativeChannel)
        {
            this.logicalInputDeviceName = logicalInputDeviceName;
            this.inputDeviceChannel = inputDeviceChannel;
            this.soundGeneratorName = soundGeneratorName;
            this.soundGeneratorRelativeChannel = soundGeneratorRelativeChannel;
            this.patchName = "";
            this.volume = 0.0;
            this.lowestNote = 0;
            this.highestNote = 127;
            this.pitchOffset = 0;
            this.pbScale = 1.0;
            this.damperRemap = DAMPER_CC;
            this.modRemap = MOD_CC;
            this.additionalCCs = "";
        }

        public static Dictionary<String, FlattenedMapping> Flatten(Mapping mapping)
        {

            Dictionary<String, FlattenedMapping> flattenedMappings = new Dictionary<string, FlattenedMapping>();

            foreach (Mapping.PerDeviceChannelMapping pdcm in mapping.perDeviceChannelMappings.Values)
            {
                foreach (MappingPatch mappingPatch in pdcm.mappingPatches)
                {
                    FlattenedMapping flattenedMapping = AssureFlattenedMappingExists(
                        flattenedMappings, 
                        pdcm.logicalInputDeviceName, 
                        pdcm.inputDeviceChannel, 
                        mappingPatch.soundGeneratorName, 
                        mappingPatch.soundGeneratorRelativeChannel
                    );
                    flattenedMapping.patchName = mappingPatch.patchName;
                    flattenedMapping.volume = mappingPatch.volume;
                }
                foreach (NoteMapping noteMapping in pdcm.noteMappings)
                {
                    FlattenedMapping flattenedMapping = AssureFlattenedMappingExists(
                        flattenedMappings,
                        pdcm.logicalInputDeviceName,
                        pdcm.inputDeviceChannel,
                        noteMapping.soundGeneratorName,
                        noteMapping.soundGeneratorRelativeChannel
                    );
                    flattenedMapping.lowestNote = noteMapping.lowestNote;
                    flattenedMapping.highestNote = noteMapping.highestNote;
                    flattenedMapping.pitchOffset = noteMapping.pitchOffset;
                }
                foreach (PitchBendMapping pbMapping in pdcm.pitchBendMappings)
                {
                    FlattenedMapping flattenedMapping = AssureFlattenedMappingExists(
                        flattenedMappings,
                        pdcm.logicalInputDeviceName,
                        pdcm.inputDeviceChannel,
                        pbMapping.soundGeneratorName,
                        pbMapping.soundGeneratorRelativeChannel
                    );
                    flattenedMapping.pbScale = pbMapping.scale;
                }
                foreach (ControlMapping ccMapping in pdcm.controlMappings)
                {
                    String formattedCcMapping = String.Format(
                        "{0},{1},{2},{3},{4}", 
                        ccMapping.sourceControlNumber, 
                        ccMapping.mappedControlNumber, 
                        ccMapping.min, 
                        ccMapping.max, 
                        ccMapping.initialValue
                    );
                    FlattenedMapping flattenedMapping = AssureFlattenedMappingExists(
                        flattenedMappings,
                        pdcm.logicalInputDeviceName,
                        pdcm.inputDeviceChannel,
                        ccMapping.soundGeneratorName,
                        ccMapping.soundGeneratorRelativeChannel
                    );
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

        static FlattenedMapping AssureFlattenedMappingExists(
            Dictionary<String, FlattenedMapping> mappingDict,
            String logicalInputDeviceName,
            int inputDeviceChannel,
            String soundGeneratorName,
            int soundGeneratorRelativeChannel)
        {
            String keyPattern = "{0}\t{1}\t{2}\t{3}";
            String key = String.Format(keyPattern, logicalInputDeviceName, inputDeviceChannel, soundGeneratorName, soundGeneratorRelativeChannel);
            FlattenedMapping flattenedMapping = null;
            if (mappingDict.ContainsKey(key))
            {
                flattenedMapping = mappingDict[key];
            }
            else
            {
                flattenedMapping = new FlattenedMapping(logicalInputDeviceName, inputDeviceChannel, soundGeneratorName, soundGeneratorRelativeChannel);
                mappingDict.Add(key, flattenedMapping);
            }
            return flattenedMapping;
        }

        public static Dictionary<String, PerDeviceChannelMapping> Unflatten(IList<FlattenedMapping> flattenedMappings)
        {
            Dictionary<String, PerDeviceChannelMapping> pdcmDict = new Dictionary<String, PerDeviceChannelMapping>();

            foreach (FlattenedMapping flattenedMapping in flattenedMappings)
            {
                PerDeviceChannelMapping pdcm = new PerDeviceChannelMapping(flattenedMapping.logicalInputDeviceName, flattenedMapping.inputDeviceChannel);
                if (pdcmDict.ContainsKey(pdcm.key))
                {
                    pdcm = pdcmDict[pdcm.key];
                }
                MappingPatch mappingPatch = new MappingPatch();
                mappingPatch.soundGeneratorName = flattenedMapping.soundGeneratorName;
                mappingPatch.soundGeneratorRelativeChannel = flattenedMapping.soundGeneratorRelativeChannel;
                mappingPatch.patchName = flattenedMapping.patchName;
                mappingPatch.volume = flattenedMapping.volume;
                pdcm.mappingPatches.Add(mappingPatch);

                NoteMapping noteMapping = new NoteMapping();
                noteMapping.soundGeneratorName = flattenedMapping.soundGeneratorName;
                noteMapping.soundGeneratorRelativeChannel = flattenedMapping.soundGeneratorRelativeChannel;
                noteMapping.lowestNote = flattenedMapping.lowestNote;
                noteMapping.highestNote = flattenedMapping.highestNote;
                noteMapping.pitchOffset = flattenedMapping.pitchOffset;
                pdcm.noteMappings.Add(noteMapping);

                PitchBendMapping pitchBendMapping = new PitchBendMapping();
                pitchBendMapping.soundGeneratorName = flattenedMapping.soundGeneratorName;
                pitchBendMapping.soundGeneratorRelativeChannel = flattenedMapping.soundGeneratorRelativeChannel;
                pitchBendMapping.scale = flattenedMapping.pbScale;
                pdcm.pitchBendMappings.Add(pitchBendMapping);

                if (flattenedMapping.damperRemap.HasValue)
                {
                    ControlMapping controlMapping = new ControlMapping();
                    controlMapping.soundGeneratorName = flattenedMapping.soundGeneratorName;
                    controlMapping.soundGeneratorRelativeChannel = flattenedMapping.soundGeneratorRelativeChannel;
                    controlMapping.sourceControlNumber = DAMPER_CC;
                    controlMapping.mappedControlNumber = (int)flattenedMapping.damperRemap;
                    controlMapping.min = 0;
                    controlMapping.max = 127;
                    controlMapping.initialValue = 0;
                    pdcm.controlMappings.Add(controlMapping);
                }
                if (flattenedMapping.modRemap.HasValue)
                {
                    ControlMapping controlMapping = new ControlMapping();
                    controlMapping.soundGeneratorName = flattenedMapping.soundGeneratorName;
                    controlMapping.soundGeneratorRelativeChannel = flattenedMapping.soundGeneratorRelativeChannel;
                    controlMapping.sourceControlNumber = MOD_CC;
                    controlMapping.mappedControlNumber = (int)flattenedMapping.modRemap;
                    controlMapping.min = 0;
                    controlMapping.max = 127;
                    controlMapping.initialValue = 0;
                    pdcm.controlMappings.Add(controlMapping);
                }

                String[] flattenedCCs = flattenedMapping.additionalCCs.Split(';');
                if (flattenedCCs.Length == 0)
                {
                    continue;
                }

                foreach (String cc in flattenedCCs)
                {
                    ControlMapping controlMapping = new ControlMapping();
                    controlMapping.soundGeneratorName = flattenedMapping.soundGeneratorName;
                    controlMapping.soundGeneratorRelativeChannel = flattenedMapping.soundGeneratorRelativeChannel;

                    String[] flattenedCCElements = cc.Split(',');
                    if (flattenedCCElements.Length == 0)
                    {
                        continue;
                    }

                    bool isValidFlattenedControlMapping = false;
                    for (int i = 0; i < flattenedCCElements.Length; ++i)
                    {
                        String flattenedCCElement = flattenedCCElements[i];

                        int parsedVal;
                        if (Int32.TryParse(flattenedCCElement, out parsedVal) == true)
                        {
                            switch (i)
                            {
                                case 0:
                                    controlMapping.sourceControlNumber = parsedVal;
                                    controlMapping.mappedControlNumber = parsedVal;     // Default everybody else, in case they're not subsequently defined.
                                    controlMapping.min = 0;
                                    controlMapping.max = 127;
                                    controlMapping.initialValue = 0;
                                    isValidFlattenedControlMapping = true;
                                    break;
                                case 1: controlMapping.mappedControlNumber = parsedVal; break;
                                case 2: controlMapping.min = parsedVal; break;
                                case 3: controlMapping.max = parsedVal; break;
                                case 4: controlMapping.initialValue = parsedVal; break;
                            }
                        }
                    }

                    if (isValidFlattenedControlMapping == true)
                    {
                        pdcm.controlMappings.Add(controlMapping);
                    }
                }
                if (pdcmDict.ContainsKey(pdcm.key))
                {
                    pdcmDict[pdcm.key] = pdcm;
                }
                else
                {
                    pdcmDict.Add(pdcm.key, pdcm);
                }
            }
            return pdcmDict;
        }
    }
}

