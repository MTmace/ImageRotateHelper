namespace ImageRotateHelper
{
    partial class QuickScan
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
            this.btnFolder = new System.Windows.Forms.Button();
            this.txtStartFolder = new System.Windows.Forms.TextBox();
            this.lblStartFolder = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.lstRotated = new System.Windows.Forms.ListBox();
            this.lblRotatedCount = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFolder
            // 
            this.btnFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolder.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFolder.Location = new System.Drawing.Point(417, 9);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(24, 23);
            this.btnFolder.TabIndex = 9;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // txtStartFolder
            // 
            this.txtStartFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartFolder.Location = new System.Drawing.Point(89, 12);
            this.txtStartFolder.Name = "txtStartFolder";
            this.txtStartFolder.Size = new System.Drawing.Size(322, 20);
            this.txtStartFolder.TabIndex = 8;
            this.txtStartFolder.TextChanged += new System.EventHandler(this.txtStartFolder_TextChanged);
            // 
            // lblStartFolder
            // 
            this.lblStartFolder.AutoSize = true;
            this.lblStartFolder.Location = new System.Drawing.Point(24, 15);
            this.lblStartFolder.Name = "lblStartFolder";
            this.lblStartFolder.Size = new System.Drawing.Size(61, 13);
            this.lblStartFolder.TabIndex = 7;
            this.lblStartFolder.Text = "Start Folder";
            // 
            // btnStartScan
            // 
            this.btnStartScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartScan.Enabled = false;
            this.btnStartScan.Location = new System.Drawing.Point(417, 36);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(87, 23);
            this.btnStartScan.TabIndex = 10;
            this.btnStartScan.Text = "Start Scanning";
            this.btnStartScan.UseVisualStyleBackColor = true;
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // lstRotated
            // 
            this.lstRotated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRotated.BackColor = System.Drawing.SystemColors.Menu;
            this.lstRotated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRotated.FormattingEnabled = true;
            this.lstRotated.Location = new System.Drawing.Point(27, 67);
            this.lstRotated.Name = "lstRotated";
            this.lstRotated.Size = new System.Drawing.Size(477, 132);
            this.lstRotated.TabIndex = 11;
            // 
            // lblRotatedCount
            // 
            this.lblRotatedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRotatedCount.AutoSize = true;
            this.lblRotatedCount.Location = new System.Drawing.Point(499, 210);
            this.lblRotatedCount.Name = "lblRotatedCount";
            this.lblRotatedCount.Size = new System.Drawing.Size(0, 13);
            this.lblRotatedCount.TabIndex = 12;
            this.lblRotatedCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(27, 205);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(432, 23);
            this.pbProgress.TabIndex = 13;
            this.pbProgress.Visible = false;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(89, 38);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(322, 20);
            this.txtFilter.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "File Filter";
            // 
            // QuickScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 247);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.lblRotatedCount);
            this.Controls.Add(this.lstRotated);
            this.Controls.Add(this.btnStartScan);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.txtStartFolder);
            this.Controls.Add(this.lblStartFolder);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickScan";
            this.Text = "QuickScan";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox txtStartFolder;
        private System.Windows.Forms.Label lblStartFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.ListBox lstRotated;
        private System.Windows.Forms.Label lblRotatedCount;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label2;
    }
}