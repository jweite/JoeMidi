 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Midi;

namespace JoeMidi1
{
    public class SoundGenerator
    {
        public String name;
        public String deviceName;
        public int nChannels;
        public int channelBase;

        // Dict, by name, of Patches available on the SoundGenerators.
        public Dictionary<String, SoundGeneratorPatch> soundGeneratorPatchDict = new Dictionary<string, SoundGeneratorPatch>();

        public SoundGenerator() { }

        public SoundGenerator(SoundGenerator original)
        {
            name = original.name;
            deviceName = original.deviceName;
            nChannels = original.nChannels;
            channelBase = original.channelBase;

            foreach (String patchName in original.soundGeneratorPatchDict.Keys)
            {
                soundGeneratorPatchDict.Add(patchName, new SoundGeneratorPatch(original.soundGeneratorPatchDict[patchName]));
            }

        }

        [JsonIgnore]
        public OutputDevice device;

        public bool bind(Dictionary<String, LogicalOutputDevice> logicalOutputDeviceDict)
        {
            if (logicalOutputDeviceDict.ContainsKey(deviceName)) {
                this.device = logicalOutputDeviceDict[deviceName].device;

                foreach (String key in soundGeneratorPatchDict.Keys)
                {
                    SoundGeneratorPatch patch = soundGeneratorPatchDict[key];
                    patch.bind(this);
                }

                return true;
            }
            else {
                this.device = null;
                return false;
            }
        }


        public static void createTrialConfiguration(Dictionary<String, SoundGenerator> soundGenerators)
        {
            // SoundGenerators
            SoundGenerator soundGenerator = new SoundGenerator();
            soundGenerator.name = "Proteus VX";
            soundGenerator.deviceName = "Output Device 1";
            soundGenerator.nChannels = 2;
            soundGenerator.channelBase = 0;

            SoundGeneratorPatch patch = new SoundGeneratorPatch();
            patch.name = "Dynamic Grand";
            patch.patchCategoryName = "Pianos";
            patch.soundGeneratorBank = -1;
            patch.soundGeneratorPatchNumber = 0;
            soundGenerator.soundGeneratorPatchDict.Add(patch.name, patch);

            patch = new SoundGeneratorPatch();
            patch.name = "EP-3";
            patch.patchCategoryName = "EPs";
            patch.soundGeneratorBank = -1;
            patch.soundGeneratorPatchNumber = 62;
            soundGenerator.soundGeneratorPatchDict.Add(patch.name, patch);

            patch = new SoundGeneratorPatch();
            patch.name = "EP-2";
            patch.patchCategoryName = "EPs";
            patch.soundGeneratorBank = -1;
            patch.soundGeneratorPatchNumber = 61;
            soundGenerator.soundGeneratorPatchDict.Add(patch.name, patch);

            patch = new SoundGeneratorPatch();
            patch.name = "Clavinetti";
            patch.patchCategoryName = "EPs";
            patch.soundGeneratorBank = -1;
            patch.soundGeneratorPatchNumber = 19;
            soundGenerator.soundGeneratorPatchDict.Add(patch.name, patch);

            soundGenerators.Add(soundGenerator.name, soundGenerator);

            soundGenerator = new SoundGenerator();
            soundGenerator.name = "VB3";
            soundGenerator.deviceName = "Output Device 1";
            soundGenerator.nChannels = 2;
            soundGenerator.channelBase = 2;

            patch = new SoundGeneratorPatch();
            patch.name = "Rock Organ 1";
            patch.patchCategoryName = "Organs";
            patch.soundGeneratorBank = -1;
            patch.soundGeneratorPatchNumber = 0;
            soundGenerator.soundGeneratorPatchDict.Add(patch.name, patch);
            
            soundGenerators.Add(soundGenerator.name, soundGenerator);

            soundGenerator = new SoundGenerator();
            soundGenerator.name = "SuperWave P8";
            soundGenerator.deviceName = "Output Device 1";
            soundGenerator.nChannels = 1;
            soundGenerator.channelBase = 4;

            patch = new SoundGeneratorPatch();
            patch.name = "Vintage Vince";
            patch.patchCategoryName = "Polysynths";
            patch.soundGeneratorBank = -1;
            patch.soundGeneratorPatchNumber = 39;
            soundGenerator.soundGeneratorPatchDict.Add(patch.name, patch);
            
            soundGenerators.Add(soundGenerator.name, soundGenerator);


            // For controlling VST Host
            soundGenerator = new SoundGenerator();
            soundGenerator.name = "Reaper";
            soundGenerator.deviceName = "Output Device 1";
            soundGenerator.nChannels = 1;
            soundGenerator.channelBase = 15;
            
            soundGenerators.Add(soundGenerator.name, soundGenerator);

        }
    }
}
