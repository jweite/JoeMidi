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
using static JoeMidi1.SimpleMapping;

namespace JoeMidi1
{
    public partial class Form1 : Form
    {
        //**************************************************************************
        // Mappings2 Editor
        //**************************************************************************

        // Refresh the entries in the mapping selector.
        private void refreshMappingToEditSelector2()
        {
            mbrcMappingSelect2.clearButtons();
            foreach (Mapping mapping in mapper.configuration.mappingsSorted)
            {
                mbrcMappingSelect2.addButton(mapping.name, mapping);
            }
        }

        // Mapping editor state varaibles
        Mapping mappingBeingEdited2 = null;
        bool creatingNewMapping2 = false;


        BindingList<FlattenedMapping> bindingList;

        // A Mapping was selected for editing
        private void mbrcMappingSelect2_Click(object sender, EventArgs e)
        {
            // Update the editor state variables to communicate to the OK method.
            Mapping mapping = (Mapping)(((Button)sender).Tag);
            mappingBeingEdited2 = mapping;
            creatingNewMapping2 = false;

            // Initialize Mapping2 Editor UI elements with this mapping
            Dictionary<String, FlattenedMapping> flattenedMappings = FlattenedMapping.Flatten(mappingBeingEdited2);

            var flattenedMappingList = flattenedMappings.Values.ToList<FlattenedMapping>();

            this.bindingList = new BindingList<FlattenedMapping>(flattenedMappingList);

            var source = new BindingSource(bindingList, null);
            
            dgvMappings.DataSource = source;

            tbMappingName2.Text = mapping.name;

            // Make Mapping2 Editor UI elements visible
            showEditorUiElements();
        }

        private void btnMappingAdd2_Click(object sender, EventArgs e)
        {
            // Create a new empty Mapping and initialize the editor state variables.
            mappingBeingEdited2 = new Mapping();
            creatingNewMapping2 = true;

            MessageBox.Show("A new empty mapping is created for editing");

            // Initialize Mapping2 Editor UI elements with this mapping
            tbMappingName2.Text = "";

            // Make Mapping2 Editor UI elements visible
            showEditorUiElements();

        }

        private void btnMappingEditCancel2_Click(object sender, EventArgs e)
        {
            mappingBeingEdited2 = null;
            creatingNewMapping2 = false;
            // dgvMappings.DataSource = null;
            tbMappingName2.Text = "";

            // Hide mapping editor UI controls
            hideEditorUiElements();

        }
        private void hideEditorUiElements()
        {
            dgvMappings.Visible = false;
            tbMappingName2.Visible = false;
            label30.Visible = false;
            btnMappingEditCancel2.Visible = false;
            btnMappingEditOk2.Visible = false;
        }
        private void showEditorUiElements()
        {
            dgvMappings.Visible = true;
            tbMappingName2.Visible = true;
            label30.Visible = true;
            btnMappingEditCancel2.Visible = true;
            btnMappingEditOk2.Visible = true;
        }

        private void btnMappingEditOk2_Click(object sender, EventArgs e)
        {
            Mapping editedMapping = new Mapping();      // Reverting from SimpleMapping to Mapping after editing here...

            if (creatingNewMapping == true)
            {
                // Make sure the new mapping name entered is OK
                String mappingName = tbMappingName.Text.Trim();
                if (mappingName.Length == 0)
                {
                    MessageBox.Show("You must enter a Mapping Name");
                    return;
                }

                if (mapper.configuration.mappings.ContainsKey(mappingName))
                {
                    MessageBox.Show("Proposed mapping name " + mappingName + " is already in use.");
                    return;
                }

                editedMapping.name = tbMappingName.Text;
            }
            else
            {
                editedMapping.name = mappingBeingEdited2.name;
            }

            var pdcmsDict = FlattenedMapping.Unflatten(this.bindingList);
            editedMapping.perDeviceChannelMappings = pdcmsDict;

            // Flesh out the mapping internals
            editedMapping.bind(mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators);

            // Store it
            if (creatingNewMapping)
            {
                mapper.configuration.mappings.Add(editedMapping.name, editedMapping);
            }
            else
            {
                mapper.configuration.mappings[editedMapping.name] = editedMapping;
            }
            mapper.configuration.dirty = true;

            // Activate it...
            mapper.SetMapping(editedMapping);

            mappingBeingEdited2 = null;

            // Hide the Mapping Editor UI elements
            hideEditorUiElements();

            // Refresh other selectors that may not be showing this (new) mapping
//            if (creatingNewMapping)
//            {
                refreshMappingToEditSelector2();
                refreshMappingToEditSelector();
                btnPatchTreeViewBySG_Click(null, null);
//            }
        }


        private void btnMappingDelete2_Click(object sender, EventArgs e)
        {
            Mapping mapping = mappingBeingEdited2;
            if (mapping == null) return;

            mapper.configuration.mappings.Remove(mapping.name);

            //Remove any SongPrograms that pointed to this mapping from their Songs.
            foreach (Song song in mapper.configuration.songDict.Values)
            {
                List<SongProgram> songProgramListClone = song.programs.ToList<SongProgram>();
                foreach (SongProgram songProgram in songProgramListClone)
                {
                    if (songProgram.mapping == mapping)
                    {
                        song.programs.Remove(songProgram);
                    }
                }
            }

            refreshMappingToEditSelector2();
            refreshMappingToEditSelector();
            btnPatchTreeViewBySG_Click(null, null);

            // Hide any editor UI elements that may be visible.
            hideEditorUiElements();
        }

        private void dgvMappings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Grid DblClick: Row " + e.RowIndex);
        }

        private void dgvMappings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
