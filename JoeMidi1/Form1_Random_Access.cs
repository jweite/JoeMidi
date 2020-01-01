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
        private void Form1_Random_Access_Load(object sender, EventArgs e)
        {
            // Populate the 8x8 button matrix with buttons to access the MidiPrograms defined.  Each gets a button in th 8x8 grid based on its midi program number.
            //  I'm not dealing in banks yet, but imagine having multiple banks of 8x8 selected by a bank select command.
            refreshRandomAccessButtonGrid();

            // If there's at least one setlist define, make the first one the current one.
            if (mapper.configuration.setlists.Count > 0)
            {
                currentSetlist = mapper.configuration.setlists[0];
            }

            // Initialize patch TreeViews and set to SG (Sound Generator) organization by "clicking" the "SG" button.
            btnPatchTreeViewBySG_Click(null, null);
            btnMappingEditPatchTreeViewBySG_Click(null, null);

            // Initialize the Input Device dropdown on the RandomAccess (and Misc) pages.
            foreach (String sourceDeviceLogicalName in mapper.configuration.logicalInputDeviceDict.Keys)
            {
                LogicalInputDevice logicalInputDevice = mapper.configuration.logicalInputDeviceDict[sourceDeviceLogicalName];
                if (logicalInputDevice.device != null)
                {
                    cbRandomAccessInputDevice.Items.Add(logicalInputDevice.logicalDeviceName);
                    lbInputDevices.Items.Add(logicalInputDevice.logicalDeviceName + " -> " + logicalInputDevice.device.Name);
                }
            }

            // Assocaite each Random Access Tab Page with the Logical Input Device it's been configured to manage
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Text.StartsWith("Random Access"))
                {
                    if (mapper.configuration.randomAccessTabInputDeviceNames.ContainsKey(tabPage.Text))
                    {
                        String inputDeviceName = mapper.configuration.randomAccessTabInputDeviceNames[tabPage.Text];
                        if (mapper.configuration.logicalInputDeviceDict.ContainsKey(inputDeviceName))
                        {
                            tabPage.Tag = mapper.configuration.logicalInputDeviceDict[mapper.configuration.randomAccessTabInputDeviceNames[tabPage.Text]];
                        }
                        else
                        {
                            tabPage.Tag = mapper.configuration.primaryInputDevice;
                        }
                    }
                    else
                    {
                        tabPage.Tag = mapper.configuration.primaryInputDevice;
                    }
                }
            }

            // Select the current tab's Logical Input Device in its Input Device selector 
            if (tabControl1.SelectedTab.Tag != null && tabControl1.SelectedTab.Tag is LogicalInputDevice)
            {
                cbRandomAccessInputDevice.Text = ((LogicalInputDevice)tabControl1.SelectedTab.Tag).logicalDeviceName;
            }

        }

        void midiProgramChangeNotificationRandomAccess(int programNum)
        {
            // Map the Casio buttons to programs 0-7
            if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[7])     // Button 8
            {
                programNum = 7;
            }
            else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[6])
            {
                programNum = 6;
            }
            else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[5])
            {
                programNum = 5;
            }
            else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[4])
            {
                programNum = 4;
            }
            else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[3])
            {
                programNum = 3;
            }
            else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[2])
            {
                programNum = 2;
            }
            else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[1])
            {
                programNum = 1;
            }
            else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[0])
            {
                programNum = 0;
            }
            mapper.RotatingProgramChange((currentRandomAccessBank * 128) + programNum);

        }

        //**************************************************************************
        // Random Access tab
        //**************************************************************************
        int currentRandomAccessBank = 0;

        const int FIRST_RANDOMACCESS_BTN_ROW = 1;
        const int FIRST_RANDOMACCESS_BTN_COL = 1;
        const int N_RANDOMACCESS_ROWS = 8;
        const int N_RANDOMACCESS_COLS = 8;

        private void refreshRandomAccessButtonGrid()
        {
            // Clear (only) the Program buttons 
            for (int row = 0; row < N_RANDOMACCESS_ROWS; ++row)
            {
                for (int col = 0; col < N_RANDOMACCESS_COLS; ++col)
                {
                    System.Windows.Forms.Control ctl = tlpRandomAccess.GetControlFromPosition(col + FIRST_RANDOMACCESS_BTN_COL, row + FIRST_RANDOMACCESS_BTN_ROW);
                    tlpRandomAccess.Controls.Remove(ctl);
                }
            }

            // Iterate over the MidiPrograms and add a button for each in the current random access bank.
            foreach (MidiProgram midiProgram in mapper.configuration.midiPrograms.Values)
            {
                if (midiProgram.mapping != null && midiProgram.myBankNumber == currentRandomAccessBank)
                {
                    // Determine the row/col of the button for this midiProgram
                    int row = (midiProgram.myPatchNumber / N_RANDOMACCESS_COLS);
                    int col = (midiProgram.myPatchNumber % N_RANDOMACCESS_COLS);

                    // Only process those that actually fit on the grid
                    if (col >= N_RANDOMACCESS_COLS) continue;
                    if (row >= N_RANDOMACCESS_ROWS) continue;

                    // Create a button for this midiProgram
                    Button btn = new Button();
                    btn.Font = buttonFont;
                    btn.Text = midiProgram.mapping.name;
                    btn.BackColor = System.Drawing.Color.DimGray;
                    btn.ForeColor = System.Drawing.Color.White;
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
                    btn.Visible = true;
                    btn.Dock = DockStyle.Fill;
                    btn.MouseDown += btnPrograms_MouseDown;
                    btn.ContextMenuStrip = this.contextMenuStrip1;

                    // Put the midiProgram in the button for use by the shared event handler
                    btn.Tag = midiProgram;
                    midiProgram.activationButton = btn;
                    btn.Click += new System.EventHandler(this.btnProgramChange_Click);
                    tlpRandomAccess.Controls.Add(btn, col + FIRST_RANDOMACCESS_BTN_COL, row + FIRST_RANDOMACCESS_BTN_ROW);
                }
            }
        }


        // Every button on the button grid uses this as its click handler.
        private void btnProgramChange_Click(object sender, EventArgs e)
        {
            // Get the patchNo from within the button that was clicked
            MidiProgram midiProgram = (MidiProgram)(((Button)sender).Tag);

            // And switch to it.
            if (midiProgram != null && midiProgram.myPatchNumber >= 0)
            {

                // Override the default intput device in the mapping to be the on for this tab
                LogicalInputDevice deviceForThisMidiProgram = mapper.configuration.primaryInputDevice;
                if (tabControl1.SelectedTab.Tag != null && tabControl1.SelectedTab.Tag is LogicalInputDevice)
                {
                    deviceForThisMidiProgram = (LogicalInputDevice)tabControl1.SelectedTab.Tag;
                }

                // Rebind it to make the device override take effect
                midiProgram.bind(mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators, mapper.configuration.mappings, deviceForThisMidiProgram);

                // Fire the program change
                mapper.ProgramChange((midiProgram.myBankNumber * 128) + midiProgram.myPatchNumber);
                mapper.masterTranspose = 0;
            }

            // Select (only) this button
            unselectAllRandomAccessButtons();
            ((Button)sender).BackColor = SystemColors.Highlight;
        }

        private void unselectAllRandomAccessButtons()
        {
            foreach (System.Windows.Forms.Control ctl in tlpRandomAccess.Controls)
            {
                if (ctl is Button && ctl.Tag is MidiProgram)
                {
                    ctl.BackColor = System.Drawing.Color.DimGray;
                }
            }
        }

        private void midiProgramActivatedNotification(MidiProgram midiProgram)
        {
            unselectAllRandomAccessButtons();
            if (midiProgram.activationButton != null)
            {
                midiProgram.activationButton.BackColor = SystemColors.Highlight;
            }
        }


        //--------------------------------------------------------------------------
        // Drag Drop: From Program Treeview to Program Grid Column Heading Button
        //--------------------------------------------------------------------------
        private void tvProgramPatches_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Console.WriteLine("Item Drag: " + e.Item);
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void btnRandAccessCol_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag Enter: " + e + ", sender = " + sender);
            e.Effect = DragDropEffects.Move;
        }

        private void btnRandAccessCol_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("btnRandAccessCol DragDrop: " + e);
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                // Figure out the table layout panel column of heading button that this TreeNode got dropped onto
                int col = tlpRandomAccess.GetColumn((System.Windows.Forms.Control)sender);

                // Find the first unoccupied row in that column
                int row;
                for (row = 0; row < N_RANDOMACCESS_ROWS; ++row)
                {
                    if (tlpRandomAccess.GetControlFromPosition(col, row + FIRST_RANDOMACCESS_BTN_ROW) == null)
                        break;
                }
                if (row == N_RANDOMACCESS_ROWS)
                {
                    MessageBox.Show("No room in this column");
                    return;
                }

                // Get the dropped node
                TreeNode droppedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                // Get the SoundGeneratorPatch or Mapping that it points to
                Object o = droppedNode.Tag;

                // Create a new midiProgram to capture the action of the random access button we're about to create
                MidiProgram midiProgram;

                // Initialize the midiProgram appropriately, depending on whether there was a SoundGeneratorPatch or a Mapping in the droppped TreeNode
                if (o is SoundGeneratorPatch)
                {
                    midiProgram = new MidiProgram((SoundGeneratorPatch)o);
                }
                else if (o is Mapping)
                {
                    midiProgram = new MidiProgram((Mapping)o);
                }
                else
                {
                    return;
                }

                // Determine the input device for this midiProgram: the tab's currently selected input device
                LogicalInputDevice deviceForThisMidiProgram = mapper.configuration.primaryInputDevice;
                if (tabControl1.SelectedTab.Tag != null && tabControl1.SelectedTab.Tag is LogicalInputDevice)
                {
                    deviceForThisMidiProgram = (LogicalInputDevice)tabControl1.SelectedTab.Tag;
                }

                // Have the midiProgram flesh out its inner details
                midiProgram.bind(mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators, mapper.configuration.mappings, deviceForThisMidiProgram);

                // Assign this Midi Program a patch number based on where it was dropped in the button matrix.  
                midiProgram.myPatchNumber = (row * N_RANDOMACCESS_COLS) + col - FIRST_RANDOMACCESS_BTN_COL;

                // Assign it a bank number based on which Random Access page it's dropped on (which is captured in currentRandomAccessBank)
                midiProgram.myBankNumber = currentRandomAccessBank;

                // Add the newly created midiProgram to the master dictionary of such things.  Mark config as dirty so this change gets saved on close.
                mapper.configuration.midiPrograms.Add(midiProgram.key, midiProgram);
                mapper.configuration.dirty = true;

                // Create a new button for this sound generator patch
                Button btn = new Button();
                btn.Font = buttonFont;
                btn.Text = midiProgram.mapping.name;
                btn.BackColor = System.Drawing.Color.DimGray;
                btn.ForeColor = System.Drawing.Color.White;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
                btn.Visible = true;
                btn.Dock = DockStyle.Fill;
                btn.MouseDown += btnPrograms_MouseDown;
                btn.ContextMenuStrip = this.contextMenuStrip1;
                btn.Tag = midiProgram;
                midiProgram.activationButton = btn;
                btn.Click += new System.EventHandler(this.btnProgramChange_Click);

                // And add it to the table layout panel in the designated spot.
                tlpRandomAccess.Controls.Add(btn, col, row + FIRST_RANDOMACCESS_BTN_ROW);
            }
        }

        //--------------------------------------------------------------------------
        // Drag Drop: Rearrange program buttons
        //--------------------------------------------------------------------------
        private void btnPrograms_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                Console.WriteLine("btnPrograms_MouseDown: " + e);
                DoDragDrop(sender, DragDropEffects.Move);
            }
        }

        private void tlpRandomAccess_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("tlpProgramButtons_DragEnter: " + e);
            e.Effect = DragDropEffects.Move;
        }

        private void tlpRandomAccess_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("tlpProgramButtons_DragDrop DragDrop: " + e);

            if (e.Data.GetDataPresent("System.Windows.Forms.Button", false))
            {
                // Get a ref to the Button being dragged
                Button droppedButton = (Button)e.Data.GetData("System.Windows.Forms.Button");

                // Find out it's starting location
                TableLayoutPanelCellPosition droppedButtonOriginalPosition = tlpRandomAccess.GetCellPosition(droppedButton);

                // Find out it's drop location 
                Point clientPoint = tlpRandomAccess.PointToClient(new Point(e.X, e.Y));
                TableLayoutPanelCellPosition droppedButtonNewPosition = GetTableLayoutPanelRowColIndexFromPoint(tlpRandomAccess, clientPoint);

                // If we've been dropped onto ourself, it's really a click event (that will no longer fire since we hooked MouseDown.  Fire it ourselves)
                if (droppedButtonNewPosition.Equals(droppedButtonOriginalPosition))
                {
                    droppedButton.PerformClick();
                }
                else
                {
                    // It's a real drag/drop.  Remove the dragged button and re-insert in the new position.
                    removeControlFromTableLayoutPanelProgramButtons(droppedButtonOriginalPosition);

                    insertButtonIntoTableLayoutPanelProgramButtons(droppedButton, droppedButtonNewPosition);

                    mapper.configuration.dirty = true;
                }
            }
        }

        //--------------------------------------------------------------------------
        // Delete a button from the grid, rippling up the ones below it in its column.
        //--------------------------------------------------------------------------
        private void removeControlFromTableLayoutPanelProgramButtons(TableLayoutPanelCellPosition tlpcp)
        {
            // Remove the control in question
            System.Windows.Forms.Control ctl = tlpRandomAccess.GetControlFromPosition(tlpcp.Column, tlpcp.Row);
            tlpRandomAccess.Controls.Remove(ctl);

            // And its associated MidiProgram (Button's tag will still reference it, though, which may be used to insert it back in its new home)
            Object o = ctl.Tag;
            if (o is MidiProgram)
            {
                MidiProgram midiProgram = (MidiProgram)o;
                midiProgram.activationButton = null;
                mapper.configuration.midiPrograms.Remove(midiProgram.key);
                mapper.configuration.dirty = true;
            }

            // Ripple up to fill the gap. 
            for (int r = tlpcp.Row; r < (N_RANDOMACCESS_ROWS + FIRST_RANDOMACCESS_BTN_ROW) - 1; ++r)
            {
                // Get the control in the next row of this col
                ctl = tlpRandomAccess.GetControlFromPosition(tlpcp.Column, r + 1);

                // If there isn't one, we're done
                if (ctl == null)
                    break;

                // Remove it
                tlpRandomAccess.Controls.Remove(ctl);

                // Add it back in, up one row
                tlpRandomAccess.Controls.Add(ctl, tlpcp.Column, r);

                // If it's pointing to a midiProgram (and it should be) then fix up its program number to reflect the button's new pos
                o = ctl.Tag;
                if (o is MidiProgram)
                {
                    MidiProgram midiProgram = (MidiProgram)o;
                    if (midiProgram.myPatchNumber > (N_RANDOMACCESS_COLS))
                    {
                        mapper.configuration.midiPrograms.Remove(midiProgram.key);
                        midiProgram.myPatchNumber -= (N_RANDOMACCESS_COLS);
                        mapper.configuration.midiPrograms.Add(midiProgram.key, midiProgram);
                        mapper.configuration.dirty = true;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------
        // Insert a button into the grid, rippling down the ones below it in its column.
        //--------------------------------------------------------------------------
        private void insertButtonIntoTableLayoutPanelProgramButtons(Button btn, TableLayoutPanelCellPosition tlpcp)
        {
            // See if there's a control there already
            System.Windows.Forms.Control ctl = tlpRandomAccess.GetControlFromPosition(tlpcp.Column, tlpcp.Row);

            // If the requested position isn't empty, we gotta make room for it first
            if (ctl != null)
            {
                // Remove the control in the last row (if there is one).
                ctl = tlpRandomAccess.GetControlFromPosition(tlpcp.Column, N_RANDOMACCESS_ROWS + FIRST_RANDOMACCESS_BTN_ROW - 1);
                if (ctl != null)
                {
                    tlpRandomAccess.Controls.Remove(ctl);
                    if (ctl.Tag is MidiProgram)
                    {
                        MidiProgram midiProgram = (MidiProgram)ctl.Tag;
                        mapper.configuration.midiPrograms.Remove(midiProgram.key);
                    }
                }

                // Ripple buttons down, bottom up to top
                for (int r = (N_RANDOMACCESS_ROWS + FIRST_RANDOMACCESS_BTN_ROW) - 1; r > tlpcp.Row; --r)
                {
                    // Get the control in the row above this one
                    ctl = tlpRandomAccess.GetControlFromPosition(tlpcp.Column, r - 1);

                    // If there isn't one, skip it
                    if (ctl == null)
                        continue;

                    // Remove it
                    tlpRandomAccess.Controls.Remove(ctl);

                    // Add it back in, down one row
                    tlpRandomAccess.Controls.Add(ctl, tlpcp.Column, r);

                    // If it's pointing to a midiProgram (and it should be) then fix up its program number to reflect the button's new pos
                    Object o = ctl.Tag;
                    if (o is MidiProgram)
                    {
                        MidiProgram midiProgram = (MidiProgram)o;
                        mapper.configuration.midiPrograms.Remove(midiProgram.key);
                        midiProgram.myPatchNumber += N_RANDOMACCESS_COLS;
                        mapper.configuration.midiPrograms.Add(midiProgram.key, midiProgram);
                        mapper.configuration.dirty = true;
                    }
                }
            }

            // Now we can add it into the open slot
            tlpRandomAccess.Controls.Add(btn, tlpcp.Column, tlpcp.Row);

            // If it's pointing to a midiProgram (and it should be) then fix up its program number to reflect the button's new pos
            Object o2 = btn.Tag;
            if (o2 is MidiProgram)
            {
                MidiProgram midiProgram = (MidiProgram)o2;
                midiProgram.myPatchNumber = ((tlpcp.Row - FIRST_RANDOMACCESS_BTN_ROW) * N_RANDOMACCESS_COLS) + (tlpcp.Column - 1);
                mapper.configuration.midiPrograms.Add(midiProgram.key, midiProgram);
            }
        }

        //--------------------------------------------------------------------------
        // Given a point in a tlp, figure out what cell in that table layout was clicked.
        //--------------------------------------------------------------------------
        TableLayoutPanelCellPosition GetTableLayoutPanelRowColIndexFromPoint(TableLayoutPanel tlp, Point point)
        {
            // Adapted from http://stackoverflow.com/questions/15449504/how-do-i-determine-the-cell-being-clicked-on-in-a-tablelayoutpanel
            if (point.X > tlp.Width || point.Y > tlp.Height)
            {
                return new TableLayoutPanelCellPosition(-1, -1);
            }

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
                w -= widths[i];
            int col = i + 1;

            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
                h -= heights[i];

            int row = i + 1;

            return new TableLayoutPanelCellPosition(col, row);
        }

        //--------------------------------------------------------------------------
        // Clicking a column heading rotates through the buttons under it, just like
        //  MIDI Program Change 0-7 do.
        //--------------------------------------------------------------------------
        private void btnRandAccessCol_Click(object sender, EventArgs e)
        {
            int col = tlpRandomAccess.GetColumn((System.Windows.Forms.Control)sender);
            mapper.RotatingProgramChange(col);
        }

        //--------------------------------------------------------------------------
        // Program Button Context Menu Handler: deletion.
        //--------------------------------------------------------------------------
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Find the Button being Deleted and its position.
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ContextMenuStrip cms = (ContextMenuStrip)tsmi.Owner;
            if (cms.SourceControl is Button)
            {
                // Request to delete a Random Access Button

                Button buttonToBeDeleted = (Button)cms.SourceControl;
                TableLayoutPanelCellPosition tlpcp = tlpRandomAccess.GetCellPosition(buttonToBeDeleted);

                // Remove it from the table layout panel
                tlpRandomAccess.Controls.Remove(buttonToBeDeleted);

                // And remove the midiProgram that underlies it
                MidiProgram midiProgram = (MidiProgram)buttonToBeDeleted.Tag;
                mapper.configuration.midiPrograms.Remove(midiProgram.key);

                // Iterate through the buttons below it in it's column of the table layout panel and shift them each up one row
                for (int row = tlpcp.Row; row < (N_RANDOMACCESS_ROWS + FIRST_RANDOMACCESS_BTN_ROW); ++row)
                {
                    Button buttonToShift = (Button)tlpRandomAccess.GetControlFromPosition(tlpcp.Column, row + 1);
                    if (buttonToShift == null) break;   // All out of buttons in this col...

                    // Remove the button from its original position and add it back into its new position
                    tlpRandomAccess.Controls.Remove(buttonToShift);
                    tlpRandomAccess.Controls.Add(buttonToShift, tlpcp.Column, row);

                    // Adjust the patch number of the midi program that underlies it.  Since that's part of its dict key, remove and re-add it to the midi programs dictionary.
                    midiProgram = (MidiProgram)buttonToShift.Tag;
                    if (midiProgram.myPatchNumber > N_RANDOMACCESS_COLS)
                    {
                        mapper.configuration.midiPrograms.Remove(midiProgram.key);
                        midiProgram.myPatchNumber -= (N_RANDOMACCESS_COLS);
                        mapper.configuration.midiPrograms.Add(midiProgram.key, midiProgram);
                    }
                }

                mapper.configuration.dirty = true;
            }
            else if (cms.SourceControl is ListBox)
            {
                // Request to delete a Mapping Patch on one of the Mapping Patch List Boxes
                ListBox listBox = (ListBox)cms.SourceControl;
                if (listBox.SelectedItem != null && listBox.SelectedItem is SimpleMapping.SimpleMappingDefinition)
                {
                    listBox.Items.Remove(listBox.SelectedItem);
                }
            }
        }

        //--------------------------------------------------------------------------
        // Button that expands/contracts the right pane with the Programs treeview in it.
        //--------------------------------------------------------------------------
        private void btnExpandRightPane_Click(object sender, EventArgs e)
        {
            int EXPANDO_COL_INDEX = tlpRandomAccess.ColumnCount - 1;

            if (tlpRandomAccess.ColumnStyles[EXPANDO_COL_INDEX].Width == rightColDesignTimeWidth)
            {
                tlpRandomAccess.ColumnStyles[EXPANDO_COL_INDEX].Width = 300;
                btnExpandRightPane.Text = ">>";
                tlpSoundGenTreeview.Visible = true;
                tvProgramPatches.Visible = true;
            }
            else
            {
                tlpRandomAccess.ColumnStyles[EXPANDO_COL_INDEX].Width = rightColDesignTimeWidth;
                btnExpandRightPane.Text = "<<";
                tlpSoundGenTreeview.Visible = false;
                tvProgramPatches.Visible = false;
            }
        }

        //--------------------------------------------------------------------------------
        // When you click on a program in the Programs treeview that program is activated.
        //--------------------------------------------------------------------------------
        private void tvProgramPatches_Click(object sender, EventArgs e)
        {
            TreeNode node = tvProgramPatches.SelectedNode;
            if (node != null)
            {
                if (node.Tag is SoundGenerator)
                {
                    // Route out to this SG, but send no program change...
                    //  Allows user to select an SG and then set sound directly from it.  Useful during configuration.

                }
                else if (node.Tag is SoundGeneratorPatch)
                {
                    // Create an MidiProgram that won't be assigned to any buttons.  Use it to formulate a Mapping and make that the current Mapping.
                    SoundGeneratorPatch soundGeneratorPatch = (SoundGeneratorPatch)node.Tag;
                    MidiProgram midiProgram = new MidiProgram();
                    midiProgram.bSingle = true;
                    midiProgram.SingleSoundGeneratorName = soundGeneratorPatch.soundGenerator.name;
                    midiProgram.SinglePatchName = soundGeneratorPatch.name;
                    // Determine the input device for this midiProgram: the tab's currently selected input device
                    LogicalInputDevice deviceForThisMidiProgram = mapper.configuration.primaryInputDevice;
                    if (tabControl1.SelectedTab.Tag != null && tabControl1.SelectedTab.Tag is LogicalInputDevice)
                    {
                        deviceForThisMidiProgram = (LogicalInputDevice)tabControl1.SelectedTab.Tag;
                    }

                    // Have the midiProgram flesh out its inner details
                    midiProgram.bind(mapper.configuration.logicalInputDeviceDict, mapper.configuration.soundGenerators, mapper.configuration.mappings, deviceForThisMidiProgram);
                    mapper.SetMapping(midiProgram.mapping);
                }
                else if (node.Tag is Mapping)
                {
                    Mapping mapping = (Mapping)node.Tag;
                    mapper.SetMapping(mapping);
                }
            }
        }

        private void tvProgramPatches_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvProgramPatches_Click(sender, null);
        }

        //--------------------------------------------------------------------------
        // Button to switch the Program treeview to SoundGenerator/Program org
        //--------------------------------------------------------------------------
        private void btnPatchTreeViewBySG_Click(object sender, EventArgs e)
        {
            soundGenTreeViewMode = "SG";
            populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvProgramPatches, soundGenTreeViewMode, true);
            btnPatchTreeViewBySG.BackColor = SystemColors.Highlight;
            btnPatchTreeViewByCategory.BackColor = System.Drawing.Color.DimGray;
        }

        //--------------------------------------------------------------------------
        // Button to switch the Program treeview to Category/Program org
        //--------------------------------------------------------------------------
        private void btnPatchTreeViewByCategory_Click(object sender, EventArgs e)
        {
            soundGenTreeViewMode = "Cat";
            populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvProgramPatches, soundGenTreeViewMode, true);
            btnPatchTreeViewBySG.BackColor = System.Drawing.Color.DimGray;
            btnPatchTreeViewByCategory.BackColor = SystemColors.Highlight;
        }

        private void cbRandomAccessInputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mapper.configuration.logicalInputDeviceDict.ContainsKey((String)cbRandomAccessInputDevice.SelectedItem))
            {
                // Make the selected InputDevice the device for the currently showing tab page.  (cbRandomAccessInputDevice only appears on RandomAccess tab pages.)
                tabControl1.SelectedTab.Tag = mapper.configuration.logicalInputDeviceDict[(String)cbRandomAccessInputDevice.SelectedItem];

                // Record the change in the configuration
                if (mapper.configuration.randomAccessTabInputDeviceNames.ContainsKey(tabControl1.SelectedTab.Text))
                {
                    mapper.configuration.randomAccessTabInputDeviceNames[tabControl1.SelectedTab.Text] = (String)cbRandomAccessInputDevice.SelectedItem;
                    mapper.configuration.dirty = true;
                }
                else
                {
                    mapper.configuration.randomAccessTabInputDeviceNames.Add(tabControl1.SelectedTab.Text, (String)cbRandomAccessInputDevice.SelectedItem);
                    mapper.configuration.dirty = true;
                }
            }
            else
            {
                // Switch control back to whatever device is currently configured for this tab.
                String tabInputDeviceName = ((LogicalInputDevice)tabControl1.SelectedTab.Tag).logicalDeviceName;
                cbRandomAccessInputDevice.Text = tabInputDeviceName;
            }
        }

        private void nudRandomAccessTranspose_ValueChanged(object sender, EventArgs e)
        {
            mapper.masterTranspose = (int)nudRandomAccessTranspose.Value;
        }

        private void vsbVol1_Scroll(object sender, ScrollEventArgs e)
        {
            vsbVol2.Value = vsbVol1.Value;      // Vol Control on Show tab tracks this one
            int vol = 127 - vsbVol1.Value;
            mapper.changeMasterVol(vol);
        }
    }
}
