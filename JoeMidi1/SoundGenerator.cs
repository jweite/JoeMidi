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
        // A SoundGenerator is a named logical data structure that represents a midi device (physical or soft) that can, er, generate sounds.
        //  JoeMidi communicates with this midi device via a JoeMidi Logical Output Device.
        //  The sound generator is addressed on one or more contiguous midi channels.

        public String name;
        public String deviceName;       // JoeMidi Logical Output Device throught which the actual sound generating device is communicated with.
        public int channelBase;         // The first midi channel allocated to this SoundGenerator, over which it will communicate with the actual sound generating device.
        public int nChannels;           // Number of midi channels allocated to this SoundGenerator, typically to support multi-timbral use.
        public int cc7Min = 0;          // For scaling cc range of sound generators
        public int cc7Max = 127;        // For scaling cc range of sound generators
        public string track;            // The Reaper track reference (name or #number) for OSC.

        [JsonIgnore]
        public double cc7Scale = 1.0;

        // Dict, by name, of Patches available on the SoundGenerators. 
        //  Most critically each would typically have a midi Bank and program# for requesting it from the actual physical device.  
        public Dictionary<String, SoundGeneratorPatch> soundGeneratorPatchDict = new Dictionary<string, SoundGeneratorPatch>();

        public SoundGenerator() { }

        public SoundGenerator(SoundGenerator original)
        {
            name = original.name;
            deviceName = original.deviceName;
            nChannels = original.nChannels;
            channelBase = original.channelBase;
            cc7Max = original.cc7Max;
            cc7Min = original.cc7Min;
            track = original.track;

            foreach (String patchName in original.soundGeneratorPatchDict.Keys)
            {
                soundGeneratorPatchDict.Add(patchName, new SoundGeneratorPatch(original.soundGeneratorPatchDict[patchName]));
            }

        }

        [JsonIgnore]
        public OutputDevice device;

        public bool bind(Dictionary<String, LogicalOutputDevice> logicalOutputDeviceDict)
        {
            cc7Scale = ((double)cc7Max - (double)cc7Min) / (double)127;     // The portion of 127 of the actual output range

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

        public bool Equals(SoundGenerator other)
        {
            return name.Equals(other.name);
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
