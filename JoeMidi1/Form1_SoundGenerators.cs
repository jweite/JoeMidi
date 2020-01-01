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
                ListViewItem.ListViewSubItem siLogicalBaseChannel = new ListViewItem.ListViewSubItem(item, String.Format("{0}", sg.channelBase + 1));
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
            nudVolMax.Value = 127;
            nudVolMin.Value = 0;
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
            soundGeneratorBeingEdited.channelBase = (int)nudSoundGeneratorBaseChannel.Value - 1;
            soundGeneratorBeingEdited.nChannels = (int)nudSoundGeneratorNumChannels.Value;
            soundGeneratorBeingEdited.cc7Min = (int)nudVolMin.Value;
            soundGeneratorBeingEdited.cc7Max = (int)nudVolMax.Value;

            if (bCreatingNewSoundGenerator == true)
            {
                soundGeneratorBeingEdited.bind(mapper.configuration.logicalOutputDeviceDict);
                mapper.configuration.soundGenerators.Add(soundGeneratorBeingEdited.name, soundGeneratorBeingEdited);
                mapper.configuration.dirty = true;
            }
            else
            {
                SoundGenerator soundGeneratorToModify = mapper.configuration.soundGenerators[soundGeneratorBeingEdited.name];
                soundGeneratorToModify.deviceName = cbSoundGeneratorDeviceName.Text;
                soundGeneratorToModify.channelBase = (int)nudSoundGeneratorBaseChannel.Value - 1;
                soundGeneratorToModify.nChannels = (int)nudSoundGeneratorNumChannels.Value;
                soundGeneratorToModify.cc7Min = (int)nudVolMin.Value;
                soundGeneratorToModify.cc7Max = (int)nudVolMax.Value;
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

            if (creatingNewSoundGeneratorPatch == true && soundGeneratorBeingEdited.soundGeneratorPatchDict.ContainsKey(patch.name))
            {
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
            if (lbSoundGeneratorPatches.SelectedItem != null)
            {
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
                nudSoundGeneratorBaseChannel.Value = soundGeneratorBeingEdited.channelBase + 1;
                nudSoundGeneratorNumChannels.Value = soundGeneratorBeingEdited.nChannels;
                nudVolMin.Value = soundGeneratorBeingEdited.cc7Min;
                nudVolMax.Value = soundGeneratorBeingEdited.cc7Max;

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
    }
}
