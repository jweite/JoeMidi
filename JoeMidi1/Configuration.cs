using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JoeMidi1
{
    class Configuration
    {
        // The overall configuration data class for JoeMidi, stored/retrieved in JSON.

        [JsonIgnore]
        public bool dirty = false;

        // A map of logial device names to a list of physical device names in priority order.  At startup JoeMidi will go through
        //  the dictionary, and for each logical device go through the list of its preferred physical devices, associating the first one
        //  found and openable with that logical device name.  This allows a single configuration to be established that adapts predictably
        //  to different devices being available on the computer at run time.
        public Dictionary<String, LogicalInputDevice> logicalInputDeviceDict = new Dictionary<String, LogicalInputDevice>();
        public Dictionary<String, LogicalOutputDevice> logicalOutputDeviceDict = new Dictionary<String, LogicalOutputDevice>();

        public String primaryInputDeviceName;           // Used as the default device when auto-creating single mappings.

        public Dictionary<String, String> randomAccessTabInputDeviceNames = new Dictionary<string, string>();

        public IDictionary<String, int[]> primaryControllerButtonProgramNumbers = new Dictionary<String, int[]>(); 

        [JsonIgnore]
        public LogicalInputDevice primaryInputDevice;

        // Dict, by name, of Sound Generating targets available.
        public Dictionary<String, SoundGenerator> soundGenerators = new Dictionary<String, SoundGenerator>();

        [JsonProperty(ItemTypeNameHandling=TypeNameHandling.Auto)]                                  // Since the entries in the Dictionary may be polymorphic.
        public Dictionary<String, Mapping> mappings = new Dictionary<string, Mapping>();

        public Dictionary<int, MidiProgram> midiPrograms = new Dictionary<int, MidiProgram>();

        public List<Mapping.PerDeviceChannelMapping> globalControlMappings = new List<Mapping.PerDeviceChannelMapping>();

        public List<Setlist> setlists = new List<Setlist>();

        public Dictionary<String, Song> songDict = new Dictionary<string, Song>();

        public List<String> patchCategories = new List<String>();

        public String volSliderInputDeviceName = "Input Device 2";

        public int volSliderMidiChannel = 0;

        public int volSliderControlNum = 75;

        public bool portraitMode = false;

        public String oscAddress = "127.0.0.1";

        public int oscPort = 8000;

        public String lastOpenedShowSetlist = "";

        [JsonIgnore]
        public int[] currentPrimaryControllerButtonProgramNumbers = new int[8];

        [JsonIgnore]
        public List<KeyValuePair<String, List<SoundGeneratorPatch>>> soundGeneratorPatchesByCategory
        {
            get
            {
                // Walk through the sound generators and their patches and assemble a dictionary of categories, each entry containing a list of patches of that category.
                Dictionary<String, List<SoundGeneratorPatch>> tempCatPatchDict = new Dictionary<String, List<SoundGeneratorPatch>>();

                foreach (SoundGenerator soundGenerator in soundGenerators.Values)
                {
                    foreach (SoundGeneratorPatch soundGeneratorPatch in soundGenerator.soundGeneratorPatchDict.Values)
                    {
                        String categoryName = (soundGeneratorPatch.patchCategoryName == null || soundGeneratorPatch.patchCategoryName.Length == 0) ? "<none>" : soundGeneratorPatch.patchCategoryName;
                        List<SoundGeneratorPatch> patchesForACategory;
                        if (tempCatPatchDict.ContainsKey(categoryName))
                        {
                            patchesForACategory = tempCatPatchDict[categoryName];
                        }
                        else
                        {
                            patchesForACategory = new List<SoundGeneratorPatch>();
                            tempCatPatchDict.Add(categoryName, patchesForACategory);
                        }
                        patchesForACategory.Add(soundGeneratorPatch);
                    }
                }

                // We want to return a sorted list of categories, each containing a sorted list of patches. 
                //  Transform the dictionary into a list of KeyValuePairs, sorting as appropriate.
                //  (We didn't just create this in the first place because the lookup overhead of lists was bad for the previous looped operation.)

                List<KeyValuePair<String, List<SoundGeneratorPatch>>> _soundGeneratorPatchesByCategory = new List<KeyValuePair<String, List<SoundGeneratorPatch>>>();
                foreach (String category in tempCatPatchDict.Keys)
                {
                    List<SoundGeneratorPatch> patchesForACategory = tempCatPatchDict[category];
                    patchesForACategory.Sort((a, b) => a.name.CompareTo(b.name));
                    KeyValuePair<String, List<SoundGeneratorPatch>> categoryEntry = new KeyValuePair<String, List<SoundGeneratorPatch>>(category, patchesForACategory);
                    _soundGeneratorPatchesByCategory.Add(categoryEntry);
                }

                _soundGeneratorPatchesByCategory.Sort((a, b) => a.Key.CompareTo(b.Key));
                return _soundGeneratorPatchesByCategory;
            }
        }
        
        public bool addSong(Song song)
        {
            if (song == null)
                return false;

            if (songDict.ContainsKey(song.name))
                return false;

            songDict.Add(song.name, song);

            dirty = true;

            return true;
        }

        public bool updateSong(String originalSongName, Song newSongValues)
        {
            if (newSongValues == null)
                return false;

            // Get a ref to the original song in the dict.  We'll update the existing Song rather than insert a new one that we'd have to re-bind.
            Song originalSong = songDict[originalSongName];

            // If the name has changed, we have to remove it from the dict and re-insert it keyed with the new name (later)
            if (!newSongValues.name.Equals(originalSongName))        
            {
                songDict.Remove(originalSongName);
            }

            // Update the original song that is (was) in the dict
            originalSong.name = newSongValues.name;
            originalSong.artist = newSongValues.artist;
            originalSong.chartFile = newSongValues.chartFile;
            originalSong.chartPage = newSongValues.chartPage;
            originalSong.songTranspose = newSongValues.songTranspose;
            originalSong.bpm = newSongValues.bpm;
            
            // There are no external references to the SongPrograms so we remove/forget the old and insert the new in their place.
            originalSong.programs.Clear();
            newSongValues.bind(logicalInputDeviceDict, soundGenerators, mappings, primaryInputDevice);
            foreach (SongProgram songProgram in newSongValues.programs)
            {
                originalSong.programs.Add(songProgram);
            }

            // If the name changed (and we removed the Song from the dict) put it back keyed with the new name
            if (!newSongValues.name.Equals(originalSongName))  
            {
                songDict.Add(originalSong.name, originalSong);
            }

            // Fix up any setlist name references to this song.
            foreach (Setlist setlist in setlists)
            {
                bool songFoundInSetlist = false;
                for (int i = 0; i < setlist.songTitles.Count; ++i)
                {
                    if (setlist.songTitles[i].Equals(originalSongName))
                    {
                        songFoundInSetlist = true;
                        setlist.songTitles[i] = newSongValues.name;
                    }
                }
                if (songFoundInSetlist)
                {
                    setlist.bind(songDict, logicalInputDeviceDict, soundGenerators, mappings, primaryInputDevice);
                }
            }

            dirty = true;
            
            return true;
        }

        public void deleteSong(Song song)
        {
            if (song == null)
            {
                return;
            }
            
            // Do the actual delete
            songDict.Remove(song.name);

            // Delete any refs to this song in the setlists
            foreach (Setlist setlist in setlists)
            {
                setlist.songTitles.Remove(song.name);
                setlist.songs.Remove(song);
            }

            // Mark config dirty so it gets written out on close.
            dirty = true;
        }

        public List<Song> getSortedSongList()
        {
            List<String> songTitleList = new List<String>();
            foreach (String songTitle in songDict.Keys)
            {
                songTitleList.Add(songTitle);
            }

            songTitleList.Sort();

            List<Song> sortedSongList = new List<Song>();
            foreach (String songTitle in songTitleList)
            {
                sortedSongList.Add(songDict[songTitle]);
            }
            
            return sortedSongList;
        }

        public bool addSetlist(Setlist setlist)
        {
            if (setlist == null)
                return false;

            setlists.Add(setlist);

            dirty = true;

            return true;
        }

        public bool deleteSetlistByName(String name)
        {
            foreach (Setlist setlist in setlists)
            {
                if (setlist.name.Equals(name))
                {
                    setlists.Remove(setlist);
                    dirty = true;
                    return true;
                }
            }
            return false;
        }

        public List<Setlist> getSortedSetlistList()
        {
            List<Setlist> l = new List<Setlist>(setlists);
            l.Sort((setlist1,setlist2)=>setlist1.name.CompareTo(setlist2.name));
            return l;
        }

        public void deleteSoundGenerator(String soundGeneratorName)
        {
            soundGenerators.Remove(soundGeneratorName);

            // Delete anything dependent on this
            foreach (Mapping mapping in mappings.Values)
            {
                foreach(Mapping.PerDeviceChannelMapping perDeviceChannelMapping in mapping.perDeviceChannelMappings.Values) {
                    perDeviceChannelMapping.mappingPatches.RemoveAll(o => o.soundGeneratorName.Equals(soundGeneratorName));
                    perDeviceChannelMapping.noteMappings.RemoveAll(o => o.soundGeneratorName.Equals(soundGeneratorName));
                    perDeviceChannelMapping.pitchBendMappings.RemoveAll(o => o.soundGeneratorName.Equals(soundGeneratorName));
                    perDeviceChannelMapping.controlMappings.RemoveAll(o => o.soundGeneratorName.Equals(soundGeneratorName));
                }
            }

            foreach ( var kvp in midiPrograms.Where(x => x.Value.SingleSoundGeneratorName.Equals(soundGeneratorName))) {
                midiPrograms.Remove(kvp.Key);
            }
            // How to delete midiPrograms dependent on mappings that were deleted...?

            foreach (String songTitle in songDict.Keys)
            {
                Song song = songDict[songTitle];
                song.programs.RemoveAll(o => o.SingleSoundGeneratorName.Equals(soundGeneratorName));
            }
        }

        public void createTrialConfiguration()
        {
            dirty = true;

            try
            {
                Mapping.PerDeviceChannelMapping globalPerInputDeviceChannelMapping = new Mapping.PerDeviceChannelMapping();
                globalPerInputDeviceChannelMapping.logicalInputDeviceName = "Input Device 1";
                globalPerInputDeviceChannelMapping.inputDeviceChannel = 0;

                ControlMapping globalControlMapping = new ControlMapping();
                globalControlMapping.soundGeneratorName = "Reaper";
                globalControlMapping.soundGeneratorRelativeChannel = 0;
                globalControlMapping.sourceControlNumber = 75;      // Top Left Rotary Knob on Oxygen
                globalControlMapping.mappedControlNumber = 9;       // I've got Reaper Master Volume mapped to this.
                globalControlMapping.min = 30;                      // This provides a nice workable vol range 
                globalControlMapping.max = 91;
                globalPerInputDeviceChannelMapping.controlMappings.Add(globalControlMapping);

                globalControlMappings.Add(globalPerInputDeviceChannelMapping);

                primaryInputDeviceName = "Input Device 1";

                LogicalInputDevice.createTrialConfiguration(logicalInputDeviceDict);

                LogicalOutputDevice.createTrialConfiguration(logicalOutputDeviceDict);

                SoundGenerator.createTrialConfiguration(soundGenerators);

                Mapping.createTrialConfiguration(mappings);

                MidiProgram.createTrialConfiguration(midiPrograms);

                Setlist.createTrialConfiguration(songDict, setlists);

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception creating trial configurations: " + e);
            }

        }


        public bool bind()
        {
            foreach (String key in logicalInputDeviceDict.Keys)
            {
                LogicalInputDevice device = logicalInputDeviceDict[key];
                if (device.bind() == false)
                {
                    return false;
                }
            }

            // Resolve the Primary Input Device.  (If non defined, pick the first on in the dict.)
            if (primaryInputDeviceName == null && logicalInputDeviceDict.Count > 0)
            {
                primaryInputDeviceName = logicalInputDeviceDict.Keys.First<String>();
                dirty = true;
            }

            if (logicalInputDeviceDict.ContainsKey(primaryInputDeviceName))
            {
                primaryInputDevice = logicalInputDeviceDict[primaryInputDeviceName];
            }
            else
            {
                MessageBox.Show("Cannot find primary logical input device by configured name " + primaryInputDeviceName);
                return false;
            }

            foreach (String key in logicalOutputDeviceDict.Keys)
            {
                LogicalOutputDevice device = logicalOutputDeviceDict[key];
                if (device.bind() == false)
                {
                    return false;
                }
            }

            foreach (String key in soundGenerators.Keys)
            {
                SoundGenerator soundGenerator = soundGenerators[key];
                if (soundGenerator.bind(logicalOutputDeviceDict) == false)
                {
                    return false;
                }
            }

            foreach (Mapping.PerDeviceChannelMapping perDeviceChannelMapping in globalControlMappings)
            {
                perDeviceChannelMapping.bind(logicalInputDeviceDict, soundGenerators);
            }

            foreach (String key in mappings.Keys)
            {
                Mapping mapping = mappings[key];
                if (mapping.bind(logicalInputDeviceDict, soundGenerators) == false)
                {
                    return false;
                }
            }

            foreach (int bankAndProgram in midiPrograms.Keys)
            {
                MidiProgram midiProgram = midiPrograms[bankAndProgram];
                midiProgram.bind(logicalInputDeviceDict, soundGenerators, mappings, primaryInputDevice);
            }

            // Drop up any non-bound midiPrograms (ie, that point to mappings or SoundGeneratorPatches that no longer exist)
            List<MidiProgram> midiProgramListClone = midiPrograms.Values.ToList<MidiProgram>();
            foreach (MidiProgram midiProgram in midiProgramListClone)
            {
                if (midiProgram.mapping == null)
                {
                    midiPrograms.Remove(midiProgram.key);
                }
            }

            foreach (String songTitle in songDict.Keys)
            {
                Song song = songDict[songTitle];
                song.bind(logicalInputDeviceDict, soundGenerators, mappings, primaryInputDevice);
            }

            foreach (Setlist setlist in setlists)
            {
                setlist.bind(songDict, logicalInputDeviceDict, soundGenerators, mappings, primaryInputDevice);
            }


            if (primaryControllerButtonProgramNumbers.Count == 0)
            {
                int[] casioPx3Buttons = new int[8] {0x0, 0x4, 0x5, 0x7, 0x12, 0x30, 0x19, 0x3D};
                primaryControllerButtonProgramNumbers.Add("CASIO USB-MIDI", casioPx3Buttons);
            }

            if (primaryControllerButtonProgramNumbers.ContainsKey(primaryInputDevice.device.Name)) {
                currentPrimaryControllerButtonProgramNumbers = primaryControllerButtonProgramNumbers[primaryInputDevice.device.Name];
            }
            else {
                currentPrimaryControllerButtonProgramNumbers = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            }

            return true;
        }

        [JsonIgnore]
        public List<Mapping> mappingsSorted
        {
            get
            {
                List<Mapping> _mappingsSorted = new List<Mapping>();
                foreach (Mapping mapping in mappings.Values)
                {
                    _mappingsSorted.Add(mapping);
                }
                _mappingsSorted.Sort((a,b) => a.name.CompareTo(b.name));
                return _mappingsSorted;
            }
        }

    }
}
