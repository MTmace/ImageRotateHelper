using ImageRotateHelper.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageRotateHelper
{
    public partial class QuickScan : Form
    {
        private RotateImage _RotateImage;

        public QuickScan()
        {
            InitializeComponent();

            this.txtStartFolder.Text = ImageRotateHelper.Properties.Settings.Default.JpegDirectory;
            this.txtFilter.Text = ImageRotateHelper.Properties.Settings.Default.FileFilter;

            _RotateImage = new RotateImage();
            _RotateImage.OnRotateComplete += _RotateImage_OnRotateComplete;

            this.pbProgress.Step = 1;
        }

        private void _RotateImage_OnRotateComplete(string imagePath)
        {
            this.lstRotated.Items.Add(imagePath);
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.txtStartFolder.Text))
                this.folderBrowserDialog1.SelectedPath = this.txtStartFolder.Text;

            DialogResult dr = this.folderBrowserDialog1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
                this.txtStartFolder.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void txtStartFolder_TextChanged(object sender, EventArgs e)
        {
            this.btnStartScan.Enabled = this.txtStartFolder.Text.Length > 0;
        }

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            this.lstRotated.Items.Clear();

            var imagePaths = this.txtFilter.Text.Split('|')
                .SelectMany(filter => Directory.GetFiles(this.txtStartFolder.Text, filter, SearchOption.AllDirectories));

            if (imagePaths.Count() > 0)
            {
                this.pbProgress.Maximum = imagePaths.Count();
                
                this.ProcessImages(imagePaths);

            }
            else
                this.lstRotated.Items.Add("no images were found to process");
        }

        private void ProcessImages(IEnumerable<string> imagePaths)
        {
            Code.ExifMetaInfo emi = null;

            try
            {
                this.pbProgress.Visible = true;

                foreach (var imagePath in imagePaths)
                {
                    emi = new Code.ExifMetaInfo(imagePath);
                    _RotateImage.ProcessImage(imagePath, emi);

                    this.pbProgress.PerformStep();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.pbProgress.Visible = false;
            }
        }
    }
}
