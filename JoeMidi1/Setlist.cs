using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JoeMidi1
{
    public class Setlist
    {
        public String name;
        public List<String> songTitles = new List<String>();

        [JsonIgnore]
        public List<Song> songs = new List<Song>();

        public Setlist() {}

        public Setlist(Setlist orig)
        {
            name = orig.name;
            foreach (String songTitle in orig.songTitles)
            {
                songTitles.Add(songTitle);
            }
        }

        public int GetSongIndex(String songName)
        {
            var songIndex = -1;
            foreach (String songTitle in songTitles)
            {
                ++songIndex;
                if (songName == songTitle)
                {
                    return songIndex;
                }
            }
            return -1;
        }

        public Song GetSong(String songName)
        {
            var songIndex = GetSongIndex(songName);
            return songIndex >= 0 && songIndex < songs.Count ? songs[songIndex] : null;
        }


        public static void createTrialConfiguration(Dictionary<String, Song> songDict, List<Setlist> setlists)
        {
            Setlist setlist = new Setlist();
            setlist.name = "Silas Knight Project";
            Song.createTrialConfiguration(songDict, setlist);
            setlists.Add(setlist);

            setlist = new Setlist();
            setlist.name = "Cave Mollies";
            Song.createTrialConfiguration(songDict, setlist);
            setlists.Add(setlist);
        }

        public void bind(Dictionary<String, Song> songDict, Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators, Dictionary<String, Mapping> mappings, LogicalInputDevice primaryInputDevice)
        {
            songs.Clear();

            foreach (String songTitle in songTitles)
            {
                if (songDict.ContainsKey(songTitle))
                {
                    Song song = songDict[songTitle];
                    songs.Add(song);
//                    song.bind(logicalInputDeviceDict, soundGenerators, mappings, primaryInputDevice);
                }
            }
        }

    }
}
