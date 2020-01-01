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
        // Misc Tab
        //**************************************************************************

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnShowFS_Click(object sender, EventArgs e)            // Archaic...
        {
            fmShowFloat form = new fmShowFloat();
            //            form.Init(mapper.configuration.setlists);
            form.Show();
        }

        private void cbPortaitMode_CheckedChanged(object sender, EventArgs e)
        {
            if (mapper == null || mapper.configuration == null)
                return;

            mapper.configuration.portraitMode = cbPortaitMode.Checked;

            if (mapper.configuration.portraitMode)
            {
                // Portrait
                tlpShowOuter.ColumnStyles[0].SizeType = SizeType.Absolute;          // Vol Slider
                tlpShowOuter.ColumnStyles[0].Width = 50;
                tlpShowOuter.ColumnStyles[1].SizeType = SizeType.Percent;           // Even Split for Setlist/Patch controls.  Chart spans.
                tlpShowOuter.ColumnStyles[1].Width = 50;
                tlpShowOuter.ColumnStyles[2].SizeType = SizeType.Percent;           // Even Split for Setlist/Patch controls.  Chart spans.
                tlpShowOuter.ColumnStyles[2].Width = 50;
                tlpShowOuter.RowStyles[0].SizeType = SizeType.Percent;              // Remainder (after Row[1]) for Chart
                tlpShowOuter.RowStyles[0].Height = 100;
                tlpShowOuter.RowStyles[1].SizeType = SizeType.Absolute;             // Fixed for Setlist/Patch
                tlpShowOuter.RowStyles[1].Height = 300;

                tlpShowOuter.SetColumn(tlpSongSetlistOuter, 1);
                tlpShowOuter.SetRow(tlpSongSetlistOuter, 1);
                tlpShowOuter.SetColumn(mbccShowSongPatches, 2);
                tlpShowOuter.SetRow(mbccShowSongPatches, 1);

                pnlPatchEdit.Left = pnlSongEdit.Left;
                pnlPatchEdit.Top = pnlSongEdit.Top + pnlSongEdit.Height;

                pnlSetlistSongSelector.Left = pnlSetlistEdit.Left;
                pnlSetlistSongSelector.Top = pnlSetlistEdit.Top + pnlSetlistEdit.Height;

                tlpMappingEditOuter.SetRowSpan(mbrcMappingSelect, 2);
                tlpMappingEditOuter.SetColumnSpan(pnlMappingEdit, 2);
                tlpMappingEditOuter.SetColumnSpan(tlpMappingEditNameAndButtons, 2);
                tlpMappingEditOuter.RowStyles[1].Height = 40;
                tlpMappingEditOuter.RowStyles[2].Height = 60;
                tlpMappingEditOuter.SetColumn(tlpMappingEditorPatches, 1);
                tlpMappingEditOuter.SetRow(tlpMappingEditorPatches, 2);

                tlpSoundGeneratorsOuter.SetRow(pnlSoundGeneratorPatchEdit, 2);
                tlpSoundGeneratorsOuter.SetColumn(pnlSoundGeneratorPatchEdit, 1);
                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorPatchEdit, 2);
                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorEdit, 2);
                tlpSoundGeneratorsOuter.RowStyles[1].Height = 50;
                tlpSoundGeneratorsOuter.RowStyles[2].Height = 50;
            }
            else
            {
                // Landscape
                tlpShowOuter.ColumnStyles[0].SizeType = SizeType.Absolute;          // Vol Slider
                tlpShowOuter.ColumnStyles[0].Width = 50;
                tlpShowOuter.ColumnStyles[1].SizeType = SizeType.Absolute;          // Setlist/Patch col
                tlpShowOuter.ColumnStyles[1].Width = 320;
                tlpShowOuter.ColumnStyles[2].SizeType = SizeType.Percent;           // Chart (spans)
                tlpShowOuter.ColumnStyles[2].Width = 100;
                tlpShowOuter.RowStyles[0].SizeType = SizeType.Percent;              // Setlist (Chart spans)
                tlpShowOuter.RowStyles[0].Height = 50;
                tlpShowOuter.RowStyles[1].SizeType = SizeType.Percent;              // Patches (Chart spans)
                tlpShowOuter.RowStyles[1].Height = 50;

                tlpShowOuter.SetColumn(tlpSongSetlistOuter, 1);
                tlpShowOuter.SetRow(tlpSongSetlistOuter, 0);
                tlpShowOuter.SetColumn(mbccShowSongPatches, 1);
                tlpShowOuter.SetRow(mbccShowSongPatches, 1);

                pnlPatchEdit.Top = pnlSongEdit.Top;
                pnlPatchEdit.Left = pnlSongEdit.Left + pnlSongEdit.Width + 70;

                pnlSetlistSongSelector.Top = pnlSetlistEdit.Top;
                pnlSetlistSongSelector.Left = pnlSetlistEdit.Left + pnlSetlistEdit.Width + 70;

                tlpMappingEditOuter.SetRowSpan(mbrcMappingSelect, 1);
                tlpMappingEditOuter.SetColumnSpan(pnlMappingEdit, 1);
                tlpMappingEditOuter.SetColumnSpan(tlpMappingEditNameAndButtons, 2);
                tlpMappingEditOuter.RowStyles[1].Height = 90;
                tlpMappingEditOuter.RowStyles[2].Height = 10;
                tlpMappingEditOuter.SetColumn(tlpMappingEditorPatches, 2);
                tlpMappingEditOuter.SetRow(tlpMappingEditorPatches, 1);

                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorPatchEdit, 1);
                tlpSoundGeneratorsOuter.SetColumnSpan(pnlSoundGeneratorEdit, 1);
                tlpSoundGeneratorsOuter.SetRow(pnlSoundGeneratorPatchEdit, 1);
                tlpSoundGeneratorsOuter.SetColumn(pnlSoundGeneratorPatchEdit, 2);
                tlpSoundGeneratorsOuter.RowStyles[1].Height = 90;
                tlpSoundGeneratorsOuter.RowStyles[2].Height = 10;

            }

            // Call showChart to move/span controls to appropriate position depending on the current chart type
            if (currentSong != null)
            {
                showChart(currentSong.chartFile, currentSong.chartPage);
            }
        }

        private void btnOpenFilesCharts_Click(object sender, EventArgs e)
        {
            string defaultChartFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToLower() + "\\charts";
            openFileDialog1.InitialDirectory = defaultChartFolder;

            string directory, baseFileName;
            try
            {
                FileInfo fi = new FileInfo(tbSongChart.Text);
                directory = fi.Directory.Name;
                baseFileName = fi.Name;
            }
            catch (ArgumentException)
            {
                directory = defaultChartFolder;
                baseFileName = "";
            }

            if (directory.Substring(1, 1) == ":" || directory.Substring(0, 1) == "\\")
            {
                openFileDialog1.InitialDirectory = directory;
            }
            else
            {
                openFileDialog1.InitialDirectory = defaultChartFolder + "\\" + directory;
            }
            openFileDialog1.FileName = baseFileName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.ToLower().StartsWith(defaultChartFolder))
                {
                    tbSongChart.Text = openFileDialog1.FileName.Substring(defaultChartFolder.Length + 1);
                }
                else
                {
                    tbSongChart.Text = openFileDialog1.FileName;
                }
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            mapper.saveConfiguration();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.cbPortaitMode.Checked = (this.Height > this.Width);
        }
    }
}
