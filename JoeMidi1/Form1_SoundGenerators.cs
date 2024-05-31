using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

            List<String> soundGeneratorNames = new List<String>(mapper.configuration.soundGenerators.Keys);
            soundGeneratorNames.Sort();

            foreach (String soundGeneratorName in soundGeneratorNames)
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
            tbTrackName.Text = "";
            tbDefaultVolume.Text = "";
            refreshCbSoundGeneratorDeviceName(null);
            refreshCbSoundGeneratorCloneOf(null);
            lbSoundGeneratorPatches.Items.Clear();
            btnSoundGeneratorPatchAdd.Enabled = true;
            btnSoundGeneratorPatchDel.Enabled = true;

            bCreatingNewSoundGenerator = true;

            pnlSoundGeneratorEdit.Visible = true;

        }

        private void btnSoundGeneratorEditCancel_Click(object sender, EventArgs e)
        {
            bCreatingNewSoundGenerator = false;
            lvSoundGenerators.SelectedIndices.Clear();
            btnSoundGeneratorPatchEditCancel_Click(sender, e);
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

        private void refreshCbSoundGeneratorCloneOf(String soundGeneratorToSelect)
        {
            cbSoundGeneratorCloneOf.Items.Clear();
            cbSoundGeneratorCloneOf.Items.Add("<None>");

            List<String> soundGeneratorNames = new List<String>(mapper.configuration.soundGenerators.Keys);
            soundGeneratorNames.Sort();
            foreach (String generatorName in soundGeneratorNames)
            {
                cbSoundGeneratorCloneOf.Items.Add(generatorName);
            }

            cbSoundGeneratorCloneOf.Text = soundGeneratorToSelect != null ? soundGeneratorToSelect : cbSoundGeneratorCloneOf.Items[0].ToString();
        }

        private bool parseNullableDouble(String s, out double? d)
        {
            double temp;
            var result = double.TryParse(s, out temp);
            // Ternary can't deal with double? return 
            if (result) {
                d = temp;
            }
            else
            {
                d = null;
            }
            return result;
        }

        private void btnSoundGeneratorEditOK_Click(object sender, EventArgs e)
        {
            // Complete any in-progress patch editing
            if (pnlSoundGeneratorPatchEdit.Visible)
            {
                btnSoundGeneratorPatchEditOK_Click(sender, e);
            }

            if (bCreatingNewSoundGenerator == true)
            {
                soundGeneratorBeingEdited.name = tbSoundGeneratorName.Text;
            }
            soundGeneratorBeingEdited.deviceName = cbSoundGeneratorDeviceName.Text;
            soundGeneratorBeingEdited.channelBase = (int)nudSoundGeneratorBaseChannel.Value - 1;
            soundGeneratorBeingEdited.nChannels = (int)nudSoundGeneratorNumChannels.Value;
            soundGeneratorBeingEdited.cc7Min = (int)nudVolMin.Value;
            soundGeneratorBeingEdited.cc7Max = (int)nudVolMax.Value;
            soundGeneratorBeingEdited.track = tbTrackName.Text;
            parseNullableDouble(tbDefaultVolume.Text, out soundGeneratorBeingEdited.volume);
            if (cbSoundGeneratorCloneOf.Text != "<None>")
            {
                soundGeneratorBeingEdited.clonePatchesFrom = cbSoundGeneratorCloneOf.Text;
            }

            if (bCreatingNewSoundGenerator == true)
            {
                soundGeneratorBeingEdited.bind(mapper.configuration.logicalOutputDeviceDict, mapper.configuration.soundGenerators);
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
                soundGeneratorToModify.track = tbTrackName.Text;
                parseNullableDouble(tbDefaultVolume.Text, out soundGeneratorToModify.volume);

                soundGeneratorToModify.soundGeneratorPatchDict.Clear();
                foreach (String patchName in soundGeneratorBeingEdited.soundGeneratorPatchDict.Keys)
                {
                    SoundGeneratorPatch patch = soundGeneratorBeingEdited.soundGeneratorPatchDict[patchName];
                    soundGeneratorToModify.soundGeneratorPatchDict.Add(patch.name, patch);
                }
                soundGeneratorToModify.bind(mapper.configuration.logicalOutputDeviceDict, mapper.configuration.soundGenerators);
                mapper.configuration.dirty = true;
            }
            refreshSoundGeneratorsListView();
            populateTreeViewWithSoundGeneratorsPatchesAndMappings(tvMappingEditorPrograms, mappingEditorTreeViewMode, false);

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
            tbFxPreset1.Text = "";
            tbFxPreset2.Text = "";
            tbFxPreset3.Text = "";
            tbFxPreset4.Text = "";
            tbFxPreset5.Text = "";
            tbVolumeOverride.Text = "";

            // If this is a bank-less SG propose the next highest PC

            Boolean bankless = true;
            int maxPc = -1;
            foreach (SoundGeneratorPatch patch in soundGeneratorBeingEdited.soundGeneratorPatchDict.Values)
            {
                if (patch.soundGeneratorBank >= 0) {
                    bankless = false;
                }
                if (patch.soundGeneratorPatchNumber > maxPc)
                {
                    maxPc = patch.soundGeneratorPatchNumber;
                }
            }
            if (bankless && maxPc < 127)
            {
                nudSoundGeneratorPatchProgramNo.Value = maxPc + 1;
            }

            creatingNewSoundGeneratorPatch = true;

            pnlSoundGeneratorPatchEdit.Visible = true;

        }

        private void lbSoundGeneratorPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSoundGeneratorPatches.SelectedItem != null)
            {
                String selectedSoundGeneratorPatchName = (String)lbSoundGeneratorPatches.SelectedItem;

                // Trim the added Bank/PC suffix if present.
                int pc_suffix_start = selectedSoundGeneratorPatchName.LastIndexOf(" (");
                if (pc_suffix_start > 0)
                {
                    selectedSoundGeneratorPatchName = selectedSoundGeneratorPatchName.Substring(0, pc_suffix_start);
                }


                SoundGeneratorPatch patch = soundGeneratorBeingEdited.soundGeneratorPatchDict[selectedSoundGeneratorPatchName];
                tbSoundGeneratorPatchName.Text = patch.name;
                tbSoundGeneratorPatchName.Enabled = false;
                cbSoundGeneratorPatchCategory.Text = patch.patchCategoryName;
                nudSoundGeneratorPatchBankNo.Value = patch.soundGeneratorBank;
                nudSoundGeneratorPatchProgramNo.Value = patch.soundGeneratorPatchNumber;
                pnlSoundGeneratorPatchEdit.Visible = true;
                if (patch.fxPresets != null)
                {
                    tbFxPreset1.Text = (patch.fxPresets.Count > 0) ? patch.fxPresets[0] : "";
                    tbFxPreset2.Text = (patch.fxPresets.Count > 1) ? patch.fxPresets[1] : "";
                    tbFxPreset3.Text = (patch.fxPresets.Count > 2) ? patch.fxPresets[2] : "";
                    tbFxPreset4.Text = (patch.fxPresets.Count > 3) ? patch.fxPresets[3] : "";
                    tbFxPreset5.Text = (patch.fxPresets.Count > 4) ? patch.fxPresets[4] : "";
                }
                tbVolumeOverride.Text = (patch.volumeOverride != null) ? String.Format("{0:0.0}", patch.volumeOverride) : "";

                creatingNewSoundGeneratorPatch = false;
            }
        }

        private void btnSoundGeneratorPatchEditCancel_Click(object sender, EventArgs e)
        {
            pnlSoundGeneratorPatchEdit.Visible = false;
        }

        private void btnSoundGeneratorPatchEditOK_Click(object sender, EventArgs e)
        {
            if (tbSoundGeneratorPatchName.Text.Length == 0)
            {
                MessageBox.Show("Patch name cannot be blank.");
                return;
            }
            SoundGeneratorPatch patch = new SoundGeneratorPatch();
            patch.name = tbSoundGeneratorPatchName.Text;
            patch.patchCategoryName = cbSoundGeneratorPatchCategory.Text;
            patch.soundGeneratorBank = (int)nudSoundGeneratorPatchBankNo.Value;
            patch.soundGeneratorPatchNumber = (int)nudSoundGeneratorPatchProgramNo.Value;
            patch.fxPresets.Clear();
            if (tbFxPreset1.Text.Length > 0) patch.fxPresets.Add(tbFxPreset1.Text);
            if (tbFxPreset2.Text.Length > 0) patch.fxPresets.Add(tbFxPreset2.Text);
            if (tbFxPreset3.Text.Length > 0) patch.fxPresets.Add(tbFxPreset3.Text);
            if (tbFxPreset4.Text.Length > 0) patch.fxPresets.Add(tbFxPreset4.Text);
            if (tbFxPreset5.Text.Length > 0) patch.fxPresets.Add(tbFxPreset5.Text);
            parseNullableDouble(tbVolumeOverride.Text, out patch.volumeOverride);

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
            List<String> patches = new List<String>(soundGeneratorBeingEdited.soundGeneratorPatchDict.Keys);
            foreach (String patchName in patches)
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
                tbTrackName.Text = soundGeneratorBeingEdited.track;
                tbDefaultVolume.Text = (soundGeneratorBeingEdited.volume != null) ? String.Format("{0:0.0}", soundGeneratorBeingEdited.volume) : "";

                bool cloningPatches = soundGeneratorBeingEdited.clonePatchesFrom != null && soundGeneratorBeingEdited.clonePatchesFrom != "";
                btnSoundGeneratorPatchDel.Enabled = !cloningPatches;
                btnSoundGeneratorPatchAdd.Enabled = !cloningPatches;

                lbSoundGeneratorPatches.Items.Clear();
                lbSoundGeneratorPatches.Items.Clear();
                List<SoundGeneratorPatch> patches = new List<SoundGeneratorPatch>(soundGeneratorBeingEdited.soundGeneratorPatchDict.Values);
                patches.Sort((x, y) => x.name.CompareTo(y.name));
                foreach (SoundGeneratorPatch patch in patches)
                {
                    if (patch.soundGeneratorBank >= 0)
                    {
                        lbSoundGeneratorPatches.Items.Add(String.Format("{0} ({1}:{2})", patch.name, patch.soundGeneratorBank, patch.soundGeneratorPatchNumber));
                    }
                    else
                    {
                        lbSoundGeneratorPatches.Items.Add(String.Format("{0} ({1})", patch.name, patch.soundGeneratorPatchNumber));
                    }
                }
                lbSoundGeneratorPatches.Enabled = !cloningPatches;

                refreshCbSoundGeneratorDeviceName(null);
                refreshCbSoundGeneratorCloneOf(soundGeneratorBeingEdited.clonePatchesFrom);


                bCreatingNewSoundGenerator = false;

                pnlSoundGeneratorEdit.Visible = true;

            }
        }
    }
}
