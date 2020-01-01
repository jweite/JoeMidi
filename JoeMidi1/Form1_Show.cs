using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Midi;
using Newtonsoft.Json;
using PdfiumViewer;

namespace JoeMidi1
{
    public partial class Form1 : Form
    {

        //**************************************************************************
        // "Show" tab
        //**************************************************************************
        public Setlist currentSetlist;

        public Song currentSong;

        private void refreshShowControls()
        {
            olvSongs.SmallImageList = null;     // This is supposed to make the 20px gap at the left disappear... but it doesn't.
            //  http://objectlistview.sourceforge.net/cs/faq.html#why-is-the-text-of-the-first-column-indented-by-about-20-pixels

            if (currentSetlist != null)
            {
                olvSongs.SetObjects(currentSetlist.songs);
                if (currentSetlist.songs.Count > 0)
                {
                    olvSongs.SelectObject(currentSetlist.songs[0]);
                    currentSong = currentSetlist.songs[0];                      // ??? Does this make sense for all refreshes, or only the initial one ???
                }
                else
                {
                    currentSong = null;
                    olvSongs.SelectObject(null);
                    mbccShowSongPatches.clearButtons();
                    rtbChart.Clear();
                    pdfChart.Visible = false;
                }
            }
            else
            {
                currentSong = null;
                olvSongs.SetObjects(null);
                mbccShowSongPatches.clearButtons();
                rtbChart.Clear();
                pdfChart.Visible = false;
            }

            setupAlphaButtons();
            flpAlphaButtons.Visible = false;
        }

        private void setupAlphaButtons()
        {
            foreach (System.Windows.Forms.Control c in flpAlphaButtons.Controls)
            {
                c.Visible = false;
            }

            if (currentSetlist == null)
            {
                return;
            }

            foreach (Song s in currentSetlist.songs)
            {
                string firstLetter = s.name.Substring(0, 1).ToUpper();
                if (firstLetter.CompareTo("A") < 0 || firstLetter.CompareTo("Z") > 0)
                {
                    firstLetter = "0";
                }

                foreach (System.Windows.Forms.Control c in flpAlphaButtons.Controls)
                {
                    Button b = (Button)c;
                    if (b.Text == firstLetter)
                    {
                        b.Visible = true;
                        break;
                    }
                }
            }
        }

        private void btnSetlists_Click(object sender, EventArgs e)
        {
            fmSetlistPicker form = new fmSetlistPicker();
            form.Init(mapper.configuration.getSortedSetlistList());
            form.ShowDialog();
            if (form.IsOK == true)
            {
                String setlistName = form.Choice;
                for (int ii = 0; ii < mapper.configuration.setlists.Count; ++ii)
                {
                    if (mapper.configuration.setlists[ii].name == setlistName)
                    {
                        currentSetlist = mapper.configuration.setlists[ii];
                        refreshShowControls();
                        break;
                    }
                }
            }
        }

        private void olvSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSong = (Song)olvSongs.SelectedObject;
            RefreshShowSongPatches(currentSong);
        }

        private void RefreshShowSongPatches(Song song)
        {
            // Refresh the SongPatches multi-button-row-control
            mbccShowSongPatches.clearButtons();
            if (song != null)
            {
                foreach (SongProgram songProgram in song.programs)
                {
                    mbccShowSongPatches.addButton(songProgram.name, songProgram);
                }

                if (setlistControlExpanded == true)
                {
                    btnSetlistExpand_Click(null, null);     // Shrink it back down
                }

                // And display the song's chart
                showChart(song.chartFile, song.chartPage);

                // Activate the first program for this song
                if (song.programs.Count > 0)
                {
                    mbccShowSongPatches.selectLogicalButton(0, true, false);
                }

                mapper.masterTranspose = song.songTranspose;
            }
        }

        private void selectNextSong()
        {
            if (olvSongs.SelectedIndex < olvSongs.Items.Count)
            {
                ++olvSongs.SelectedIndex;
                olvSongs.SelectedItem.EnsureVisible();
            }
        }

        private void selectPrevSong()
        {
            if (olvSongs.SelectedIndex > 0)
            {
                --olvSongs.SelectedIndex;
                olvSongs.SelectedItem.EnsureVisible();
            }
        }

        private void mbccShowSongPatches_Click(object sender, EventArgs e)
        {
            SongProgram songProgram = (SongProgram)(((Button)sender).Tag);
            if (songProgram != null && songProgram.mapping != null)
            {
                // NOTE: if patch is deleted from sound gen, the songProgram still exists, but has no mapping.  For now we do nothing,
                //  but perhaps it'd be better to notify user...  But, maybe best not to interrupt the performance...
                mapper.SetMapping(songProgram.mapping);
            }
        }

        private void showChart(String chartFileName, int chartPage)
        {
            if (chartFileName == null) return;

            String myDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String directoryPath = myDocsFolder + @"\JoeMidi";
            String filePath;
            if (chartFileName.ToLower().StartsWith("c:") || chartFileName.ToLower().StartsWith("d:"))
            {
                // Absolute Path
                filePath = chartFileName;
            }
            else if (chartFileName.StartsWith(@"\"))
            {
                // Absolute Path w/ no drive: use drive letter only from Personal folder path.
                filePath = myDocsFolder.Substring(0, 2) + chartFileName;
            }
            else
            {
                // Relative Path
                filePath = directoryPath + @"\" + chartFileName;
            }

            if (File.Exists(filePath) == true)
            {
                if (filePath.ToLower().EndsWith(".rtf"))
                {
                    String chartContent = File.ReadAllText(filePath);
                    rtbChart.Rtf = chartContent;
                    // For consistency it'd be nice if we could honor chart-page for rtfs, though less essential, since they're less likely to be scans of songbooks.
                    positionAndShowChartControl(rtbChart, pdfChart);
                }
                else if (filePath.ToLower().EndsWith(".pdf"))
                {
                    var doc = PdfDocument.Load(filePath);
                    pdfChart.Load(doc);
                    pdfChart.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
                    pdfChart.Page = chartPage - 1;
                    positionAndShowChartControl(pdfChart, rtbChart);
                }
                else
                {
                    rtbChart.Rtf = "Cannot display chart files of this type.\n" + filePath;
                    pdfChart.Visible = false;
                    rtbChart.Visible = true;
                }
            }
            else
            {
                rtbChart.Rtf = "";
                rtbChart.Visible = false;
                pdfChart.Visible = false;
            }
        }

        private void positionAndShowChartControl(System.Windows.Forms.Control controlToShow, System.Windows.Forms.Control controlToHide)
        {
            controlToHide.Visible = false;

            if (mapper.configuration.portraitMode)
            {
                tlpShowOuter.SetColumn(controlToShow, 1);
                tlpShowOuter.SetRow(controlToShow, 0);
                tlpShowOuter.SetColumnSpan(controlToShow, 2);
                tlpShowOuter.SetRowSpan(controlToShow, 1);
            }
            else
            {
                tlpShowOuter.SetColumn(controlToShow, 2);
                tlpShowOuter.SetRow(controlToShow, 0);
                tlpShowOuter.SetRowSpan(controlToShow, 2);
                tlpShowOuter.SetColumnSpan(controlToShow, 1);
            }

            controlToShow.Visible = true;
        }

        private void btnNextSong_Click(object sender, EventArgs e)
        {
            if (olvSongs.SelectedIndex < olvSongs.Items.Count - 1)
            {
                ++olvSongs.SelectedIndex;
                olvSongs.SelectedItem.EnsureVisible();
            }

        }

        private void btnPrevSong_Click(object sender, EventArgs e)
        {
            if (olvSongs.SelectedIndex > 0)
            {
                --olvSongs.SelectedIndex;
                olvSongs.SelectedItem.EnsureVisible();
            }
        }

        //--------------------------------------------------------------------------
        // Button to toggle the Setlist Song Order between alpha order and originally defined order
        //--------------------------------------------------------------------------
        int lastSelectedSongIndex = -1;
        private void btnSetlistSongOrderToggle_Click(object sender, EventArgs e)
        {
            if (btnSetlistSongOrderToggle.Text.Equals("Alpha"))
            {
                lastSelectedSongIndex = olvSongs.SelectedIndex;
                btnSetlistSongOrderToggle.Text = "SL Ord";
                olvSongs.Sorting = SortOrder.Ascending;
                olvSongs.SelectedIndex = 0;
            }
            else
            {
                btnSetlistSongOrderToggle.Text = "Alpha";
                olvSongs.Sorting = SortOrder.None;
                olvSongs.SetObjects(currentSetlist.songs);
                if (lastSelectedSongIndex >= 0)
                {
                    olvSongs.SelectedIndex = lastSelectedSongIndex;
                }
            }
        }

        bool setlistControlExpanded = false;

        private void btnSetlistExpand_Click(object sender, EventArgs e)
        {
            if (setlistControlExpanded == false)
            {
                if (mapper.configuration.portraitMode == true)
                {
                    // For mysterious reasons I can't move the control in Row1 up into Row 0 if there's a control there...
                    tlpShowOuter.Controls.Remove(pdfChart);
                    tlpShowOuter.Controls.Remove(rtbChart);
                    tlpShowOuter.SetRow(tlpSongSetlistOuter, 0);
                    tlpShowOuter.SetRowSpan(tlpSongSetlistOuter, 2);
                }
                else
                {
                    // ...though I don't have that problem spanning down into a cell with a control.
                    tlpShowOuter.SetRow(tlpSongSetlistOuter, 0);
                    tlpShowOuter.SetRowSpan(tlpSongSetlistOuter, 2);
                }
                flpAlphaButtons.Visible = true;
                setlistControlExpanded = true;
            }
            else
            {
                if (mapper.configuration.portraitMode == true)
                {
                    tlpShowOuter.SetRowSpan(tlpSongSetlistOuter, 1);
                    tlpShowOuter.SetRow(tlpSongSetlistOuter, 1);
                    tlpShowOuter.Controls.Add(pdfChart, 0, 0);
                    tlpShowOuter.Controls.Add(rtbChart, 1, 0);
                }
                else
                {
                    tlpShowOuter.SetRowSpan(tlpSongSetlistOuter, 1);
                    tlpShowOuter.SetRow(tlpSongSetlistOuter, 0);
                }
                showChart(currentSong.chartFile, currentSong.chartPage);
                flpAlphaButtons.Visible = false;
                setlistControlExpanded = false;
            }
        }

        private void flpAlphaButtons_Resize(object sender, EventArgs e)
        {
            const int MIN_ALPHA_BUTTON_HEIGHT = 20;     // Based on font used for this button.

            int alphaButtonHeight = (flpAlphaButtons.Height - (flpAlphaButtons.Margin.Top + flpAlphaButtons.Margin.Bottom)) / 27;
            if (alphaButtonHeight < MIN_ALPHA_BUTTON_HEIGHT)
            {
                alphaButtonHeight = MIN_ALPHA_BUTTON_HEIGHT;
            }

            foreach (System.Windows.Forms.Control ctl in flpAlphaButtons.Controls)
            {
                // if typeof(ctl) != Button then next...

                Button b2 = (Button)ctl;
                b2.Height = alphaButtonHeight;
            }
        }

        private void btnAlpha_Click_1(object sender, EventArgs e)
        {
            string letterClicked = ((Button)sender).Text;
            if (letterClicked == "0")
            {
                olvSongs.EnsureVisible(0);
            }
            else
            {
                int i = 0;
                foreach (Object o in olvSongs.Objects)
                {

                    Song s = (Song)o;
                    if (s.name.StartsWith(letterClicked))
                    {
                        olvSongs.TopItemIndex = i;
                        break;
                    }
                    ++i;
                }
            }
        }

        private void vsbVol2_Scroll(object sender, ScrollEventArgs e)
        {
            vsbVol1.Value = vsbVol2.Value;      // Vol Control on Random Access tabs track this one.
            int vol = 127 - vsbVol2.Value;
            mapper.changeMasterVol(vol);
        }
    }
}
