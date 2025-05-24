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
        // Songs tab
        //**************************************************************************
        Song songBeingEdited;
        String originalSongTitle = "";
        bool creatingNewSong = false;
        bool creatingNewSongProgram = false;

        private void refreshSongEditSelector(string selectSong = null)
        {
            mbccSongEditSelector.clearButtons();
            foreach (Song song in mapper.configuration.getSortedSongList())
            {
                mbccSongEditSelector.addButton(song.name, song);
            }
            if (selectSong != null && selectSong != "")
            {
                mbccSongEditSelector.selectByName(selectSong);
            }
        }

        private void btnAddSong_Click(object sender, EventArgs e)
        {
            songBeingEdited = new Song();
            originalSongTitle = "";

            // Initialize the song edit fields
            tbSongTitle.Text = "";
            tbSongArtist.Text = "";
            tbSongChart.Text = "";
            nudSongChartPage.Value = 1;
            nudSongTranspose.Value = 0;
            nudBPM.Value = 0;
            lbSongPatches.Items.Clear();

            // Make note for commit-time that we're creating (not updating.)
            creatingNewSong = true;

            // Show the song editor controls. 
            pnlSongEdit.Visible = true;
            tbSongTitle.Select();
        }

        private void mbccSongEditSelector_Click(object sender, EventArgs e)
        {
            Song song = (Song)(((Button)sender).Tag);
            beginSongEdit(song);
        }

        private void beginSongEdit(Song song)
        {
            // Prepare for Editing of Song
            songBeingEdited = new Song(song);                               // Make a deep copy of the Song that we'll edit.
            originalSongTitle = songBeingEdited.name;                       // Save orig name so we can find orig Song in model to update it.

            // Set the song editor UI fields with info from the selected song
            tbSongTitle.Text = songBeingEdited.name;
            tbSongArtist.Text = songBeingEdited.artist;
            string chartsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\charts\";
            if (songBeingEdited.chartFile.ToLower().StartsWith(chartsFolder.ToLower()))
            {
                tbSongChart.Text = songBeingEdited.chartFile.Substring(chartsFolder.Length);
            }
            else
            {
                tbSongChart.Text = songBeingEdited.chartFile;
            }
            nudSongChartPage.Value = songBeingEdited.chartPage;
            nudSongTranspose.Value = songBeingEdited.songTranspose;
            nudBPM.Value = songBeingEdited.bpm;
            lbSongPatches.Items.Clear();
            foreach (SongProgram songProgram in songBeingEdited.programs)
            {
                lbSongPatches.Items.Add(songProgram.name);
            }

            // Make note for commit-time that we're updating (not creating.)
            creatingNewSong = false;

            // Show the song editor controls.  Hide the patch editor in case it happened to be up for the previous song. (Any edits to its patches are implicity abandoned...)
            pnlSongEdit.Visible = true;
            pnlPatchEdit.Visible = false;
            tbSongTitle.Select();
        }

        private void btnSongEditCancel_Click(object sender, EventArgs e)
        {
            cancelSongEdit();
        }

        private void cancelSongEdit()
        {
            songBeingEdited = null;
            originalSongTitle = "";
            pnlSongEdit.Visible = false;
            pnlPatchEdit.Visible = false;
        }

        private void btnSongEditOK_Click(object sender, EventArgs e)
        {
            completeSongEdit();
        }

        private void completeSongEdit()
        {
            // Gather the changed fields into the temp song object.  (Edits to its Programs will have already been applied...)
            songBeingEdited.name = tbSongTitle.Text;
            songBeingEdited.artist = tbSongArtist.Text;

            if (tbSongChart.Text.Length > 0 && tbSongChart.Text.Substring(1, 1) != ":" && tbSongChart.Text.Substring(0, 1) != "\\")
            {
                // Relative.  Make it absolute to the charts directory.
                string chartsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\charts\";
                songBeingEdited.chartFile = chartsFolder + tbSongChart.Text;
            }
            else
            {
                // Absolute.  Leave it alone
                songBeingEdited.chartFile = tbSongChart.Text;
            }
            songBeingEdited.chartPage = (int)nudSongChartPage.Value;
            songBeingEdited.songTranspose = (int)nudSongTranspose.Value;
            songBeingEdited.bpm = (int)nudBPM.Value;

            // Make the changes to the actual model objects
            if (creatingNewSong == true)
            {
                mapper.configuration.addSong(songBeingEdited);
            }
            else
            {
                mapper.configuration.updateSong(originalSongTitle, songBeingEdited);

                // Force the Setlist Songs list to reflect any change made to songs of the current setlist
                Song currentlySelectedSong = (Song)olvSongs.SelectedObject;
                olvSongs.SetObjects(currentSetlist.songs);
                olvSongs.SelectedObject = currentlySelectedSong;
                currentSong = currentlySelectedSong;
            }

            // Make sure edits are reflected in the "All" setlist.
            mapper.refreshAllPseudoSetlist();
            if (currentSetlist.name == "(All)")
            {
                refreshShowControls();
            }

            // Clean up edit state
            songBeingEdited = null;
            originalSongTitle = "";

            // Hide song edit controls
            pnlSongEdit.Visible = false;
            pnlPatchEdit.Visible = false;

            // Refresh the SongEditSelector in case this is a new song or its title changed
            refreshSongEditSelector(tbSongTitle.Text);
        }

        private void btnSongDel_Click(object sender, EventArgs e)
        {
            // If a song's open for editing and delete is pressed, delete the song open for editing.
            if (songBeingEdited != null)
            {
                deleteSong(songBeingEdited);
            }
            else if (mbccSongEditSelector.LastSelectedButtonTag != null)
            {
                // Otherwise, if there's a currently selected song in the Song Editor Selector delete it.
                deleteSong((Song)mbccSongEditSelector.LastSelectedButtonTag);
            }
        }

        private void deleteSong(Song songToDelete)
        {

            // Do the delete in the model
            mapper.configuration.deleteSong(songToDelete);

            // Refresh the SongEditSelector to reflect the deletion
            refreshSongEditSelector();

            // Force the Setlist Songs list to reflect any change made to songs of the current setlist
            olvSongs.SetObjects(currentSetlist.songs);

            // Make sure this delete is reflected in the "All" setlist.
            mapper.refreshAllPseudoSetlist();
            if (currentSetlist.name == "(All)")
            {
                refreshShowControls();
            }

            // Clean up editor state: you can no longer be editing a song you deleted.
            songBeingEdited = null;
            originalSongTitle = "";

            // Hide song edit controls in case they were open.
            pnlSongEdit.Visible = false;
            pnlPatchEdit.Visible = false;
        }

        SongProgram songProgramBeingEdited;
        int originalSongProgramIndex;

        private void BtnPatchAdd_Click(object sender, EventArgs e)
        {
            originalSongProgramIndex = -1;
            songProgramBeingEdited = new SongProgram();

            tbSongPatchPart.Text = "";
            nudSongPatchBank.Value = -1;
            nudSongPatchProgramNo.Value = -1;
            tbSongRelVol.Text = "0.0";
            populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvSongPatchPatches, "SG", true);
            creatingNewSongProgram = true;
            pnlPatchEdit.Visible = true;
            pnlSongEdit.Enabled = false;
        }

        private void lbSongPatches_DoubleClick(object sender, EventArgs e)
        {
            // Initiate editing of Song's patch

            int listIndex = lbSongPatches.SelectedIndex;

            if (listIndex >= 0 && listIndex < songBeingEdited.programs.Count)
            {
                originalSongProgramIndex = listIndex;
                songProgramBeingEdited = new SongProgram(songBeingEdited.programs[listIndex]);

                tbSongPatchPart.Text = songProgramBeingEdited.part;
                nudSongPatchBank.Value = songProgramBeingEdited.myBankNumber;
                tbSongRelVol.Text = songProgramBeingEdited.relativeVolume.ToString();
                nudSongPatchProgramNo.Value = songProgramBeingEdited.myPatchNumber;
                populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvSongPatchPatches, "SG", true);

                // Select song's patch in the song patches treeview
                foreach (TreeNode parent in tvSongPatchPatches.Nodes)
                {
                    if ((songProgramBeingEdited.bSingle == true && parent.Text.Equals(songProgramBeingEdited.SingleSoundGeneratorName) ||
                        songProgramBeingEdited.bSingle == false && parent.Tag == null))
                    {
                        foreach (TreeNode child in parent.Nodes)
                        {
                            if (child.Text.Equals(songProgramBeingEdited.name))
                            {
                                tvSongPatchPatches.SelectedNode = child;
                                break;
                            }
                        }
                        tvSongPatchPatches.Select();                 // HideSelection=false makes selection vis, but it's grey...  This doesn't seem to focus the control.
                        break;
                    }
                }

                creatingNewSongProgram = false;
                pnlSongEdit.Enabled = false;
                pnlPatchEdit.Visible = true;
            }
        }

        private void btnPatchEditCancel_Click(object sender, EventArgs e)
        {
            originalSongProgramIndex = -1;
            songProgramBeingEdited = null;
            pnlSongEdit.Enabled = true;
            pnlPatchEdit.Visible = false;
        }

        private void btnPatchEditOK_Click(object sender, EventArgs e)
        {
            TreeNode node = tvSongPatchPatches.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("You must pick a patch");
                return;
            }

            double relativeVolume = 0.0;
            if (tbSongRelVol.Text.Trim().Length > 0)
            {
                if (!double.TryParse(tbSongRelVol.Text.Trim(), out relativeVolume))
                {
                    MessageBox.Show("Illegal Relative Volume value entered: " + tbSongRelVol.Text);
                    return;
                }
            }

            songProgramBeingEdited.part = tbSongPatchPart.Text;
            songProgramBeingEdited.relativeVolume = relativeVolume;
            songProgramBeingEdited.myBankNumber = (int)nudSongPatchBank.Value;
            songProgramBeingEdited.myPatchNumber = (int)nudSongPatchProgramNo.Value;

            Object patchOrMapping = node.Tag;
            if (patchOrMapping is SoundGeneratorPatch)
            {
                songProgramBeingEdited.bSingle = true;

                SoundGeneratorPatch patch = (SoundGeneratorPatch)patchOrMapping;
                songProgramBeingEdited.SinglePatchName = patch.name;
                TreeNode parent = node.Parent;
                if (parent == null)
                {
                    MessageBox.Show("Bug: Null Sound Generator in Tree View");
                }
                songProgramBeingEdited.SingleSoundGeneratorName = ((SoundGenerator)parent.Tag).name;
                songProgramBeingEdited.MappingName = null;
            }
            else if (patchOrMapping is Mapping)
            {
                songProgramBeingEdited.bSingle = false;
                Mapping mapping = (Mapping)patchOrMapping;
                songProgramBeingEdited.MappingName = mapping.name;
                songProgramBeingEdited.SinglePatchName = null;
                songProgramBeingEdited.SingleSoundGeneratorName = null;
            }
            else
            {
                MessageBox.Show("You've picked a Sound Generator.  Instead, pick one of its patches");
                return;
            }

            songProgramBeingEdited.bind(mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators, mapper.configuration.mappings, mapper.configuration.primaryInputDevice);


            if (creatingNewSongProgram == true)
            {
                songBeingEdited.programs.Add(songProgramBeingEdited);
            }
            else
            {
                songBeingEdited.programs[originalSongProgramIndex] = songProgramBeingEdited;
            }

            refreshLbSongPatches();

            pnlSongEdit.Enabled = true;
            pnlPatchEdit.Visible = false;

        }

        private void tvSongPatchPatches_DoubleClick(object sender, EventArgs e)
        {
            btnPatchEditOK_Click(sender, e);
        }

        private void btnPatchDel_Click(object sender, EventArgs e)
        {
            int songPatchIndex = lbSongPatches.SelectedIndex;
            if (songPatchIndex >= 0 && songPatchIndex < songBeingEdited.programs.Count)
            {
                songBeingEdited.programs.RemoveAt(songPatchIndex);
                refreshLbSongPatches();
                pnlPatchEdit.Visible = false;
            }
        }

        private void refreshLbSongPatches()
        {
            lbSongPatches.Items.Clear();
            foreach (SongProgram songProgram in songBeingEdited.programs)
            {
                lbSongPatches.Items.Add(songProgram.name);
            }
        }

        private void btnPatchUp_Click(object sender, EventArgs e)
        {
            int songPatchIndex = lbSongPatches.SelectedIndex;
            if (songPatchIndex > 0 && songPatchIndex < songBeingEdited.programs.Count)
            {
                SongProgram programBeingMoved = songBeingEdited.programs[songPatchIndex];
                songBeingEdited.programs.RemoveAt(songPatchIndex);
                songBeingEdited.programs.Insert(songPatchIndex - 1, programBeingMoved);
                refreshLbSongPatches();
                lbSongPatches.SelectedIndex = songPatchIndex - 1;
            }
        }

        private void btnPatchDown_Click(object sender, EventArgs e)
        {
            int songPatchIndex = lbSongPatches.SelectedIndex;
            if (songPatchIndex >= 0 && songPatchIndex < songBeingEdited.programs.Count - 1)
            {
                SongProgram programBeingMoved = songBeingEdited.programs[songPatchIndex];
                songBeingEdited.programs.RemoveAt(songPatchIndex);
                songBeingEdited.programs.Insert(songPatchIndex + 1, programBeingMoved);
                refreshLbSongPatches();
                lbSongPatches.SelectedIndex = songPatchIndex + 1;
            }

        }
    }
}