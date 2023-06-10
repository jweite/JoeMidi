using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JoeMidi1
{
    public class SoundGeneratorPatch
    {
        // Represents a Patch (Sound) that can be requested of a SoundGenerator, with the midi params necessary to request it.

        public String name;
        public String patchCategoryName;   
        public int soundGeneratorBank;                            // Should only sent if positive int 
        public int soundGeneratorPatchNumber;                     // Should only be sent if 0-127.
        public List<String> fxPresets;                            // FX#:PresetName for up to 5 FX Slots, set in Reaper by OSC

        public SoundGeneratorPatch() {
            fxPresets = new List<string>();
        }

        public SoundGeneratorPatch(SoundGeneratorPatch orig)
        {
            name = orig.name;
            patchCategoryName = orig.patchCategoryName;
            soundGeneratorBank = orig.soundGeneratorBank;
            soundGeneratorPatchNumber = orig.soundGeneratorPatchNumber;
            fxPresets = new List<string>();
            if (orig.fxPresets != null)
            {
                foreach (String preset in orig.fxPresets)
                {
                    fxPresets.Add(preset);
                }
            }
        }

        [JsonIgnore]
        public SoundGenerator soundGenerator;

        public void bind(SoundGenerator _soundGenerator)
        {
            soundGenerator = _soundGenerator;
        }

    }
}
