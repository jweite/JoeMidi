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
    public partial class fmPatchPicker : Form
    {
        Form1 parent;
        bool bOK = false;

        // Two organization modes for the patch treeview:  SG (SoundGenerator) or Cat (Category)
        String mappingEditorTreeViewMode = "SG";

        public String SoundGeneratorName { get; set; }

        public String PatchName { get; set; }

        public fmPatchPicker()
        {
            InitializeComponent();
        }

        public void Init(Form1 _parent)
        {
            this.parent = _parent;
            SoundGeneratorName = "";
            PatchName = "";
            btnSG_Click(null, null);
        }

        private void btnSG_Click(object sender, EventArgs e)
        {
            mappingEditorTreeViewMode = "SG";
            parent.populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvFmPatchPickerPrograms, mappingEditorTreeViewMode, false);
            btnSG.BackColor = SystemColors.Highlight;
            btnCat.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnCat_Click(object sender, EventArgs e)
        {
            mappingEditorTreeViewMode = "Cat";
            parent.populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvFmPatchPickerPrograms, mappingEditorTreeViewMode, false);
            btnSG.BackColor = System.Drawing.Color.DimGray;
            btnCat.BackColor = SystemColors.Highlight;
        }

        public bool IsOK { get { return bOK; } }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TreeNode node = tvFmPatchPickerPrograms.SelectedNode;
            if (node == null)
            {
                bOK = false;
                this.Hide();
                return;
            }

            if (node.Parent == null)    // Just an SG selected
            {
                SoundGeneratorName = node.Text;
                PatchName = "";
            }
            else
            {
                PatchName = node.Text;
                SoundGeneratorName = node.Parent.Text;
            }
            bOK = true;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bOK = false;
            this.Hide();
        }

        public DialogResult ShowMe(Form parentForm)
        {
            foreach (TreeNode parent in tvFmPatchPickerPrograms.Nodes)
            {
                if (parent.Text.Equals(this.SoundGeneratorName))
                {
                    tvFmPatchPickerPrograms.SelectedNode = parent;
                    foreach (TreeNode child in parent.Nodes)
                    {
                        if (child.Text.Equals(this.PatchName))
                        {
                            tvFmPatchPickerPrograms.SelectedNode = child;
                        }
                    }
                }
            }
            tvFmPatchPickerPrograms.Focus();
            var dialogResult = this.ShowDialog(parentForm);
            this.Activate();
            this.BringToFront();
            return dialogResult;
        }

        private void tvFmPatchPickerPrograms_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }

        private void fmPatchPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
