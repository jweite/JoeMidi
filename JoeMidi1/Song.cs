using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoeMidi1
{
    public class Song
    {
        public String name;
        public String artist;
        public String chartFile;
        public int chartPage;
        public List<SongProgram> programs = new List<SongProgram>();
        public int songTranspose;
        public int bpm;

        public Song() {
            chartPage = 1;
            songTranspose = 0;
            bpm = 0;
        }

        public Song(Song orig)
        {
            name = orig.name;
            artist = orig.artist;
            chartFile = orig.chartFile;
            chartPage = orig.chartPage;
            songTranspose = orig.songTranspose;
            bpm = orig.bpm;

            foreach (SongProgram program in orig.programs)
            {
                programs.Add(new SongProgram(program));
            }
        }

        public static void createTrialConfiguration(Dictionary<String, Song> songDict, Setlist setList)
        {
            if (setList.name.Equals("Silas Knight Project"))
            {
                Song song = new Song();
                song.name = "Josie";
                song.artist = "Steely Dan";
                song.chartFile = "Josie 2.rtf";
                song.chartPage = 1;
                SongProgram.createTrialConfiguration(song);

                songDict.Add(song.name, song);
                setList.songTitles.Add(song.name);

                song = new Song();
                song.name = "Celebration";
                song.artist = "Kool";
                song.chartFile = "Celebration.rtf";
                SongProgram.createTrialConfiguration(song);

                songDict.Add(song.name, song);
                setList.songTitles.Add(song.name);
            }
            else if (setList.name.Equals("Cave Mollies"))
            {
                Song song = new Song();
                song.name = "Jealous";
                song.artist = "Black Crowes";
                song.chartFile = "Jealous.rtf";
                SongProgram.createTrialConfiguration(song);

                songDict.Add(song.name, song);
                setList.songTitles.Add(song.name);

                song = new Song();
                song.name = "Loving Cup";
                song.artist = "Stones";
                song.chartFile = "Loving Cup.rtf";
                SongProgram.createTrialConfiguration(song);

                songDict.Add(song.name, song);
                setList.songTitles.Add(song.name);
            }
            else
            {
                MessageBox.Show("Unknown setlist " + setList.name + " presented to Song.createTrialConfiguration");
            }
        }

        public void bind(Dictionary<String, LogicalInputDevice> logicalInputDeviceDict, Dictionary<String, SoundGenerator> soundGenerators, Dictionary<String, Mapping> mappings, LogicalInputDevice primaryInputDevice)
        {
            foreach (SongProgram songProgram in programs)
            {
                songProgram.bind(logicalInputDeviceDict, soundGenerators, mappings, primaryInputDevice);
            }
        }
    }
}
