﻿namespace JoeMidi1
{
    partial class Form1
    {
        /// <summary>label
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "SG1",
            "Output Device 1",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "SG2",
            "Output Device 1",
            "2",
            "2"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpRandomAccess1 = new System.Windows.Forms.TabPage();
            this.tlpRandomAccess = new System.Windows.Forms.TableLayoutPanel();
            this.btnExpandRightPane = new System.Windows.Forms.Button();
            this.btnRandAccessCol8 = new System.Windows.Forms.Button();
            this.btnRandAccessCol7 = new System.Windows.Forms.Button();
            this.btnRandAccessCol6 = new System.Windows.Forms.Button();
            this.btnRandAccessCol5 = new System.Windows.Forms.Button();
            this.btnRandAccessCol4 = new System.Windows.Forms.Button();
            this.btnRandAccessCol3 = new System.Windows.Forms.Button();
            this.btnRandAccessCol2 = new System.Windows.Forms.Button();
            this.btnRandAccessCol1 = new System.Windows.Forms.Button();
            this.tlpSoundGenTreeview = new System.Windows.Forms.TableLayoutPanel();
            this.tvProgramPatches = new System.Windows.Forms.TreeView();
            this.btnPatchTreeViewBySG = new System.Windows.Forms.Button();
            this.btnPatchTreeViewByCategory = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.cbRandomAccessInputDevice = new System.Windows.Forms.ComboBox();
            this.nudRandomAccessTranspose = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.vsbVol1 = new System.Windows.Forms.VScrollBar();
            this.tpRandomAccess2 = new System.Windows.Forms.TabPage();
            this.tpRandomAccess3 = new System.Windows.Forms.TabPage();
            this.tpRandomAccess4 = new System.Windows.Forms.TabPage();
            this.tpShow = new System.Windows.Forms.TabPage();
            this.tlpShowOuter = new System.Windows.Forms.TableLayoutPanel();
            this.pdfChart = new PdfiumViewer.PdfRenderer();
            this.rtbChart = new System.Windows.Forms.RichTextBox();
            this.tlpSongSetlistOuter = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetlistExpand = new System.Windows.Forms.Button();
            this.olvSongs = new BrightIdeasSoftware.ObjectListView();
            this.olvColSong = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnSetlists = new System.Windows.Forms.Button();
            this.btnNextSong = new System.Windows.Forms.Button();
            this.btnSetlistSongOrderToggle = new System.Windows.Forms.Button();
            this.btnPrevSong = new System.Windows.Forms.Button();
            this.flpAlphaButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAlpha0 = new System.Windows.Forms.Button();
            this.btnAlphaA = new System.Windows.Forms.Button();
            this.btnAlphaB = new System.Windows.Forms.Button();
            this.btnAlphaC = new System.Windows.Forms.Button();
            this.btnAlphaD = new System.Windows.Forms.Button();
            this.btnAlpheE = new System.Windows.Forms.Button();
            this.btnAlphaF = new System.Windows.Forms.Button();
            this.btnAlphaG = new System.Windows.Forms.Button();
            this.btnAlphaH = new System.Windows.Forms.Button();
            this.btnAlphaI = new System.Windows.Forms.Button();
            this.btnAlphaJ = new System.Windows.Forms.Button();
            this.btnAlphaK = new System.Windows.Forms.Button();
            this.btnAlphaL = new System.Windows.Forms.Button();
            this.btnAlphaM = new System.Windows.Forms.Button();
            this.btnAlphaN = new System.Windows.Forms.Button();
            this.btnAlphaO = new System.Windows.Forms.Button();
            this.btnAlphaP = new System.Windows.Forms.Button();
            this.btnAlphaQ = new System.Windows.Forms.Button();
            this.btnAlphaR = new System.Windows.Forms.Button();
            this.btnAlphaS = new System.Windows.Forms.Button();
            this.btnAlphaT = new System.Windows.Forms.Button();
            this.btnAlphaU = new System.Windows.Forms.Button();
            this.btnAlphaV = new System.Windows.Forms.Button();
            this.btnAlphaW = new System.Windows.Forms.Button();
            this.btnAlphaX = new System.Windows.Forms.Button();
            this.btnAlphaY = new System.Windows.Forms.Button();
            this.btnAlphaZ = new System.Windows.Forms.Button();
            this.mbccShowSongPatches = new MultiButtonColControl2.MultiButtonColControl();
            this.vsbVol2 = new System.Windows.Forms.VScrollBar();
            this.tpSongs = new System.Windows.Forms.TabPage();
            this.pnlPatchEdit = new System.Windows.Forms.Panel();
            this.tvSongPatchPatches = new System.Windows.Forms.TreeView();
            this.lblSongPatchPart = new System.Windows.Forms.Label();
            this.nudSongPatchProgramNo = new System.Windows.Forms.NumericUpDown();
            this.lblSongPatchProgramNo = new System.Windows.Forms.Label();
            this.nudSongPatchBank = new System.Windows.Forms.NumericUpDown();
            this.tbSongPatchPart = new System.Windows.Forms.TextBox();
            this.lblSongPatchBank = new System.Windows.Forms.Label();
            this.btnPatchEditCancel = new System.Windows.Forms.Button();
            this.btnPatchEditOK = new System.Windows.Forms.Button();
            this.pnlSongEdit = new System.Windows.Forms.Panel();
            this.nudBPM = new System.Windows.Forms.NumericUpDown();
            this.lblBpm = new System.Windows.Forms.Label();
            this.btnOpenFilesCharts = new System.Windows.Forms.Button();
            this.nudSongChartPage = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.nudSongTranspose = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.btnPatchDel = new System.Windows.Forms.Button();
            this.btnPatchAdd = new System.Windows.Forms.Button();
            this.btnPatchDown = new System.Windows.Forms.Button();
            this.btnPatchUp = new System.Windows.Forms.Button();
            this.btnSongEditCancel = new System.Windows.Forms.Button();
            this.btnSongEditOK = new System.Windows.Forms.Button();
            this.lblSongPatches = new System.Windows.Forms.Label();
            this.lbSongPatches = new System.Windows.Forms.ListBox();
            this.tbSongChart = new System.Windows.Forms.TextBox();
            this.lblSongChart = new System.Windows.Forms.Label();
            this.tbSongArtist = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSongTitle = new System.Windows.Forms.TextBox();
            this.lblSongTitle = new System.Windows.Forms.Label();
            this.tlpSongSelOuter = new System.Windows.Forms.TableLayoutPanel();
            this.mbccSongEditSelector = new MultiButtonColControl2.MultiButtonColControl();
            this.tlpSongSelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSongDel = new System.Windows.Forms.Button();
            this.btnAddSong = new System.Windows.Forms.Button();
            this.tpSetlists = new System.Windows.Forms.TabPage();
            this.pnlSetlistSongSelector = new System.Windows.Forms.Panel();
            this.cbSetEditorSonglistSetSelector = new System.Windows.Forms.ComboBox();
            this.tvSongsForSetlists = new System.Windows.Forms.TreeView();
            this.pnlSetlistEdit = new System.Windows.Forms.Panel();
            this.btnSetlistSort = new System.Windows.Forms.Button();
            this.btnSetlistDeleteSong = new System.Windows.Forms.Button();
            this.btnSetlistAddSong = new System.Windows.Forms.Button();
            this.btnSetlistSongDown = new System.Windows.Forms.Button();
            this.btnSetlistSongUp = new System.Windows.Forms.Button();
            this.btnSetlistEditCancel = new System.Windows.Forms.Button();
            this.btnSetlistEditOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSetlistSongs = new System.Windows.Forms.ListBox();
            this.tbSetlistName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tlpSetlistSelOuter = new System.Windows.Forms.TableLayoutPanel();
            this.mbccSetlistEditSelector = new MultiButtonColControl2.MultiButtonColControl();
            this.tlpSetlistSelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetlistAdd = new System.Windows.Forms.Button();
            this.btnSetlistDel = new System.Windows.Forms.Button();
            this.tpMappings = new System.Windows.Forms.TabPage();
            this.tlpMappingEditOuter = new System.Windows.Forms.TableLayoutPanel();
            this.mbrcMappingSelect = new MultiButtonColControl2.MultiButtonColControl();
            this.tlpMappingEditButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnMappingDelete = new System.Windows.Forms.Button();
            this.btnMappingAdd = new System.Windows.Forms.Button();
            this.tlpMappingEditNameAndButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnMappingEditOK = new System.Windows.Forms.Button();
            this.btnMappingEditCancel = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.tbMappingName = new System.Windows.Forms.TextBox();
            this.pnlMappingEdit = new System.Windows.Forms.Panel();
            this.lblVolume = new System.Windows.Forms.Label();
            this.tbVolume = new System.Windows.Forms.TextBox();
            this.lblMappingPBScale = new System.Windows.Forms.Label();
            this.lblMappingTransOcts = new System.Windows.Forms.Label();
            this.lblMappingTransSemis = new System.Windows.Forms.Label();
            this.cbMappingDefDamperToggle = new System.Windows.Forms.CheckBox();
            this.nudMappingDefDamperRemap = new System.Windows.Forms.NumericUpDown();
            this.cbMappingDefDamperEna = new System.Windows.Forms.CheckBox();
            this.tbMappingDefIniCC7 = new System.Windows.Forms.TrackBar();
            this.cbMappingDefVolEna = new System.Windows.Forms.CheckBox();
            this.cbMappingDefModWheelEna = new System.Windows.Forms.CheckBox();
            this.lblMappingEditPBScale = new System.Windows.Forms.Label();
            this.tbPBScale = new System.Windows.Forms.TrackBar();
            this.nudMappingDefTransposeSemis = new System.Windows.Forms.NumericUpDown();
            this.lblMappingEditTranspose = new System.Windows.Forms.Label();
            this.nudMappingDefTransposeOct = new System.Windows.Forms.NumericUpDown();
            this.cbMappingSplitDevice2 = new System.Windows.Forms.CheckBox();
            this.nudMappingSplitDevice2 = new System.Windows.Forms.NumericUpDown();
            this.cbMappingSplitDevice1 = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.nudMappingSplitDevice1 = new System.Windows.Forms.NumericUpDown();
            this.lblMappingInputDevice2 = new System.Windows.Forms.Label();
            this.lbMappingDevice2LowerPatches = new System.Windows.Forms.ListBox();
            this.lbMappingDevice2UpperPatches = new System.Windows.Forms.ListBox();
            this.lblMappingInputDevice1 = new System.Windows.Forms.Label();
            this.lbMappingDevice1LowerPatches = new System.Windows.Forms.ListBox();
            this.lbMappingDevice1UpperPatches = new System.Windows.Forms.ListBox();
            this.tlpMappingEditorPatches = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMappingEditPatchTreeViewBySG = new System.Windows.Forms.Button();
            this.btnMappingEditPatchTreeViewByCategory = new System.Windows.Forms.Button();
            this.tvMappingEditorPrograms = new System.Windows.Forms.TreeView();
            this.tpSoundGenerators = new System.Windows.Forms.TabPage();
            this.tlpSoundGeneratorsOuter = new System.Windows.Forms.TableLayoutPanel();
            this.lvSoundGenerators = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpSoundGeneratorEditButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnDeleteSoundGenerator = new System.Windows.Forms.Button();
            this.btnAddSoundGenerator = new System.Windows.Forms.Button();
            this.pnlSoundGeneratorEdit = new System.Windows.Forms.Panel();
            this.cbSoundGeneratorCloneOf = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.tbDefaultVolume = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tbTrackName = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.nudVolMax = new System.Windows.Forms.NumericUpDown();
            this.nudVolMin = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.btnSoundGeneratorEditOK = new System.Windows.Forms.Button();
            this.btnSoundGeneratorEditCancel = new System.Windows.Forms.Button();
            this.btnSoundGeneratorPatchDel = new System.Windows.Forms.Button();
            this.btnSoundGeneratorPatchAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lbSoundGeneratorPatches = new System.Windows.Forms.ListBox();
            this.nudSoundGeneratorNumChannels = new System.Windows.Forms.NumericUpDown();
            this.nudSoundGeneratorBaseChannel = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbSoundGeneratorDeviceName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbSoundGeneratorName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlSoundGeneratorPatchEdit = new System.Windows.Forms.Panel();
            this.tbVolumeOverride = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.tbFxPreset5 = new System.Windows.Forms.TextBox();
            this.tbFxPreset4 = new System.Windows.Forms.TextBox();
            this.tbFxPreset3 = new System.Windows.Forms.TextBox();
            this.tbFxPreset2 = new System.Windows.Forms.TextBox();
            this.tbFxPreset1 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnSoundGeneratorPatchEditOK = new System.Windows.Forms.Button();
            this.btnSoundGeneratorPatchEditCancel = new System.Windows.Forms.Button();
            this.nudSoundGeneratorPatchProgramNo = new System.Windows.Forms.NumericUpDown();
            this.nudSoundGeneratorPatchBankNo = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbSoundGeneratorPatchCategory = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbSoundGeneratorPatchName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tpMisc = new System.Windows.Forms.TabPage();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.lbPhysicalOutputDevices = new System.Windows.Forms.ListBox();
            this.label22 = new System.Windows.Forms.Label();
            this.lbPhysicalInputDevices = new System.Windows.Forms.ListBox();
            this.cbPortaitMode = new System.Windows.Forms.CheckBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lbInputDevices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbOutputDevices = new System.Windows.Forms.ListBox();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.metronomeTimer = new System.Windows.Forms.Timer(this.components);
            this.metronomeFlashTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpRandomAccess1.SuspendLayout();
            this.tlpRandomAccess.SuspendLayout();
            this.tlpSoundGenTreeview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomAccessTranspose)).BeginInit();
            this.tpShow.SuspendLayout();
            this.tlpShowOuter.SuspendLayout();
            this.tlpSongSetlistOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).BeginInit();
            this.flpAlphaButtons.SuspendLayout();
            this.tpSongs.SuspendLayout();
            this.pnlPatchEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongPatchProgramNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongPatchBank)).BeginInit();
            this.pnlSongEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongChartPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongTranspose)).BeginInit();
            this.tlpSongSelOuter.SuspendLayout();
            this.tlpSongSelButtons.SuspendLayout();
            this.tpSetlists.SuspendLayout();
            this.pnlSetlistSongSelector.SuspendLayout();
            this.pnlSetlistEdit.SuspendLayout();
            this.tlpSetlistSelOuter.SuspendLayout();
            this.tlpSetlistSelButtons.SuspendLayout();
            this.tpMappings.SuspendLayout();
            this.tlpMappingEditOuter.SuspendLayout();
            this.tlpMappingEditButtons.SuspendLayout();
            this.tlpMappingEditNameAndButtons.SuspendLayout();
            this.pnlMappingEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingDefDamperRemap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMappingDefIniCC7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPBScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingDefTransposeSemis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingDefTransposeOct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingSplitDevice2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingSplitDevice1)).BeginInit();
            this.tlpMappingEditorPatches.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tpSoundGenerators.SuspendLayout();
            this.tlpSoundGeneratorsOuter.SuspendLayout();
            this.tlpSoundGeneratorEditButtons.SuspendLayout();
            this.pnlSoundGeneratorEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorNumChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorBaseChannel)).BeginInit();
            this.pnlSoundGeneratorPatchEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorPatchProgramNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorPatchBankNo)).BeginInit();
            this.tpMisc.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            this.contextMenuStrip1.Text = "contextMenuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem1.Text = "Delete";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpRandomAccess1);
            this.tabControl1.Controls.Add(this.tpRandomAccess2);
            this.tabControl1.Controls.Add(this.tpRandomAccess3);
            this.tabControl1.Controls.Add(this.tpRandomAccess4);
            this.tabControl1.Controls.Add(this.tpShow);
            this.tabControl1.Controls.Add(this.tpSongs);
            this.tabControl1.Controls.Add(this.tpSetlists);
            this.tabControl1.Controls.Add(this.tpMappings);
            this.tabControl1.Controls.Add(this.tpSoundGenerators);
            this.tabControl1.Controls.Add(this.tpMisc);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(90, 40);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1155, 673);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tpRandomAccess1
            // 
            this.tpRandomAccess1.BackColor = System.Drawing.Color.Black;
            this.tpRandomAccess1.Controls.Add(this.tlpRandomAccess);
            this.tpRandomAccess1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpRandomAccess1.ForeColor = System.Drawing.Color.White;
            this.tpRandomAccess1.Location = new System.Drawing.Point(4, 44);
            this.tpRandomAccess1.Name = "tpRandomAccess1";
            this.tpRandomAccess1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpRandomAccess1.Size = new System.Drawing.Size(1147, 625);
            this.tpRandomAccess1.TabIndex = 0;
            this.tpRandomAccess1.Tag = "0";
            this.tpRandomAccess1.Text = "Random Access 1";
            // 
            // tlpRandomAccess
            // 
            this.tlpRandomAccess.AllowDrop = true;
            this.tlpRandomAccess.ColumnCount = 10;
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tlpRandomAccess.Controls.Add(this.btnExpandRightPane, 9, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol8, 8, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol7, 7, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol6, 6, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol5, 5, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol4, 4, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol3, 3, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol2, 2, 0);
            this.tlpRandomAccess.Controls.Add(this.btnRandAccessCol1, 1, 0);
            this.tlpRandomAccess.Controls.Add(this.tlpSoundGenTreeview, 9, 1);
            this.tlpRandomAccess.Controls.Add(this.label18, 1, 9);
            this.tlpRandomAccess.Controls.Add(this.cbRandomAccessInputDevice, 2, 9);
            this.tlpRandomAccess.Controls.Add(this.nudRandomAccessTranspose, 7, 9);
            this.tlpRandomAccess.Controls.Add(this.label17, 6, 9);
            this.tlpRandomAccess.Controls.Add(this.vsbVol1, 0, 0);
            this.tlpRandomAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRandomAccess.Location = new System.Drawing.Point(3, 3);
            this.tlpRandomAccess.Name = "tlpRandomAccess";
            this.tlpRandomAccess.RowCount = 10;
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpRandomAccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpRandomAccess.Size = new System.Drawing.Size(1141, 619);
            this.tlpRandomAccess.TabIndex = 13;
            this.tlpRandomAccess.DragDrop += new System.Windows.Forms.DragEventHandler(this.tlpRandomAccess_DragDrop);
            this.tlpRandomAccess.DragEnter += new System.Windows.Forms.DragEventHandler(this.tlpRandomAccess_DragEnter);
            // 
            // btnExpandRightPane
            // 
            this.btnExpandRightPane.AutoSize = true;
            this.btnExpandRightPane.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExpandRightPane.BackColor = System.Drawing.Color.DimGray;
            this.btnExpandRightPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExpandRightPane.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnExpandRightPane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpandRightPane.Location = new System.Drawing.Point(1053, 3);
            this.btnExpandRightPane.Name = "btnExpandRightPane";
            this.btnExpandRightPane.Size = new System.Drawing.Size(85, 40);
            this.btnExpandRightPane.TabIndex = 17;
            this.btnExpandRightPane.Text = "<<";
            this.btnExpandRightPane.UseVisualStyleBackColor = false;
            this.btnExpandRightPane.Click += new System.EventHandler(this.btnExpandRightPane_Click);
            // 
            // btnRandAccessCol8
            // 
            this.btnRandAccessCol8.AllowDrop = true;
            this.btnRandAccessCol8.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol8.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol8.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol8.Location = new System.Drawing.Point(928, 3);
            this.btnRandAccessCol8.Name = "btnRandAccessCol8";
            this.btnRandAccessCol8.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol8.TabIndex = 16;
            this.btnRandAccessCol8.Text = "8";
            this.btnRandAccessCol8.UseVisualStyleBackColor = false;
            this.btnRandAccessCol8.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol8.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol8.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // btnRandAccessCol7
            // 
            this.btnRandAccessCol7.AllowDrop = true;
            this.btnRandAccessCol7.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol7.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol7.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol7.Location = new System.Drawing.Point(803, 3);
            this.btnRandAccessCol7.Name = "btnRandAccessCol7";
            this.btnRandAccessCol7.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol7.TabIndex = 15;
            this.btnRandAccessCol7.Text = "7";
            this.btnRandAccessCol7.UseVisualStyleBackColor = false;
            this.btnRandAccessCol7.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol7.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol7.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // btnRandAccessCol6
            // 
            this.btnRandAccessCol6.AllowDrop = true;
            this.btnRandAccessCol6.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol6.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol6.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol6.Location = new System.Drawing.Point(678, 3);
            this.btnRandAccessCol6.Name = "btnRandAccessCol6";
            this.btnRandAccessCol6.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol6.TabIndex = 14;
            this.btnRandAccessCol6.Text = "6";
            this.btnRandAccessCol6.UseVisualStyleBackColor = false;
            this.btnRandAccessCol6.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol6.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol6.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // btnRandAccessCol5
            // 
            this.btnRandAccessCol5.AllowDrop = true;
            this.btnRandAccessCol5.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol5.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol5.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol5.Location = new System.Drawing.Point(553, 3);
            this.btnRandAccessCol5.Name = "btnRandAccessCol5";
            this.btnRandAccessCol5.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol5.TabIndex = 13;
            this.btnRandAccessCol5.Text = "5";
            this.btnRandAccessCol5.UseVisualStyleBackColor = false;
            this.btnRandAccessCol5.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol5.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol5.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // btnRandAccessCol4
            // 
            this.btnRandAccessCol4.AllowDrop = true;
            this.btnRandAccessCol4.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol4.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol4.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol4.Location = new System.Drawing.Point(428, 3);
            this.btnRandAccessCol4.Name = "btnRandAccessCol4";
            this.btnRandAccessCol4.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol4.TabIndex = 12;
            this.btnRandAccessCol4.Text = "4";
            this.btnRandAccessCol4.UseVisualStyleBackColor = false;
            this.btnRandAccessCol4.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol4.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol4.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // btnRandAccessCol3
            // 
            this.btnRandAccessCol3.AllowDrop = true;
            this.btnRandAccessCol3.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol3.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol3.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol3.Location = new System.Drawing.Point(303, 3);
            this.btnRandAccessCol3.Name = "btnRandAccessCol3";
            this.btnRandAccessCol3.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol3.TabIndex = 11;
            this.btnRandAccessCol3.Text = "3";
            this.btnRandAccessCol3.UseVisualStyleBackColor = false;
            this.btnRandAccessCol3.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol3.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol3.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // btnRandAccessCol2
            // 
            this.btnRandAccessCol2.AllowDrop = true;
            this.btnRandAccessCol2.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol2.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol2.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol2.Location = new System.Drawing.Point(178, 3);
            this.btnRandAccessCol2.Name = "btnRandAccessCol2";
            this.btnRandAccessCol2.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol2.TabIndex = 10;
            this.btnRandAccessCol2.Text = "2";
            this.btnRandAccessCol2.UseVisualStyleBackColor = false;
            this.btnRandAccessCol2.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol2.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol2.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // btnRandAccessCol1
            // 
            this.btnRandAccessCol1.AllowDrop = true;
            this.btnRandAccessCol1.BackColor = System.Drawing.Color.DimGray;
            this.btnRandAccessCol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRandAccessCol1.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnRandAccessCol1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandAccessCol1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandAccessCol1.Location = new System.Drawing.Point(53, 3);
            this.btnRandAccessCol1.Name = "btnRandAccessCol1";
            this.btnRandAccessCol1.Size = new System.Drawing.Size(119, 40);
            this.btnRandAccessCol1.TabIndex = 9;
            this.btnRandAccessCol1.Text = "1";
            this.btnRandAccessCol1.UseVisualStyleBackColor = false;
            this.btnRandAccessCol1.Click += new System.EventHandler(this.btnRandAccessCol_Click);
            this.btnRandAccessCol1.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragDrop);
            this.btnRandAccessCol1.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnRandAccessCol_DragEnter);
            // 
            // tlpSoundGenTreeview
            // 
            this.tlpSoundGenTreeview.ColumnCount = 2;
            this.tlpSoundGenTreeview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSoundGenTreeview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSoundGenTreeview.Controls.Add(this.tvProgramPatches, 0, 0);
            this.tlpSoundGenTreeview.Controls.Add(this.btnPatchTreeViewBySG, 0, 1);
            this.tlpSoundGenTreeview.Controls.Add(this.btnPatchTreeViewByCategory, 1, 1);
            this.tlpSoundGenTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSoundGenTreeview.Location = new System.Drawing.Point(1053, 49);
            this.tlpSoundGenTreeview.Name = "tlpSoundGenTreeview";
            this.tlpSoundGenTreeview.RowCount = 2;
            this.tlpRandomAccess.SetRowSpan(this.tlpSoundGenTreeview, 8);
            this.tlpSoundGenTreeview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSoundGenTreeview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpSoundGenTreeview.Size = new System.Drawing.Size(85, 514);
            this.tlpSoundGenTreeview.TabIndex = 19;
            this.tlpSoundGenTreeview.Visible = false;
            // 
            // tvProgramPatches
            // 
            this.tlpSoundGenTreeview.SetColumnSpan(this.tvProgramPatches, 2);
            this.tvProgramPatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProgramPatches.HideSelection = false;
            this.tvProgramPatches.Location = new System.Drawing.Point(3, 3);
            this.tvProgramPatches.Name = "tvProgramPatches";
            this.tvProgramPatches.Size = new System.Drawing.Size(79, 458);
            this.tvProgramPatches.TabIndex = 18;
            this.tvProgramPatches.Visible = false;
            this.tvProgramPatches.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvProgramPatches_ItemDrag);
            this.tvProgramPatches.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProgramPatches_AfterSelect);
            // 
            // btnPatchTreeViewBySG
            // 
            this.btnPatchTreeViewBySG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPatchTreeViewBySG.Location = new System.Drawing.Point(3, 467);
            this.btnPatchTreeViewBySG.Name = "btnPatchTreeViewBySG";
            this.btnPatchTreeViewBySG.Size = new System.Drawing.Size(36, 44);
            this.btnPatchTreeViewBySG.TabIndex = 19;
            this.btnPatchTreeViewBySG.Text = "SG";
            this.btnPatchTreeViewBySG.UseVisualStyleBackColor = true;
            this.btnPatchTreeViewBySG.Click += new System.EventHandler(this.btnPatchTreeViewBySG_Click);
            // 
            // btnPatchTreeViewByCategory
            // 
            this.btnPatchTreeViewByCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPatchTreeViewByCategory.Location = new System.Drawing.Point(45, 467);
            this.btnPatchTreeViewByCategory.Name = "btnPatchTreeViewByCategory";
            this.btnPatchTreeViewByCategory.Size = new System.Drawing.Size(37, 44);
            this.btnPatchTreeViewByCategory.TabIndex = 20;
            this.btnPatchTreeViewByCategory.Text = "Cat";
            this.btnPatchTreeViewByCategory.UseVisualStyleBackColor = true;
            this.btnPatchTreeViewByCategory.Click += new System.EventHandler(this.btnPatchTreeViewByCategory_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(53, 566);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(119, 48);
            this.label18.TabIndex = 22;
            this.label18.Text = "Input Device:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbRandomAccessInputDevice
            // 
            this.cbRandomAccessInputDevice.AllowDrop = true;
            this.cbRandomAccessInputDevice.BackColor = System.Drawing.Color.DimGray;
            this.tlpRandomAccess.SetColumnSpan(this.cbRandomAccessInputDevice, 3);
            this.cbRandomAccessInputDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbRandomAccessInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRandomAccessInputDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRandomAccessInputDevice.ForeColor = System.Drawing.Color.White;
            this.cbRandomAccessInputDevice.FormattingEnabled = true;
            this.cbRandomAccessInputDevice.Location = new System.Drawing.Point(178, 569);
            this.cbRandomAccessInputDevice.Name = "cbRandomAccessInputDevice";
            this.cbRandomAccessInputDevice.Size = new System.Drawing.Size(369, 32);
            this.cbRandomAccessInputDevice.TabIndex = 23;
            this.cbRandomAccessInputDevice.SelectedIndexChanged += new System.EventHandler(this.cbRandomAccessInputDevice_SelectedIndexChanged);
            // 
            // nudRandomAccessTranspose
            // 
            this.nudRandomAccessTranspose.BackColor = System.Drawing.Color.DimGray;
            this.nudRandomAccessTranspose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudRandomAccessTranspose.ForeColor = System.Drawing.Color.White;
            this.nudRandomAccessTranspose.Location = new System.Drawing.Point(803, 569);
            this.nudRandomAccessTranspose.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudRandomAccessTranspose.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.nudRandomAccessTranspose.Name = "nudRandomAccessTranspose";
            this.nudRandomAccessTranspose.Size = new System.Drawing.Size(119, 32);
            this.nudRandomAccessTranspose.TabIndex = 20;
            this.nudRandomAccessTranspose.ValueChanged += new System.EventHandler(this.nudRandomAccessTranspose_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(678, 566);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 24);
            this.label17.TabIndex = 21;
            this.label17.Text = "Transpose:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vsbVol1
            // 
            this.vsbVol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vsbVol1.Location = new System.Drawing.Point(0, 0);
            this.vsbVol1.Maximum = 127;
            this.vsbVol1.Name = "vsbVol1";
            this.tlpRandomAccess.SetRowSpan(this.vsbVol1, 9);
            this.vsbVol1.Size = new System.Drawing.Size(50, 566);
            this.vsbVol1.TabIndex = 24;
            this.vsbVol1.Value = 27;
            this.vsbVol1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbVol1_Scroll);
            // 
            // tpRandomAccess2
            // 
            this.tpRandomAccess2.BackColor = System.Drawing.Color.Black;
            this.tpRandomAccess2.Location = new System.Drawing.Point(4, 44);
            this.tpRandomAccess2.Name = "tpRandomAccess2";
            this.tpRandomAccess2.Size = new System.Drawing.Size(1147, 625);
            this.tpRandomAccess2.TabIndex = 6;
            this.tpRandomAccess2.Tag = "1";
            this.tpRandomAccess2.Text = "Random Access 2";
            // 
            // tpRandomAccess3
            // 
            this.tpRandomAccess3.BackColor = System.Drawing.Color.Black;
            this.tpRandomAccess3.Location = new System.Drawing.Point(4, 44);
            this.tpRandomAccess3.Name = "tpRandomAccess3";
            this.tpRandomAccess3.Size = new System.Drawing.Size(1147, 625);
            this.tpRandomAccess3.TabIndex = 7;
            this.tpRandomAccess3.Tag = "2";
            this.tpRandomAccess3.Text = "Random Access 3";
            // 
            // tpRandomAccess4
            // 
            this.tpRandomAccess4.BackColor = System.Drawing.Color.Black;
            this.tpRandomAccess4.Location = new System.Drawing.Point(4, 44);
            this.tpRandomAccess4.Name = "tpRandomAccess4";
            this.tpRandomAccess4.Size = new System.Drawing.Size(1147, 625);
            this.tpRandomAccess4.TabIndex = 8;
            this.tpRandomAccess4.Tag = "3";
            this.tpRandomAccess4.Text = "Random Access 4";
            // 
            // tpShow
            // 
            this.tpShow.BackColor = System.Drawing.Color.Black;
            this.tpShow.Controls.Add(this.tlpShowOuter);
            this.tpShow.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpShow.Location = new System.Drawing.Point(4, 44);
            this.tpShow.Name = "tpShow";
            this.tpShow.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpShow.Size = new System.Drawing.Size(1147, 625);
            this.tpShow.TabIndex = 1;
            this.tpShow.Tag = "";
            this.tpShow.Text = "Show";
            // 
            // tlpShowOuter
            // 
            this.tlpShowOuter.ColumnCount = 3;
            this.tlpShowOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpShowOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShowOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpShowOuter.Controls.Add(this.pdfChart, 1, 0);
            this.tlpShowOuter.Controls.Add(this.rtbChart, 1, 0);
            this.tlpShowOuter.Controls.Add(this.tlpSongSetlistOuter, 0, 1);
            this.tlpShowOuter.Controls.Add(this.mbccShowSongPatches, 3, 1);
            this.tlpShowOuter.Controls.Add(this.vsbVol2, 0, 0);
            this.tlpShowOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpShowOuter.Location = new System.Drawing.Point(3, 3);
            this.tlpShowOuter.Name = "tlpShowOuter";
            this.tlpShowOuter.RowCount = 2;
            this.tlpShowOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpShowOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpShowOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpShowOuter.Size = new System.Drawing.Size(1141, 619);
            this.tlpShowOuter.TabIndex = 2;
            // 
            // pdfChart
            // 
            this.pdfChart.BackColor = System.Drawing.Color.Maroon;
            this.pdfChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfChart.Location = new System.Drawing.Point(51, 1);
            this.pdfChart.Margin = new System.Windows.Forms.Padding(1);
            this.pdfChart.Name = "pdfChart";
            this.pdfChart.Page = 0;
            this.pdfChart.Rotation = PdfiumViewer.PdfRotation.Rotate0;
            this.pdfChart.Size = new System.Drawing.Size(543, 317);
            this.pdfChart.TabIndex = 16;
            this.pdfChart.Text = "pdfRenderer1";
            this.pdfChart.Visible = false;
            this.pdfChart.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitHeight;
            // 
            // rtbChart
            // 
            this.rtbChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChart.Location = new System.Drawing.Point(598, 3);
            this.rtbChart.Name = "rtbChart";
            this.rtbChart.Size = new System.Drawing.Size(540, 313);
            this.rtbChart.TabIndex = 15;
            this.rtbChart.Text = "";
            // 
            // tlpSongSetlistOuter
            // 
            this.tlpSongSetlistOuter.ColumnCount = 6;
            this.tlpSongSetlistOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSongSetlistOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSongSetlistOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSongSetlistOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSongSetlistOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSongSetlistOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpSongSetlistOuter.Controls.Add(this.btnSetlistExpand, 3, 0);
            this.tlpSongSetlistOuter.Controls.Add(this.olvSongs, 0, 1);
            this.tlpSongSetlistOuter.Controls.Add(this.btnSetlists, 1, 0);
            this.tlpSongSetlistOuter.Controls.Add(this.btnNextSong, 4, 0);
            this.tlpSongSetlistOuter.Controls.Add(this.btnSetlistSongOrderToggle, 2, 0);
            this.tlpSongSetlistOuter.Controls.Add(this.btnPrevSong, 0, 0);
            this.tlpSongSetlistOuter.Controls.Add(this.flpAlphaButtons, 5, 0);
            this.tlpSongSetlistOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSongSetlistOuter.Location = new System.Drawing.Point(53, 322);
            this.tlpSongSetlistOuter.Name = "tlpSongSetlistOuter";
            this.tlpSongSetlistOuter.RowCount = 2;
            this.tlpSongSetlistOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpSongSetlistOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSongSetlistOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpSongSetlistOuter.Size = new System.Drawing.Size(539, 294);
            this.tlpSongSetlistOuter.TabIndex = 13;
            // 
            // btnSetlistExpand
            // 
            this.btnSetlistExpand.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistExpand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetlistExpand.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnSetlistExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetlistExpand.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistExpand.ForeColor = System.Drawing.Color.White;
            this.btnSetlistExpand.Location = new System.Drawing.Point(297, 3);
            this.btnSetlistExpand.Name = "btnSetlistExpand";
            this.btnSetlistExpand.Size = new System.Drawing.Size(92, 50);
            this.btnSetlistExpand.TabIndex = 6;
            this.btnSetlistExpand.Text = "^^^";
            this.btnSetlistExpand.UseVisualStyleBackColor = false;
            this.btnSetlistExpand.Click += new System.EventHandler(this.btnSetlistExpand_Click);
            // 
            // olvSongs
            // 
            this.olvSongs.AllColumns.Add(this.olvColSong);
            this.olvSongs.BackColor = System.Drawing.Color.DimGray;
            this.olvSongs.CellEditUseWholeCell = false;
            this.olvSongs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColSong});
            this.tlpSongSetlistOuter.SetColumnSpan(this.olvSongs, 5);
            this.olvSongs.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvSongs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvSongs.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvSongs.ForeColor = System.Drawing.Color.White;
            this.olvSongs.FullRowSelect = true;
            this.olvSongs.GridLines = true;
            this.olvSongs.HideSelection = false;
            this.olvSongs.Location = new System.Drawing.Point(3, 59);
            this.olvSongs.MultiSelect = false;
            this.olvSongs.Name = "olvSongs";
            this.olvSongs.PersistentCheckBoxes = false;
            this.olvSongs.RowHeight = 50;
            this.olvSongs.ShowGroups = false;
            this.olvSongs.Size = new System.Drawing.Size(484, 232);
            this.olvSongs.TabIndex = 0;
            this.olvSongs.UseCompatibleStateImageBehavior = false;
            this.olvSongs.View = System.Windows.Forms.View.Details;
            this.olvSongs.SelectedIndexChanged += new System.EventHandler(this.olvSongs_SelectedIndexChanged);
            // 
            // olvColSong
            // 
            this.olvColSong.AspectName = "name";
            this.olvColSong.FillsFreeSpace = true;
            this.olvColSong.Groupable = false;
            this.olvColSong.Text = "Songs";
            this.olvColSong.Width = 100;
            // 
            // btnSetlists
            // 
            this.btnSetlists.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetlists.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnSetlists.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetlists.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlists.ForeColor = System.Drawing.Color.White;
            this.btnSetlists.Location = new System.Drawing.Point(101, 3);
            this.btnSetlists.Name = "btnSetlists";
            this.btnSetlists.Size = new System.Drawing.Size(92, 50);
            this.btnSetlists.TabIndex = 1;
            this.btnSetlists.Text = "SetL";
            this.btnSetlists.UseVisualStyleBackColor = false;
            this.btnSetlists.Click += new System.EventHandler(this.btnSetlists_Click);
            // 
            // btnNextSong
            // 
            this.btnNextSong.BackColor = System.Drawing.Color.DimGray;
            this.btnNextSong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNextSong.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnNextSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextSong.Font = new System.Drawing.Font("Wingdings", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnNextSong.ForeColor = System.Drawing.Color.White;
            this.btnNextSong.Location = new System.Drawing.Point(395, 3);
            this.btnNextSong.Name = "btnNextSong";
            this.btnNextSong.Size = new System.Drawing.Size(92, 50);
            this.btnNextSong.TabIndex = 2;
            this.btnNextSong.Text = "ê";
            this.btnNextSong.UseVisualStyleBackColor = false;
            this.btnNextSong.Click += new System.EventHandler(this.btnNextSong_Click);
            // 
            // btnSetlistSongOrderToggle
            // 
            this.btnSetlistSongOrderToggle.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistSongOrderToggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetlistSongOrderToggle.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnSetlistSongOrderToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetlistSongOrderToggle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistSongOrderToggle.ForeColor = System.Drawing.Color.White;
            this.btnSetlistSongOrderToggle.Location = new System.Drawing.Point(199, 3);
            this.btnSetlistSongOrderToggle.Name = "btnSetlistSongOrderToggle";
            this.btnSetlistSongOrderToggle.Size = new System.Drawing.Size(92, 50);
            this.btnSetlistSongOrderToggle.TabIndex = 4;
            this.btnSetlistSongOrderToggle.Text = "Alpha";
            this.btnSetlistSongOrderToggle.UseVisualStyleBackColor = false;
            this.btnSetlistSongOrderToggle.Click += new System.EventHandler(this.btnSetlistSongOrderToggle_Click);
            // 
            // btnPrevSong
            // 
            this.btnPrevSong.BackColor = System.Drawing.Color.DimGray;
            this.btnPrevSong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrevSong.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnPrevSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevSong.Font = new System.Drawing.Font("Wingdings", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnPrevSong.ForeColor = System.Drawing.Color.White;
            this.btnPrevSong.Location = new System.Drawing.Point(3, 3);
            this.btnPrevSong.Name = "btnPrevSong";
            this.btnPrevSong.Size = new System.Drawing.Size(92, 50);
            this.btnPrevSong.TabIndex = 5;
            this.btnPrevSong.Text = "é";
            this.btnPrevSong.UseVisualStyleBackColor = false;
            this.btnPrevSong.Click += new System.EventHandler(this.btnPrevSong_Click);
            // 
            // flpAlphaButtons
            // 
            this.flpAlphaButtons.Controls.Add(this.btnAlpha0);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaA);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaB);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaC);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaD);
            this.flpAlphaButtons.Controls.Add(this.btnAlpheE);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaF);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaG);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaH);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaI);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaJ);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaK);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaL);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaM);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaN);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaO);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaP);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaQ);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaR);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaS);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaT);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaU);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaV);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaW);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaX);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaY);
            this.flpAlphaButtons.Controls.Add(this.btnAlphaZ);
            this.flpAlphaButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAlphaButtons.Location = new System.Drawing.Point(492, 2);
            this.flpAlphaButtons.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flpAlphaButtons.Name = "flpAlphaButtons";
            this.tlpSongSetlistOuter.SetRowSpan(this.flpAlphaButtons, 2);
            this.flpAlphaButtons.Size = new System.Drawing.Size(45, 290);
            this.flpAlphaButtons.TabIndex = 7;
            this.flpAlphaButtons.Resize += new System.EventHandler(this.flpAlphaButtons_Resize);
            // 
            // btnAlpha0
            // 
            this.btnAlpha0.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlpha0.Location = new System.Drawing.Point(0, 0);
            this.btnAlpha0.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlpha0.Name = "btnAlpha0";
            this.btnAlpha0.Size = new System.Drawing.Size(18, 8);
            this.btnAlpha0.TabIndex = 1;
            this.btnAlpha0.Text = "0";
            this.btnAlpha0.UseVisualStyleBackColor = true;
            this.btnAlpha0.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaA
            // 
            this.btnAlphaA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaA.Location = new System.Drawing.Point(18, 0);
            this.btnAlphaA.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaA.Name = "btnAlphaA";
            this.btnAlphaA.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaA.TabIndex = 2;
            this.btnAlphaA.Text = "A";
            this.btnAlphaA.UseVisualStyleBackColor = true;
            this.btnAlphaA.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaB
            // 
            this.btnAlphaB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaB.Location = new System.Drawing.Point(0, 8);
            this.btnAlphaB.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaB.Name = "btnAlphaB";
            this.btnAlphaB.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaB.TabIndex = 3;
            this.btnAlphaB.Text = "B";
            this.btnAlphaB.UseVisualStyleBackColor = true;
            this.btnAlphaB.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaC
            // 
            this.btnAlphaC.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaC.Location = new System.Drawing.Point(18, 8);
            this.btnAlphaC.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaC.Name = "btnAlphaC";
            this.btnAlphaC.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaC.TabIndex = 4;
            this.btnAlphaC.Text = "C";
            this.btnAlphaC.UseVisualStyleBackColor = true;
            this.btnAlphaC.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaD
            // 
            this.btnAlphaD.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaD.Location = new System.Drawing.Point(0, 16);
            this.btnAlphaD.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaD.Name = "btnAlphaD";
            this.btnAlphaD.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaD.TabIndex = 5;
            this.btnAlphaD.Text = "D";
            this.btnAlphaD.UseVisualStyleBackColor = true;
            this.btnAlphaD.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlpheE
            // 
            this.btnAlpheE.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlpheE.Location = new System.Drawing.Point(18, 16);
            this.btnAlpheE.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlpheE.Name = "btnAlpheE";
            this.btnAlpheE.Size = new System.Drawing.Size(18, 8);
            this.btnAlpheE.TabIndex = 6;
            this.btnAlpheE.Text = "E";
            this.btnAlpheE.UseVisualStyleBackColor = true;
            this.btnAlpheE.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaF
            // 
            this.btnAlphaF.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaF.Location = new System.Drawing.Point(0, 24);
            this.btnAlphaF.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaF.Name = "btnAlphaF";
            this.btnAlphaF.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaF.TabIndex = 7;
            this.btnAlphaF.Text = "F";
            this.btnAlphaF.UseVisualStyleBackColor = true;
            this.btnAlphaF.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaG
            // 
            this.btnAlphaG.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaG.Location = new System.Drawing.Point(18, 24);
            this.btnAlphaG.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaG.Name = "btnAlphaG";
            this.btnAlphaG.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaG.TabIndex = 8;
            this.btnAlphaG.Text = "G";
            this.btnAlphaG.UseVisualStyleBackColor = true;
            this.btnAlphaG.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaH
            // 
            this.btnAlphaH.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaH.Location = new System.Drawing.Point(0, 32);
            this.btnAlphaH.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaH.Name = "btnAlphaH";
            this.btnAlphaH.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaH.TabIndex = 9;
            this.btnAlphaH.Text = "H";
            this.btnAlphaH.UseVisualStyleBackColor = true;
            this.btnAlphaH.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaI
            // 
            this.btnAlphaI.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaI.Location = new System.Drawing.Point(18, 32);
            this.btnAlphaI.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaI.Name = "btnAlphaI";
            this.btnAlphaI.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaI.TabIndex = 10;
            this.btnAlphaI.Text = "I";
            this.btnAlphaI.UseVisualStyleBackColor = true;
            this.btnAlphaI.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaJ
            // 
            this.btnAlphaJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaJ.Location = new System.Drawing.Point(0, 40);
            this.btnAlphaJ.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaJ.Name = "btnAlphaJ";
            this.btnAlphaJ.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaJ.TabIndex = 11;
            this.btnAlphaJ.Text = "J";
            this.btnAlphaJ.UseVisualStyleBackColor = true;
            this.btnAlphaJ.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaK
            // 
            this.btnAlphaK.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaK.Location = new System.Drawing.Point(18, 40);
            this.btnAlphaK.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaK.Name = "btnAlphaK";
            this.btnAlphaK.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaK.TabIndex = 12;
            this.btnAlphaK.Text = "K";
            this.btnAlphaK.UseVisualStyleBackColor = true;
            this.btnAlphaK.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaL
            // 
            this.btnAlphaL.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaL.Location = new System.Drawing.Point(0, 48);
            this.btnAlphaL.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaL.Name = "btnAlphaL";
            this.btnAlphaL.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaL.TabIndex = 13;
            this.btnAlphaL.Text = "L";
            this.btnAlphaL.UseVisualStyleBackColor = true;
            this.btnAlphaL.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaM
            // 
            this.btnAlphaM.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaM.Location = new System.Drawing.Point(18, 48);
            this.btnAlphaM.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaM.Name = "btnAlphaM";
            this.btnAlphaM.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaM.TabIndex = 14;
            this.btnAlphaM.Text = "M";
            this.btnAlphaM.UseVisualStyleBackColor = true;
            this.btnAlphaM.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaN
            // 
            this.btnAlphaN.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaN.Location = new System.Drawing.Point(0, 56);
            this.btnAlphaN.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaN.Name = "btnAlphaN";
            this.btnAlphaN.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaN.TabIndex = 15;
            this.btnAlphaN.Text = "N";
            this.btnAlphaN.UseVisualStyleBackColor = true;
            this.btnAlphaN.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaO
            // 
            this.btnAlphaO.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaO.Location = new System.Drawing.Point(18, 56);
            this.btnAlphaO.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaO.Name = "btnAlphaO";
            this.btnAlphaO.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaO.TabIndex = 16;
            this.btnAlphaO.Text = "O";
            this.btnAlphaO.UseVisualStyleBackColor = true;
            this.btnAlphaO.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaP
            // 
            this.btnAlphaP.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaP.Location = new System.Drawing.Point(0, 64);
            this.btnAlphaP.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaP.Name = "btnAlphaP";
            this.btnAlphaP.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaP.TabIndex = 17;
            this.btnAlphaP.Text = "P";
            this.btnAlphaP.UseVisualStyleBackColor = true;
            this.btnAlphaP.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaQ
            // 
            this.btnAlphaQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaQ.Location = new System.Drawing.Point(18, 64);
            this.btnAlphaQ.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaQ.Name = "btnAlphaQ";
            this.btnAlphaQ.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaQ.TabIndex = 18;
            this.btnAlphaQ.Text = "Q";
            this.btnAlphaQ.UseVisualStyleBackColor = true;
            this.btnAlphaQ.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaR
            // 
            this.btnAlphaR.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaR.Location = new System.Drawing.Point(0, 72);
            this.btnAlphaR.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaR.Name = "btnAlphaR";
            this.btnAlphaR.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaR.TabIndex = 19;
            this.btnAlphaR.Text = "R";
            this.btnAlphaR.UseVisualStyleBackColor = true;
            this.btnAlphaR.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaS
            // 
            this.btnAlphaS.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaS.Location = new System.Drawing.Point(18, 72);
            this.btnAlphaS.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaS.Name = "btnAlphaS";
            this.btnAlphaS.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaS.TabIndex = 20;
            this.btnAlphaS.Text = "S";
            this.btnAlphaS.UseVisualStyleBackColor = true;
            this.btnAlphaS.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaT
            // 
            this.btnAlphaT.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaT.Location = new System.Drawing.Point(0, 80);
            this.btnAlphaT.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaT.Name = "btnAlphaT";
            this.btnAlphaT.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaT.TabIndex = 21;
            this.btnAlphaT.Text = "T";
            this.btnAlphaT.UseVisualStyleBackColor = true;
            this.btnAlphaT.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaU
            // 
            this.btnAlphaU.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaU.Location = new System.Drawing.Point(18, 80);
            this.btnAlphaU.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaU.Name = "btnAlphaU";
            this.btnAlphaU.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaU.TabIndex = 22;
            this.btnAlphaU.Text = "U";
            this.btnAlphaU.UseVisualStyleBackColor = true;
            this.btnAlphaU.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaV
            // 
            this.btnAlphaV.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaV.Location = new System.Drawing.Point(0, 88);
            this.btnAlphaV.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaV.Name = "btnAlphaV";
            this.btnAlphaV.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaV.TabIndex = 23;
            this.btnAlphaV.Text = "V";
            this.btnAlphaV.UseVisualStyleBackColor = true;
            this.btnAlphaV.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaW
            // 
            this.btnAlphaW.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaW.Location = new System.Drawing.Point(18, 88);
            this.btnAlphaW.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaW.Name = "btnAlphaW";
            this.btnAlphaW.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaW.TabIndex = 24;
            this.btnAlphaW.Text = "W";
            this.btnAlphaW.UseVisualStyleBackColor = true;
            this.btnAlphaW.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaX
            // 
            this.btnAlphaX.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaX.Location = new System.Drawing.Point(0, 96);
            this.btnAlphaX.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaX.Name = "btnAlphaX";
            this.btnAlphaX.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaX.TabIndex = 25;
            this.btnAlphaX.Text = "X";
            this.btnAlphaX.UseVisualStyleBackColor = true;
            this.btnAlphaX.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaY
            // 
            this.btnAlphaY.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaY.Location = new System.Drawing.Point(18, 96);
            this.btnAlphaY.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaY.Name = "btnAlphaY";
            this.btnAlphaY.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaY.TabIndex = 26;
            this.btnAlphaY.Text = "Y";
            this.btnAlphaY.UseVisualStyleBackColor = true;
            this.btnAlphaY.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // btnAlphaZ
            // 
            this.btnAlphaZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaZ.Location = new System.Drawing.Point(0, 104);
            this.btnAlphaZ.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlphaZ.Name = "btnAlphaZ";
            this.btnAlphaZ.Size = new System.Drawing.Size(18, 8);
            this.btnAlphaZ.TabIndex = 27;
            this.btnAlphaZ.Text = "Z";
            this.btnAlphaZ.UseVisualStyleBackColor = true;
            this.btnAlphaZ.Click += new System.EventHandler(this.btnAlpha_Click_1);
            // 
            // mbccShowSongPatches
            // 
            this.mbccShowSongPatches.ButtonBackColor = System.Drawing.SystemColors.Control;
            this.mbccShowSongPatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbccShowSongPatches.Location = new System.Drawing.Point(595, 319);
            this.mbccShowSongPatches.Margin = new System.Windows.Forms.Padding(0);
            this.mbccShowSongPatches.Name = "mbccShowSongPatches";
            this.mbccShowSongPatches.ShowAlphaButtons = false;
            this.mbccShowSongPatches.ShowArrowButtons = true;
            this.mbccShowSongPatches.ShowLogicalButtonNumberBadge = false;
            this.mbccShowSongPatches.ShowScrollbar = false;
            this.mbccShowSongPatches.Size = new System.Drawing.Size(546, 300);
            this.mbccShowSongPatches.TabIndex = 10;
            this.mbccShowSongPatches.Click += new System.EventHandler(this.mbccShowSongPatches_Click);
            // 
            // vsbVol2
            // 
            this.vsbVol2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vsbVol2.Location = new System.Drawing.Point(0, 0);
            this.vsbVol2.Maximum = 127;
            this.vsbVol2.Name = "vsbVol2";
            this.tlpShowOuter.SetRowSpan(this.vsbVol2, 2);
            this.vsbVol2.Size = new System.Drawing.Size(50, 619);
            this.vsbVol2.TabIndex = 7;
            this.vsbVol2.Value = 27;
            // 
            // tpSongs
            // 
            this.tpSongs.BackColor = System.Drawing.Color.Black;
            this.tpSongs.Controls.Add(this.pnlPatchEdit);
            this.tpSongs.Controls.Add(this.pnlSongEdit);
            this.tpSongs.Controls.Add(this.tlpSongSelOuter);
            this.tpSongs.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpSongs.Location = new System.Drawing.Point(4, 44);
            this.tpSongs.Name = "tpSongs";
            this.tpSongs.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpSongs.Size = new System.Drawing.Size(1147, 625);
            this.tpSongs.TabIndex = 2;
            this.tpSongs.Text = "Songs";
            // 
            // pnlPatchEdit
            // 
            this.pnlPatchEdit.Controls.Add(this.tvSongPatchPatches);
            this.pnlPatchEdit.Controls.Add(this.lblSongPatchPart);
            this.pnlPatchEdit.Controls.Add(this.nudSongPatchProgramNo);
            this.pnlPatchEdit.Controls.Add(this.lblSongPatchProgramNo);
            this.pnlPatchEdit.Controls.Add(this.nudSongPatchBank);
            this.pnlPatchEdit.Controls.Add(this.tbSongPatchPart);
            this.pnlPatchEdit.Controls.Add(this.lblSongPatchBank);
            this.pnlPatchEdit.Controls.Add(this.btnPatchEditCancel);
            this.pnlPatchEdit.Controls.Add(this.btnPatchEditOK);
            this.pnlPatchEdit.Location = new System.Drawing.Point(882, 0);
            this.pnlPatchEdit.Name = "pnlPatchEdit";
            this.pnlPatchEdit.Size = new System.Drawing.Size(447, 479);
            this.pnlPatchEdit.TabIndex = 2;
            this.pnlPatchEdit.Visible = false;
            // 
            // tvSongPatchPatches
            // 
            this.tvSongPatchPatches.BackColor = System.Drawing.Color.DimGray;
            this.tvSongPatchPatches.ForeColor = System.Drawing.Color.White;
            this.tvSongPatchPatches.HideSelection = false;
            this.tvSongPatchPatches.Location = new System.Drawing.Point(99, 121);
            this.tvSongPatchPatches.Name = "tvSongPatchPatches";
            this.tvSongPatchPatches.Size = new System.Drawing.Size(317, 283);
            this.tvSongPatchPatches.TabIndex = 27;
            this.tvSongPatchPatches.DoubleClick += new System.EventHandler(this.tvSongPatchPatches_DoubleClick);
            // 
            // lblSongPatchPart
            // 
            this.lblSongPatchPart.AutoSize = true;
            this.lblSongPatchPart.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongPatchPart.ForeColor = System.Drawing.Color.White;
            this.lblSongPatchPart.Location = new System.Drawing.Point(52, 48);
            this.lblSongPatchPart.Name = "lblSongPatchPart";
            this.lblSongPatchPart.Size = new System.Drawing.Size(32, 15);
            this.lblSongPatchPart.TabIndex = 21;
            this.lblSongPatchPart.Text = "Part:";
            // 
            // nudSongPatchProgramNo
            // 
            this.nudSongPatchProgramNo.BackColor = System.Drawing.Color.DimGray;
            this.nudSongPatchProgramNo.ForeColor = System.Drawing.Color.White;
            this.nudSongPatchProgramNo.Location = new System.Drawing.Point(328, 80);
            this.nudSongPatchProgramNo.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudSongPatchProgramNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudSongPatchProgramNo.Name = "nudSongPatchProgramNo";
            this.nudSongPatchProgramNo.Size = new System.Drawing.Size(87, 32);
            this.nudSongPatchProgramNo.TabIndex = 26;
            this.nudSongPatchProgramNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // lblSongPatchProgramNo
            // 
            this.lblSongPatchProgramNo.AutoSize = true;
            this.lblSongPatchProgramNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongPatchProgramNo.ForeColor = System.Drawing.Color.White;
            this.lblSongPatchProgramNo.Location = new System.Drawing.Point(258, 87);
            this.lblSongPatchProgramNo.Name = "lblSongPatchProgramNo";
            this.lblSongPatchProgramNo.Size = new System.Drawing.Size(68, 15);
            this.lblSongPatchProgramNo.TabIndex = 25;
            this.lblSongPatchProgramNo.Text = "Program #:";
            // 
            // nudSongPatchBank
            // 
            this.nudSongPatchBank.BackColor = System.Drawing.Color.DimGray;
            this.nudSongPatchBank.ForeColor = System.Drawing.Color.White;
            this.nudSongPatchBank.Location = new System.Drawing.Point(99, 81);
            this.nudSongPatchBank.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudSongPatchBank.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudSongPatchBank.Name = "nudSongPatchBank";
            this.nudSongPatchBank.Size = new System.Drawing.Size(87, 32);
            this.nudSongPatchBank.TabIndex = 24;
            this.nudSongPatchBank.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // tbSongPatchPart
            // 
            this.tbSongPatchPart.BackColor = System.Drawing.Color.DimGray;
            this.tbSongPatchPart.ForeColor = System.Drawing.Color.White;
            this.tbSongPatchPart.Location = new System.Drawing.Point(99, 41);
            this.tbSongPatchPart.Name = "tbSongPatchPart";
            this.tbSongPatchPart.Size = new System.Drawing.Size(317, 32);
            this.tbSongPatchPart.TabIndex = 22;
            // 
            // lblSongPatchBank
            // 
            this.lblSongPatchBank.AutoSize = true;
            this.lblSongPatchBank.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongPatchBank.ForeColor = System.Drawing.Color.White;
            this.lblSongPatchBank.Location = new System.Drawing.Point(48, 89);
            this.lblSongPatchBank.Name = "lblSongPatchBank";
            this.lblSongPatchBank.Size = new System.Drawing.Size(38, 15);
            this.lblSongPatchBank.TabIndex = 23;
            this.lblSongPatchBank.Text = "Bank:";
            // 
            // btnPatchEditCancel
            // 
            this.btnPatchEditCancel.BackColor = System.Drawing.Color.DimGray;
            this.btnPatchEditCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPatchEditCancel.ForeColor = System.Drawing.Color.White;
            this.btnPatchEditCancel.Location = new System.Drawing.Point(99, 415);
            this.btnPatchEditCancel.Name = "btnPatchEditCancel";
            this.btnPatchEditCancel.Size = new System.Drawing.Size(145, 54);
            this.btnPatchEditCancel.TabIndex = 26;
            this.btnPatchEditCancel.Text = "Cancel";
            this.btnPatchEditCancel.UseVisualStyleBackColor = false;
            this.btnPatchEditCancel.Click += new System.EventHandler(this.btnPatchEditCancel_Click);
            // 
            // btnPatchEditOK
            // 
            this.btnPatchEditOK.BackColor = System.Drawing.Color.DimGray;
            this.btnPatchEditOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPatchEditOK.ForeColor = System.Drawing.Color.White;
            this.btnPatchEditOK.Location = new System.Drawing.Point(270, 415);
            this.btnPatchEditOK.Name = "btnPatchEditOK";
            this.btnPatchEditOK.Size = new System.Drawing.Size(145, 54);
            this.btnPatchEditOK.TabIndex = 27;
            this.btnPatchEditOK.Text = "OK";
            this.btnPatchEditOK.UseVisualStyleBackColor = false;
            this.btnPatchEditOK.Click += new System.EventHandler(this.btnPatchEditOK_Click);
            // 
            // pnlSongEdit
            // 
            this.pnlSongEdit.Controls.Add(this.nudBPM);
            this.pnlSongEdit.Controls.Add(this.lblBpm);
            this.pnlSongEdit.Controls.Add(this.btnOpenFilesCharts);
            this.pnlSongEdit.Controls.Add(this.nudSongChartPage);
            this.pnlSongEdit.Controls.Add(this.label21);
            this.pnlSongEdit.Controls.Add(this.nudSongTranspose);
            this.pnlSongEdit.Controls.Add(this.label16);
            this.pnlSongEdit.Controls.Add(this.btnPatchDel);
            this.pnlSongEdit.Controls.Add(this.btnPatchAdd);
            this.pnlSongEdit.Controls.Add(this.btnPatchDown);
            this.pnlSongEdit.Controls.Add(this.btnPatchUp);
            this.pnlSongEdit.Controls.Add(this.btnSongEditCancel);
            this.pnlSongEdit.Controls.Add(this.btnSongEditOK);
            this.pnlSongEdit.Controls.Add(this.lblSongPatches);
            this.pnlSongEdit.Controls.Add(this.lbSongPatches);
            this.pnlSongEdit.Controls.Add(this.tbSongChart);
            this.pnlSongEdit.Controls.Add(this.lblSongChart);
            this.pnlSongEdit.Controls.Add(this.tbSongArtist);
            this.pnlSongEdit.Controls.Add(this.label1);
            this.pnlSongEdit.Controls.Add(this.tbSongTitle);
            this.pnlSongEdit.Controls.Add(this.lblSongTitle);
            this.pnlSongEdit.Location = new System.Drawing.Point(414, 3);
            this.pnlSongEdit.Name = "pnlSongEdit";
            this.pnlSongEdit.Size = new System.Drawing.Size(447, 476);
            this.pnlSongEdit.TabIndex = 1;
            this.pnlSongEdit.Visible = false;
            // 
            // nudBPM
            // 
            this.nudBPM.Location = new System.Drawing.Point(212, 150);
            this.nudBPM.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudBPM.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudBPM.Name = "nudBPM";
            this.nudBPM.Size = new System.Drawing.Size(74, 32);
            this.nudBPM.TabIndex = 13;
            // 
            // lblBpm
            // 
            this.lblBpm.AutoSize = true;
            this.lblBpm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBpm.ForeColor = System.Drawing.Color.White;
            this.lblBpm.Location = new System.Drawing.Point(174, 159);
            this.lblBpm.Name = "lblBpm";
            this.lblBpm.Size = new System.Drawing.Size(35, 15);
            this.lblBpm.TabIndex = 12;
            this.lblBpm.Text = "BPM:";
            this.lblBpm.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOpenFilesCharts
            // 
            this.btnOpenFilesCharts.BackColor = System.Drawing.Color.DimGray;
            this.btnOpenFilesCharts.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFilesCharts.ForeColor = System.Drawing.Color.White;
            this.btnOpenFilesCharts.Location = new System.Drawing.Point(372, 111);
            this.btnOpenFilesCharts.Name = "btnOpenFilesCharts";
            this.btnOpenFilesCharts.Size = new System.Drawing.Size(44, 30);
            this.btnOpenFilesCharts.TabIndex = 9;
            this.btnOpenFilesCharts.Text = "Files";
            this.btnOpenFilesCharts.UseVisualStyleBackColor = false;
            this.btnOpenFilesCharts.Click += new System.EventHandler(this.btnOpenFilesCharts_Click);
            // 
            // nudSongChartPage
            // 
            this.nudSongChartPage.Location = new System.Drawing.Point(100, 150);
            this.nudSongChartPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudSongChartPage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSongChartPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSongChartPage.Name = "nudSongChartPage";
            this.nudSongChartPage.Size = new System.Drawing.Size(44, 32);
            this.nudSongChartPage.TabIndex = 11;
            this.nudSongChartPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(20, 159);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 15);
            this.label21.TabIndex = 10;
            this.label21.Text = "Chart Page:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // nudSongTranspose
            // 
            this.nudSongTranspose.Location = new System.Drawing.Point(347, 150);
            this.nudSongTranspose.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudSongTranspose.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.nudSongTranspose.Name = "nudSongTranspose";
            this.nudSongTranspose.Size = new System.Drawing.Size(69, 32);
            this.nudSongTranspose.TabIndex = 15;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(303, 159);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 15);
            this.label16.TabIndex = 14;
            this.label16.Text = "Xpose:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnPatchDel
            // 
            this.btnPatchDel.BackColor = System.Drawing.Color.DimGray;
            this.btnPatchDel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatchDel.ForeColor = System.Drawing.Color.White;
            this.btnPatchDel.Location = new System.Drawing.Point(372, 299);
            this.btnPatchDel.Name = "btnPatchDel";
            this.btnPatchDel.Size = new System.Drawing.Size(44, 39);
            this.btnPatchDel.TabIndex = 19;
            this.btnPatchDel.Text = "-";
            this.btnPatchDel.UseVisualStyleBackColor = false;
            this.btnPatchDel.Click += new System.EventHandler(this.btnPatchDel_Click);
            // 
            // btnPatchAdd
            // 
            this.btnPatchAdd.BackColor = System.Drawing.Color.DimGray;
            this.btnPatchAdd.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatchAdd.ForeColor = System.Drawing.Color.White;
            this.btnPatchAdd.Location = new System.Drawing.Point(372, 254);
            this.btnPatchAdd.Name = "btnPatchAdd";
            this.btnPatchAdd.Size = new System.Drawing.Size(44, 39);
            this.btnPatchAdd.TabIndex = 18;
            this.btnPatchAdd.Text = "+";
            this.btnPatchAdd.UseVisualStyleBackColor = false;
            this.btnPatchAdd.Click += new System.EventHandler(this.BtnPatchAdd_Click);
            // 
            // btnPatchDown
            // 
            this.btnPatchDown.BackColor = System.Drawing.Color.DimGray;
            this.btnPatchDown.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnPatchDown.ForeColor = System.Drawing.Color.White;
            this.btnPatchDown.Location = new System.Drawing.Point(372, 346);
            this.btnPatchDown.Name = "btnPatchDown";
            this.btnPatchDown.Size = new System.Drawing.Size(44, 61);
            this.btnPatchDown.TabIndex = 20;
            this.btnPatchDown.Text = "";
            this.btnPatchDown.UseVisualStyleBackColor = false;
            this.btnPatchDown.Click += new System.EventHandler(this.btnPatchDown_Click);
            // 
            // btnPatchUp
            // 
            this.btnPatchUp.BackColor = System.Drawing.Color.DimGray;
            this.btnPatchUp.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnPatchUp.ForeColor = System.Drawing.Color.White;
            this.btnPatchUp.Location = new System.Drawing.Point(372, 187);
            this.btnPatchUp.Name = "btnPatchUp";
            this.btnPatchUp.Size = new System.Drawing.Size(44, 61);
            this.btnPatchUp.TabIndex = 17;
            this.btnPatchUp.Text = "";
            this.btnPatchUp.UseVisualStyleBackColor = false;
            this.btnPatchUp.Click += new System.EventHandler(this.btnPatchUp_Click);
            // 
            // btnSongEditCancel
            // 
            this.btnSongEditCancel.BackColor = System.Drawing.Color.DimGray;
            this.btnSongEditCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSongEditCancel.ForeColor = System.Drawing.Color.White;
            this.btnSongEditCancel.Location = new System.Drawing.Point(99, 415);
            this.btnSongEditCancel.Name = "btnSongEditCancel";
            this.btnSongEditCancel.Size = new System.Drawing.Size(145, 54);
            this.btnSongEditCancel.TabIndex = 17;
            this.btnSongEditCancel.Text = "Cancel";
            this.btnSongEditCancel.UseVisualStyleBackColor = false;
            this.btnSongEditCancel.Click += new System.EventHandler(this.btnSongEditCancel_Click);
            // 
            // btnSongEditOK
            // 
            this.btnSongEditOK.BackColor = System.Drawing.Color.DimGray;
            this.btnSongEditOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSongEditOK.ForeColor = System.Drawing.Color.White;
            this.btnSongEditOK.Location = new System.Drawing.Point(271, 415);
            this.btnSongEditOK.Name = "btnSongEditOK";
            this.btnSongEditOK.Size = new System.Drawing.Size(145, 54);
            this.btnSongEditOK.TabIndex = 18;
            this.btnSongEditOK.Text = "OK";
            this.btnSongEditOK.UseVisualStyleBackColor = false;
            this.btnSongEditOK.Click += new System.EventHandler(this.btnSongEditOK_Click);
            // 
            // lblSongPatches
            // 
            this.lblSongPatches.AutoSize = true;
            this.lblSongPatches.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongPatches.ForeColor = System.Drawing.Color.White;
            this.lblSongPatches.Location = new System.Drawing.Point(35, 204);
            this.lblSongPatches.Name = "lblSongPatches";
            this.lblSongPatches.Size = new System.Drawing.Size(55, 15);
            this.lblSongPatches.TabIndex = 11;
            this.lblSongPatches.Text = "Patches:";
            this.lblSongPatches.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbSongPatches
            // 
            this.lbSongPatches.BackColor = System.Drawing.Color.DimGray;
            this.lbSongPatches.ForeColor = System.Drawing.Color.White;
            this.lbSongPatches.FormattingEnabled = true;
            this.lbSongPatches.ItemHeight = 24;
            this.lbSongPatches.Location = new System.Drawing.Point(99, 195);
            this.lbSongPatches.Name = "lbSongPatches";
            this.lbSongPatches.Size = new System.Drawing.Size(267, 124);
            this.lbSongPatches.TabIndex = 16;
            this.lbSongPatches.DoubleClick += new System.EventHandler(this.lbSongPatches_DoubleClick);
            // 
            // tbSongChart
            // 
            this.tbSongChart.BackColor = System.Drawing.Color.DimGray;
            this.tbSongChart.ForeColor = System.Drawing.Color.White;
            this.tbSongChart.Location = new System.Drawing.Point(99, 111);
            this.tbSongChart.Name = "tbSongChart";
            this.tbSongChart.Size = new System.Drawing.Size(267, 32);
            this.tbSongChart.TabIndex = 8;
            // 
            // lblSongChart
            // 
            this.lblSongChart.AutoSize = true;
            this.lblSongChart.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongChart.ForeColor = System.Drawing.Color.White;
            this.lblSongChart.Location = new System.Drawing.Point(49, 113);
            this.lblSongChart.Name = "lblSongChart";
            this.lblSongChart.Size = new System.Drawing.Size(40, 15);
            this.lblSongChart.TabIndex = 7;
            this.lblSongChart.Text = "Chart:";
            this.lblSongChart.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbSongArtist
            // 
            this.tbSongArtist.BackColor = System.Drawing.Color.DimGray;
            this.tbSongArtist.ForeColor = System.Drawing.Color.White;
            this.tbSongArtist.Location = new System.Drawing.Point(99, 74);
            this.tbSongArtist.Name = "tbSongArtist";
            this.tbSongArtist.Size = new System.Drawing.Size(317, 32);
            this.tbSongArtist.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(51, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Artist:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbSongTitle
            // 
            this.tbSongTitle.BackColor = System.Drawing.Color.DimGray;
            this.tbSongTitle.ForeColor = System.Drawing.Color.White;
            this.tbSongTitle.Location = new System.Drawing.Point(99, 36);
            this.tbSongTitle.Name = "tbSongTitle";
            this.tbSongTitle.Size = new System.Drawing.Size(317, 32);
            this.tbSongTitle.TabIndex = 4;
            // 
            // lblSongTitle
            // 
            this.lblSongTitle.AutoSize = true;
            this.lblSongTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongTitle.ForeColor = System.Drawing.Color.White;
            this.lblSongTitle.Location = new System.Drawing.Point(28, 45);
            this.lblSongTitle.Name = "lblSongTitle";
            this.lblSongTitle.Size = new System.Drawing.Size(65, 15);
            this.lblSongTitle.TabIndex = 3;
            this.lblSongTitle.Text = "Song Title:";
            // 
            // tlpSongSelOuter
            // 
            this.tlpSongSelOuter.ColumnCount = 1;
            this.tlpSongSelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSongSelOuter.Controls.Add(this.mbccSongEditSelector, 0, 1);
            this.tlpSongSelOuter.Controls.Add(this.tlpSongSelButtons, 0, 0);
            this.tlpSongSelOuter.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlpSongSelOuter.Location = new System.Drawing.Point(3, 3);
            this.tlpSongSelOuter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSongSelOuter.Name = "tlpSongSelOuter";
            this.tlpSongSelOuter.RowCount = 2;
            this.tlpSongSelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpSongSelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSongSelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSongSelOuter.Size = new System.Drawing.Size(408, 619);
            this.tlpSongSelOuter.TabIndex = 0;
            // 
            // mbccSongEditSelector
            // 
            this.mbccSongEditSelector.ButtonBackColor = System.Drawing.Color.DimGray;
            this.mbccSongEditSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbccSongEditSelector.ForeColor = System.Drawing.Color.White;
            this.mbccSongEditSelector.Location = new System.Drawing.Point(4, 54);
            this.mbccSongEditSelector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbccSongEditSelector.Name = "mbccSongEditSelector";
            this.tlpSongSelOuter.SetRowSpan(this.mbccSongEditSelector, 2);
            this.mbccSongEditSelector.ShowAlphaButtons = true;
            this.mbccSongEditSelector.ShowArrowButtons = false;
            this.mbccSongEditSelector.ShowLogicalButtonNumberBadge = false;
            this.mbccSongEditSelector.ShowScrollbar = true;
            this.mbccSongEditSelector.Size = new System.Drawing.Size(400, 561);
            this.mbccSongEditSelector.TabIndex = 0;
            this.mbccSongEditSelector.Click += new System.EventHandler(this.mbccSongEditSelector_Click);
            // 
            // tlpSongSelButtons
            // 
            this.tlpSongSelButtons.ColumnCount = 2;
            this.tlpSongSelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSongSelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSongSelButtons.Controls.Add(this.btnSongDel, 0, 0);
            this.tlpSongSelButtons.Controls.Add(this.btnAddSong, 0, 0);
            this.tlpSongSelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSongSelButtons.Location = new System.Drawing.Point(0, 0);
            this.tlpSongSelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSongSelButtons.Name = "tlpSongSelButtons";
            this.tlpSongSelButtons.RowCount = 1;
            this.tlpSongSelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSongSelButtons.Size = new System.Drawing.Size(408, 50);
            this.tlpSongSelButtons.TabIndex = 1;
            // 
            // btnSongDel
            // 
            this.btnSongDel.BackColor = System.Drawing.Color.DimGray;
            this.btnSongDel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSongDel.Location = new System.Drawing.Point(207, 3);
            this.btnSongDel.Name = "btnSongDel";
            this.btnSongDel.Size = new System.Drawing.Size(198, 44);
            this.btnSongDel.TabIndex = 2;
            this.btnSongDel.Text = "-";
            this.btnSongDel.UseVisualStyleBackColor = false;
            this.btnSongDel.Click += new System.EventHandler(this.btnSongDel_Click);
            // 
            // btnAddSong
            // 
            this.btnAddSong.BackColor = System.Drawing.Color.DimGray;
            this.btnAddSong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddSong.Location = new System.Drawing.Point(3, 3);
            this.btnAddSong.Name = "btnAddSong";
            this.btnAddSong.Size = new System.Drawing.Size(198, 44);
            this.btnAddSong.TabIndex = 1;
            this.btnAddSong.Text = "+";
            this.btnAddSong.UseVisualStyleBackColor = false;
            this.btnAddSong.Click += new System.EventHandler(this.btnAddSong_Click);
            // 
            // tpSetlists
            // 
            this.tpSetlists.BackColor = System.Drawing.Color.Black;
            this.tpSetlists.Controls.Add(this.pnlSetlistSongSelector);
            this.tpSetlists.Controls.Add(this.pnlSetlistEdit);
            this.tpSetlists.Controls.Add(this.tlpSetlistSelOuter);
            this.tpSetlists.Location = new System.Drawing.Point(4, 44);
            this.tpSetlists.Name = "tpSetlists";
            this.tpSetlists.Size = new System.Drawing.Size(1147, 625);
            this.tpSetlists.TabIndex = 3;
            this.tpSetlists.Text = "Setlists";
            // 
            // pnlSetlistSongSelector
            // 
            this.pnlSetlistSongSelector.Controls.Add(this.cbSetEditorSonglistSetSelector);
            this.pnlSetlistSongSelector.Controls.Add(this.tvSongsForSetlists);
            this.pnlSetlistSongSelector.Location = new System.Drawing.Point(847, 32);
            this.pnlSetlistSongSelector.Name = "pnlSetlistSongSelector";
            this.pnlSetlistSongSelector.Size = new System.Drawing.Size(422, 459);
            this.pnlSetlistSongSelector.TabIndex = 2;
            this.pnlSetlistSongSelector.Visible = false;
            // 
            // cbSetEditorSonglistSetSelector
            // 
            this.cbSetEditorSonglistSetSelector.FormattingEnabled = true;
            this.cbSetEditorSonglistSetSelector.Location = new System.Drawing.Point(97, 34);
            this.cbSetEditorSonglistSetSelector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbSetEditorSonglistSetSelector.Name = "cbSetEditorSonglistSetSelector";
            this.cbSetEditorSonglistSetSelector.Size = new System.Drawing.Size(317, 32);
            this.cbSetEditorSonglistSetSelector.TabIndex = 14;
            this.cbSetEditorSonglistSetSelector.SelectedIndexChanged += new System.EventHandler(this.cbSetEditorSonglistSetSelector_SelectedIndexChanged);
            // 
            // tvSongsForSetlists
            // 
            this.tvSongsForSetlists.BackColor = System.Drawing.Color.DimGray;
            this.tvSongsForSetlists.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvSongsForSetlists.ForeColor = System.Drawing.Color.White;
            this.tvSongsForSetlists.HideSelection = false;
            this.tvSongsForSetlists.Location = new System.Drawing.Point(97, 79);
            this.tvSongsForSetlists.Name = "tvSongsForSetlists";
            this.tvSongsForSetlists.Size = new System.Drawing.Size(317, 367);
            this.tvSongsForSetlists.TabIndex = 13;
            this.tvSongsForSetlists.DoubleClick += new System.EventHandler(this.tvSongsForSetlists_DoubleClick);
            // 
            // pnlSetlistEdit
            // 
            this.pnlSetlistEdit.Controls.Add(this.btnSetlistSort);
            this.pnlSetlistEdit.Controls.Add(this.btnSetlistDeleteSong);
            this.pnlSetlistEdit.Controls.Add(this.btnSetlistAddSong);
            this.pnlSetlistEdit.Controls.Add(this.btnSetlistSongDown);
            this.pnlSetlistEdit.Controls.Add(this.btnSetlistSongUp);
            this.pnlSetlistEdit.Controls.Add(this.btnSetlistEditCancel);
            this.pnlSetlistEdit.Controls.Add(this.btnSetlistEditOK);
            this.pnlSetlistEdit.Controls.Add(this.label5);
            this.pnlSetlistEdit.Controls.Add(this.lbSetlistSongs);
            this.pnlSetlistEdit.Controls.Add(this.tbSetlistName);
            this.pnlSetlistEdit.Controls.Add(this.label4);
            this.pnlSetlistEdit.Location = new System.Drawing.Point(415, 32);
            this.pnlSetlistEdit.Name = "pnlSetlistEdit";
            this.pnlSetlistEdit.Size = new System.Drawing.Size(427, 459);
            this.pnlSetlistEdit.TabIndex = 1;
            this.pnlSetlistEdit.Visible = false;
            // 
            // btnSetlistSort
            // 
            this.btnSetlistSort.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistSort.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistSort.ForeColor = System.Drawing.Color.White;
            this.btnSetlistSort.Location = new System.Drawing.Point(202, 318);
            this.btnSetlistSort.Name = "btnSetlistSort";
            this.btnSetlistSort.Size = new System.Drawing.Size(112, 54);
            this.btnSetlistSort.TabIndex = 13;
            this.btnSetlistSort.Text = "Sort";
            this.btnSetlistSort.UseVisualStyleBackColor = false;
            this.btnSetlistSort.Click += new System.EventHandler(this.btnSetlistSort_Click);
            // 
            // btnSetlistDeleteSong
            // 
            this.btnSetlistDeleteSong.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistDeleteSong.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistDeleteSong.ForeColor = System.Drawing.Color.White;
            this.btnSetlistDeleteSong.Location = new System.Drawing.Point(370, 227);
            this.btnSetlistDeleteSong.Name = "btnSetlistDeleteSong";
            this.btnSetlistDeleteSong.Size = new System.Drawing.Size(44, 62);
            this.btnSetlistDeleteSong.TabIndex = 9;
            this.btnSetlistDeleteSong.Text = ">";
            this.btnSetlistDeleteSong.UseVisualStyleBackColor = false;
            this.btnSetlistDeleteSong.Click += new System.EventHandler(this.btnSetlistDeleteSong_Click);
            // 
            // btnSetlistAddSong
            // 
            this.btnSetlistAddSong.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistAddSong.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistAddSong.ForeColor = System.Drawing.Color.White;
            this.btnSetlistAddSong.Location = new System.Drawing.Point(370, 161);
            this.btnSetlistAddSong.Name = "btnSetlistAddSong";
            this.btnSetlistAddSong.Size = new System.Drawing.Size(44, 60);
            this.btnSetlistAddSong.TabIndex = 8;
            this.btnSetlistAddSong.Text = "<";
            this.btnSetlistAddSong.UseVisualStyleBackColor = false;
            this.btnSetlistAddSong.Click += new System.EventHandler(this.btnSetlistAddSong_Click);
            // 
            // btnSetlistSongDown
            // 
            this.btnSetlistSongDown.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistSongDown.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSetlistSongDown.ForeColor = System.Drawing.Color.White;
            this.btnSetlistSongDown.Location = new System.Drawing.Point(370, 310);
            this.btnSetlistSongDown.Name = "btnSetlistSongDown";
            this.btnSetlistSongDown.Size = new System.Drawing.Size(44, 61);
            this.btnSetlistSongDown.TabIndex = 10;
            this.btnSetlistSongDown.Text = "";
            this.btnSetlistSongDown.UseVisualStyleBackColor = false;
            this.btnSetlistSongDown.Click += new System.EventHandler(this.btnSetlistSongDown_Click);
            // 
            // btnSetlistSongUp
            // 
            this.btnSetlistSongUp.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistSongUp.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSetlistSongUp.ForeColor = System.Drawing.Color.White;
            this.btnSetlistSongUp.Location = new System.Drawing.Point(370, 79);
            this.btnSetlistSongUp.Name = "btnSetlistSongUp";
            this.btnSetlistSongUp.Size = new System.Drawing.Size(44, 61);
            this.btnSetlistSongUp.TabIndex = 7;
            this.btnSetlistSongUp.Text = "";
            this.btnSetlistSongUp.UseVisualStyleBackColor = false;
            this.btnSetlistSongUp.Click += new System.EventHandler(this.btnSetlistSongUp_Click);
            // 
            // btnSetlistEditCancel
            // 
            this.btnSetlistEditCancel.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistEditCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSetlistEditCancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistEditCancel.ForeColor = System.Drawing.Color.White;
            this.btnSetlistEditCancel.Location = new System.Drawing.Point(97, 392);
            this.btnSetlistEditCancel.Name = "btnSetlistEditCancel";
            this.btnSetlistEditCancel.Size = new System.Drawing.Size(156, 54);
            this.btnSetlistEditCancel.TabIndex = 11;
            this.btnSetlistEditCancel.Text = "Cancel";
            this.btnSetlistEditCancel.UseVisualStyleBackColor = false;
            this.btnSetlistEditCancel.Click += new System.EventHandler(this.btnSetlistEditCancel_Click);
            // 
            // btnSetlistEditOK
            // 
            this.btnSetlistEditOK.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistEditOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSetlistEditOK.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistEditOK.ForeColor = System.Drawing.Color.White;
            this.btnSetlistEditOK.Location = new System.Drawing.Point(269, 392);
            this.btnSetlistEditOK.Name = "btnSetlistEditOK";
            this.btnSetlistEditOK.Size = new System.Drawing.Size(145, 54);
            this.btnSetlistEditOK.TabIndex = 12;
            this.btnSetlistEditOK.Text = "OK";
            this.btnSetlistEditOK.UseVisualStyleBackColor = false;
            this.btnSetlistEditOK.Click += new System.EventHandler(this.btnSetlistEditOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(46, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Songs:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbSetlistSongs
            // 
            this.lbSetlistSongs.BackColor = System.Drawing.Color.DimGray;
            this.lbSetlistSongs.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSetlistSongs.ForeColor = System.Drawing.Color.White;
            this.lbSetlistSongs.FormattingEnabled = true;
            this.lbSetlistSongs.ItemHeight = 24;
            this.lbSetlistSongs.Location = new System.Drawing.Point(97, 79);
            this.lbSetlistSongs.Name = "lbSetlistSongs";
            this.lbSetlistSongs.Size = new System.Drawing.Size(267, 148);
            this.lbSetlistSongs.TabIndex = 6;
            // 
            // tbSetlistName
            // 
            this.tbSetlistName.BackColor = System.Drawing.Color.DimGray;
            this.tbSetlistName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSetlistName.ForeColor = System.Drawing.Color.White;
            this.tbSetlistName.Location = new System.Drawing.Point(97, 36);
            this.tbSetlistName.Name = "tbSetlistName";
            this.tbSetlistName.Size = new System.Drawing.Size(317, 32);
            this.tbSetlistName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Setlist Name:";
            // 
            // tlpSetlistSelOuter
            // 
            this.tlpSetlistSelOuter.ColumnCount = 1;
            this.tlpSetlistSelOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSetlistSelOuter.Controls.Add(this.mbccSetlistEditSelector, 0, 1);
            this.tlpSetlistSelOuter.Controls.Add(this.tlpSetlistSelButtons, 0, 0);
            this.tlpSetlistSelOuter.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlpSetlistSelOuter.Location = new System.Drawing.Point(0, 0);
            this.tlpSetlistSelOuter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSetlistSelOuter.Name = "tlpSetlistSelOuter";
            this.tlpSetlistSelOuter.RowCount = 3;
            this.tlpSetlistSelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpSetlistSelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSetlistSelOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSetlistSelOuter.Size = new System.Drawing.Size(411, 625);
            this.tlpSetlistSelOuter.TabIndex = 0;
            // 
            // mbccSetlistEditSelector
            // 
            this.mbccSetlistEditSelector.ButtonBackColor = System.Drawing.Color.DimGray;
            this.mbccSetlistEditSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbccSetlistEditSelector.ForeColor = System.Drawing.Color.White;
            this.mbccSetlistEditSelector.Location = new System.Drawing.Point(4, 54);
            this.mbccSetlistEditSelector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbccSetlistEditSelector.Name = "mbccSetlistEditSelector";
            this.tlpSetlistSelOuter.SetRowSpan(this.mbccSetlistEditSelector, 2);
            this.mbccSetlistEditSelector.ShowAlphaButtons = false;
            this.mbccSetlistEditSelector.ShowArrowButtons = true;
            this.mbccSetlistEditSelector.ShowLogicalButtonNumberBadge = false;
            this.mbccSetlistEditSelector.ShowScrollbar = true;
            this.mbccSetlistEditSelector.Size = new System.Drawing.Size(403, 567);
            this.mbccSetlistEditSelector.TabIndex = 2;
            this.mbccSetlistEditSelector.Click += new System.EventHandler(this.mbccSetlistEditSelector_Click);
            // 
            // tlpSetlistSelButtons
            // 
            this.tlpSetlistSelButtons.ColumnCount = 2;
            this.tlpSetlistSelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSetlistSelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSetlistSelButtons.Controls.Add(this.btnSetlistAdd, 0, 0);
            this.tlpSetlistSelButtons.Controls.Add(this.btnSetlistDel, 1, 0);
            this.tlpSetlistSelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSetlistSelButtons.Location = new System.Drawing.Point(0, 0);
            this.tlpSetlistSelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSetlistSelButtons.Name = "tlpSetlistSelButtons";
            this.tlpSetlistSelButtons.RowCount = 1;
            this.tlpSetlistSelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSetlistSelButtons.Size = new System.Drawing.Size(411, 50);
            this.tlpSetlistSelButtons.TabIndex = 0;
            // 
            // btnSetlistAdd
            // 
            this.btnSetlistAdd.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetlistAdd.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistAdd.ForeColor = System.Drawing.Color.White;
            this.btnSetlistAdd.Location = new System.Drawing.Point(3, 3);
            this.btnSetlistAdd.Name = "btnSetlistAdd";
            this.btnSetlistAdd.Size = new System.Drawing.Size(199, 44);
            this.btnSetlistAdd.TabIndex = 0;
            this.btnSetlistAdd.Text = "+";
            this.btnSetlistAdd.UseVisualStyleBackColor = false;
            this.btnSetlistAdd.Click += new System.EventHandler(this.btnSetlistAdd_Click);
            // 
            // btnSetlistDel
            // 
            this.btnSetlistDel.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistDel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetlistDel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistDel.ForeColor = System.Drawing.Color.White;
            this.btnSetlistDel.Location = new System.Drawing.Point(208, 3);
            this.btnSetlistDel.Name = "btnSetlistDel";
            this.btnSetlistDel.Size = new System.Drawing.Size(200, 44);
            this.btnSetlistDel.TabIndex = 1;
            this.btnSetlistDel.Text = "-";
            this.btnSetlistDel.UseVisualStyleBackColor = false;
            this.btnSetlistDel.Click += new System.EventHandler(this.btnSetlistDel_Click);
            // 
            // tpMappings
            // 
            this.tpMappings.BackColor = System.Drawing.Color.Black;
            this.tpMappings.Controls.Add(this.tlpMappingEditOuter);
            this.tpMappings.Location = new System.Drawing.Point(4, 44);
            this.tpMappings.Name = "tpMappings";
            this.tpMappings.Size = new System.Drawing.Size(1147, 625);
            this.tpMappings.TabIndex = 9;
            this.tpMappings.Text = "Mappings";
            // 
            // tlpMappingEditOuter
            // 
            this.tlpMappingEditOuter.ColumnCount = 3;
            this.tlpMappingEditOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMappingEditOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMappingEditOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMappingEditOuter.Controls.Add(this.mbrcMappingSelect, 0, 1);
            this.tlpMappingEditOuter.Controls.Add(this.tlpMappingEditButtons, 0, 0);
            this.tlpMappingEditOuter.Controls.Add(this.tlpMappingEditNameAndButtons, 1, 0);
            this.tlpMappingEditOuter.Controls.Add(this.pnlMappingEdit, 1, 1);
            this.tlpMappingEditOuter.Controls.Add(this.tlpMappingEditorPatches, 2, 1);
            this.tlpMappingEditOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMappingEditOuter.Location = new System.Drawing.Point(0, 0);
            this.tlpMappingEditOuter.Name = "tlpMappingEditOuter";
            this.tlpMappingEditOuter.RowCount = 3;
            this.tlpMappingEditOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMappingEditOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tlpMappingEditOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMappingEditOuter.Size = new System.Drawing.Size(1147, 625);
            this.tlpMappingEditOuter.TabIndex = 0;
            // 
            // mbrcMappingSelect
            // 
            this.mbrcMappingSelect.ButtonBackColor = System.Drawing.Color.DimGray;
            this.mbrcMappingSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbrcMappingSelect.ForeColor = System.Drawing.Color.White;
            this.mbrcMappingSelect.Location = new System.Drawing.Point(12, 63);
            this.mbrcMappingSelect.Margin = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.mbrcMappingSelect.Name = "mbrcMappingSelect";
            this.mbrcMappingSelect.ShowAlphaButtons = false;
            this.mbrcMappingSelect.ShowArrowButtons = true;
            this.mbrcMappingSelect.ShowLogicalButtonNumberBadge = false;
            this.mbrcMappingSelect.ShowScrollbar = true;
            this.mbrcMappingSelect.Size = new System.Drawing.Size(262, 491);
            this.mbrcMappingSelect.TabIndex = 2;
            this.mbrcMappingSelect.Click += new System.EventHandler(this.mbrcMappingSelect_Click);
            // 
            // tlpMappingEditButtons
            // 
            this.tlpMappingEditButtons.ColumnCount = 2;
            this.tlpMappingEditButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMappingEditButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMappingEditButtons.Controls.Add(this.btnMappingDelete, 1, 0);
            this.tlpMappingEditButtons.Controls.Add(this.btnMappingAdd, 0, 0);
            this.tlpMappingEditButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMappingEditButtons.Location = new System.Drawing.Point(3, 3);
            this.tlpMappingEditButtons.Name = "tlpMappingEditButtons";
            this.tlpMappingEditButtons.RowCount = 1;
            this.tlpMappingEditButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMappingEditButtons.Size = new System.Drawing.Size(280, 44);
            this.tlpMappingEditButtons.TabIndex = 2;
            // 
            // btnMappingDelete
            // 
            this.btnMappingDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMappingDelete.Location = new System.Drawing.Point(143, 3);
            this.btnMappingDelete.Name = "btnMappingDelete";
            this.btnMappingDelete.Size = new System.Drawing.Size(134, 38);
            this.btnMappingDelete.TabIndex = 1;
            this.btnMappingDelete.Text = "-";
            this.btnMappingDelete.UseVisualStyleBackColor = true;
            this.btnMappingDelete.Click += new System.EventHandler(this.btnMappingDelete_Click);
            // 
            // btnMappingAdd
            // 
            this.btnMappingAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMappingAdd.Location = new System.Drawing.Point(3, 3);
            this.btnMappingAdd.Name = "btnMappingAdd";
            this.btnMappingAdd.Size = new System.Drawing.Size(134, 38);
            this.btnMappingAdd.TabIndex = 0;
            this.btnMappingAdd.Text = "+";
            this.btnMappingAdd.UseVisualStyleBackColor = true;
            this.btnMappingAdd.Click += new System.EventHandler(this.btnMappingAdd_Click);
            // 
            // tlpMappingEditNameAndButtons
            // 
            this.tlpMappingEditNameAndButtons.ColumnCount = 4;
            this.tlpMappingEditNameAndButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tlpMappingEditNameAndButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMappingEditNameAndButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMappingEditNameAndButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMappingEditNameAndButtons.Controls.Add(this.btnMappingEditOK, 3, 0);
            this.tlpMappingEditNameAndButtons.Controls.Add(this.btnMappingEditCancel, 2, 0);
            this.tlpMappingEditNameAndButtons.Controls.Add(this.label15, 0, 0);
            this.tlpMappingEditNameAndButtons.Controls.Add(this.tbMappingName, 1, 0);
            this.tlpMappingEditNameAndButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMappingEditNameAndButtons.Location = new System.Drawing.Point(289, 3);
            this.tlpMappingEditNameAndButtons.Name = "tlpMappingEditNameAndButtons";
            this.tlpMappingEditNameAndButtons.RowCount = 1;
            this.tlpMappingEditNameAndButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMappingEditNameAndButtons.Size = new System.Drawing.Size(567, 44);
            this.tlpMappingEditNameAndButtons.TabIndex = 5;
            this.tlpMappingEditNameAndButtons.Visible = false;
            // 
            // btnMappingEditOK
            // 
            this.btnMappingEditOK.BackColor = System.Drawing.Color.DimGray;
            this.btnMappingEditOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMappingEditOK.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnMappingEditOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMappingEditOK.ForeColor = System.Drawing.Color.White;
            this.btnMappingEditOK.Location = new System.Drawing.Point(470, 3);
            this.btnMappingEditOK.Name = "btnMappingEditOK";
            this.btnMappingEditOK.Size = new System.Drawing.Size(94, 38);
            this.btnMappingEditOK.TabIndex = 6;
            this.btnMappingEditOK.Text = "OK";
            this.btnMappingEditOK.UseVisualStyleBackColor = false;
            this.btnMappingEditOK.Click += new System.EventHandler(this.btnMappingEditOK_Click);
            // 
            // btnMappingEditCancel
            // 
            this.btnMappingEditCancel.BackColor = System.Drawing.Color.DimGray;
            this.btnMappingEditCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMappingEditCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGreen;
            this.btnMappingEditCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMappingEditCancel.ForeColor = System.Drawing.Color.White;
            this.btnMappingEditCancel.Location = new System.Drawing.Point(370, 3);
            this.btnMappingEditCancel.Name = "btnMappingEditCancel";
            this.btnMappingEditCancel.Size = new System.Drawing.Size(94, 38);
            this.btnMappingEditCancel.TabIndex = 5;
            this.btnMappingEditCancel.Text = "Cancel";
            this.btnMappingEditCancel.UseVisualStyleBackColor = false;
            this.btnMappingEditCancel.Click += new System.EventHandler(this.btnMappingEditCancel_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(164, 44);
            this.label15.TabIndex = 3;
            this.label15.Text = "Mapping Name:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMappingName
            // 
            this.tbMappingName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMappingName.BackColor = System.Drawing.Color.DimGray;
            this.tbMappingName.ForeColor = System.Drawing.Color.Black;
            this.tbMappingName.Location = new System.Drawing.Point(173, 6);
            this.tbMappingName.Name = "tbMappingName";
            this.tbMappingName.Size = new System.Drawing.Size(191, 32);
            this.tbMappingName.TabIndex = 4;
            // 
            // pnlMappingEdit
            // 
            this.pnlMappingEdit.Controls.Add(this.lblVolume);
            this.pnlMappingEdit.Controls.Add(this.tbVolume);
            this.pnlMappingEdit.Controls.Add(this.lblMappingPBScale);
            this.pnlMappingEdit.Controls.Add(this.lblMappingTransOcts);
            this.pnlMappingEdit.Controls.Add(this.lblMappingTransSemis);
            this.pnlMappingEdit.Controls.Add(this.cbMappingDefDamperToggle);
            this.pnlMappingEdit.Controls.Add(this.nudMappingDefDamperRemap);
            this.pnlMappingEdit.Controls.Add(this.cbMappingDefDamperEna);
            this.pnlMappingEdit.Controls.Add(this.tbMappingDefIniCC7);
            this.pnlMappingEdit.Controls.Add(this.cbMappingDefVolEna);
            this.pnlMappingEdit.Controls.Add(this.cbMappingDefModWheelEna);
            this.pnlMappingEdit.Controls.Add(this.lblMappingEditPBScale);
            this.pnlMappingEdit.Controls.Add(this.tbPBScale);
            this.pnlMappingEdit.Controls.Add(this.nudMappingDefTransposeSemis);
            this.pnlMappingEdit.Controls.Add(this.lblMappingEditTranspose);
            this.pnlMappingEdit.Controls.Add(this.nudMappingDefTransposeOct);
            this.pnlMappingEdit.Controls.Add(this.cbMappingSplitDevice2);
            this.pnlMappingEdit.Controls.Add(this.nudMappingSplitDevice2);
            this.pnlMappingEdit.Controls.Add(this.cbMappingSplitDevice1);
            this.pnlMappingEdit.Controls.Add(this.label20);
            this.pnlMappingEdit.Controls.Add(this.label19);
            this.pnlMappingEdit.Controls.Add(this.nudMappingSplitDevice1);
            this.pnlMappingEdit.Controls.Add(this.lblMappingInputDevice2);
            this.pnlMappingEdit.Controls.Add(this.lbMappingDevice2LowerPatches);
            this.pnlMappingEdit.Controls.Add(this.lbMappingDevice2UpperPatches);
            this.pnlMappingEdit.Controls.Add(this.lblMappingInputDevice1);
            this.pnlMappingEdit.Controls.Add(this.lbMappingDevice1LowerPatches);
            this.pnlMappingEdit.Controls.Add(this.lbMappingDevice1UpperPatches);
            this.pnlMappingEdit.Location = new System.Drawing.Point(289, 53);
            this.pnlMappingEdit.Name = "pnlMappingEdit";
            this.pnlMappingEdit.Size = new System.Drawing.Size(567, 511);
            this.pnlMappingEdit.TabIndex = 6;
            this.pnlMappingEdit.Visible = false;
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.ForeColor = System.Drawing.Color.White;
            this.lblVolume.Location = new System.Drawing.Point(320, 383);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(84, 25);
            this.lblVolume.TabIndex = 31;
            this.lblVolume.Text = "Volume";
            this.lblVolume.Visible = false;
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(412, 380);
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(155, 32);
            this.tbVolume.TabIndex = 30;
            this.tbVolume.Visible = false;
            this.tbVolume.TextChanged += new System.EventHandler(this.tbVolume_TextChanged);
            // 
            // lblMappingPBScale
            // 
            this.lblMappingPBScale.AutoSize = true;
            this.lblMappingPBScale.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblMappingPBScale.Location = new System.Drawing.Point(70, 430);
            this.lblMappingPBScale.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMappingPBScale.Name = "lblMappingPBScale";
            this.lblMappingPBScale.Size = new System.Drawing.Size(102, 25);
            this.lblMappingPBScale.TabIndex = 29;
            this.lblMappingPBScale.Text = "PB Scale";
            this.lblMappingPBScale.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMappingTransOcts
            // 
            this.lblMappingTransOcts.AutoSize = true;
            this.lblMappingTransOcts.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblMappingTransOcts.Location = new System.Drawing.Point(14, 300);
            this.lblMappingTransOcts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMappingTransOcts.Name = "lblMappingTransOcts";
            this.lblMappingTransOcts.Size = new System.Drawing.Size(123, 25);
            this.lblMappingTransOcts.TabIndex = 28;
            this.lblMappingTransOcts.Text = "Trans: Octs";
            this.lblMappingTransOcts.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMappingTransSemis
            // 
            this.lblMappingTransSemis.AutoSize = true;
            this.lblMappingTransSemis.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblMappingTransSemis.Location = new System.Drawing.Point(135, 300);
            this.lblMappingTransSemis.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMappingTransSemis.Name = "lblMappingTransSemis";
            this.lblMappingTransSemis.Size = new System.Drawing.Size(139, 25);
            this.lblMappingTransSemis.TabIndex = 27;
            this.lblMappingTransSemis.Text = "Trans: Semis";
            // 
            // cbMappingDefDamperToggle
            // 
            this.cbMappingDefDamperToggle.AutoSize = true;
            this.cbMappingDefDamperToggle.ForeColor = System.Drawing.Color.White;
            this.cbMappingDefDamperToggle.Location = new System.Drawing.Point(508, 431);
            this.cbMappingDefDamperToggle.Name = "cbMappingDefDamperToggle";
            this.cbMappingDefDamperToggle.Size = new System.Drawing.Size(96, 29);
            this.cbMappingDefDamperToggle.TabIndex = 26;
            this.cbMappingDefDamperToggle.Text = "Toggle";
            this.cbMappingDefDamperToggle.UseVisualStyleBackColor = true;
            this.cbMappingDefDamperToggle.Visible = false;
            this.cbMappingDefDamperToggle.CheckedChanged += new System.EventHandler(this.cbMappingDefDamperToggle_CheckedChanged);
            // 
            // nudMappingDefDamperRemap
            // 
            this.nudMappingDefDamperRemap.BackColor = System.Drawing.Color.DimGray;
            this.nudMappingDefDamperRemap.ForeColor = System.Drawing.Color.White;
            this.nudMappingDefDamperRemap.Location = new System.Drawing.Point(412, 430);
            this.nudMappingDefDamperRemap.Name = "nudMappingDefDamperRemap";
            this.nudMappingDefDamperRemap.Size = new System.Drawing.Size(77, 32);
            this.nudMappingDefDamperRemap.TabIndex = 25;
            this.nudMappingDefDamperRemap.Visible = false;
            this.nudMappingDefDamperRemap.ValueChanged += new System.EventHandler(this.nudMappingDefDamperRemap_ValueChanged);
            // 
            // cbMappingDefDamperEna
            // 
            this.cbMappingDefDamperEna.AutoSize = true;
            this.cbMappingDefDamperEna.ForeColor = System.Drawing.Color.White;
            this.cbMappingDefDamperEna.Location = new System.Drawing.Point(302, 431);
            this.cbMappingDefDamperEna.Name = "cbMappingDefDamperEna";
            this.cbMappingDefDamperEna.Size = new System.Drawing.Size(108, 29);
            this.cbMappingDefDamperEna.TabIndex = 24;
            this.cbMappingDefDamperEna.Text = "Damper";
            this.cbMappingDefDamperEna.UseVisualStyleBackColor = true;
            this.cbMappingDefDamperEna.Visible = false;
            this.cbMappingDefDamperEna.CheckedChanged += new System.EventHandler(this.cbMappingDefDamperEna_CheckedChanged);
            // 
            // tbMappingDefIniCC7
            // 
            this.tbMappingDefIniCC7.Location = new System.Drawing.Point(412, 336);
            this.tbMappingDefIniCC7.Maximum = 127;
            this.tbMappingDefIniCC7.Minimum = -1;
            this.tbMappingDefIniCC7.Name = "tbMappingDefIniCC7";
            this.tbMappingDefIniCC7.Size = new System.Drawing.Size(202, 45);
            this.tbMappingDefIniCC7.TabIndex = 23;
            this.tbMappingDefIniCC7.Visible = false;
            this.tbMappingDefIniCC7.Scroll += new System.EventHandler(this.tbMappingDefIniVol_Scroll);
            // 
            // cbMappingDefVolEna
            // 
            this.cbMappingDefVolEna.AutoSize = true;
            this.cbMappingDefVolEna.ForeColor = System.Drawing.Color.White;
            this.cbMappingDefVolEna.Location = new System.Drawing.Point(302, 339);
            this.cbMappingDefVolEna.Name = "cbMappingDefVolEna";
            this.cbMappingDefVolEna.Size = new System.Drawing.Size(75, 29);
            this.cbMappingDefVolEna.TabIndex = 22;
            this.cbMappingDefVolEna.Text = "CC7";
            this.cbMappingDefVolEna.UseVisualStyleBackColor = true;
            this.cbMappingDefVolEna.Visible = false;
            this.cbMappingDefVolEna.CheckedChanged += new System.EventHandler(this.cbMappingDefVolEna_CheckedChanged);
            // 
            // cbMappingDefModWheelEna
            // 
            this.cbMappingDefModWheelEna.AutoSize = true;
            this.cbMappingDefModWheelEna.ForeColor = System.Drawing.Color.White;
            this.cbMappingDefModWheelEna.Location = new System.Drawing.Point(302, 300);
            this.cbMappingDefModWheelEna.Name = "cbMappingDefModWheelEna";
            this.cbMappingDefModWheelEna.Size = new System.Drawing.Size(141, 29);
            this.cbMappingDefModWheelEna.TabIndex = 21;
            this.cbMappingDefModWheelEna.Text = "Mod Wheel";
            this.cbMappingDefModWheelEna.UseVisualStyleBackColor = true;
            this.cbMappingDefModWheelEna.Visible = false;
            this.cbMappingDefModWheelEna.CheckedChanged += new System.EventHandler(this.cbMappingDefModWheelEna_CheckedChanged);
            // 
            // lblMappingEditPBScale
            // 
            this.lblMappingEditPBScale.AutoSize = true;
            this.lblMappingEditPBScale.Location = new System.Drawing.Point(-88, 411);
            this.lblMappingEditPBScale.Name = "lblMappingEditPBScale";
            this.lblMappingEditPBScale.Size = new System.Drawing.Size(108, 25);
            this.lblMappingEditPBScale.TabIndex = 20;
            this.lblMappingEditPBScale.Text = "PB Scale:";
            this.lblMappingEditPBScale.Visible = false;
            // 
            // tbPBScale
            // 
            this.tbPBScale.Location = new System.Drawing.Point(26, 390);
            this.tbPBScale.Maximum = 12;
            this.tbPBScale.Minimum = -12;
            this.tbPBScale.Name = "tbPBScale";
            this.tbPBScale.Size = new System.Drawing.Size(202, 45);
            this.tbPBScale.TabIndex = 19;
            this.tbPBScale.Visible = false;
            this.tbPBScale.ValueChanged += new System.EventHandler(this.tbPBScale_ValueChanged);
            // 
            // nudMappingDefTransposeSemis
            // 
            this.nudMappingDefTransposeSemis.BackColor = System.Drawing.Color.DimGray;
            this.nudMappingDefTransposeSemis.ForeColor = System.Drawing.Color.White;
            this.nudMappingDefTransposeSemis.Location = new System.Drawing.Point(140, 342);
            this.nudMappingDefTransposeSemis.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.nudMappingDefTransposeSemis.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            -2147483648});
            this.nudMappingDefTransposeSemis.Name = "nudMappingDefTransposeSemis";
            this.nudMappingDefTransposeSemis.Size = new System.Drawing.Size(87, 32);
            this.nudMappingDefTransposeSemis.TabIndex = 18;
            this.nudMappingDefTransposeSemis.Visible = false;
            this.nudMappingDefTransposeSemis.ValueChanged += new System.EventHandler(this.nudMappingDefTranspose_ValueChanged);
            // 
            // lblMappingEditTranspose
            // 
            this.lblMappingEditTranspose.AutoSize = true;
            this.lblMappingEditTranspose.Location = new System.Drawing.Point(-100, 344);
            this.lblMappingEditTranspose.Name = "lblMappingEditTranspose";
            this.lblMappingEditTranspose.Size = new System.Drawing.Size(120, 25);
            this.lblMappingEditTranspose.TabIndex = 17;
            this.lblMappingEditTranspose.Text = "Transpose:";
            this.lblMappingEditTranspose.Visible = false;
            // 
            // nudMappingDefTransposeOct
            // 
            this.nudMappingDefTransposeOct.BackColor = System.Drawing.Color.DimGray;
            this.nudMappingDefTransposeOct.ForeColor = System.Drawing.Color.White;
            this.nudMappingDefTransposeOct.Location = new System.Drawing.Point(26, 342);
            this.nudMappingDefTransposeOct.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMappingDefTransposeOct.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.nudMappingDefTransposeOct.Name = "nudMappingDefTransposeOct";
            this.nudMappingDefTransposeOct.Size = new System.Drawing.Size(91, 32);
            this.nudMappingDefTransposeOct.TabIndex = 16;
            this.nudMappingDefTransposeOct.Visible = false;
            this.nudMappingDefTransposeOct.ValueChanged += new System.EventHandler(this.nudMappingDefTranspose_ValueChanged);
            // 
            // cbMappingSplitDevice2
            // 
            this.cbMappingSplitDevice2.AutoSize = true;
            this.cbMappingSplitDevice2.ForeColor = System.Drawing.Color.White;
            this.cbMappingSplitDevice2.Location = new System.Drawing.Point(356, 110);
            this.cbMappingSplitDevice2.Name = "cbMappingSplitDevice2";
            this.cbMappingSplitDevice2.Size = new System.Drawing.Size(73, 29);
            this.cbMappingSplitDevice2.TabIndex = 13;
            this.cbMappingSplitDevice2.Text = "Split";
            this.cbMappingSplitDevice2.UseVisualStyleBackColor = true;
            this.cbMappingSplitDevice2.CheckedChanged += new System.EventHandler(this.cbMappingSplitDevice2_CheckedChanged);
            // 
            // nudMappingSplitDevice2
            // 
            this.nudMappingSplitDevice2.BackColor = System.Drawing.Color.DimGray;
            this.nudMappingSplitDevice2.ForeColor = System.Drawing.Color.White;
            this.nudMappingSplitDevice2.Location = new System.Drawing.Point(454, 110);
            this.nudMappingSplitDevice2.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudMappingSplitDevice2.Name = "nudMappingSplitDevice2";
            this.nudMappingSplitDevice2.Size = new System.Drawing.Size(78, 32);
            this.nudMappingSplitDevice2.TabIndex = 14;
            this.nudMappingSplitDevice2.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // cbMappingSplitDevice1
            // 
            this.cbMappingSplitDevice1.AutoSize = true;
            this.cbMappingSplitDevice1.ForeColor = System.Drawing.Color.White;
            this.cbMappingSplitDevice1.Location = new System.Drawing.Point(59, 110);
            this.cbMappingSplitDevice1.Name = "cbMappingSplitDevice1";
            this.cbMappingSplitDevice1.Size = new System.Drawing.Size(73, 29);
            this.cbMappingSplitDevice1.TabIndex = 9;
            this.cbMappingSplitDevice1.Text = "Split";
            this.cbMappingSplitDevice1.UseVisualStyleBackColor = true;
            this.cbMappingSplitDevice1.CheckedChanged += new System.EventHandler(this.cbMappingSplitDevice1_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(250, 169);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(71, 25);
            this.label20.TabIndex = 10;
            this.label20.Text = "Lower";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(256, 48);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(71, 25);
            this.label19.TabIndex = 9;
            this.label19.Text = "Upper";
            // 
            // nudMappingSplitDevice1
            // 
            this.nudMappingSplitDevice1.BackColor = System.Drawing.Color.DimGray;
            this.nudMappingSplitDevice1.ForeColor = System.Drawing.Color.White;
            this.nudMappingSplitDevice1.Location = new System.Drawing.Point(138, 109);
            this.nudMappingSplitDevice1.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudMappingSplitDevice1.Name = "nudMappingSplitDevice1";
            this.nudMappingSplitDevice1.Size = new System.Drawing.Size(78, 32);
            this.nudMappingSplitDevice1.TabIndex = 10;
            this.nudMappingSplitDevice1.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // lblMappingInputDevice2
            // 
            this.lblMappingInputDevice2.AutoSize = true;
            this.lblMappingInputDevice2.ForeColor = System.Drawing.Color.White;
            this.lblMappingInputDevice2.Location = new System.Drawing.Point(326, 8);
            this.lblMappingInputDevice2.Name = "lblMappingInputDevice2";
            this.lblMappingInputDevice2.Size = new System.Drawing.Size(80, 25);
            this.lblMappingInputDevice2.TabIndex = 6;
            this.lblMappingInputDevice2.Text = "label20";
            this.lblMappingInputDevice2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMappingDevice2LowerPatches
            // 
            this.lbMappingDevice2LowerPatches.AllowDrop = true;
            this.lbMappingDevice2LowerPatches.BackColor = System.Drawing.Color.Black;
            this.lbMappingDevice2LowerPatches.ContextMenuStrip = this.contextMenuStrip1;
            this.lbMappingDevice2LowerPatches.DisplayMember = "programName";
            this.lbMappingDevice2LowerPatches.ForeColor = System.Drawing.Color.White;
            this.lbMappingDevice2LowerPatches.FormattingEnabled = true;
            this.lbMappingDevice2LowerPatches.ItemHeight = 24;
            this.lbMappingDevice2LowerPatches.Location = new System.Drawing.Point(331, 158);
            this.lbMappingDevice2LowerPatches.Name = "lbMappingDevice2LowerPatches";
            this.lbMappingDevice2LowerPatches.Size = new System.Drawing.Size(220, 52);
            this.lbMappingDevice2LowerPatches.TabIndex = 15;
            this.lbMappingDevice2LowerPatches.ValueMember = "programName";
            this.lbMappingDevice2LowerPatches.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragDrop);
            this.lbMappingDevice2LowerPatches.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragEnter);
            this.lbMappingDevice2LowerPatches.DoubleClick += new System.EventHandler(this.lbMappingPatches_DoubleClick);
            this.lbMappingDevice2LowerPatches.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbMappingPatches_MouseUp);
            // 
            // lbMappingDevice2UpperPatches
            // 
            this.lbMappingDevice2UpperPatches.AllowDrop = true;
            this.lbMappingDevice2UpperPatches.BackColor = System.Drawing.Color.Black;
            this.lbMappingDevice2UpperPatches.ContextMenuStrip = this.contextMenuStrip1;
            this.lbMappingDevice2UpperPatches.DisplayMember = "programName";
            this.lbMappingDevice2UpperPatches.ForeColor = System.Drawing.Color.White;
            this.lbMappingDevice2UpperPatches.FormattingEnabled = true;
            this.lbMappingDevice2UpperPatches.ItemHeight = 24;
            this.lbMappingDevice2UpperPatches.Location = new System.Drawing.Point(331, 37);
            this.lbMappingDevice2UpperPatches.Name = "lbMappingDevice2UpperPatches";
            this.lbMappingDevice2UpperPatches.Size = new System.Drawing.Size(220, 52);
            this.lbMappingDevice2UpperPatches.TabIndex = 12;
            this.lbMappingDevice2UpperPatches.ValueMember = "programName";
            this.lbMappingDevice2UpperPatches.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragDrop);
            this.lbMappingDevice2UpperPatches.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragEnter);
            this.lbMappingDevice2UpperPatches.DoubleClick += new System.EventHandler(this.lbMappingPatches_DoubleClick);
            this.lbMappingDevice2UpperPatches.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbMappingPatches_MouseUp);
            // 
            // lblMappingInputDevice1
            // 
            this.lblMappingInputDevice1.AutoSize = true;
            this.lblMappingInputDevice1.ForeColor = System.Drawing.Color.White;
            this.lblMappingInputDevice1.Location = new System.Drawing.Point(24, 7);
            this.lblMappingInputDevice1.Name = "lblMappingInputDevice1";
            this.lblMappingInputDevice1.Size = new System.Drawing.Size(80, 25);
            this.lblMappingInputDevice1.TabIndex = 7;
            this.lblMappingInputDevice1.Text = "label19";
            this.lblMappingInputDevice1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMappingDevice1LowerPatches
            // 
            this.lbMappingDevice1LowerPatches.AllowDrop = true;
            this.lbMappingDevice1LowerPatches.BackColor = System.Drawing.Color.Black;
            this.lbMappingDevice1LowerPatches.ContextMenuStrip = this.contextMenuStrip1;
            this.lbMappingDevice1LowerPatches.DisplayMember = "programName";
            this.lbMappingDevice1LowerPatches.ForeColor = System.Drawing.Color.White;
            this.lbMappingDevice1LowerPatches.FormattingEnabled = true;
            this.lbMappingDevice1LowerPatches.ItemHeight = 24;
            this.lbMappingDevice1LowerPatches.Location = new System.Drawing.Point(25, 158);
            this.lbMappingDevice1LowerPatches.Name = "lbMappingDevice1LowerPatches";
            this.lbMappingDevice1LowerPatches.Size = new System.Drawing.Size(220, 52);
            this.lbMappingDevice1LowerPatches.TabIndex = 11;
            this.lbMappingDevice1LowerPatches.ValueMember = "programName";
            this.lbMappingDevice1LowerPatches.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragDrop);
            this.lbMappingDevice1LowerPatches.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragEnter);
            this.lbMappingDevice1LowerPatches.DoubleClick += new System.EventHandler(this.lbMappingPatches_DoubleClick);
            this.lbMappingDevice1LowerPatches.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbMappingPatches_MouseUp);
            // 
            // lbMappingDevice1UpperPatches
            // 
            this.lbMappingDevice1UpperPatches.AllowDrop = true;
            this.lbMappingDevice1UpperPatches.BackColor = System.Drawing.Color.Black;
            this.lbMappingDevice1UpperPatches.ContextMenuStrip = this.contextMenuStrip1;
            this.lbMappingDevice1UpperPatches.DisplayMember = "programName";
            this.lbMappingDevice1UpperPatches.ForeColor = System.Drawing.Color.White;
            this.lbMappingDevice1UpperPatches.FormattingEnabled = true;
            this.lbMappingDevice1UpperPatches.ItemHeight = 24;
            this.lbMappingDevice1UpperPatches.Location = new System.Drawing.Point(32, 37);
            this.lbMappingDevice1UpperPatches.Name = "lbMappingDevice1UpperPatches";
            this.lbMappingDevice1UpperPatches.Size = new System.Drawing.Size(220, 52);
            this.lbMappingDevice1UpperPatches.TabIndex = 8;
            this.lbMappingDevice1UpperPatches.ValueMember = "programName";
            this.lbMappingDevice1UpperPatches.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragDrop);
            this.lbMappingDevice1UpperPatches.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbMappingDevicePatches_DragEnter);
            this.lbMappingDevice1UpperPatches.DoubleClick += new System.EventHandler(this.lbMappingPatches_DoubleClick);
            this.lbMappingDevice1UpperPatches.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbMappingPatches_MouseUp);
            // 
            // tlpMappingEditorPatches
            // 
            this.tlpMappingEditorPatches.ColumnCount = 1;
            this.tlpMappingEditorPatches.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMappingEditorPatches.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tlpMappingEditorPatches.Controls.Add(this.tvMappingEditorPrograms, 0, 0);
            this.tlpMappingEditorPatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMappingEditorPatches.Location = new System.Drawing.Point(861, 52);
            this.tlpMappingEditorPatches.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tlpMappingEditorPatches.Name = "tlpMappingEditorPatches";
            this.tlpMappingEditorPatches.RowCount = 2;
            this.tlpMappingEditorPatches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.2437F));
            this.tlpMappingEditorPatches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.7563F));
            this.tlpMappingEditorPatches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMappingEditorPatches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMappingEditorPatches.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpMappingEditorPatches.Size = new System.Drawing.Size(284, 513);
            this.tlpMappingEditorPatches.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnMappingEditPatchTreeViewBySG, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnMappingEditPatchTreeViewByCategory, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 460);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(278, 50);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // btnMappingEditPatchTreeViewBySG
            // 
            this.btnMappingEditPatchTreeViewBySG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMappingEditPatchTreeViewBySG.Location = new System.Drawing.Point(3, 3);
            this.btnMappingEditPatchTreeViewBySG.Name = "btnMappingEditPatchTreeViewBySG";
            this.btnMappingEditPatchTreeViewBySG.Size = new System.Drawing.Size(133, 44);
            this.btnMappingEditPatchTreeViewBySG.TabIndex = 16;
            this.btnMappingEditPatchTreeViewBySG.Text = "SG";
            this.btnMappingEditPatchTreeViewBySG.UseVisualStyleBackColor = true;
            this.btnMappingEditPatchTreeViewBySG.Click += new System.EventHandler(this.btnMappingEditPatchTreeViewBySG_Click);
            // 
            // btnMappingEditPatchTreeViewByCategory
            // 
            this.btnMappingEditPatchTreeViewByCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMappingEditPatchTreeViewByCategory.Location = new System.Drawing.Point(142, 3);
            this.btnMappingEditPatchTreeViewByCategory.Name = "btnMappingEditPatchTreeViewByCategory";
            this.btnMappingEditPatchTreeViewByCategory.Size = new System.Drawing.Size(133, 44);
            this.btnMappingEditPatchTreeViewByCategory.TabIndex = 17;
            this.btnMappingEditPatchTreeViewByCategory.Text = "Cat";
            this.btnMappingEditPatchTreeViewByCategory.UseVisualStyleBackColor = true;
            this.btnMappingEditPatchTreeViewByCategory.Click += new System.EventHandler(this.btnMappingEditPatchTreeViewByCategory_Click);
            // 
            // tvMappingEditorPrograms
            // 
            this.tvMappingEditorPrograms.BackColor = System.Drawing.Color.Black;
            this.tvMappingEditorPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMappingEditorPrograms.ForeColor = System.Drawing.Color.White;
            this.tvMappingEditorPrograms.Location = new System.Drawing.Point(3, 3);
            this.tvMappingEditorPrograms.Name = "tvMappingEditorPrograms";
            this.tvMappingEditorPrograms.Size = new System.Drawing.Size(278, 451);
            this.tvMappingEditorPrograms.TabIndex = 8;
            this.tvMappingEditorPrograms.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvMappingEditorPrograms_ItemDrag);
            // 
            // tpSoundGenerators
            // 
            this.tpSoundGenerators.Controls.Add(this.tlpSoundGeneratorsOuter);
            this.tpSoundGenerators.Location = new System.Drawing.Point(4, 44);
            this.tpSoundGenerators.Name = "tpSoundGenerators";
            this.tpSoundGenerators.Size = new System.Drawing.Size(1147, 625);
            this.tpSoundGenerators.TabIndex = 5;
            this.tpSoundGenerators.Text = "SoundGenerators";
            this.tpSoundGenerators.UseVisualStyleBackColor = true;
            // 
            // tlpSoundGeneratorsOuter
            // 
            this.tlpSoundGeneratorsOuter.ColumnCount = 3;
            this.tlpSoundGeneratorsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpSoundGeneratorsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpSoundGeneratorsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpSoundGeneratorsOuter.Controls.Add(this.lvSoundGenerators, 0, 1);
            this.tlpSoundGeneratorsOuter.Controls.Add(this.tlpSoundGeneratorEditButtons, 0, 0);
            this.tlpSoundGeneratorsOuter.Controls.Add(this.pnlSoundGeneratorEdit, 1, 1);
            this.tlpSoundGeneratorsOuter.Controls.Add(this.pnlSoundGeneratorPatchEdit, 2, 1);
            this.tlpSoundGeneratorsOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSoundGeneratorsOuter.Location = new System.Drawing.Point(0, 0);
            this.tlpSoundGeneratorsOuter.Name = "tlpSoundGeneratorsOuter";
            this.tlpSoundGeneratorsOuter.RowCount = 3;
            this.tlpSoundGeneratorsOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpSoundGeneratorsOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tlpSoundGeneratorsOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpSoundGeneratorsOuter.Size = new System.Drawing.Size(1147, 625);
            this.tlpSoundGeneratorsOuter.TabIndex = 0;
            // 
            // lvSoundGenerators
            // 
            this.lvSoundGenerators.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvSoundGenerators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSoundGenerators.FullRowSelect = true;
            this.lvSoundGenerators.GridLines = true;
            this.lvSoundGenerators.HideSelection = false;
            this.lvSoundGenerators.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvSoundGenerators.Location = new System.Drawing.Point(3, 43);
            this.lvSoundGenerators.MultiSelect = false;
            this.lvSoundGenerators.Name = "lvSoundGenerators";
            this.lvSoundGenerators.Size = new System.Drawing.Size(452, 520);
            this.lvSoundGenerators.TabIndex = 2;
            this.lvSoundGenerators.UseCompatibleStateImageBehavior = false;
            this.lvSoundGenerators.View = System.Windows.Forms.View.Details;
            this.lvSoundGenerators.SelectedIndexChanged += new System.EventHandler(this.lvSoundGenerators_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Sound Generator";
            this.columnHeader1.Width = 190;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Output Device";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Base Channel";
            this.columnHeader3.Width = 96;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "# Channels";
            this.columnHeader4.Width = 72;
            // 
            // tlpSoundGeneratorEditButtons
            // 
            this.tlpSoundGeneratorEditButtons.ColumnCount = 2;
            this.tlpSoundGeneratorEditButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSoundGeneratorEditButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSoundGeneratorEditButtons.Controls.Add(this.btnDeleteSoundGenerator, 0, 0);
            this.tlpSoundGeneratorEditButtons.Controls.Add(this.btnAddSoundGenerator, 0, 0);
            this.tlpSoundGeneratorEditButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSoundGeneratorEditButtons.Location = new System.Drawing.Point(3, 3);
            this.tlpSoundGeneratorEditButtons.Name = "tlpSoundGeneratorEditButtons";
            this.tlpSoundGeneratorEditButtons.RowCount = 1;
            this.tlpSoundGeneratorEditButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSoundGeneratorEditButtons.Size = new System.Drawing.Size(452, 34);
            this.tlpSoundGeneratorEditButtons.TabIndex = 3;
            // 
            // btnDeleteSoundGenerator
            // 
            this.btnDeleteSoundGenerator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteSoundGenerator.Enabled = false;
            this.btnDeleteSoundGenerator.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSoundGenerator.Location = new System.Drawing.Point(229, 3);
            this.btnDeleteSoundGenerator.Name = "btnDeleteSoundGenerator";
            this.btnDeleteSoundGenerator.Size = new System.Drawing.Size(220, 28);
            this.btnDeleteSoundGenerator.TabIndex = 2;
            this.btnDeleteSoundGenerator.Text = "-";
            this.btnDeleteSoundGenerator.UseVisualStyleBackColor = true;
            this.btnDeleteSoundGenerator.Click += new System.EventHandler(this.btnDeleteSoundGenerator_Click);
            // 
            // btnAddSoundGenerator
            // 
            this.btnAddSoundGenerator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddSoundGenerator.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSoundGenerator.Location = new System.Drawing.Point(3, 3);
            this.btnAddSoundGenerator.Name = "btnAddSoundGenerator";
            this.btnAddSoundGenerator.Size = new System.Drawing.Size(220, 28);
            this.btnAddSoundGenerator.TabIndex = 1;
            this.btnAddSoundGenerator.Text = "+";
            this.btnAddSoundGenerator.UseVisualStyleBackColor = true;
            this.btnAddSoundGenerator.Click += new System.EventHandler(this.btnAddSoundGenerator_Click);
            // 
            // pnlSoundGeneratorEdit
            // 
            this.pnlSoundGeneratorEdit.Controls.Add(this.cbSoundGeneratorCloneOf);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label29);
            this.pnlSoundGeneratorEdit.Controls.Add(this.tbDefaultVolume);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label27);
            this.pnlSoundGeneratorEdit.Controls.Add(this.tbTrackName);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label25);
            this.pnlSoundGeneratorEdit.Controls.Add(this.nudVolMax);
            this.pnlSoundGeneratorEdit.Controls.Add(this.nudVolMin);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label24);
            this.pnlSoundGeneratorEdit.Controls.Add(this.btnSoundGeneratorEditOK);
            this.pnlSoundGeneratorEdit.Controls.Add(this.btnSoundGeneratorEditCancel);
            this.pnlSoundGeneratorEdit.Controls.Add(this.btnSoundGeneratorPatchDel);
            this.pnlSoundGeneratorEdit.Controls.Add(this.btnSoundGeneratorPatchAdd);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label6);
            this.pnlSoundGeneratorEdit.Controls.Add(this.lbSoundGeneratorPatches);
            this.pnlSoundGeneratorEdit.Controls.Add(this.nudSoundGeneratorNumChannels);
            this.pnlSoundGeneratorEdit.Controls.Add(this.nudSoundGeneratorBaseChannel);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label7);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label8);
            this.pnlSoundGeneratorEdit.Controls.Add(this.cbSoundGeneratorDeviceName);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label9);
            this.pnlSoundGeneratorEdit.Controls.Add(this.tbSoundGeneratorName);
            this.pnlSoundGeneratorEdit.Controls.Add(this.label10);
            this.pnlSoundGeneratorEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSoundGeneratorEdit.Location = new System.Drawing.Point(461, 43);
            this.pnlSoundGeneratorEdit.Name = "pnlSoundGeneratorEdit";
            this.pnlSoundGeneratorEdit.Size = new System.Drawing.Size(338, 520);
            this.pnlSoundGeneratorEdit.TabIndex = 4;
            this.pnlSoundGeneratorEdit.Visible = false;
            // 
            // cbSoundGeneratorCloneOf
            // 
            this.cbSoundGeneratorCloneOf.FormattingEnabled = true;
            this.cbSoundGeneratorCloneOf.Location = new System.Drawing.Point(88, 312);
            this.cbSoundGeneratorCloneOf.Name = "cbSoundGeneratorCloneOf";
            this.cbSoundGeneratorCloneOf.Size = new System.Drawing.Size(235, 32);
            this.cbSoundGeneratorCloneOf.TabIndex = 34;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(14, 319);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(73, 18);
            this.label29.TabIndex = 48;
            this.label29.Text = "Clone Of:";
            // 
            // tbDefaultVolume
            // 
            this.tbDefaultVolume.Location = new System.Drawing.Point(229, 405);
            this.tbDefaultVolume.Name = "tbDefaultVolume";
            this.tbDefaultVolume.Size = new System.Drawing.Size(95, 32);
            this.tbDefaultVolume.TabIndex = 47;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(198, 410);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(34, 18);
            this.label27.TabIndex = 46;
            this.label27.Text = "Vol:";
            // 
            // tbTrackName
            // 
            this.tbTrackName.Location = new System.Drawing.Point(89, 405);
            this.tbTrackName.Name = "tbTrackName";
            this.tbTrackName.Size = new System.Drawing.Size(98, 32);
            this.tbTrackName.TabIndex = 43;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(40, 410);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(50, 18);
            this.label25.TabIndex = 42;
            this.label25.Text = "Track:";
            // 
            // nudVolMax
            // 
            this.nudVolMax.Location = new System.Drawing.Point(239, 358);
            this.nudVolMax.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudVolMax.Name = "nudVolMax";
            this.nudVolMax.Size = new System.Drawing.Size(54, 32);
            this.nudVolMax.TabIndex = 41;
            this.nudVolMax.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            // 
            // nudVolMin
            // 
            this.nudVolMin.Location = new System.Drawing.Point(133, 358);
            this.nudVolMin.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudVolMin.Name = "nudVolMin";
            this.nudVolMin.Size = new System.Drawing.Size(54, 32);
            this.nudVolMin.TabIndex = 40;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(21, 366);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(107, 18);
            this.label24.TabIndex = 39;
            this.label24.Text = "CC7 Min/Max:";
            // 
            // btnSoundGeneratorEditOK
            // 
            this.btnSoundGeneratorEditOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoundGeneratorEditOK.Location = new System.Drawing.Point(215, 455);
            this.btnSoundGeneratorEditOK.Name = "btnSoundGeneratorEditOK";
            this.btnSoundGeneratorEditOK.Size = new System.Drawing.Size(115, 52);
            this.btnSoundGeneratorEditOK.TabIndex = 45;
            this.btnSoundGeneratorEditOK.Text = "OK";
            this.btnSoundGeneratorEditOK.UseVisualStyleBackColor = true;
            this.btnSoundGeneratorEditOK.Click += new System.EventHandler(this.btnSoundGeneratorEditOK_Click);
            // 
            // btnSoundGeneratorEditCancel
            // 
            this.btnSoundGeneratorEditCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoundGeneratorEditCancel.Location = new System.Drawing.Point(97, 455);
            this.btnSoundGeneratorEditCancel.Name = "btnSoundGeneratorEditCancel";
            this.btnSoundGeneratorEditCancel.Size = new System.Drawing.Size(115, 52);
            this.btnSoundGeneratorEditCancel.TabIndex = 44;
            this.btnSoundGeneratorEditCancel.Text = "Cancel";
            this.btnSoundGeneratorEditCancel.UseVisualStyleBackColor = true;
            this.btnSoundGeneratorEditCancel.Click += new System.EventHandler(this.btnSoundGeneratorEditCancel_Click);
            // 
            // btnSoundGeneratorPatchDel
            // 
            this.btnSoundGeneratorPatchDel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoundGeneratorPatchDel.Location = new System.Drawing.Point(330, 255);
            this.btnSoundGeneratorPatchDel.Name = "btnSoundGeneratorPatchDel";
            this.btnSoundGeneratorPatchDel.Size = new System.Drawing.Size(35, 75);
            this.btnSoundGeneratorPatchDel.TabIndex = 36;
            this.btnSoundGeneratorPatchDel.Text = "-";
            this.btnSoundGeneratorPatchDel.UseVisualStyleBackColor = true;
            this.btnSoundGeneratorPatchDel.Click += new System.EventHandler(this.btnSoundGeneratorPatchDel_Click);
            // 
            // btnSoundGeneratorPatchAdd
            // 
            this.btnSoundGeneratorPatchAdd.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoundGeneratorPatchAdd.Location = new System.Drawing.Point(330, 154);
            this.btnSoundGeneratorPatchAdd.Name = "btnSoundGeneratorPatchAdd";
            this.btnSoundGeneratorPatchAdd.Size = new System.Drawing.Size(35, 81);
            this.btnSoundGeneratorPatchAdd.TabIndex = 35;
            this.btnSoundGeneratorPatchAdd.Text = "+";
            this.btnSoundGeneratorPatchAdd.UseVisualStyleBackColor = true;
            this.btnSoundGeneratorPatchAdd.Click += new System.EventHandler(this.btnSoundGeneratorPatchAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 18);
            this.label6.TabIndex = 34;
            this.label6.Text = "Patches:";
            // 
            // lbSoundGeneratorPatches
            // 
            this.lbSoundGeneratorPatches.FormattingEnabled = true;
            this.lbSoundGeneratorPatches.ItemHeight = 24;
            this.lbSoundGeneratorPatches.Location = new System.Drawing.Point(89, 153);
            this.lbSoundGeneratorPatches.Name = "lbSoundGeneratorPatches";
            this.lbSoundGeneratorPatches.Size = new System.Drawing.Size(235, 124);
            this.lbSoundGeneratorPatches.TabIndex = 33;
            this.lbSoundGeneratorPatches.SelectedIndexChanged += new System.EventHandler(this.lbSoundGeneratorPatches_SelectedIndexChanged);
            // 
            // nudSoundGeneratorNumChannels
            // 
            this.nudSoundGeneratorNumChannels.Location = new System.Drawing.Point(88, 111);
            this.nudSoundGeneratorNumChannels.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudSoundGeneratorNumChannels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoundGeneratorNumChannels.Name = "nudSoundGeneratorNumChannels";
            this.nudSoundGeneratorNumChannels.Size = new System.Drawing.Size(54, 32);
            this.nudSoundGeneratorNumChannels.TabIndex = 31;
            this.nudSoundGeneratorNumChannels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudSoundGeneratorBaseChannel
            // 
            this.nudSoundGeneratorBaseChannel.Location = new System.Drawing.Point(269, 111);
            this.nudSoundGeneratorBaseChannel.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudSoundGeneratorBaseChannel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoundGeneratorBaseChannel.Name = "nudSoundGeneratorBaseChannel";
            this.nudSoundGeneratorBaseChannel.Size = new System.Drawing.Size(54, 32);
            this.nudSoundGeneratorBaseChannel.TabIndex = 32;
            this.nudSoundGeneratorBaseChannel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(158, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "Base Channel:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 18);
            this.label8.TabIndex = 29;
            this.label8.Text = "Num Chns:";
            // 
            // cbSoundGeneratorDeviceName
            // 
            this.cbSoundGeneratorDeviceName.FormattingEnabled = true;
            this.cbSoundGeneratorDeviceName.Location = new System.Drawing.Point(88, 68);
            this.cbSoundGeneratorDeviceName.Name = "cbSoundGeneratorDeviceName";
            this.cbSoundGeneratorDeviceName.Size = new System.Drawing.Size(235, 32);
            this.cbSoundGeneratorDeviceName.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(25, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 18);
            this.label9.TabIndex = 27;
            this.label9.Text = "Device:";
            // 
            // tbSoundGeneratorName
            // 
            this.tbSoundGeneratorName.Location = new System.Drawing.Point(88, 27);
            this.tbSoundGeneratorName.Name = "tbSoundGeneratorName";
            this.tbSoundGeneratorName.Size = new System.Drawing.Size(236, 32);
            this.tbSoundGeneratorName.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(31, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 18);
            this.label10.TabIndex = 25;
            this.label10.Text = "Name:";
            // 
            // pnlSoundGeneratorPatchEdit
            // 
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.tbVolumeOverride);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.label28);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.tbFxPreset5);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.tbFxPreset4);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.tbFxPreset3);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.tbFxPreset2);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.tbFxPreset1);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.label26);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.btnSoundGeneratorPatchEditOK);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.btnSoundGeneratorPatchEditCancel);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.nudSoundGeneratorPatchProgramNo);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.nudSoundGeneratorPatchBankNo);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.label11);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.label12);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.cbSoundGeneratorPatchCategory);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.label13);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.tbSoundGeneratorPatchName);
            this.pnlSoundGeneratorPatchEdit.Controls.Add(this.label14);
            this.pnlSoundGeneratorPatchEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSoundGeneratorPatchEdit.Location = new System.Drawing.Point(805, 43);
            this.pnlSoundGeneratorPatchEdit.Name = "pnlSoundGeneratorPatchEdit";
            this.pnlSoundGeneratorPatchEdit.Size = new System.Drawing.Size(339, 520);
            this.pnlSoundGeneratorPatchEdit.TabIndex = 5;
            this.pnlSoundGeneratorPatchEdit.Visible = false;
            // 
            // tbVolumeOverride
            // 
            this.tbVolumeOverride.Location = new System.Drawing.Point(106, 378);
            this.tbVolumeOverride.Name = "tbVolumeOverride";
            this.tbVolumeOverride.Size = new System.Drawing.Size(236, 32);
            this.tbVolumeOverride.TabIndex = 55;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(59, 384);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(34, 18);
            this.label28.TabIndex = 54;
            this.label28.Text = "Vol:";
            // 
            // tbFxPreset5
            // 
            this.tbFxPreset5.Location = new System.Drawing.Point(105, 333);
            this.tbFxPreset5.Name = "tbFxPreset5";
            this.tbFxPreset5.Size = new System.Drawing.Size(236, 32);
            this.tbFxPreset5.TabIndex = 53;
            // 
            // tbFxPreset4
            // 
            this.tbFxPreset4.Location = new System.Drawing.Point(104, 288);
            this.tbFxPreset4.Name = "tbFxPreset4";
            this.tbFxPreset4.Size = new System.Drawing.Size(236, 32);
            this.tbFxPreset4.TabIndex = 51;
            // 
            // tbFxPreset3
            // 
            this.tbFxPreset3.Location = new System.Drawing.Point(103, 244);
            this.tbFxPreset3.Name = "tbFxPreset3";
            this.tbFxPreset3.Size = new System.Drawing.Size(236, 32);
            this.tbFxPreset3.TabIndex = 49;
            // 
            // tbFxPreset2
            // 
            this.tbFxPreset2.Location = new System.Drawing.Point(102, 201);
            this.tbFxPreset2.Name = "tbFxPreset2";
            this.tbFxPreset2.Size = new System.Drawing.Size(236, 32);
            this.tbFxPreset2.TabIndex = 47;
            // 
            // tbFxPreset1
            // 
            this.tbFxPreset1.Location = new System.Drawing.Point(101, 159);
            this.tbFxPreset1.Name = "tbFxPreset1";
            this.tbFxPreset1.Size = new System.Drawing.Size(236, 32);
            this.tbFxPreset1.TabIndex = 45;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(7, 165);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(91, 18);
            this.label26.TabIndex = 44;
            this.label26.Text = "FX Presets:";
            // 
            // btnSoundGeneratorPatchEditOK
            // 
            this.btnSoundGeneratorPatchEditOK.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoundGeneratorPatchEditOK.Location = new System.Drawing.Point(220, 428);
            this.btnSoundGeneratorPatchEditOK.Name = "btnSoundGeneratorPatchEditOK";
            this.btnSoundGeneratorPatchEditOK.Size = new System.Drawing.Size(115, 52);
            this.btnSoundGeneratorPatchEditOK.TabIndex = 40;
            this.btnSoundGeneratorPatchEditOK.Text = "OK";
            this.btnSoundGeneratorPatchEditOK.UseVisualStyleBackColor = true;
            this.btnSoundGeneratorPatchEditOK.Click += new System.EventHandler(this.btnSoundGeneratorPatchEditOK_Click);
            // 
            // btnSoundGeneratorPatchEditCancel
            // 
            this.btnSoundGeneratorPatchEditCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoundGeneratorPatchEditCancel.Location = new System.Drawing.Point(101, 428);
            this.btnSoundGeneratorPatchEditCancel.Name = "btnSoundGeneratorPatchEditCancel";
            this.btnSoundGeneratorPatchEditCancel.Size = new System.Drawing.Size(115, 52);
            this.btnSoundGeneratorPatchEditCancel.TabIndex = 39;
            this.btnSoundGeneratorPatchEditCancel.Text = "Cancel";
            this.btnSoundGeneratorPatchEditCancel.UseVisualStyleBackColor = true;
            this.btnSoundGeneratorPatchEditCancel.Click += new System.EventHandler(this.btnSoundGeneratorPatchEditCancel_Click);
            // 
            // nudSoundGeneratorPatchProgramNo
            // 
            this.nudSoundGeneratorPatchProgramNo.Location = new System.Drawing.Point(102, 114);
            this.nudSoundGeneratorPatchProgramNo.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudSoundGeneratorPatchProgramNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudSoundGeneratorPatchProgramNo.Name = "nudSoundGeneratorPatchProgramNo";
            this.nudSoundGeneratorPatchProgramNo.Size = new System.Drawing.Size(54, 32);
            this.nudSoundGeneratorPatchProgramNo.TabIndex = 30;
            this.nudSoundGeneratorPatchProgramNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudSoundGeneratorPatchBankNo
            // 
            this.nudSoundGeneratorPatchBankNo.Location = new System.Drawing.Point(242, 114);
            this.nudSoundGeneratorPatchBankNo.Maximum = new decimal(new int[] {
            16129,
            0,
            0,
            0});
            this.nudSoundGeneratorPatchBankNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudSoundGeneratorPatchBankNo.Name = "nudSoundGeneratorPatchBankNo";
            this.nudSoundGeneratorPatchBankNo.Size = new System.Drawing.Size(95, 32);
            this.nudSoundGeneratorPatchBankNo.TabIndex = 29;
            this.nudSoundGeneratorPatchBankNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(178, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 18);
            this.label11.TabIndex = 28;
            this.label11.Text = "Bank No:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 120);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 18);
            this.label12.TabIndex = 27;
            this.label12.Text = "Patch No:";
            // 
            // cbSoundGeneratorPatchCategory
            // 
            this.cbSoundGeneratorPatchCategory.FormattingEnabled = true;
            this.cbSoundGeneratorPatchCategory.Location = new System.Drawing.Point(101, 69);
            this.cbSoundGeneratorPatchCategory.Name = "cbSoundGeneratorPatchCategory";
            this.cbSoundGeneratorPatchCategory.Size = new System.Drawing.Size(235, 32);
            this.cbSoundGeneratorPatchCategory.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(21, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 18);
            this.label13.TabIndex = 25;
            this.label13.Text = "Category:";
            // 
            // tbSoundGeneratorPatchName
            // 
            this.tbSoundGeneratorPatchName.Location = new System.Drawing.Point(101, 27);
            this.tbSoundGeneratorPatchName.Name = "tbSoundGeneratorPatchName";
            this.tbSoundGeneratorPatchName.Size = new System.Drawing.Size(236, 32);
            this.tbSoundGeneratorPatchName.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(44, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 18);
            this.label14.TabIndex = 23;
            this.label14.Text = "Name:";
            // 
            // tpMisc
            // 
            this.tpMisc.Controls.Add(this.btnSaveConfig);
            this.tpMisc.Controls.Add(this.label23);
            this.tpMisc.Controls.Add(this.lbPhysicalOutputDevices);
            this.tpMisc.Controls.Add(this.label22);
            this.tpMisc.Controls.Add(this.lbPhysicalInputDevices);
            this.tpMisc.Controls.Add(this.cbPortaitMode);
            this.tpMisc.Controls.Add(this.btnQuit);
            this.tpMisc.Controls.Add(this.lbInputDevices);
            this.tpMisc.Controls.Add(this.label2);
            this.tpMisc.Controls.Add(this.label3);
            this.tpMisc.Controls.Add(this.lbOutputDevices);
            this.tpMisc.Location = new System.Drawing.Point(4, 44);
            this.tpMisc.Name = "tpMisc";
            this.tpMisc.Size = new System.Drawing.Size(1147, 625);
            this.tpMisc.TabIndex = 4;
            this.tpMisc.Text = "Misc";
            this.tpMisc.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(344, 14);
            this.btnSaveConfig.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(287, 54);
            this.btnSaveConfig.TabIndex = 21;
            this.btnSaveConfig.Text = "Save Config";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(18, 456);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(245, 25);
            this.label23.TabIndex = 20;
            this.label23.Text = "Physical Output Devices";
            // 
            // lbPhysicalOutputDevices
            // 
            this.lbPhysicalOutputDevices.FormattingEnabled = true;
            this.lbPhysicalOutputDevices.ItemHeight = 24;
            this.lbPhysicalOutputDevices.Location = new System.Drawing.Point(19, 484);
            this.lbPhysicalOutputDevices.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbPhysicalOutputDevices.Name = "lbPhysicalOutputDevices";
            this.lbPhysicalOutputDevices.Size = new System.Drawing.Size(613, 52);
            this.lbPhysicalOutputDevices.TabIndex = 19;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(18, 196);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(228, 25);
            this.label22.TabIndex = 18;
            this.label22.Text = "Physical Input Devices";
            // 
            // lbPhysicalInputDevices
            // 
            this.lbPhysicalInputDevices.FormattingEnabled = true;
            this.lbPhysicalInputDevices.ItemHeight = 24;
            this.lbPhysicalInputDevices.Location = new System.Drawing.Point(20, 224);
            this.lbPhysicalInputDevices.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lbPhysicalInputDevices.Name = "lbPhysicalInputDevices";
            this.lbPhysicalInputDevices.Size = new System.Drawing.Size(612, 52);
            this.lbPhysicalInputDevices.TabIndex = 17;
            // 
            // cbPortaitMode
            // 
            this.cbPortaitMode.AutoSize = true;
            this.cbPortaitMode.Checked = true;
            this.cbPortaitMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPortaitMode.Location = new System.Drawing.Point(19, 577);
            this.cbPortaitMode.Name = "cbPortaitMode";
            this.cbPortaitMode.Size = new System.Drawing.Size(164, 29);
            this.cbPortaitMode.TabIndex = 16;
            this.cbPortaitMode.Text = "Portrait Mode";
            this.cbPortaitMode.UseVisualStyleBackColor = true;
            this.cbPortaitMode.CheckedChanged += new System.EventHandler(this.cbPortaitMode_CheckedChanged);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(19, 14);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(313, 54);
            this.btnQuit.TabIndex = 15;
            this.btnQuit.Text = "Shutdown";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lbInputDevices
            // 
            this.lbInputDevices.FormattingEnabled = true;
            this.lbInputDevices.ItemHeight = 24;
            this.lbInputDevices.Location = new System.Drawing.Point(19, 109);
            this.lbInputDevices.Name = "lbInputDevices";
            this.lbInputDevices.Size = new System.Drawing.Size(613, 52);
            this.lbInputDevices.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "Logical Input Devices:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Logical Output Devices:";
            // 
            // lbOutputDevices
            // 
            this.lbOutputDevices.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOutputDevices.FormattingEnabled = true;
            this.lbOutputDevices.ItemHeight = 24;
            this.lbOutputDevices.Location = new System.Drawing.Point(19, 371);
            this.lbOutputDevices.Name = "lbOutputDevices";
            this.lbOutputDevices.Size = new System.Drawing.Size(613, 28);
            this.lbOutputDevices.TabIndex = 12;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Chart files|*.pdf;*.rtf|All files|*.*";
            // 
            // metronomeTimer
            // 
            this.metronomeTimer.Enabled = true;
            this.metronomeTimer.Interval = 500;
            this.metronomeTimer.Tick += new System.EventHandler(this.metronomeTimer_Tick);
            // 
            // metronomeFlashTimer
            // 
            this.metronomeFlashTimer.Tick += new System.EventHandler(this.metronomeFlashTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1155, 673);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "JoeMidi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpRandomAccess1.ResumeLayout(false);
            this.tlpRandomAccess.ResumeLayout(false);
            this.tlpRandomAccess.PerformLayout();
            this.tlpSoundGenTreeview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRandomAccessTranspose)).EndInit();
            this.tpShow.ResumeLayout(false);
            this.tlpShowOuter.ResumeLayout(false);
            this.tlpSongSetlistOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).EndInit();
            this.flpAlphaButtons.ResumeLayout(false);
            this.tpSongs.ResumeLayout(false);
            this.pnlPatchEdit.ResumeLayout(false);
            this.pnlPatchEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongPatchProgramNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongPatchBank)).EndInit();
            this.pnlSongEdit.ResumeLayout(false);
            this.pnlSongEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongChartPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSongTranspose)).EndInit();
            this.tlpSongSelOuter.ResumeLayout(false);
            this.tlpSongSelButtons.ResumeLayout(false);
            this.tpSetlists.ResumeLayout(false);
            this.pnlSetlistSongSelector.ResumeLayout(false);
            this.pnlSetlistEdit.ResumeLayout(false);
            this.pnlSetlistEdit.PerformLayout();
            this.tlpSetlistSelOuter.ResumeLayout(false);
            this.tlpSetlistSelButtons.ResumeLayout(false);
            this.tpMappings.ResumeLayout(false);
            this.tlpMappingEditOuter.ResumeLayout(false);
            this.tlpMappingEditButtons.ResumeLayout(false);
            this.tlpMappingEditNameAndButtons.ResumeLayout(false);
            this.tlpMappingEditNameAndButtons.PerformLayout();
            this.pnlMappingEdit.ResumeLayout(false);
            this.pnlMappingEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingDefDamperRemap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMappingDefIniCC7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPBScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingDefTransposeSemis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingDefTransposeOct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingSplitDevice2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMappingSplitDevice1)).EndInit();
            this.tlpMappingEditorPatches.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tpSoundGenerators.ResumeLayout(false);
            this.tlpSoundGeneratorsOuter.ResumeLayout(false);
            this.tlpSoundGeneratorEditButtons.ResumeLayout(false);
            this.pnlSoundGeneratorEdit.ResumeLayout(false);
            this.pnlSoundGeneratorEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVolMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorNumChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorBaseChannel)).EndInit();
            this.pnlSoundGeneratorPatchEdit.ResumeLayout(false);
            this.pnlSoundGeneratorPatchEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorPatchProgramNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoundGeneratorPatchBankNo)).EndInit();
            this.tpMisc.ResumeLayout(false);
            this.tpMisc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpRandomAccess1;
        private System.Windows.Forms.TableLayoutPanel tlpRandomAccess;
        private System.Windows.Forms.TreeView tvProgramPatches;
        private System.Windows.Forms.Button btnExpandRightPane;
        private System.Windows.Forms.Button btnRandAccessCol8;
        private System.Windows.Forms.Button btnRandAccessCol7;
        private System.Windows.Forms.Button btnRandAccessCol6;
        private System.Windows.Forms.Button btnRandAccessCol5;
        private System.Windows.Forms.Button btnRandAccessCol4;
        private System.Windows.Forms.Button btnRandAccessCol3;
        private System.Windows.Forms.Button btnRandAccessCol2;
        private System.Windows.Forms.Button btnRandAccessCol1;
        private System.Windows.Forms.TabPage tpShow;
        private System.Windows.Forms.TabPage tpSongs;
        private System.Windows.Forms.Panel pnlPatchEdit;
        private System.Windows.Forms.TreeView tvSongPatchPatches;
        private System.Windows.Forms.Label lblSongPatchPart;
        private System.Windows.Forms.NumericUpDown nudSongPatchProgramNo;
        private System.Windows.Forms.Label lblSongPatchProgramNo;
        private System.Windows.Forms.NumericUpDown nudSongPatchBank;
        private System.Windows.Forms.TextBox tbSongPatchPart;
        private System.Windows.Forms.Label lblSongPatchBank;
        private System.Windows.Forms.Button btnPatchEditCancel;
        private System.Windows.Forms.Button btnPatchEditOK;
        private System.Windows.Forms.Panel pnlSongEdit;
        private System.Windows.Forms.Button btnPatchDel;
        private System.Windows.Forms.Button btnPatchAdd;
        private System.Windows.Forms.Button btnPatchDown;
        private System.Windows.Forms.Button btnPatchUp;
        private System.Windows.Forms.Button btnSongEditCancel;
        private System.Windows.Forms.Button btnSongEditOK;
        private System.Windows.Forms.Label lblSongPatches;
        private System.Windows.Forms.ListBox lbSongPatches;
        private System.Windows.Forms.TextBox tbSongChart;
        private System.Windows.Forms.Label lblSongChart;
        private System.Windows.Forms.TextBox tbSongArtist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSongTitle;
        private System.Windows.Forms.Label lblSongTitle;
        private System.Windows.Forms.TableLayoutPanel tlpSongSelOuter;
        private MultiButtonColControl2.MultiButtonColControl mbccSongEditSelector;
        private System.Windows.Forms.TableLayoutPanel tlpSongSelButtons;
        private System.Windows.Forms.Button btnSongDel;
        private System.Windows.Forms.Button btnAddSong;
        private System.Windows.Forms.TabPage tpSetlists;
        private System.Windows.Forms.Panel pnlSetlistSongSelector;
        private System.Windows.Forms.TreeView tvSongsForSetlists;
        private System.Windows.Forms.Panel pnlSetlistEdit;
        private System.Windows.Forms.Button btnSetlistDeleteSong;
        private System.Windows.Forms.Button btnSetlistAddSong;
        private System.Windows.Forms.Button btnSetlistSongDown;
        private System.Windows.Forms.Button btnSetlistSongUp;
        private System.Windows.Forms.Button btnSetlistEditCancel;
        private System.Windows.Forms.Button btnSetlistEditOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbSetlistSongs;
        private System.Windows.Forms.TextBox tbSetlistName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tlpSetlistSelOuter;
        private MultiButtonColControl2.MultiButtonColControl mbccSetlistEditSelector;
        private System.Windows.Forms.TableLayoutPanel tlpSetlistSelButtons;
        private System.Windows.Forms.Button btnSetlistAdd;
        private System.Windows.Forms.Button btnSetlistDel;
        private System.Windows.Forms.TabPage tpSoundGenerators;
        private System.Windows.Forms.TableLayoutPanel tlpSoundGeneratorsOuter;
        private System.Windows.Forms.ListView lvSoundGenerators;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TableLayoutPanel tlpSoundGeneratorEditButtons;
        private System.Windows.Forms.Button btnDeleteSoundGenerator;
        private System.Windows.Forms.Button btnAddSoundGenerator;
        private System.Windows.Forms.Panel pnlSoundGeneratorEdit;
        private System.Windows.Forms.Button btnSoundGeneratorEditOK;
        private System.Windows.Forms.Button btnSoundGeneratorEditCancel;
        private System.Windows.Forms.Button btnSoundGeneratorPatchDel;
        private System.Windows.Forms.Button btnSoundGeneratorPatchAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbSoundGeneratorPatches;
        private System.Windows.Forms.NumericUpDown nudSoundGeneratorNumChannels;
        private System.Windows.Forms.NumericUpDown nudSoundGeneratorBaseChannel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbSoundGeneratorDeviceName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbSoundGeneratorName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlSoundGeneratorPatchEdit;
        private System.Windows.Forms.Button btnSoundGeneratorPatchEditOK;
        private System.Windows.Forms.Button btnSoundGeneratorPatchEditCancel;
        private System.Windows.Forms.NumericUpDown nudSoundGeneratorPatchProgramNo;
        private System.Windows.Forms.NumericUpDown nudSoundGeneratorPatchBankNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbSoundGeneratorPatchCategory;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbSoundGeneratorPatchName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tpMisc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbOutputDevices;
        private System.Windows.Forms.TableLayoutPanel tlpSoundGenTreeview;
        private System.Windows.Forms.Button btnPatchTreeViewBySG;
        private System.Windows.Forms.Button btnPatchTreeViewByCategory;
        private System.Windows.Forms.TabPage tpRandomAccess2;
        private System.Windows.Forms.TabPage tpRandomAccess3;
        private System.Windows.Forms.TabPage tpRandomAccess4;
        private System.Windows.Forms.TabPage tpMappings;
        private System.Windows.Forms.TableLayoutPanel tlpMappingEditOuter;
        private System.Windows.Forms.TableLayoutPanel tlpMappingEditButtons;
        private System.Windows.Forms.Button btnMappingDelete;
        private System.Windows.Forms.Button btnMappingAdd;
        private System.Windows.Forms.TableLayoutPanel tlpMappingEditNameAndButtons;
        private System.Windows.Forms.Button btnMappingEditOK;
        private System.Windows.Forms.Button btnMappingEditCancel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbMappingName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown nudSongTranspose;
        private System.Windows.Forms.NumericUpDown nudRandomAccessTranspose;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbRandomAccessInputDevice;
        private System.Windows.Forms.ListBox lbInputDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuit;
        private System.ServiceProcess.ServiceController serviceController1;
        private System.Windows.Forms.VScrollBar vsbVol1;
        private System.Windows.Forms.TableLayoutPanel tlpShowOuter;
        private System.Windows.Forms.TableLayoutPanel tlpSongSetlistOuter;
        private BrightIdeasSoftware.ObjectListView olvSongs;
        private BrightIdeasSoftware.OLVColumn olvColSong;
        private System.Windows.Forms.Button btnSetlists;
        private System.Windows.Forms.Button btnNextSong;
        private System.Windows.Forms.Button btnSetlistSongOrderToggle;
        private System.Windows.Forms.Button btnPrevSong;
        private MultiButtonColControl2.MultiButtonColControl mbccShowSongPatches;
        private System.Windows.Forms.VScrollBar vsbVol2;
        private PdfiumViewer.PdfRenderer pdfChart;
        private System.Windows.Forms.RichTextBox rtbChart;
        private System.Windows.Forms.CheckBox cbPortaitMode;
        private System.Windows.Forms.Button btnSetlistExpand;
        private System.Windows.Forms.FlowLayoutPanel flpAlphaButtons;
        private System.Windows.Forms.Button btnAlpha0;
        private System.Windows.Forms.Button btnAlphaA;
        private System.Windows.Forms.Button btnAlphaB;
        private System.Windows.Forms.Button btnAlphaC;
        private System.Windows.Forms.Button btnAlphaD;
        private System.Windows.Forms.Button btnAlpheE;
        private System.Windows.Forms.Button btnAlphaF;
        private System.Windows.Forms.Button btnAlphaG;
        private System.Windows.Forms.Button btnAlphaH;
        private System.Windows.Forms.Button btnAlphaI;
        private System.Windows.Forms.Button btnAlphaJ;
        private System.Windows.Forms.Button btnAlphaK;
        private System.Windows.Forms.Button btnAlphaL;
        private System.Windows.Forms.Button btnAlphaM;
        private System.Windows.Forms.Button btnAlphaN;
        private System.Windows.Forms.Button btnAlphaO;
        private System.Windows.Forms.Button btnAlphaP;
        private System.Windows.Forms.Button btnAlphaQ;
        private System.Windows.Forms.Button btnAlphaR;
        private System.Windows.Forms.Button btnAlphaS;
        private System.Windows.Forms.Button btnAlphaT;
        private System.Windows.Forms.Button btnAlphaU;
        private System.Windows.Forms.Button btnAlphaV;
        private System.Windows.Forms.Button btnAlphaW;
        private System.Windows.Forms.Button btnAlphaX;
        private System.Windows.Forms.Button btnAlphaY;
        private System.Windows.Forms.Button btnAlphaZ;
        private System.Windows.Forms.ComboBox cbSetEditorSonglistSetSelector;
        private System.Windows.Forms.NumericUpDown nudSongChartPage;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnOpenFilesCharts;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MultiButtonColControl2.MultiButtonColControl mbrcMappingSelect;
        private System.Windows.Forms.Panel pnlMappingEdit;
        private System.Windows.Forms.Label lblMappingPBScale;
        private System.Windows.Forms.Label lblMappingTransOcts;
        private System.Windows.Forms.Label lblMappingTransSemis;
        private System.Windows.Forms.CheckBox cbMappingDefDamperToggle;
        private System.Windows.Forms.NumericUpDown nudMappingDefDamperRemap;
        private System.Windows.Forms.CheckBox cbMappingDefDamperEna;
        private System.Windows.Forms.TrackBar tbMappingDefIniCC7;
        private System.Windows.Forms.CheckBox cbMappingDefVolEna;
        private System.Windows.Forms.CheckBox cbMappingDefModWheelEna;
        private System.Windows.Forms.Label lblMappingEditPBScale;
        private System.Windows.Forms.TrackBar tbPBScale;
        private System.Windows.Forms.NumericUpDown nudMappingDefTransposeSemis;
        private System.Windows.Forms.Label lblMappingEditTranspose;
        private System.Windows.Forms.NumericUpDown nudMappingDefTransposeOct;
        private System.Windows.Forms.CheckBox cbMappingSplitDevice2;
        private System.Windows.Forms.NumericUpDown nudMappingSplitDevice2;
        private System.Windows.Forms.CheckBox cbMappingSplitDevice1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nudMappingSplitDevice1;
        private System.Windows.Forms.Label lblMappingInputDevice2;
        private System.Windows.Forms.ListBox lbMappingDevice2LowerPatches;
        private System.Windows.Forms.ListBox lbMappingDevice2UpperPatches;
        private System.Windows.Forms.Label lblMappingInputDevice1;
        private System.Windows.Forms.ListBox lbMappingDevice1LowerPatches;
        private System.Windows.Forms.ListBox lbMappingDevice1UpperPatches;
        private System.Windows.Forms.TableLayoutPanel tlpMappingEditorPatches;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnMappingEditPatchTreeViewBySG;
        private System.Windows.Forms.Button btnMappingEditPatchTreeViewByCategory;
        private System.Windows.Forms.TreeView tvMappingEditorPrograms;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ListBox lbPhysicalInputDevices;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ListBox lbPhysicalOutputDevices;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown nudVolMax;
        private System.Windows.Forms.NumericUpDown nudVolMin;
        private System.Windows.Forms.Button btnSetlistSort;
        private System.Windows.Forms.Timer metronomeTimer;
        private System.Windows.Forms.Timer metronomeFlashTimer;
        private System.Windows.Forms.NumericUpDown nudBPM;
        private System.Windows.Forms.Label lblBpm;
        private System.Windows.Forms.TextBox tbTrackName;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbFxPreset5;
        private System.Windows.Forms.TextBox tbFxPreset4;
        private System.Windows.Forms.TextBox tbFxPreset3;
        private System.Windows.Forms.TextBox tbFxPreset2;
        private System.Windows.Forms.TextBox tbFxPreset1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tbDefaultVolume;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbVolumeOverride;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TextBox tbVolume;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cbSoundGeneratorCloneOf;
    }
}

