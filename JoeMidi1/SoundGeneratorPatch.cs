using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JoeMidi1
{
    public class SoundGeneratorPatch
    {
        public String name;
        public String patchCategoryName;   
        public int soundGeneratorBank;                            // Should only sent if positive int 
        public int soundGeneratorPatchNumber;                     // Should only be sent if 0-127

        public SoundGeneratorPatch() { }

        public SoundGeneratorPatch(SoundGeneratorPatch orig)
        {
            name = orig.name;
            patchCategoryName = orig.patchCategoryName;
            soundGeneratorBank = orig.soundGeneratorBank;
            soundGeneratorPatchNumber = orig.soundGeneratorPatchNumber;
        }

        [JsonIgnore]
        public SoundGenerator soundGenerator;

        public void bind(SoundGenerator _soundGenerator)
        {
            soundGenerator = _soundGenerator;
        }

    }
}
