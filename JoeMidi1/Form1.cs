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
        // Form Common
        //**************************************************************************
        float rightColDesignTimeWidth;

        Font buttonFont = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        Mapper mapper;

        String soundGenTreeViewMode = "SG";

        String currentlySelectedTabName = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // this.TopMost = true;
            // this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            currentlySelectedTabName = tabControl1.TabPages[0].Text;

            mapper = new Mapper();

            // Plug the method that we'll receive notification about midi program changes from into the mapper
            mapper.midiProgramChangeNotification = new Mapper.MidiProgramChange(midiProgramChangeNotification);

            mapper.midiProgramActivatedNotification = new Mapper.MidiProgramActivated(midiProgramActivatedNotification);

            mapper.startMapper();

            rightColDesignTimeWidth = tlpRandomAccess.ColumnStyles[tlpRandomAccess.ColumnCount-1].Width;

            //-------------------------------------------
            // Random Access Tab
            //-------------------------------------------

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
                if (tabPage.Text.StartsWith("Random Access") )
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
            if (tabControl1.SelectedTab.Tag != null && tabControl1.SelectedTab.Tag is LogicalInputDevice) {
                cbRandomAccessInputDevice.Text = ((LogicalInputDevice)tabControl1.SelectedTab.Tag).logicalDeviceName;
            }

            //-------------------------------------------
            // Show tab
            //-------------------------------------------
            refreshShowControls();

            //-------------------------------------------
            // Songs tab
            //-------------------------------------------
            refreshSongEditSelector();

            //-------------------------------------------
            // Setlists tab
            //-------------------------------------------
            refreshSetlistEditSelector();

            //-------------------------------------------
            // Mappings Tab
            //-------------------------------------------
            refreshMappingToEditSelector();
            btnMappingEditPatchTreeViewBySG_Click(null, null);

            //-------------------------------------------
            // SoundGenerators tab
            //-------------------------------------------
            refreshSoundGeneratorsListView();

            //-------------------------------------------
            // Misc Tab
            //-------------------------------------------
            foreach (String outputDeviceLogicalName in mapper.configuration.logicalOutputDeviceDict.Keys)
            {
                LogicalOutputDevice logicalOutputDevice = mapper.configuration.logicalOutputDeviceDict[outputDeviceLogicalName];
                if (logicalOutputDevice.device != null)
                {
                    lbOutputDevices.Items.Add(logicalOutputDevice.logicalDeviceName + " -> " + logicalOutputDevice.device.Name);
                }
            }

            lbPhysicalInputDevices.Items.Clear();
            foreach (InputDevice device in InputDevice.InstalledDevices)
            {
                lbPhysicalInputDevices.Items.Add(device.Name);
            }

            lbPhysicalOutputDevices.Items.Clear();
            foreach (OutputDevice device in OutputDevice.InstalledDevices)
            {
                lbPhysicalOutputDevices.Items.Add(device.Name);
            }

            cbPortaitMode.Checked = mapper.configuration.portraitMode;
            cbPortaitMode_CheckedChanged(null, null);

            //-------------------------------------------
            // Activate the first MidiProgram known to the mapper
            //-------------------------------------------
            mapper.selectFirstMidiProgram();

        }

        void midiProgramChangeNotification(int programNum)
        {
            if (currentlySelectedTabName.Equals("Show"))
            {
                // Map the Casio PX3 Basic Program Change buttons to show functions.
                if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[7])     // Button 8
                {
                    // Select Next Song Program
                    mbccShowSongPatches.selectNextLogicalButton(true);
                }
                else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[6])
                {
                    // Select Prev Song Program
                    mbccShowSongPatches.selectPrevLogicalButton(true);
                }
                else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[5])
                {
                    // Select Next Song
                    olvSongs.BeginInvoke(new MethodInvoker(selectNextSong));
                }
                else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[4])
                {
                    // Select Previous Song
                    olvSongs.BeginInvoke(new MethodInvoker(selectPrevSong));
                }
                else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[3]) 
                {
                    // Pick song patch 1 - 4
                    mbccShowSongPatches.selectLogicalButton(3, true, false);
                }
                else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[2])
                {
                    // Pick song patch 1 - 4
                    mbccShowSongPatches.selectLogicalButton(2, true, false);
                }
                else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[1])
                {
                    // Pick song patch 1 - 4
                    mbccShowSongPatches.selectLogicalButton(1, true, false);
                }
                else if (programNum == mapper.configuration.currentPrimaryControllerButtonProgramNumbers[0])
                {
                    // Pick song patch 1 - 4
                    mbccShowSongPatches.selectLogicalButton(0, true, false);
                }

            }
            else if (currentlySelectedTabName.StartsWith("Random Access"))
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
        }

        // Populates a treeview with either SoundGenerators/SoundGeneratorPatches (mode="SG") or Categories/SoundGeneratorPatches (mode="Cat")
        //  Presently used by both Random Access and Song tabs.
        private void populateTreeViewWithSoundGeneratorsPatchesAndMappings(TreeView tv, String mode, bool bIncludeMappings)
        {
            tv.Nodes.Clear();

            if (mode.Equals("Cat"))
            {
                foreach (KeyValuePair<String, List<SoundGeneratorPatch>> categoryEntry in mapper.configuration.soundGeneratorPatchesByCategory)
                {
                    TreeNode categoryNode = new TreeNode(categoryEntry.Key);
                    foreach (SoundGeneratorPatch patch in categoryEntry.Value)
                    {
                        TreeNode sgPatch = new TreeNode(patch.name + " - " + patch.soundGenerator.name);
                        sgPatch.Tag = patch;
                        categoryNode.Nodes.Add(sgPatch);
                    }
                    tv.Nodes.Add(categoryNode);
                }
            }
            else
            {
                foreach (String soundGeneratorName in mapper.configuration.soundGenerators.Keys)
                {
                    SoundGenerator soundGenerator = mapper.configuration.soundGenerators[soundGeneratorName];
                    TreeNode sgNode = new TreeNode(soundGeneratorName);
                    sgNode.Tag = soundGenerator;
                    foreach (String soundGeneratorPatchName in soundGenerator.soundGeneratorPatchDict.Keys)
                    {
                        SoundGeneratorPatch soundGeneratorPatch = soundGenerator.soundGeneratorPatchDict[soundGeneratorPatchName];
                        TreeNode sgpNode = new TreeNode(soundGeneratorPatchName);
                        sgpNode.Tag = soundGeneratorPatch;
                        sgNode.Nodes.Add(sgpNode);
                    }
                    tv.Nodes.Add(sgNode);
                }
            }

            // Mappings are optionally added under a single heading. (I'm not categorizing mappings because they may draw together patches from multiple categories.)
            if (bIncludeMappings)
            {
                TreeNode mappingsNode = new TreeNode("Mappings");
                foreach (String mappingName in mapper.configuration.mappings.Keys)
                {
                    Mapping mapping = mapper.configuration.mappings[mappingName];
                    TreeNode mappingNode = new TreeNode(mappingName);
                    mappingNode.Tag = mapping;
                    mappingsNode.Nodes.Add(mappingNode);
                }
                tv.Nodes.Add(mappingsNode);
            }
        }

        // Tab Control tab selection events switch RandomAccess banks and customize the Form name with the current setlist name
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            currentlySelectedTabName = e.TabPage.Text;

            if (e.TabPage.Text.Equals("Show"))
            {
                this.Text = (currentSetlist != null) ? "Setlist: " + currentSetlist.name : "No Setlist Selected";
            }
            else if (e.TabPage.Text.StartsWith("Random Access")) {

                // Get the new current bank # from the selected tab's last char.
                String randomAccessPageNumber = e.TabPage.Text.Substring(e.TabPage.Text.Length - 1);        // Last char is page #
                currentRandomAccessBank = Int16.Parse(randomAccessPageNumber) - 1;

                // Move the entire grid of buttons, etc, to the newly selected Random Access tab
                tlpRandomAccess.Parent = e.TabPage;

                // Repaint the buttons to reflect the new bank #
                refreshRandomAccessButtonGrid();

                // Standard Form Name
                this.Text = "JoeMidi";

                // Change the Input Device Dropdown selection to be the one currently set for this tab.
                if (e.TabPage.Tag != null && e.TabPage.Tag is LogicalInputDevice)
                {
                    cbRandomAccessInputDevice.Text = ((LogicalInputDevice)e.TabPage.Tag).logicalDeviceName;
                }

                mapper.masterTranspose = 0;
                nudRandomAccessTranspose.Value = 0;

            }
            else
            {
                // Standard Form Name
                this.Text = "JoeMidi";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mapper.stopMapper();
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
                midiProgram.myPatchNumber = ((tlpcp.Row - FIRST_RANDOMACCESS_BTN_ROW) * N_RANDOMACCESS_COLS) + (tlpcp.Column-1);
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
            if (node == null) {
                MessageBox.Show("You must pick a patch");
                return;
            }

            songProgramBeingEdited.part = tbSongPatchPart.Text;
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
            else {
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
            if (songPatchIndex >= 0 && songPatchIndex < songBeingEdited.programs.Count-1)
            {
                SongProgram programBeingMoved = songBeingEdited.programs[songPatchIndex];
                songBeingEdited.programs.RemoveAt(songPatchIndex);
                songBeingEdited.programs.Insert(songPatchIndex + 1, programBeingMoved);
                refreshLbSongPatches();
                lbSongPatches.SelectedIndex = songPatchIndex + 1;
            }

        }

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
            if (songIndex >= 0 && songIndex < setlistBeingEdited.songTitles.Count-1)
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

        //**************************************************************************
        // Sound Generators tab
        //**************************************************************************

        private void refreshSoundGeneratorsListView()
        {
            lvSoundGenerators.Items.Clear();

            foreach (String soundGeneratorName in mapper.configuration.soundGenerators.Keys)
            {
                SoundGenerator sg = mapper.configuration.soundGenerators[soundGeneratorName];

                ListViewItem item = new ListViewItem(sg.name);
                item.Tag = sg;
                ListViewItem.ListViewSubItem siLogicalOutputDevice = new ListViewItem.ListViewSubItem(item, sg.deviceName);
                item.SubItems.Add(siLogicalOutputDevice);
                ListViewItem.ListViewSubItem siLogicalBaseChannel = new ListViewItem.ListViewSubItem(item, String.Format("{0}", sg.channelBase+1));
                item.SubItems.Add(siLogicalBaseChannel);
                ListViewItem.ListViewSubItem siNumChannels = new ListViewItem.ListViewSubItem(item, String.Format("{0}", sg.nChannels));
                item.SubItems.Add(siNumChannels);

                lvSoundGenerators.Items.Add(item);
            }

            cbSoundGeneratorPatchCategory.Items.Clear();
            cbSoundGeneratorPatchCategory.Items.AddRange(mapper.configuration.patchCategories.ToArray());

        }

        private void btnDeleteSoundGenerator_Click(object sender, EventArgs e)
        {
            return; // This method is friggin dangerous.  And the dependency deletions in Configuration aren't 100% worked out.  
                    // Just do it by hand in the JSON file!.
            
            //ListView.SelectedListViewItemCollection selectedItems = lvSoundGenerators.SelectedItems;
            //if (selectedItems.Count > 0)
            //{
            //    ListViewItem item = selectedItems[0];

            //    mapper.configuration.deleteSoundGenerator(item.Text);

            //    refreshSoundGeneratorsListView();

            //}
        }

        SoundGenerator soundGeneratorBeingEdited = null;
        bool bCreatingNewSoundGenerator = true;

        private void btnAddSoundGenerator_Click(object sender, EventArgs e)
        {
            soundGeneratorBeingEdited = new SoundGenerator();

            tbSoundGeneratorName.Text = "";
            cbSoundGeneratorDeviceName.Text = "";
            nudSoundGeneratorBaseChannel.Value = 1;            
            nudSoundGeneratorNumChannels.Value = 1;
            refreshCbSoundGeneratorDeviceName(null);
            lbSoundGeneratorPatches.Items.Clear();

            bCreatingNewSoundGenerator = true;

            pnlSoundGeneratorEdit.Visible = true;

        }

        private void btnSoundGeneratorEditCancel_Click(object sender, EventArgs e)
        {
            bCreatingNewSoundGenerator = false;
            pnlSoundGeneratorEdit.Visible = false;
        }

        private void refreshCbSoundGeneratorDeviceName(String deviceNameToSelect)
        {
            cbSoundGeneratorDeviceName.Items.Clear();
            foreach (LogicalOutputDevice device in mapper.configuration.logicalOutputDeviceDict.Values)
            {
                cbSoundGeneratorDeviceName.Items.Add(device.logicalDeviceName);
            }
            if (deviceNameToSelect != null)
            {
                cbSoundGeneratorDeviceName.Text = deviceNameToSelect;
            }
        }

        private void btnSoundGeneratorEditOK_Click(object sender, EventArgs e)
        {
            if (bCreatingNewSoundGenerator == true)
            {
                soundGeneratorBeingEdited.name = tbSoundGeneratorName.Text;
            }
            soundGeneratorBeingEdited.deviceName = cbSoundGeneratorDeviceName.Text;
            soundGeneratorBeingEdited.channelBase = (int)nudSoundGeneratorBaseChannel.Value-1;
            soundGeneratorBeingEdited.nChannels = (int)nudSoundGeneratorNumChannels.Value;

            if (bCreatingNewSoundGenerator == true)
            {
                soundGeneratorBeingEdited.bind(mapper.configuration.logicalOutputDeviceDict);
                mapper.configuration.soundGenerators.Add(soundGeneratorBeingEdited.name, soundGeneratorBeingEdited);
                mapper.configuration.dirty = true;
            }
            else {
                SoundGenerator soundGeneratorToModify = mapper.configuration.soundGenerators[soundGeneratorBeingEdited.name];
                soundGeneratorToModify.deviceName = cbSoundGeneratorDeviceName.Text;
                soundGeneratorToModify.channelBase = (int)nudSoundGeneratorBaseChannel.Value-1;
                soundGeneratorToModify.nChannels = (int)nudSoundGeneratorNumChannels.Value;
                soundGeneratorToModify.soundGeneratorPatchDict.Clear();
                foreach (String patchName in soundGeneratorBeingEdited.soundGeneratorPatchDict.Keys)
                {
                    SoundGeneratorPatch patch = soundGeneratorBeingEdited.soundGeneratorPatchDict[patchName];
                    soundGeneratorToModify.soundGeneratorPatchDict.Add(patch.name, patch);
                }
                soundGeneratorToModify.bind(mapper.configuration.logicalOutputDeviceDict);
                mapper.configuration.dirty = true;
            }
            refreshSoundGeneratorsListView();
            populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvProgramPatches, soundGenTreeViewMode, true);

            bCreatingNewSoundGenerator = false;
            pnlSoundGeneratorEdit.Visible = false;
            pnlSoundGeneratorPatchEdit.Visible = false;

        }

        bool creatingNewSoundGeneratorPatch = false;

        private void btnSoundGeneratorPatchAdd_Click(object sender, EventArgs e)
        {
            tbSoundGeneratorPatchName.Text = "";
            tbSoundGeneratorPatchName.Enabled = true;
            cbSoundGeneratorPatchCategory.Text = "";
            nudSoundGeneratorPatchBankNo.Value = -1;
            nudSoundGeneratorPatchProgramNo.Value = 0;

            creatingNewSoundGeneratorPatch = true;

            pnlSoundGeneratorPatchEdit.Visible = true;

        }

        private void lbSoundGeneratorPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSoundGeneratorPatches.SelectedItem != null)
            {
                String selectedSoundGeneratorPatchName = (String)lbSoundGeneratorPatches.SelectedItem;

                SoundGeneratorPatch patch = soundGeneratorBeingEdited.soundGeneratorPatchDict[selectedSoundGeneratorPatchName];
                tbSoundGeneratorPatchName.Text = patch.name;
                tbSoundGeneratorPatchName.Enabled = false;
                cbSoundGeneratorPatchCategory.Text = patch.patchCategoryName;
                nudSoundGeneratorPatchBankNo.Value = patch.soundGeneratorBank;
                nudSoundGeneratorPatchProgramNo.Value = patch.soundGeneratorPatchNumber;
                pnlSoundGeneratorPatchEdit.Visible = true;

                creatingNewSoundGeneratorPatch = false;
            }
        }

        private void btnSoundGeneratorPatchEditCancel_Click(object sender, EventArgs e)
        {
            pnlSoundGeneratorPatchEdit.Visible = false;
        }

        private void btnSoundGeneratorPatchEditOK_Click(object sender, EventArgs e)
        {
            SoundGeneratorPatch patch = new SoundGeneratorPatch();
            patch.name = tbSoundGeneratorPatchName.Text;
            patch.patchCategoryName = cbSoundGeneratorPatchCategory.Text;
            patch.soundGeneratorBank = (int)nudSoundGeneratorPatchBankNo.Value;
            patch.soundGeneratorPatchNumber = (int)nudSoundGeneratorPatchProgramNo.Value;
            
            if (creatingNewSoundGeneratorPatch == true && soundGeneratorBeingEdited.soundGeneratorPatchDict.ContainsKey(patch.name)) {
                MessageBox.Show("Patch Name " + patch.name + " already in use in this Sound Generator");
                return;
            }

            if (creatingNewSoundGeneratorPatch == true)
            {
                soundGeneratorBeingEdited.soundGeneratorPatchDict.Add(patch.name, patch);
            }
            else
            {
                soundGeneratorBeingEdited.soundGeneratorPatchDict[patch.name] = patch;
            }

            refreshLbSoundGeneratorPatches();

            pnlSoundGeneratorPatchEdit.Visible = false;

        }

        private void refreshLbSoundGeneratorPatches()
        {
            lbSoundGeneratorPatches.Items.Clear();
            foreach (String patchName in soundGeneratorBeingEdited.soundGeneratorPatchDict.Keys)
            {
                lbSoundGeneratorPatches.Items.Add(patchName);
            }
        }

        private void btnSoundGeneratorPatchDel_Click(object sender, EventArgs e)
        {
            if (lbSoundGeneratorPatches.SelectedItem != null) {
                String selectedSoundGeneratorPatchName = (String)lbSoundGeneratorPatches.SelectedItem;
                soundGeneratorBeingEdited.soundGeneratorPatchDict.Remove(selectedSoundGeneratorPatchName);
                refreshLbSoundGeneratorPatches();
                pnlSoundGeneratorPatchEdit.Visible = false;
            }

        }

        private void lvSoundGenerators_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItems = lvSoundGenerators.SelectedItems;
            if (selectedItems.Count > 0)
            {
                soundGeneratorBeingEdited = new SoundGenerator((SoundGenerator)selectedItems[0].Tag);

                tbSoundGeneratorName.Text = soundGeneratorBeingEdited.name;
                cbSoundGeneratorDeviceName.Text = soundGeneratorBeingEdited.deviceName;
                nudSoundGeneratorBaseChannel.Value = soundGeneratorBeingEdited.channelBase+1;
                nudSoundGeneratorNumChannels.Value = soundGeneratorBeingEdited.nChannels;

                lbSoundGeneratorPatches.Items.Clear();
                foreach (SoundGeneratorPatch patch in soundGeneratorBeingEdited.soundGeneratorPatchDict.Values)
                {
                    lbSoundGeneratorPatches.Items.Add(patch.name);
                }

                refreshCbSoundGeneratorDeviceName(null);

                bCreatingNewSoundGenerator = false;

                pnlSoundGeneratorEdit.Visible = true;

            }
        }

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
        private void refreshMappingToEditSelector()
        {
            mbrcMappingSelect.clearButtons();            
            foreach (Mapping mapping in mapper.configuration.mappingsSorted)
            {
                if (mapping is SimpleMapping)
                {
                    mbrcMappingSelect.addButton(mapping.name, (SimpleMapping)mapping);
                }
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
                if (mapping.perDeviceSimpleMappings.Count > 1) {

                    // Make InputDevice2 mapping def controls visible
                    cbMappingSplitDevice2.Visible = true;

                    SimpleMapping.PerDeviceSimpleMapping perDeviceMapping = mapping.perDeviceSimpleMappings[1];
                    lblMappingInputDevice2.Text =  perDeviceMapping.inputDeviceName;
                    cbMappingSplitDevice2.Checked = perDeviceMapping.splitPoint > 0;
                    nudMappingSplitDevice2.Value = (perDeviceMapping.splitPoint > 0) ? perDeviceMapping.splitPoint : 60;
                    nudMappingSplitDevice2.Visible = perDeviceMapping.splitPoint > 0;
                    lbMappingDevice2UpperPatches.Items.Clear();
                    lbMappingDevice2UpperPatches.Visible = true;
                    lbMappingDevice2LowerPatches.Items.Clear();
                    lbMappingDevice2LowerPatches.Visible = perDeviceMapping.splitPoint > 0;

                    foreach( SimpleMapping.SimpleMappingDefinition mappingDefinition in perDeviceMapping.simpleMappingDefinitions)
                    {
                        if (mappingDefinition.bLower) {
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
            else {
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
                refreshMappingToEditSelector();
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
                        receivingListBox.Items.Add(mappingDefinition);
                    }
                }
            }
        }

        private void lbMappingPatches_MouseUp(object sender, MouseEventArgs e)
        {
            // Make right-click select on the mapping editor's patch list boxes.
            if (e.Button.Equals(MouseButtons.Right)) {

                // Unselect the other listboxes.
                lbMappingDevice1LowerPatches.SelectedIndex = -1;
                lbMappingDevice1UpperPatches.SelectedIndex = -1;
                lbMappingDevice2LowerPatches.SelectedIndex = -1;
                lbMappingDevice2UpperPatches.SelectedIndex = -1;

                ListBox lb = (ListBox)sender;
                int i = lb.IndexFromPoint(e.X, e.Y);
                if (i != ListBox.NoMatches) {
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
            cbMappingDefVolEna.Checked = mappingDef.bEnaVolControl;
            cbMappingDefDamperEna.Checked = mappingDef.bEnaDamperControl;
            tbMappingDefIniVol.Value = mappingDef.initialVolume;
            nudMappingDefDamperRemap.Value = (mappingDef.damperRemap >= 0) ? mappingDef.damperRemap : 64;
            cbMappingDefDamperToggle.Checked = mappingDef.bDamplerToggle;

            showSimpleMappingDefEditorControls(true);
        }

        private void showSimpleMappingDefEditorControls(bool show)
        {
            lblMappingEditPBScale.Visible = show;
            lblMappingEditTranspose.Visible = show;
            lblMappingPBScale.Visible = show;
            lblMappingTransOcts.Visible = show;
            lblMappingTransSemis.Visible = show;
            nudMappingDefTransposeOct.Visible = show;
            nudMappingDefTransposeSemis.Visible = show;
            cbMappingDefDamperEna.Visible = show;
            cbMappingDefDamperToggle.Visible = show;
            cbMappingDefModWheelEna.Visible = show;
            nudMappingDefDamperRemap.Visible = show;
            tbMappingDefIniVol.Visible = show;
            cbMappingDefVolEna.Visible = show;
            tbPBScale.Visible = show;
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
            simpleMappingDefBeingEdited.bEnaVolControl = cbMappingDefVolEna.Checked;
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
            simpleMappingDefBeingEdited.initialVolume = tbMappingDefIniVol.Value;
        }

        private void cbMappingDefDamperToggle_CheckedChanged(object sender, EventArgs e)
        {
            simpleMappingDefBeingEdited.bDamplerToggle = cbMappingDefDamperToggle.Checked;
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
            btnPatchTreeViewBySG_Click(null, null);
                
            // Hide any editor UI elements that may be visible.
            pnlMappingEdit.Visible = false;
            tlpMappingEditNameAndButtons.Visible = false;
        }

        //**************************************************************************
        // Misc Tab
        //**************************************************************************

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnShowFS_Click(object sender, EventArgs e)            // Archaic...
        {
            fmShowFloat form = new fmShowFloat();
//            form.Init(mapper.configuration.setlists);
            form.Show();
        }

        private void cbPortaitMode_CheckedChanged(object sender, EventArgs e)
        {
            if (mapper == null || mapper.configuration == null)
                return;

            mapper.configuration.portraitMode = cbPortaitMode.Checked;

            if (mapper.configuration.portraitMode)
            {
                // Portrait
                tlpShowOuter.ColumnStyles[0].SizeType = SizeType.Absolute;          // Vol Slider
                tlpShowOuter.ColumnStyles[0].Width = 50;
                tlpShowOuter.ColumnStyles[1].SizeType = SizeType.Percent;           // Even Split for Setlist/Patch controls.  Chart spans.
                tlpShowOuter.ColumnStyles[1].Width = 50;
                tlpShowOuter.ColumnStyles[2].SizeType = SizeType.Percent;           // Even Split for Setlist/Patch controls.  Chart spans.
                tlpShowOuter.ColumnStyles[2].Width = 50;
                tlpShowOuter.RowStyles[0].SizeType = SizeType.Percent;              // Remainder (after Row[1]) for Chart
                tlpShowOuter.RowStyles[0].Height = 100;
                tlpShowOuter.RowStyles[1].SizeType = SizeType.Absolute;             // Fixed for Setlist/Patch
                tlpShowOuter.RowStyles[1].Height = 300;

                tlpShowOuter.SetColumn(tlpSongSetlistOuter, 1);
                tlpShowOuter.SetRow(tlpSongSetlistOuter, 1);
                tlpShowOuter.SetColumn(mbccShowSongPatches, 2);
                tlpShowOuter.SetRow(mbccShowSongPatches, 1);

                pnlPatchEdit.Left = pnlSongEdit.Left;
                pnlPatchEdit.Top = pnlSongEdit.Top + pnlSongEdit.Height;

                pnlSetlistSongSelector.Left = pnlSetlistEdit.Left;
                pnlSetlistSongSelector.Top = pnlSetlistEdit.Top + pnlSetlistEdit.Height;

                tlpMappingEditOuter.SetRowSpan(mbrcMappingSelect, 2);
                tlpMappingEditOuter.SetColumnSpan(pnlMappingEdit, 2);
                tlpMappingEditOuter.SetColumnSpan(tlpMappingEditNameAndButtons, 2);
                tlpMappingEditOuter.RowStyles[1].Height = 40;
                tlpMappingEditOuter.RowStyles[2].Height = 60;
                tlpMappingEditOuter.SetColumn(tlpMappingEditorPatches, 1);
                tlpMappingEditOuter.SetRow(tlpMappingEditorPatches, 2);

                tlpSoundGeneratorsOuter.SetRow(pnlSoundGeneratorPatchEdit, 2);
                tlpSoundGeneratorsOuter.SetColumn(pnlSoundGeneratorPatchEdit, 1);
                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorPatchEdit, 2);
                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorEdit, 2);
                tlpSoundGeneratorsOuter.RowStyles[1].Height = 50;
                tlpSoundGeneratorsOuter.RowStyles[2].Height = 50;
            }
            else
            {
                // Landscape
                tlpShowOuter.ColumnStyles[0].SizeType = SizeType.Absolute;          // Vol Slider
                tlpShowOuter.ColumnStyles[0].Width = 50;
                tlpShowOuter.ColumnStyles[1].SizeType = SizeType.Absolute;          // Setlist/Patch col
                tlpShowOuter.ColumnStyles[1].Width = 320;
                tlpShowOuter.ColumnStyles[2].SizeType = SizeType.Percent;           // Chart (spans)
                tlpShowOuter.ColumnStyles[2].Width = 100;
                tlpShowOuter.RowStyles[0].SizeType = SizeType.Percent;              // Setlist (Chart spans)
                tlpShowOuter.RowStyles[0].Height = 50;
                tlpShowOuter.RowStyles[1].SizeType = SizeType.Percent;              // Patches (Chart spans)
                tlpShowOuter.RowStyles[1].Height = 50;

                tlpShowOuter.SetColumn(tlpSongSetlistOuter, 1);
                tlpShowOuter.SetRow(tlpSongSetlistOuter, 0);
                tlpShowOuter.SetColumn(mbccShowSongPatches, 1);
                tlpShowOuter.SetRow(mbccShowSongPatches, 1);

                pnlPatchEdit.Top = pnlSongEdit.Top;
                pnlPatchEdit.Left = pnlSongEdit.Left + pnlSongEdit.Width + 70;

                pnlSetlistSongSelector.Top = pnlSetlistEdit.Top;
                pnlSetlistSongSelector.Left = pnlSetlistEdit.Left + pnlSetlistEdit.Width + 70;

                tlpMappingEditOuter.SetRowSpan(mbrcMappingSelect, 1);
                tlpMappingEditOuter.SetColumnSpan(pnlMappingEdit, 1);
                tlpMappingEditOuter.SetColumnSpan(tlpMappingEditNameAndButtons, 2);
                tlpMappingEditOuter.RowStyles[1].Height = 90;
                tlpMappingEditOuter.RowStyles[2].Height = 10;
                tlpMappingEditOuter.SetColumn(tlpMappingEditorPatches, 2);
                tlpMappingEditOuter.SetRow(tlpMappingEditorPatches, 1);

                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorPatchEdit, 1);
                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorEdit, 1);
                tlpSoundGeneratorsOuter.SetRow(pnlSoundGeneratorPatchEdit, 1);
                tlpSoundGeneratorsOuter.SetColumn(pnlSoundGeneratorPatchEdit, 2);
                tlpSoundGeneratorsOuter.RowStyles[1].Height = 90;
                tlpSoundGeneratorsOuter.RowStyles[2].Height = 10;

            }

            // Call showChart to move/span controls to appropriate position depending on the current chart type
            if (currentSong != null)
            {
                showChart(currentSong.chartFile, currentSong.chartPage);
            }
        }

        private void btnOpenFilesCharts_Click(object sender, EventArgs e)
        {
            string defaultChartFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToLower() + "\\charts";
            openFileDialog1.InitialDirectory = defaultChartFolder;

            string directory, baseFileName;
            try
            {
                FileInfo fi = new FileInfo(tbSongChart.Text);
                directory = fi.Directory.Name;
                baseFileName = fi.Name;
            }
            catch (ArgumentException)
            {
                directory = defaultChartFolder;
                baseFileName = "";
            }

            if (directory.Substring(1,1) == ":" || directory.Substring(0, 1) == "\\")
            {
                openFileDialog1.InitialDirectory = directory;
            }
            else
            {
                openFileDialog1.InitialDirectory = defaultChartFolder + "\\" + directory;
            }
            openFileDialog1.FileName = baseFileName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.ToLower().StartsWith(defaultChartFolder)) {
                    tbSongChart.Text = openFileDialog1.FileName.Substring(defaultChartFolder.Length+1);
                }
                else {
                    tbSongChart.Text = openFileDialog1.FileName;
                }
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            mapper.saveConfiguration();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.cbPortaitMode.Checked = (this.Height > this.Width);
        }
    }
}
