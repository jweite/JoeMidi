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
        private void refreshMappingToEditSelector2(String nameToSelect = null)
        {
            mbrcMappingSelect2.clearButtons();
            foreach (Mapping mapping in mapper.configuration.mappingsSorted)
            {
                mbrcMappingSelect2.addButton(mapping.name, mapping);
            }
            if (nameToSelect != null)
            {
                mbrcMappingSelect2.selectByName(nameToSelect);
            }
        }

        private void SetMappingGridComboBoxChoices()
        {
            DataGridViewComboBoxColumn logicalInputDeviceColumn = dgvMappings.Columns[0] as DataGridViewComboBoxColumn;
            foreach (String key in mapper.configuration.logicalInputDeviceDict.Keys)
            {
                LogicalInputDevice logicalInputDevice = mapper.configuration.logicalInputDeviceDict[key];
                logicalInputDeviceColumn.Items.Add(logicalInputDevice.logicalDeviceName);
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
            btnMappingDelete2.Enabled = true;
            btnMappingDelete2.ForeColor = Color.Black;

        }

        private void btnMappingAdd2_Click(object sender, EventArgs e)
        {
            // Create a new empty Mapping and initialize the editor state variables.
            mappingBeingEdited2 = new Mapping();
            creatingNewMapping2 = true;

            Dictionary<String, FlattenedMapping> flattenedMappings = FlattenedMapping.Flatten(mappingBeingEdited2);

            var flattenedMappingList = flattenedMappings.Values.ToList<FlattenedMapping>();

            this.bindingList = new BindingList<FlattenedMapping>(flattenedMappingList);

            var source = new BindingSource(bindingList, null);

            dgvMappings.DataSource = source;

            // Initialize Mapping2 Editor UI elements with this mapping
            tbMappingName2.Text = "";
            dgvMappings.Rows.Clear();

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
            btnMappingDelete2.ForeColor = Color.Gray;
            btnMappingDelete2.Enabled = false;
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
            
            if (creatingNewMapping2 == false && tbMappingName2.Text.Trim() != mappingBeingEdited2.name)
            {
                // We're saving an existing mapping using a new name, effectively creating a new mapping.
                creatingNewMapping2 = true;
            }

            if (creatingNewMapping2 == true)
            {
                // Make sure the new mapping name entered is OK
                String mappingName = tbMappingName2.Text.Trim();
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

                editedMapping.name = mappingName;
            }
            else
            {
                editedMapping.name = mappingBeingEdited2.name;
            }

            var pdcmsDict = FlattenedMapping.Unflatten(this.bindingList);
            editedMapping.perDeviceChannelMappings = pdcmsDict;

            // Flesh out the mapping internals
            try
            {
                editedMapping.bind(mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators);
            }
            catch (ConfigurationException ex) 
            {
                MessageBox.Show(ex.Message);
                return;
            }

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

            // Hide the Mapping Editor UI elements
            hideEditorUiElements();
            btnMappingDelete2.ForeColor = Color.Gray;
            btnMappingDelete2.Enabled = false;

            // Refresh other selectors that may not be showing this (new) mapping
            refreshMappingToEditSelector2(editedMapping.name);
            refreshMappingToEditSelector(editedMapping.name);
            btnPatchTreeViewBySG_Click(null, null);

            mappingBeingEdited2 = null;
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
            btnMappingDelete2.ForeColor = Color.Gray;
            btnMappingDelete2.Enabled = false;
        }

        private void dgvMappings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                if (MessageBox.Show("Delete mapping row " + (e.RowIndex + 1) + "?", "Delete Mapping?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    dgvMappings.Rows.RemoveAt(e.RowIndex);
                }
            }
            else if ((e.ColumnIndex == 2 || e.ColumnIndex == 4) && e.RowIndex >= 0)
            {
                fmPatchPicker fmPatchPicker = new fmPatchPicker();
                fmPatchPicker.Init(this);
                fmPatchPicker.SoundGeneratorName = (String)dgvMappings.Rows[e.RowIndex].Cells["soundGeneratorName"].Value;
                fmPatchPicker.PatchName = (String)dgvMappings.Rows[e.RowIndex].Cells["patchName"].Value;
                fmPatchPicker.ShowMe();
                if (fmPatchPicker.IsOK == true)
                {
                    dgvMappings.Rows[e.RowIndex].Cells["soundGeneratorName"].Value = fmPatchPicker.SoundGeneratorName;
                    dgvMappings.Rows[e.RowIndex].Cells["patchName"].Value = fmPatchPicker.PatchName;
                    dgvMappings.Refresh();
                }
            }
        }

        private void dgvMappings_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // When I do this it prevents the grid's auto-creation of a new row below the current one...
            //  The Flattened Mapping defaults seem to do the trick for all except Logical Input Device.

            //e.Row.Cells["logicalInputDeviceName"].Value = "Input Device 1";        // ToDo: Get this value from the config!
            //e.Row.Cells["inputDeviceChannel"].Value = 0;
            //e.Row.Cells["soundGeneratorRelativeChannel"].Value = 0;
            //e.Row.Cells["lowestNote"].Value = 0;
            //e.Row.Cells["highestNote"].Value = 127;
            //e.Row.Cells["pitchOffset"].Value = 0;
            //e.Row.Cells["pbScale"].Value = 1.0;
            //e.Row.Cells["damperRemap"].Value = 64;
            //e.Row.Cells["modRemap"].Value = 1;
        }

        private void dgvMappings_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            bool valid = true;
            String formattedValue = (String)e.FormattedValue;

            switch (e.ColumnIndex)
            {
                // Source Channel
                case 1: valid = isValidIntRange(formattedValue, 0, 15); break;

                // Dest Channel
                case 3: valid = isValidIntRange(formattedValue, 0, 15); break;

                // Volume
                case 5: valid = isBlank(formattedValue) || isValidDoubleRange(formattedValue, -Double.MaxValue, 15); break;

                // Low Note
                case 6: valid = isValidIntRange(formattedValue, 0, 127); break;

                // High Note
                case 7: valid = isValidIntRange(formattedValue, 0, 127); break;

                // Transpose
                case 8: valid = isValidIntRange(formattedValue, -127, 127); break;

                // PB Scale
                case 9: valid = isValidDoubleRange(formattedValue, 0.0, 1.0); break;

                // Damper CC Remap
                case 10: valid = isBlank(formattedValue) || isValidIntRange(formattedValue, 0, 127); break;

                // Mod CC Remap
                case 11: valid = isBlank(formattedValue) || isValidIntRange(formattedValue, 0, 127); break;
            }

            if (!valid)
            {
                MessageBox.Show(String.Format("Illegal Value of {0} in column {1}", (String)e.FormattedValue, e.ColumnIndex));
                e.Cancel = true;
            }
        }

        private bool isValidIntRange(String value, int min, int max)
        {
            int parsedVal;
            if (Int32.TryParse(value, out parsedVal) == false)
            {
                return false;
            }
            return (parsedVal >= min && parsedVal <= max);
        }

        private bool isValidDoubleRange(String value, double min, double max)
        {
            double parsedVal;
            if (Double.TryParse(value, out parsedVal) == false)
            {
                return false;
            }
            return (parsedVal >= min && parsedVal <= max);
        }

        private bool isBlank(String value)
        {
            return value.Trim().Length == 0;
        }
    }
}
