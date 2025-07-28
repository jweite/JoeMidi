using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JoeMidi1
{
    public partial class fmShowFloat : Form
    {
        public fmShowFloat()
        {
            InitializeComponent();
        }

        private void fmShowFloat_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            // this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void fmShowFloat_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
