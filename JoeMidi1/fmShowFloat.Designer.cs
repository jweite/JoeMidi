namespace JoeMidi1
{
    partial class fmShowFloat
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
            this.ucShowSetlist1 = new JoeMidi1.ucShowSetlist();
            this.SuspendLayout();
            // 
            // ucShowSetlist1
            // 
            this.ucShowSetlist1.Location = new System.Drawing.Point(-3, 175);
            this.ucShowSetlist1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucShowSetlist1.Name = "ucShowSetlist1";
            this.ucShowSetlist1.Size = new System.Drawing.Size(500, 343);
            this.ucShowSetlist1.TabIndex = 0;
            // 
            // fmShowFloat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 519);
            this.Controls.Add(this.ucShowSetlist1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "fmShowFloat";
            this.Text = "fmShowPop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmShowFloat_FormClosing);
            this.Load += new System.EventHandler(this.fmShowFloat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucShowSetlist ucShowSetlist1;
    }
}