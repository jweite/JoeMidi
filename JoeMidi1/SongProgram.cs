using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoeMidi1
{
    public class SongProgram : MidiProgram
    {
        public String part;

        public SongProgram() { }

        public SongProgram(SongProgram orig) : base(orig)
        {
            part = orig.part;
        }

        public static void createTrialConfiguration(Song song)
        {
            if (song.name.Equals("Josie"))
            {
                song.programs.Clear();
                
                SongProgram songProgram = new SongProgram();
                songProgram.part = "Verse";
                songProgram.myBankNumber = 0;
                songProgram.myPatchNumber = 0;
                songProgram.bSingle = true;
                songProgram.SingleSoundGeneratorName = "Proteus VX";
                songProgram.SinglePatchName = "EP-3";
                song.programs.Add(songProgram);

                songProgram = new SongProgram();
                songProgram.part = "";
                songProgram.myBankNumber = 0;
                songProgram.myPatchNumber = 1;
                songProgram.bSingle = false;
                songProgram.MappingName = "Vintage Vince";
                song.programs.Add(songProgram);

            }
            else if (song.name.Equals("Celebration"))
            {
                song.programs.Clear();
                SongProgram songProgram = new SongProgram();
                songProgram.part = "";
                songProgram.myBankNumber = 0;
                songProgram.myPatchNumber = 0;
                songProgram.bSingle = true;
                songProgram.SingleSoundGeneratorName = "Proteus VX";
                songProgram.SinglePatchName = "Dynamic Grand";
                song.programs.Add(songProgram);
            }
            else if (song.name.Equals("Loving Cup"))
            {
                song.programs.Clear();
                SongProgram songProgram = new SongProgram();
                songProgram.part = "";
                songProgram.myBankNumber = 0;
                songProgram.myPatchNumber = 0;
                songProgram.bSingle = true;
                songProgram.SingleSoundGeneratorName = "Proteus VX";
                songProgram.SinglePatchName = "Dynamic Grand";
                song.programs.Add(songProgram);

                songProgram = new SongProgram();
                songProgram.part = "";
                songProgram.myBankNumber = 0;
                songProgram.myPatchNumber = 1;
                songProgram.bSingle = false;
                songProgram.MappingName = "Rock Organ 1";
                song.programs.Add(songProgram);

            }
            else if (song.name.Equals("Jealous"))
            {
                song.programs.Clear();
                SongProgram songProgram = new SongProgram();
                songProgram.part = "";
                songProgram.myBankNumber = 0;
                songProgram.myPatchNumber = 0;
                songProgram.bSingle = true;
                songProgram.SingleSoundGeneratorName = "Proteus VX";
                songProgram.SinglePatchName = "Dynamic Grand";
                song.programs.Add(songProgram);
            }
        }
    }
}
