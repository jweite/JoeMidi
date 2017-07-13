namespace JoeMidi1
{
    partial class ucShowSetlist
    {
        /// <summary> 
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpSetlistsOuter = new System.Windows.Forms.TableLayoutPanel();
            this.olvSongs = new BrightIdeasSoftware.ObjectListView();
            this.olvColSong = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnSetlists = new System.Windows.Forms.Button();
            this.btnNextSong = new System.Windows.Forms.Button();
            this.btnSetlistSongOrderToggle = new System.Windows.Forms.Button();
            this.btnPrevSong = new System.Windows.Forms.Button();
            this.tlpSetlistsOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpSetlistsOuter
            // 
            this.tlpSetlistsOuter.ColumnCount = 4;
            this.tlpSetlistsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSetlistsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSetlistsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSetlistsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSetlistsOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSetlistsOuter.Controls.Add(this.olvSongs, 0, 1);
            this.tlpSetlistsOuter.Controls.Add(this.btnSetlists, 1, 0);
            this.tlpSetlistsOuter.Controls.Add(this.btnNextSong, 3, 0);
            this.tlpSetlistsOuter.Controls.Add(this.btnSetlistSongOrderToggle, 2, 0);
            this.tlpSetlistsOuter.Controls.Add(this.btnPrevSong, 0, 0);
            this.tlpSetlistsOuter.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlpSetlistsOuter.Location = new System.Drawing.Point(0, 0);
            this.tlpSetlistsOuter.Name = "tlpSetlistsOuter";
            this.tlpSetlistsOuter.RowCount = 3;
            this.tlpSetlistsOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpSetlistsOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSetlistsOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSetlistsOuter.Size = new System.Drawing.Size(375, 435);
            this.tlpSetlistsOuter.TabIndex = 2;
            this.tlpSetlistsOuter.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpSetlistsOuter_Paint);
            // 
            // olvSongs
            // 
            this.olvSongs.AllColumns.Add(this.olvColSong);
            this.olvSongs.BackColor = System.Drawing.Color.DimGray;
            this.olvSongs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColSong});
            this.tlpSetlistsOuter.SetColumnSpan(this.olvSongs, 4);
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
            this.olvSongs.Size = new System.Drawing.Size(369, 353);
            this.olvSongs.TabIndex = 0;
            this.olvSongs.UseCompatibleStateImageBehavior = false;
            this.olvSongs.View = System.Windows.Forms.View.Details;
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
            this.btnSetlists.Location = new System.Drawing.Point(96, 3);
            this.btnSetlists.Name = "btnSetlists";
            this.btnSetlists.Size = new System.Drawing.Size(87, 50);
            this.btnSetlists.TabIndex = 1;
            this.btnSetlists.Text = "SetL";
            this.btnSetlists.UseVisualStyleBackColor = false;
            // 
            // btnNextSong
            // 
            this.btnNextSong.BackColor = System.Drawing.Color.DimGray;
            this.btnNextSong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNextSong.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnNextSong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextSong.Font = new System.Drawing.Font("Wingdings", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnNextSong.ForeColor = System.Drawing.Color.White;
            this.btnNextSong.Location = new System.Drawing.Point(282, 3);
            this.btnNextSong.Name = "btnNextSong";
            this.btnNextSong.Size = new System.Drawing.Size(90, 50);
            this.btnNextSong.TabIndex = 2;
            this.btnNextSong.Text = "ê";
            this.btnNextSong.UseVisualStyleBackColor = false;
            // 
            // btnSetlistSongOrderToggle
            // 
            this.btnSetlistSongOrderToggle.BackColor = System.Drawing.Color.DimGray;
            this.btnSetlistSongOrderToggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetlistSongOrderToggle.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnSetlistSongOrderToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetlistSongOrderToggle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetlistSongOrderToggle.ForeColor = System.Drawing.Color.White;
            this.btnSetlistSongOrderToggle.Location = new System.Drawing.Point(189, 3);
            this.btnSetlistSongOrderToggle.Name = "btnSetlistSongOrderToggle";
            this.btnSetlistSongOrderToggle.Size = new System.Drawing.Size(87, 50);
            this.btnSetlistSongOrderToggle.TabIndex = 4;
            this.btnSetlistSongOrderToggle.Text = "Alpha";
            this.btnSetlistSongOrderToggle.UseVisualStyleBackColor = false;
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
            this.btnPrevSong.Size = new System.Drawing.Size(87, 50);
            this.btnPrevSong.TabIndex = 5;
            this.btnPrevSong.Text = "é";
            this.btnPrevSong.UseVisualStyleBackColor = false;
            // 
            // ucShowSetlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpSetlistsOuter);
            this.Name = "ucShowSetlist";
            this.Size = new System.Drawing.Size(375, 435);
            this.tlpSetlistsOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvSongs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpSetlistsOuter;
        private BrightIdeasSoftware.ObjectListView olvSongs;
        private BrightIdeasSoftware.OLVColumn olvColSong;
        private System.Windows.Forms.Button btnSetlists;
        private System.Windows.Forms.Button btnNextSong;
        private System.Windows.Forms.Button btnSetlistSongOrderToggle;
        private System.Windows.Forms.Button btnPrevSong;
    }
}
