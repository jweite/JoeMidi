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
    public partial class fmSetlistPicker : Form
    {
        bool bOK = false;

        public fmSetlistPicker()
        {
            InitializeComponent();
        }

        public void Init(List<Setlist> setlists)
        {
            lbSetlists.Items.Clear();
            foreach (Setlist setlist in setlists)
            {
                lbSetlists.Items.Add(setlist.name);
            }
        }

        public String Choice { get { return (String)lbSetlists.SelectedItem; } }

        public int ChoiceIndex { get { return lbSetlists.SelectedIndex; } }

        public bool IsOK { get { return bOK; } }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bOK = true;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bOK = false;
            this.Hide();
        }

        private void lbSetlists_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(btnOK, null);
        }

        private void lbSetlists_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fmSetlistPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
