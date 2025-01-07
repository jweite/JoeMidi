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
        // Mappings Editor
        //**************************************************************************

        // Two organization modes for the patch treeview:  SG (SoundGenerator) or Cat (Category)
        String mappingEditorTreeViewMode = "SG";

        private void btnMappingEditPatchTreeViewBySG_Click(object sender, EventArgs e)
        {
            mappingEditorTreeViewMode = "SG";
            populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvMappingEditorPrograms, mappingEditorTreeViewMode, false);
            btnPatchTreeViewBySG.BackColor = SystemColors.Highlight;
            btnPatchTreeViewByCategory.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnMappingEditPatchTreeViewByCategory_Click(object sender, EventArgs e)
        {
            mappingEditorTreeViewMode = "Cat";
            populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvMappingEditorPrograms, mappingEditorTreeViewMode, false);
            btnPatchTreeViewBySG.BackColor = System.Drawing.Color.DimGray;
            btnPatchTreeViewByCategory.BackColor = SystemColors.Highlight;
        }

        // Refresh the entries in the mapping selector.  We only expose SimpleMappings in the UI.  (Hand-authored Mappings can be more sophisticated than the UI can display today)
        private void refreshMappingToEditSelector(String nameToSelect = null)
        {
            mbrcMappingSelect.clearButtons();
            foreach (Mapping mapping in mapper.configuration.mappingsSorted)
            {
                if (mapping is SimpleMapping)
                {
                    mbrcMappingSelect.addButton(mapping.name, (SimpleMapping)mapping);
                }
            }
            if (nameToSelect != null)
            {
                mbrcMappingSelect.selectByName(nameToSelect);
            }
        }

        // Mapping editor state varaibles
        SimpleMapping mappingBeingEdited = null;
        bool creatingNewMapping = false;

        // A Mapping was selected for editing
        private void mbrcMappingSelect_Click(object sender, EventArgs e)
        {
            // Make sure it's a SimpleMapping
            if (((Button)sender).Tag is SimpleMapping)
            {
                SimpleMapping mapping = (SimpleMapping)(((Button)sender).Tag);

                // In case it's a direct-switch from one mapping to another.
                showSimpleMappingDefEditorControls(false);

                // Update the editor state variables to communicate to the OK method.
                mappingBeingEdited = mapping;
                creatingNewMapping = false;

                enableUiDeviceLabelsForFirstTwoInputDevices();

                // Populate UI fields.
                tbMappingName.Text = mapping.name;
                tbMappingName.ReadOnly = true;              // You can't edit mapping name.

                // If there are mappings defined for at least one Input Device then update the UI elements on the left side with data from the first one defined.
                if (mapping.perDeviceSimpleMappings.Count > 0)
                {

                    SimpleMapping.PerDeviceSimpleMapping perDeviceSimpleMapping = mapping.perDeviceSimpleMappings[0];
                    cbMappingSplitDevice1.Checked = perDeviceSimpleMapping.splitPoint > 0;
                    nudMappingSplitDevice1.Value = (perDeviceSimpleMapping.splitPoint > 0) ? perDeviceSimpleMapping.splitPoint : 60;
                    nudMappingSplitDevice1.Visible = perDeviceSimpleMapping.splitPoint > 0;
                    lbMappingDevice1UpperPatches.Items.Clear();
                    lbMappingDevice1UpperPatches.Visible = true;
                    lbMappingDevice1LowerPatches.Items.Clear();
                    lbMappingDevice1LowerPatches.Visible = perDeviceSimpleMapping.splitPoint > 0;

                    foreach (SimpleMapping.SimpleMappingDefinition mappingDefinition in perDeviceSimpleMapping.simpleMappingDefinitions)
                    {
                        if (mappingDefinition.bLower)
                        {
                            lbMappingDevice1LowerPatches.Items.Add(mappingDefinition);
                        }
                        else
                        {
                            lbMappingDevice1UpperPatches.Items.Add(mappingDefinition);
                        }
                    }
                }

                // If there are mappings defined for at least two Input Device then update the UI elements on the right side with the second. We ignore any beyond that.
                if (mapping.perDeviceSimpleMappings.Count > 1)
                {

                    // Make InputDevice2 mapping def controls visible
                    cbMappingSplitDevice2.Visible = true;

                    SimpleMapping.PerDeviceSimpleMapping perDeviceMapping = mapping.perDeviceSimpleMappings[1];
                    lblMappingInputDevice2.Text = perDeviceMapping.inputDeviceName;
                    cbMappingSplitDevice2.Checked = perDeviceMapping.splitPoint > 0;
                    nudMappingSplitDevice2.Value = (perDeviceMapping.splitPoint > 0) ? perDeviceMapping.splitPoint : 60;
                    nudMappingSplitDevice2.Visible = perDeviceMapping.splitPoint > 0;
                    lbMappingDevice2UpperPatches.Items.Clear();
                    lbMappingDevice2UpperPatches.Visible = true;
                    lbMappingDevice2LowerPatches.Items.Clear();
                    lbMappingDevice2LowerPatches.Visible = perDeviceMapping.splitPoint > 0;

                    foreach (SimpleMapping.SimpleMappingDefinition mappingDefinition in perDeviceMapping.simpleMappingDefinitions)
                    {
                        if (mappingDefinition.bLower)
                        {
                            lbMappingDevice2LowerPatches.Items.Add(mappingDefinition);
                        }
                        else
                        {
                            lbMappingDevice2UpperPatches.Items.Add(mappingDefinition);
                        }
                    }
                }
                else
                {
                    lbMappingDevice2LowerPatches.Visible = false;
                    cbMappingSplitDevice2.Checked = false;
                    nudMappingSplitDevice2.Value = 60;
                    nudMappingSplitDevice2.Visible = false;
                }

                // Expose the Mapping Editor UI elements
                pnlMappingEdit.Visible = true;
                tlpMappingEditNameAndButtons.Visible = true;
                tvMappingEditorPrograms.Focus();
            }
            else
            {
                MessageBox.Show("Presently one can only edit SimpleMappings with this UI");
            }
        }

        private void btnMappingAdd_Click(object sender, EventArgs e)
        {
            // Create a new empty SimpleMapping and initialize the editor state variables.
            mappingBeingEdited = new SimpleMapping();
            creatingNewMapping = true;

            // Initialize the UI elements
            tbMappingName.Text = "";
            tbMappingName.ReadOnly = false;

            enableUiDeviceLabelsForFirstTwoInputDevices();
            showSimpleMappingDefEditorControls(false);

            lbMappingDevice1LowerPatches.Items.Clear();
            lbMappingDevice2LowerPatches.Items.Clear();
            lbMappingDevice1UpperPatches.Items.Clear();
            lbMappingDevice2UpperPatches.Items.Clear();
            cbMappingSplitDevice1.Checked = false;
            cbMappingSplitDevice2.Checked = false;
            nudMappingSplitDevice1.Value = 60;
            nudMappingSplitDevice2.Value = 60;
            nudMappingSplitDevice1.Visible = false;             // Only made visible when Split checkbox is checked.
            nudMappingSplitDevice2.Visible = false;             // Only made visible when Split checkbox is checked.
            lbMappingDevice1LowerPatches.Visible = false;       // Only made visible when Split checkbox is checked.
            lbMappingDevice2LowerPatches.Visible = false;       // Only made visible when Split checkbox is checked.

            // Make the editor UI visible
            pnlMappingEdit.Visible = true;
            tlpMappingEditNameAndButtons.Visible = true;
            tbMappingName.Select();

        }

        private void enableUiDeviceLabelsForFirstTwoInputDevices()
        {
            lblMappingInputDevice1.Text = mapper.configuration.primaryInputDeviceName;

            // Only make InputDevice2 mapping def fields visible if we find a second InputDevices configured (ie, with logicalInputDeviceNumber == 2)
            if (mapper.configuration.logicalInputDeviceDict.Count > 1)
            {
                LogicalInputDevice secondaryInputDevice = null;
                // Look for logicalInputDevice with number == 2
                foreach (LogicalInputDevice inputDevice in mapper.configuration.logicalInputDeviceDict.Values)
                {
                    if (inputDevice.logicalInputDeviceNumber == 2)
                    {
                        secondaryInputDevice = inputDevice;
                        break;
                    }
                }
                if (secondaryInputDevice != null)
                {
                    // Found it.  Enable UI elements for it
                    lblMappingInputDevice2.Text = secondaryInputDevice.logicalDeviceName;
                    lblMappingInputDevice2.Visible = true;
                    cbMappingSplitDevice2.Visible = true;
                }
                else
                {
                    // Not found.  We probably should use whatever non-primary one we find, but for now we're hiding InputDevice2 mapping.
                    lblMappingInputDevice2.Visible = false;
                    cbMappingSplitDevice2.Visible = false;
                }
            }
            else
            {
                // Only one InputDevice.  Hide all InputDevice2 mapping def fields
                lblMappingInputDevice2.Visible = false;
                cbMappingSplitDevice2.Visible = false;
            }
        }


        // Set visibility of the split def control and the lower patch listbox based on status of the split checkbox.
        private void cbMappingSplitDevice1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMappingSplitDevice1.Checked)
            {
                lbMappingDevice1LowerPatches.Visible = true;
                nudMappingSplitDevice1.Visible = true;
            }
            else
            {
                lbMappingDevice1LowerPatches.Visible = false;
                nudMappingSplitDevice1.Visible = false;
            }
        }

        private void cbMappingSplitDevice2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMappingSplitDevice2.Checked)
            {
                lbMappingDevice2LowerPatches.Visible = true;
                nudMappingSplitDevice2.Visible = true;
            }
            else
            {
                lbMappingDevice2LowerPatches.Visible = false;
                nudMappingSplitDevice2.Visible = false;
            }

        }

        private void btnMappingEditCancel_Click(object sender, EventArgs e)
        {
            pnlMappingEdit.Visible = false;
            tlpMappingEditNameAndButtons.Visible = false;
            showSimpleMappingDefEditorControls(false);
        }

        private void btnMappingEditOK_Click(object sender, EventArgs e)
        {
            if (creatingNewMapping == true)
            {
                // Make sure the new mapping name entered is OK
                if (tbMappingName.Text.Length == 0)
                {
                    MessageBox.Show("You must enter a Mapping Name");
                    return;
                }

                if (mapper.configuration.mappings.ContainsKey(tbMappingName.Text))
                {
                    MessageBox.Show("Proposed mapping name " + tbMappingName.Text + " is already in use.");
                    return;
                }

                mappingBeingEdited.name = tbMappingName.Text;
            }
            else
            {
                mappingBeingEdited.perDeviceSimpleMappings.Clear();
            }

            // Create and populate the PerDeviceMapping for the first (leftmost) device
            SimpleMapping.PerDeviceSimpleMapping device1SimpleMapping = new SimpleMapping.PerDeviceSimpleMapping();
            device1SimpleMapping.inputDeviceName = lblMappingInputDevice1.Text;
            device1SimpleMapping.inputDeviceChannel = 0;  // Defaulting this...
            device1SimpleMapping.splitPoint = (cbMappingSplitDevice1.Checked) ? (int)nudMappingSplitDevice1.Value : -1;
            foreach (Object o in lbMappingDevice1UpperPatches.Items)
            {
                if (o is SimpleMapping.SimpleMappingDefinition)
                {
                    device1SimpleMapping.simpleMappingDefinitions.Add((SimpleMapping.SimpleMappingDefinition)o);
                }
            }
            if (device1SimpleMapping.splitPoint > 0)
            {
                foreach (Object o in lbMappingDevice1LowerPatches.Items)
                {
                    if (o is SimpleMapping.SimpleMappingDefinition)
                    {
                        device1SimpleMapping.simpleMappingDefinitions.Add((SimpleMapping.SimpleMappingDefinition)o);
                    }
                }
            }
            if (device1SimpleMapping.simpleMappingDefinitions.Count > 0)
            {
                mappingBeingEdited.perDeviceSimpleMappings.Add(device1SimpleMapping);
            }

            // If second device's UI fields are active create and populate the PerDeviceMapping for the second (rightmost) device
            SimpleMapping.PerDeviceSimpleMapping device2SimpleMapping = new SimpleMapping.PerDeviceSimpleMapping();
            device2SimpleMapping.inputDeviceName = lblMappingInputDevice2.Text;
            device2SimpleMapping.inputDeviceChannel = 0;  // Defaulting this...
            device2SimpleMapping.splitPoint = (cbMappingSplitDevice2.Checked == true) ? (int)nudMappingSplitDevice2.Value : -1;
            foreach (Object o in lbMappingDevice2UpperPatches.Items)
            {
                if (o is SimpleMapping.SimpleMappingDefinition)
                {
                    device2SimpleMapping.simpleMappingDefinitions.Add((SimpleMapping.SimpleMappingDefinition)o);
                }
            }
            if (device2SimpleMapping.splitPoint > 0)
            {
                foreach (Object o in lbMappingDevice2LowerPatches.Items)
                {
                    if (o is SimpleMapping.SimpleMappingDefinition)
                    {
                        device2SimpleMapping.simpleMappingDefinitions.Add((SimpleMapping.SimpleMappingDefinition)o);
                    }
                }
            }
            if (device2SimpleMapping.simpleMappingDefinitions.Count > 0)
            {
                mappingBeingEdited.perDeviceSimpleMappings.Add(device2SimpleMapping);
            }

            // Flesh out the mapping internals
            mappingBeingEdited.bind(mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators);

            // Store it
            if (creatingNewMapping)
            {
                mapper.configuration.mappings.Add(mappingBeingEdited.name, mappingBeingEdited);
            }
            else
            {
                mapper.configuration.mappings[mappingBeingEdited.name] = mappingBeingEdited;
            }
            mapper.configuration.dirty = true;

            // Activate it...
            mapper.SetMapping(mappingBeingEdited);

            // Hide the UI elements
            pnlMappingEdit.Visible = false;
            tlpMappingEditNameAndButtons.Visible = false;
            showSimpleMappingDefEditorControls(false);

            // Refresh selectors that may not be showing this (new) mapping
            if (creatingNewMapping)
            {
                refreshMappingToEditSelector(mappingBeingEdited.name);
                refreshMappingToEditSelector2(mappingBeingEdited.name);
                btnPatchTreeViewBySG_Click(null, null);
            }

        }


        // Drag-drop from Programs tree view to mapping patch list boxes.  
        private void tvMappingEditorPrograms_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Console.WriteLine("Item Drag: " + e.Item);
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void lbMappingDevicePatches_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag Enter: " + e + ", sender = " + sender);
            e.Effect = DragDropEffects.Move;
        }

        private void lbMappingDevicePatches_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("btnRandAccessCol DragDrop: " + e);
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                TreeNode droppedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (droppedNode.Tag is SoundGeneratorPatch)
                {
                    SoundGeneratorPatch soundGeneratorPatch = (SoundGeneratorPatch)droppedNode.Tag;

                    if (sender is ListBox)
                    {
                        ListBox receivingListBox = (ListBox)sender;

                        // The list boxes are not populated with SoundGeneratorPatches, but with SimpleMappingDefinitions derived from them.
                        SimpleMapping.SimpleMappingDefinition mappingDefinition = new SimpleMapping.SimpleMappingDefinition();
                        mappingDefinition.programName = soundGeneratorPatch.name;
                        mappingDefinition.soundGeneratorName = soundGeneratorPatch.soundGenerator.name;
                        mappingDefinition.transpose = 0;
                        mappingDefinition.pbScale = 1.0F;
                        mappingDefinition.bLower = receivingListBox.Name.Contains("Lower");
                        mappingDefinition.damperRemap = 64;
                        mappingDefinition.bEnaDamperControl = true;
                        mappingDefinition.bEnaCC7 = true;
                        mappingDefinition.initialCC7 = -1;
                        mappingDefinition.bEnaModControl = true;
                        receivingListBox.Items.Add(mappingDefinition);
                    }
                }
            }
        }

        private void lbMappingPatches_MouseUp(object sender, MouseEventArgs e)
        {
            // Make right-click select on the mapping editor's patch list boxes.
            if (e.Button.Equals(MouseButtons.Right))
            {

                // Unselect the other listboxes.
                lbMappingDevice1LowerPatches.SelectedIndex = -1;
                lbMappingDevice1UpperPatches.SelectedIndex = -1;
                lbMappingDevice2LowerPatches.SelectedIndex = -1;
                lbMappingDevice2UpperPatches.SelectedIndex = -1;

                ListBox lb = (ListBox)sender;
                int i = lb.IndexFromPoint(e.X, e.Y);
                if (i != ListBox.NoMatches)
                {
                    lb.SelectedIndex = i;
                    showSimpleMappingDefEditorControls(false);
                }
            }
        }

        SimpleMapping.SimpleMappingDefinition simpleMappingDefBeingEdited = null;

        private void lbMappingPatches_DoubleClick(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;

            if (lb.SelectedItem == null)
            {
                return;
            }

            // Unselect the other listboxes.
            lbMappingDevice1LowerPatches.SelectedIndex = (lb == lbMappingDevice1LowerPatches) ? lb.SelectedIndex : -1;
            lbMappingDevice1UpperPatches.SelectedIndex = (lb == lbMappingDevice1UpperPatches) ? lb.SelectedIndex : -1;
            lbMappingDevice2LowerPatches.SelectedIndex = (lb == lbMappingDevice2LowerPatches) ? lb.SelectedIndex : -1;
            lbMappingDevice2UpperPatches.SelectedIndex = (lb == lbMappingDevice2UpperPatches) ? lb.SelectedIndex : -1;

            SimpleMapping.SimpleMappingDefinition mappingDef = (SimpleMapping.SimpleMappingDefinition)lb.SelectedItem;
            simpleMappingDefBeingEdited = mappingDef;

            tbPBScale.Value = (int)(mappingDef.pbScale * 12.0F);
            nudMappingDefTransposeOct.Value = mappingDef.transpose / 12;
            nudMappingDefTransposeSemis.Value = mappingDef.transpose % 12;

            cbMappingDefModWheelEna.Checked = mappingDef.bEnaModControl;
            cbMappingDefVolEna.Checked = mappingDef.bEnaCC7;
            cbMappingDefDamperEna.Checked = mappingDef.bEnaDamperControl;
            tbMappingDefIniCC7.Value = mappingDef.initialCC7;
            nudMappingDefDamperRemap.Value = (mappingDef.damperRemap >= 0) ? mappingDef.damperRemap : 64;
            cbMappingDefDamperToggle.Checked = mappingDef.bDamplerToggle;
            tbVolume.Text = (mappingDef.volumeOverride != null) ? String.Format("{0:0.0}", mappingDef.volumeOverride) : "";

            showSimpleMappingDefEditorControls(true);
        }

        private void showSimpleMappingDefEditorControls(bool show)
        {
            lblMappingEditPBScale.Visible = show;
            lblMappingEditTranspose.Visible = show;
            lblMappingPBScale.Visible = show;
            lblVolume.Visible = show;
            lblMappingTransOcts.Visible = show;
            lblMappingTransSemis.Visible = show;
            nudMappingDefTransposeOct.Visible = show;
            nudMappingDefTransposeSemis.Visible = show;
            cbMappingDefDamperEna.Visible = show;
            cbMappingDefDamperToggle.Visible = show;
            cbMappingDefModWheelEna.Visible = show;
            nudMappingDefDamperRemap.Visible = show;
            tbMappingDefIniCC7.Visible = show;
            cbMappingDefVolEna.Visible = show;
            tbPBScale.Visible = show;
            tbVolume.Visible = show;
        }

        private void nudMappingDefTranspose_ValueChanged(object sender, EventArgs e)
        {
            if (simpleMappingDefBeingEdited != null)
            {
                simpleMappingDefBeingEdited.transpose = (int)((nudMappingDefTransposeOct.Value * 12) + nudMappingDefTransposeSemis.Value);
            }
        }

        private void tbPBScale_ValueChanged(object sender, EventArgs e)
        {
            if (simpleMappingDefBeingEdited != null)
            {
                simpleMappingDefBeingEdited.pbScale = ((float)tbPBScale.Value) / 12F;
            }
        }

        private void cbMappingDefModWheelEna_CheckedChanged(object sender, EventArgs e)
        {
            simpleMappingDefBeingEdited.bEnaModControl = cbMappingDefModWheelEna.Checked;
        }

        private void cbMappingDefVolEna_CheckedChanged(object sender, EventArgs e)
        {
            simpleMappingDefBeingEdited.bEnaCC7 = cbMappingDefVolEna.Checked;
        }

        private void cbMappingDefDamperEna_CheckedChanged(object sender, EventArgs e)
        {
            simpleMappingDefBeingEdited.bEnaDamperControl = cbMappingDefDamperEna.Checked;
        }

        private void nudMappingDefDamperRemap_ValueChanged(object sender, EventArgs e)
        {
            simpleMappingDefBeingEdited.damperRemap = (int)nudMappingDefDamperRemap.Value;
        }

        private void tbMappingDefIniVol_Scroll(object sender, EventArgs e)
        {
            simpleMappingDefBeingEdited.initialCC7 = tbMappingDefIniCC7.Value;
        }

        private void cbMappingDefDamperToggle_CheckedChanged(object sender, EventArgs e)
        {
            simpleMappingDefBeingEdited.bDamplerToggle = cbMappingDefDamperToggle.Checked;
        }

        private void tbVolume_TextChanged(object sender, EventArgs e)
        {
            double d;
            if (tbVolume.Text.Length > 0 && double.TryParse(tbVolume.Text, out d))
            {
                simpleMappingDefBeingEdited.volumeOverride = d;
            }
            else
            {
                simpleMappingDefBeingEdited.volumeOverride = null;
            }
        }


        private void btnMappingDelete_Click(object sender, EventArgs e)
        {
            SimpleMapping mapping = mappingBeingEdited;
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

            refreshMappingToEditSelector();
            refreshMappingToEditSelector2();
            btnPatchTreeViewBySG_Click(null, null);

            // Hide any editor UI elements that may be visible.
            pnlMappingEdit.Visible = false;
            tlpMappingEditNameAndButtons.Visible = false;
        }
    }
}
