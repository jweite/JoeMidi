 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Midi;
using System.IO;

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
        public double? volume = null;   // The Reaper track volume in db.  If null, track volume will be left unchanged from the Reaper project default.
        public List<String> fxPresetDefaults;  // FX#:PresetName for up to 5 FX Slots, set in Reaper by OSC
        public Dictionary<String, int> fxSlotNames;
        public String clonePatchesFrom; // Another Sound Generator from which this one inherits patche definitions.
        public String reaperPresetFilePath; // To enable auto-generation of SoundGeneratorPatches from Reaper presets.

        [JsonIgnore]
        public double cc7Scale = 1.0;

        // Dict, by name, of Patches available on the SoundGenerators. 
        //  Most critically each would typically have a midi Bank and program# for requesting it from the actual physical device.  
        public Dictionary<String, SoundGeneratorPatch> soundGeneratorPatchDict;

        public SoundGenerator() {
            soundGeneratorPatchDict = new Dictionary<string, SoundGeneratorPatch>();
            fxPresetDefaults = new List<string>();
            fxSlotNames = new Dictionary<string, int>();
        }

        public SoundGenerator(SoundGenerator original)
        {
            soundGeneratorPatchDict = new Dictionary<string, SoundGeneratorPatch>();
            fxPresetDefaults = new List<string>();
            fxSlotNames = new Dictionary<string, int>();

            name = original.name;
            deviceName = original.deviceName;
            nChannels = original.nChannels;
            channelBase = original.channelBase;
            cc7Max = original.cc7Max;
            cc7Min = original.cc7Min;
            track = original.track;
            volume = original.volume;
            clonePatchesFrom = original.clonePatchesFrom;

            foreach (String patchName in original.soundGeneratorPatchDict.Keys)
            {
                soundGeneratorPatchDict.Add(patchName, new SoundGeneratorPatch(original.soundGeneratorPatchDict[patchName]));
            }

            foreach (String fxPresetDefault in original.fxPresetDefaults)
            {
                fxPresetDefaults.Add(fxPresetDefault);
            }

            foreach (var fxSlotName in fxSlotNames.Keys)
            {
                fxSlotNames[fxSlotName] = original.fxSlotNames[fxSlotName];
            }
        }

        [JsonIgnore]
        public OutputDevice device;

        public void bind(Dictionary<String, LogicalOutputDevice> logicalOutputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators)
        {
            cc7Scale = ((double)cc7Max - (double)cc7Min) / (double)127;     // The portion of 127 of the actual output range

            if (logicalOutputDeviceDict.ContainsKey(deviceName)) {
                this.device = logicalOutputDeviceDict[deviceName].device;

                if (this.clonePatchesFrom != null && soundGenerators.ContainsKey(this.clonePatchesFrom)) {
                    SoundGenerator cloneFrom = soundGenerators[this.clonePatchesFrom];
                    soundGeneratorPatchDict.Clear();
                    foreach (var patchName in cloneFrom.soundGeneratorPatchDict.Keys)
                    {
                        soundGeneratorPatchDict.Add(patchName, new SoundGeneratorPatch(cloneFrom.soundGeneratorPatchDict[patchName]));
                    }
                }

                foreach (String key in soundGeneratorPatchDict.Keys)
                {
                    SoundGeneratorPatch patch = soundGeneratorPatchDict[key];
                    patch.bind(this);
                }
            }
            else {
                this.device = null;
                throw new ConfigurationException("Exception binding sound generator: cannot find logical device " + deviceName);
            }
        }

        public bool Equals(SoundGenerator other)
        {
            return name.Equals(other.name);
        }

        public void AutoGeneratePatchesFromReaperPresets()
        {
            if (File.Exists(this.reaperPresetFilePath))
            {
                var lines = File.ReadLines(this.reaperPresetFilePath);
                foreach (var line in lines) {
                    if (line.StartsWith("Name="))
                    {
                        var presetName = line.Substring(5);     // Length of "Name="
                        if (!this.soundGeneratorPatchDict.ContainsKey(presetName)) {
                            var patch = new SoundGeneratorPatch();
                            patch.name = presetName;
                            patch.soundGeneratorBank = -1;
                            patch.soundGeneratorPatchNumber = -1;       // Enables OSC-based Preset Changing
                            patch.fxPresets.Add("VSTi:" + presetName);  // Defines OSC-based Preset to select.
                            patch.bind(this);
                            this.soundGeneratorPatchDict.Add(presetName, patch);
                        }
                    }
                }
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
