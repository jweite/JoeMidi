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

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                mapper.ConfigurationSubDirectory = args[1];
            }
            
            // Plug the method that we'll receive notification about midi program changes from into the mapper
            mapper.midiProgramChangeNotification = new Mapper.MidiProgramChange(midiProgramChangeNotification);

            mapper.midiProgramActivatedNotification = new Mapper.MidiProgramActivated(midiProgramActivatedNotification);

            mapper.startMapper();

            rightColDesignTimeWidth = tlpRandomAccess.ColumnStyles[tlpRandomAccess.ColumnCount - 1].Width;

            Form1_Random_Access_Load(sender, e);

            setCurrentSetlist(mapper.configuration.lastOpenedShowSetlist);

            refreshShowControls();

            refreshSongEditSelector();

            refreshSetlistEditSelector();

            refreshMappingToEditSelector();
            btnMappingEditPatchTreeViewBySG_Click(null, null);

            refreshSoundGeneratorsListView();

            Form1_MiscTab_Load(sender, e);

            mapper.selectFirstMidiProgram();

        }

        void midiProgramChangeNotification(int programNum)
        {
            if (currentlySelectedTabName.Equals("Show"))
            {
                midiProgramChangeNotificationShow(programNum);
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
                List<String> soundGeneratorNames = new List<String>(mapper.configuration.soundGenerators.Keys);
                soundGeneratorNames.Sort();

                foreach (String soundGeneratorName in soundGeneratorNames)
                {
                    SoundGenerator soundGenerator = mapper.configuration.soundGenerators[soundGeneratorName];
                    TreeNode sgNode = new TreeNode(soundGeneratorName);
                    sgNode.Tag = soundGenerator;
                    List<String> patchNames = new List<String>(soundGenerator.soundGeneratorPatchDict.Keys);
                    patchNames.Sort();
                    foreach (String soundGeneratorPatchName in patchNames)
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
                List<String> sortedMappingNames = new List<String>(mapper.configuration.mappings.Keys);
                sortedMappingNames.Sort();

                foreach (String mappingName in sortedMappingNames)
                {
                    Mapping mapping = mapper.configuration.mappings[mappingName];
                    TreeNode mappingNode = new TreeNode(mappingName);
                    mappingNode.Tag = mapping;
                    mappingsNode.Nodes.Add(mappingNode);
                }
                tv.Nodes.Add(mappingsNode);
            }
        }

        private void setFormTitle()
        {
            this.Text = (currentSetlist != null) ? "Setlist: " + currentSetlist.name : "No Setlist Selected";
        }

        // Tab Control tab selection events switch RandomAccess banks and customize the Form name with the current setlist name
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            currentlySelectedTabName = e.TabPage.Text;
            metronomeTimer.Enabled = false;

            if (e.TabPage.Text.Equals("Show"))
            {
                setFormTitle();
                if (currentSong != null && currentSong.bpm > 0)
                {
                    metronomeTimer.Enabled = true;
                }
            }
            else if (e.TabPage.Text.StartsWith("Random Access"))
            {

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

        private void metronomeTimer_Tick(object sender, EventArgs e)
        {
            Icon = Properties.Resources.Black;
            metronomeFlashTimer.Enabled = true;
        }

        private void metronomeFlashTimer_Tick(object sender, EventArgs e)
        {
            Icon = Properties.Resources.White;
            metronomeFlashTimer.Enabled = false;
        }

        public void setMetronomeBPM(int bpm)
        {
            if (bpm > 0)
            {
                metronomeTimer.Interval = 60000 / bpm;
                metronomeTimer.Enabled = true;
            }
            else
            {
                metronomeTimer.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
