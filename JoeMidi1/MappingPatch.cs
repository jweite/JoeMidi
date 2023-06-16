using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JoeMidi1
{
    public class MappingPatch : SoundGeneratorChannel
    {
        // Class for capturing a bank/program change for mapping (i.e. to be sent out when the mapping is activated).

        public String patchName;

        [JsonIgnore]
        public int patchNumber;

        [JsonIgnore]
        public int bank;

        [JsonIgnore]
        public string track;

        [JsonIgnore]
        public Dictionary<int, String> fxPresets;        // fxSlotNum:fxPresetName

        [JsonIgnore]
        public double? volume = null;

        public static void createTrialConfiguration(int whichMappingToCreate, List<MappingPatch> mappingPatches)
        {
            MappingPatch mappingPatch = new MappingPatch();

            switch (whichMappingToCreate)
            {
                case 2:
                    mappingPatch.soundGeneratorName = "VB3";
                    mappingPatch.soundGeneratorRelativeChannel = 0;
                    mappingPatch.patchName = "Rock Organ 1";
                    mappingPatches.Add(mappingPatch);
                    break;
                case 3:
                    mappingPatch.soundGeneratorName = "SuperWave P8";
                    mappingPatch.soundGeneratorRelativeChannel = 0;
                    mappingPatch.patchName = "Vintage Vince";
                    mappingPatches.Add(mappingPatch);
                    break;
                default:
                    MessageBox.Show("Unknown trial configuration mappingPatch requested: " + whichMappingToCreate);
                    break;
            }
        }

        public override bool bind(Dictionary<String, SoundGenerator> soundGenerators)
        {
            if (base.bind(soundGenerators) == false)
            {
                return false;
            }

            if (soundGenerator.soundGeneratorPatchDict.ContainsKey(patchName)) {
                SoundGeneratorPatch soundGeneratorPatch = soundGenerator.soundGeneratorPatchDict[patchName];
                bank = soundGeneratorPatch.soundGeneratorBank;
                patchNumber = soundGeneratorPatch.soundGeneratorPatchNumber;
                track = soundGenerator.track;

                // FX Presets
                if (fxPresets == null)
                {
                    fxPresets = new Dictionary<int, String>();
                }
                else
                {
                    fxPresets.Clear();
                }

                // Merge the sound generator default FX presets with the patch's patch-specific ones by preset slot.
                if (soundGenerator.fxPresetDefaults != null) {
                    foreach (String fxPresetDefault in soundGenerator.fxPresetDefaults)
                    {
                        var split = fxPresetDefault.Split(':');     // Expected format: fxSlotNum:fxPresetName
                        if (split.Length == 2)
                        {
                            int fxNum = 0;
                            if (int.TryParse(split[0], out fxNum) && fxNum > 0)
                            {
                                fxPresets.Add(fxNum, split[1]);
                            }
                        }
                    }
                }
                if (soundGeneratorPatch.fxPresets != null) {
                    foreach (String fxPreset in soundGeneratorPatch.fxPresets)
                    {
                        var split = fxPreset.Split(':');     // Expected format: fxSlotNum:fxPresetName
                        if (split.Length == 2)
                        {
                            int fxNum = 0;
                            if (split[0].StartsWith("#"))
                            {
                                int.TryParse(split[0].Substring(1), out fxNum);
                            }
                            else
                            {
                                if (soundGenerator.fxSlotNames.ContainsKey(split[0])) {
                                    fxNum = soundGenerator.fxSlotNames[split[0]];
                                }
                            }

                            if (fxNum > 0)
                            {
                                if (fxPresets.ContainsKey(fxNum)) {
                                    fxPresets[fxNum] = split[1];
                                }
                                else
                                {
                                    fxPresets.Add(fxNum, split[1]);
                                }
                            }
                        }
                    }
                }

                if (soundGeneratorPatch.volumeOverride != null)
                {
                    volume = soundGeneratorPatch.volumeOverride;
                }
                else if (soundGenerator.volume != null)
                {
                    volume = soundGenerator.volume;
                }
                else
                {
                    volume = null;
                }

                return true;
            }
            else {
                return false;
            }
        }
    }
}
