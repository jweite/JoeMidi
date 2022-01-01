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
        // Setlists tab
        //**************************************************************************
        Setlist setlistBeingEdited;
        String originalSetlistName = "";
        bool creatingNewSetlist = false;

        private void refreshSetlistEditSelector()
        {
            mbccSetlistEditSelector.clearButtons();
            List<Setlist> l = mapper.configuration.getSortedSetlistList();
            foreach (Setlist setlist in l)
            {
                mbccSetlistEditSelector.addButton(setlist.name, setlist);
                cbSetEditorSonglistSetSelector.Items.Add(setlist.name);
            }

            if (cbSetEditorSonglistSetSelector.SelectedIndex < 0)
            {
                cbSetEditorSonglistSetSelector.SelectedIndex = 0;
            }
        }

        private void btnSetlistAdd_Click(object sender, EventArgs e)
        {
            setlistBeingEdited = new Setlist();
            originalSetlistName = "";

            // Initialize the setlist edit fields
            tbSetlistName.Text = "";
            lbSetlistSongs.Items.Clear();

            // Make note for commit-time that we're creating (not updating.)
            creatingNewSetlist = true;

            // Show the setlist editor controls. 
            refreshSongsForSetlistsControl();

            pnlSetlistSongSelector.Visible = true;
            pnlSetlistEdit.Visible = true;

            tbSetlistName.Select();
        }

        private void mbccSetlistEditSelector_Click(object sender, EventArgs e)
        {
            Setlist setlist = (Setlist)(((Button)sender).Tag);
            beginSetlistEdit(setlist);
        }

        private void refreshLbSetlistSongs(String selectedItem = "")
        {
            int selectedIndex = -1;
            lbSetlistSongs.Items.Clear();
            foreach (String songTitle in setlistBeingEdited.songTitles)
            {
                lbSetlistSongs.Items.Add(songTitle);
                if (songTitle == selectedItem && selectedIndex < 0)
                {
                    selectedIndex = lbSetlistSongs.Items.Count - 1;
                }
            }
            // Mysteriously the last song isn't scrollable.  Adding a dummy entry
            lbSetlistSongs.Items.Add("");

            if (selectedIndex >= 0)
            {
                lbSetlistSongs.SelectedIndex = selectedIndex;
            }
        }

        private void beginSetlistEdit(Setlist setlist)
        {
            // Prepare for Editing of Setlist
            setlistBeingEdited = new Setlist(setlist);                      // Make a deep (but unbound) copy of the Setlist that we'll edit.
            originalSetlistName = setlistBeingEdited.name;                  // Save orig name so we can find orig Setlist in model to update it.

            // Set the setlist editor UI fields with info from the selected setlist
            tbSetlistName.Text = setlistBeingEdited.name;

            // Refresh the list controls' content
            refreshLbSetlistSongs();
            refreshSongsForSetlistsControl();

            // Make note for commit-time that we're updating (not creating.)
            creatingNewSetlist = false;

            // Show the setlist editor controls.  Hide the song selector in case it happened to be up for the previous setlist. (Any edits to its songs are implicity abandoned...)
            pnlSetlistEdit.Visible = true;
            pnlSetlistSongSelector.Visible = true;
            tbSetlistName.Select();
            // btnSetlistDel.Enabled = true;
        }

        private void btnSetlistEditCancel_Click(object sender, EventArgs e)
        {
            cancelSetlistEdit();
        }

        private void cancelSetlistEdit()
        {
            setlistBeingEdited = null;
            originalSetlistName = "";
            pnlSetlistEdit.Visible = false;
            pnlSetlistSongSelector.Visible = false;
            // btnSetlistDel.Enabled = false;
        }

        private void btnSetlistEditOK_Click(object sender, EventArgs e)
        {
            if (tbSetlistName.Text.Length == 0)
            {
                MessageBox.Show("You must enter a setlist name.");
                return;
            }
            else
            {
                completeSetlistEdit();
            }
        }

        private void completeSetlistEdit()
        {
            // Gather the changed fields into the temp Setlist object.  (Edits to its Songs will have already been applied...)
            setlistBeingEdited.name = tbSetlistName.Text;

            // Bind the now completed Setlist
            setlistBeingEdited.bind(mapper.configuration.songDict, mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators, mapper.configuration.mappings, mapper.configuration.primaryInputDevice);

            // Make the changes to the actual model objects
            if (creatingNewSetlist == true)
            {
                mapper.configuration.addSetlist(setlistBeingEdited);
            }
            else
            {
                mapper.configuration.deleteSetlistByName(originalSetlistName);
                mapper.configuration.addSetlist(setlistBeingEdited);

                // TBD: change any UI elements dependent on this setlist.
            }

            // The edit could change what's on the Show tab.  Refresh it.
            if (currentSetlist.name == originalSetlistName)
            {
                currentSetlist = setlistBeingEdited;
                refreshShowControls();
            }

            // Refresh the SetlistEditSelector in case this is a new setlist or its name changed
            refreshSetlistEditSelector();

            // Clean up edit state
            setlistBeingEdited = null;
            originalSetlistName = "";

            // Hide setlist edit controls
            pnlSetlistEdit.Visible = false;
            pnlSetlistSongSelector.Visible = false;
            // btnSetlistDel.Enabled = false;
        }

        private void btnSetlistDel_Click(object sender, EventArgs e)
        {
            if (setlistBeingEdited != null)
            {
                deleteSetlistSelectedForEditing(setlistBeingEdited.name);
            }
            else if (mbccSetlistEditSelector.LastSelectedButtonText != null)
            {
                deleteSetlistSelectedForEditing(mbccSetlistEditSelector.LastSelectedButtonText);
            }
        }

        private void deleteSetlistSelectedForEditing(string setlistName)
        {
            // Do the delete in the model
            mapper.configuration.deleteSetlistByName(setlistName);

            // Refresh the SetlistEditSelector to reflect the deletion
            refreshSetlistEditSelector();

            if (currentSetlist.name.Equals(setlistName))
            {
                currentSetlist = mapper.configuration.setlists.Find(sl => sl.name == "(All)");
                refreshShowControls();
            }

            // Clean up editor state
            setlistBeingEdited = null;
            originalSetlistName = "";

            // Hide setlist edit controls in case they were open.
            pnlSetlistEdit.Visible = false;
            pnlSetlistSongSelector.Visible = false;
            // btnSetlistDel.Enabled = false;
        }

        private void btnSetlistDeleteSong_Click(object sender, EventArgs e)
        {
            int songIndex = lbSetlistSongs.SelectedIndex;
            if (songIndex >= 0 && songIndex < setlistBeingEdited.songTitles.Count)
            {
                setlistBeingEdited.songTitles.RemoveAt(songIndex);
                refreshLbSetlistSongs();
            }
        }

        private void cbSetEditorSonglistSetSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshSongsForSetlistsControl();
        }

        private void refreshSongsForSetlistsControl()
        {
            tvSongsForSetlists.Nodes.Clear();
            IList<Song> songs = null;

            if (cbSetEditorSonglistSetSelector.SelectedIndex >= 0)
            {
                string selectedSetlist = (string)cbSetEditorSonglistSetSelector.Items[cbSetEditorSonglistSetSelector.SelectedIndex];
                Setlist setlist = mapper.configuration.setlists.Find(sl => sl.name == selectedSetlist);
                if (setlist != null)
                {
                    songs = setlist.songs;
                }
            }

            if (songs == null)
            {
                songs = mapper.configuration.getSortedSongList();
            }

            foreach (Song song in songs)
            {
                TreeNode node = new TreeNode(song.name + " - " + song.artist);
                node.Tag = song;
                tvSongsForSetlists.Nodes.Add(node);
            }
        }

        private void btnSetlistAddSong_Click(object sender, EventArgs e)
        {
            setlistAddSong();
            tvSongsForSetlists.Focus();
        }

        private void tvSongsForSetlists_DoubleClick(object sender, EventArgs e)
        {
            setlistAddSong();
        }

        private void setlistAddSong()
        {
            TreeNode selectedSongNode = tvSongsForSetlists.SelectedNode;
            if (selectedSongNode == null)
            {
                MessageBox.Show("You must select a song...");
                return;
            }
            Song selectedSong = (Song)selectedSongNode.Tag;

            setlistBeingEdited.songTitles.Add(selectedSong.name);

            refreshLbSetlistSongs(selectedSong.name);

            pnlSetlistSongSelector.Visible = true;

        }

        private void btnSetlistSongUp_Click(object sender, EventArgs e)
        {
            int songIndex = lbSetlistSongs.SelectedIndex;
            if (songIndex > 0 && songIndex < setlistBeingEdited.songTitles.Count)
            {
                // Change setlist
                String songTitleBeingMoved = setlistBeingEdited.songTitles[songIndex];
                setlistBeingEdited.songTitles.RemoveAt(songIndex);
                setlistBeingEdited.songTitles.Insert(songIndex - 1, songTitleBeingMoved);

                // Make identical change to list box
                object songListBoxItem = lbSetlistSongs.Items[songIndex];
                lbSetlistSongs.Items[songIndex] = lbSetlistSongs.Items[songIndex - 1];
                lbSetlistSongs.Items[songIndex - 1] = songListBoxItem;
                lbSetlistSongs.SelectedIndex = songIndex - 1;
            }
        }

        private void btnSetlistSongDown_Click(object sender, EventArgs e)
        {
            int songIndex = lbSetlistSongs.SelectedIndex;
            if (songIndex >= 0 && songIndex < setlistBeingEdited.songTitles.Count - 1)
            {
                // Change setlist
                String songTitleBeingMoved = setlistBeingEdited.songTitles[songIndex];
                setlistBeingEdited.songTitles.RemoveAt(songIndex);
                setlistBeingEdited.songTitles.Insert(songIndex + 1, songTitleBeingMoved);

                // Make identical change to list box
                object songListBoxItem = lbSetlistSongs.Items[songIndex];
                lbSetlistSongs.Items[songIndex] = lbSetlistSongs.Items[songIndex + 1];
                lbSetlistSongs.Items[songIndex + 1] = songListBoxItem;
                lbSetlistSongs.SelectedIndex = songIndex + 1;
            }
        }

        private void btnSetlistSort_Click(object sender, EventArgs e)
        {
            String selectedSong = "";
            int songIndex = lbSetlistSongs.SelectedIndex;
            if (songIndex > 0 && songIndex < setlistBeingEdited.songTitles.Count)
            {
                selectedSong = lbSetlistSongs.Items[songIndex].ToString();
            }

            setlistBeingEdited.songTitles.Sort();
        
            refreshLbSetlistSongs(selectedSong);

        }
    }
}
