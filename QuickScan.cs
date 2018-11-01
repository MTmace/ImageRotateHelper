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
        public QuickScan()
        {
            InitializeComponent();

            this.txtStartFolder.Text = ImageRotateHelper.Properties.Settings.Default.JpegDirectory;
            this.txtFilter.Text = ImageRotateHelper.Properties.Settings.Default.FileFilter;
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
            this.lblRotatedCount.Text = string.Empty;
            this.lstRotated.Items.Clear();

            IEnumerable<FileInfo> images = this.GetImages(this.txtStartFolder.Text);
            int imageCount = images.Count();

            if (imageCount > 0)
                this.lblRotatedCount.Text = imageCount.ToString();
            else
                this.lstRotated.Items.Add("no images were found to process");
        }

        private IEnumerable<FileInfo> GetImages(string directoryPath)
        {
            List<FileInfo> images = new List<FileInfo>();

            try
            {
                foreach (string f in Directory.GetFiles(directoryPath, this.txtFilter.Text, SearchOption.AllDirectories))
                {
                    images.Add(new FileInfo(f));
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return images;
        }
    }
}
