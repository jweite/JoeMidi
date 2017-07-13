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
        public String patchName;

        [JsonIgnore]
        public int patchNumber;

        [JsonIgnore]
        public int bank;

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
                return true;
            }
            else {
                return false;
            }
        }
    }
}
