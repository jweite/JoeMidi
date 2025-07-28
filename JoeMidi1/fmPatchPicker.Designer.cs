namespace JoeMidi1
{
    partial class fmPatchPicker
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tvFmPatchPickerPrograms = new System.Windows.Forms.TreeView();
            this.btnSG = new System.Windows.Forms.Button();
            this.btnCat = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvFmPatchPickerPrograms
            // 
            this.tvFmPatchPickerPrograms.BackColor = System.Drawing.Color.Black;
            this.tvFmPatchPickerPrograms.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvFmPatchPickerPrograms.ForeColor = System.Drawing.Color.White;
            this.tvFmPatchPickerPrograms.Location = new System.Drawing.Point(5, 5);
            this.tvFmPatchPickerPrograms.Margin = new System.Windows.Forms.Padding(4);
            this.tvFmPatchPickerPrograms.Name = "tvFmPatchPickerPrograms";
            this.tvFmPatchPickerPrograms.Size = new System.Drawing.Size(441, 566);
            this.tvFmPatchPickerPrograms.TabIndex = 18;
            this.tvFmPatchPickerPrograms.DoubleClick += new System.EventHandler(this.tvFmPatchPickerPrograms_DoubleClick);
            // 
            // btnSG
            // 
            this.btnSG.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSG.Location = new System.Drawing.Point(5, 578);
            this.btnSG.Name = "btnSG";
            this.btnSG.Size = new System.Drawing.Size(214, 63);
            this.btnSG.TabIndex = 19;
            this.btnSG.Text = "SG";
            this.btnSG.UseVisualStyleBackColor = true;
            this.btnSG.Click += new System.EventHandler(this.btnSG_Click);
            // 
            // btnCat
            // 
            this.btnCat.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCat.Location = new System.Drawing.Point(225, 578);
            this.btnCat.Name = "btnCat";
            this.btnCat.Size = new System.Drawing.Size(221, 63);
            this.btnCat.TabIndex = 20;
            this.btnCat.Text = "Cat";
            this.btnCat.UseVisualStyleBackColor = true;
            this.btnCat.Click += new System.EventHandler(this.btnCat_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(225, 647);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(221, 63);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(5, 647);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(214, 63);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // fmPatchPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 720);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCat);
            this.Controls.Add(this.btnSG);
            this.Controls.Add(this.tvFmPatchPickerPrograms);
            this.Name = "fmPatchPicker";
            this.Text = "fmPatchPicker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmPatchPicker_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvFmPatchPickerPrograms;
        private System.Windows.Forms.Button btnSG;
        private System.Windows.Forms.Button btnCat;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}