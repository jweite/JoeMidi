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
            Form1_Random_Access_Load(sender, e);

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
                midiProgramChangeNotificationRandomAccess(programNum);
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
    }
}
