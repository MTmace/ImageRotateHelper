using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageRotateHelper
{
    public partial class ApplicationSettings : Form
    {
        public ApplicationSettings()
        {
            InitializeComponent();
            this.txtJpegFolder.Text = ImageRotateHelper.Properties.Settings.Default.JpegDirectory;
            this.txtFilter.Text = ImageRotateHelper.Properties.Settings.Default.FileFilter;
            this.HasChanged = false;

            this.txtJpegFolder.Focus();
        }

        public bool HasChanged 
        { 
            get; 
            private set; 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // VALIDATE DIRECTORY PATH
            if (System.IO.Directory.Exists(this.txtJpegFolder.Text))
            {
                if (ImageRotateHelper.Properties.Settings.Default.JpegDirectory != this.txtJpegFolder.Text || ImageRotateHelper.Properties.Settings.Default.FileFilter != this.txtFilter.Text)
                {
                    this.HasChanged = true;
                    ImageRotateHelper.Properties.Settings.Default.JpegDirectory = this.txtJpegFolder.Text;
                    ImageRotateHelper.Properties.Settings.Default.FileFilter = this.txtFilter.Text;
                    
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Jpeg directory is not valid.", "Directory Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtJpegFolder.Focus();
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.txtJpegFolder.Text))
                this.folderBrowserDialog1.SelectedPath = this.txtJpegFolder.Text;

            DialogResult dr = this.folderBrowserDialog1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
                this.txtJpegFolder.Text = this.folderBrowserDialog1.SelectedPath;
        }
    }
}
