using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ImageRotateHelper.Code
{
    public class RotateHelperApplicationContext : ApplicationContext
    {
        #region '   Declarations    '

        NotifyIcon _NotifyIcon = new NotifyIcon();
        ApplicationSettings _SettingsWindow;
        QuickScan _QuickScanWindow;
        System.IO.FileSystemWatcher _DirectoryMonitor;
        MenuItem _MISettingsMenu;
        MenuItem _MIStartMenu;
        MenuItem _MIStopMenu;
        MenuItem _MIQuickScanMenu;
        MenuItem _MIExitMenu;
        RotateImage _RotateImage;

        #endregion
        #region '   Constructors    '

        public RotateHelperApplicationContext()
        {
            this.Initialize();
        }

        #endregion
        #region '   Properties  '

        public System.Drawing.Icon AppIcon
        {
            get
            {
                System.Drawing.Bitmap icon = ImageRotateHelper.Properties.Resources.icon02;
                icon.MakeTransparent(System.Drawing.Color.White);
                System.IntPtr ich = icon.GetHicon();
                icon.Dispose();

                return System.Drawing.Icon.FromHandle(ich);
            }

        }

        #endregion
        #region '   Methods    '

        private void Initialize()
        {
            _MISettingsMenu = new MenuItem("Settings", new EventHandler(MISettings_Click));
            _MIStartMenu = new MenuItem("Start Monitoring", new EventHandler(MIStart_Click));
            _MIStopMenu = new MenuItem("Stop Monitoring", new EventHandler(MIStop_Click));
            _MIQuickScanMenu = new MenuItem("Quick Scan", new EventHandler(MIQuickScan_Click));
            _MIExitMenu = new MenuItem("Exit", new EventHandler(MIExit_Click));

            _NotifyIcon.Icon = this.AppIcon;
            _NotifyIcon.MouseClick += new MouseEventHandler(NotifyIcon_MouseClick);

            _NotifyIcon.ContextMenu = this.BuildContextMenu();
            _NotifyIcon.Visible = true;

            this.InitiateDirectoryMonitor();
            this.StartMonitoring();
            _RotateImage = new RotateImage();
        }

        private ContextMenu BuildContextMenu()
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add(_MIStartMenu);
            cm.MenuItems.Add(_MIStopMenu);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add(_MIQuickScanMenu);
            cm.MenuItems.Add(_MISettingsMenu);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add(_MIExitMenu);

            return cm;
        }

        private void SpoofRightClick()
        {
            // construct a right-click spoof input
            INPUT[] rightClick = new INPUT[2];

            MOUSEINPUT rightDown = new MOUSEINPUT();
            rightDown.dwFlags = Win32.MOUSEEVENTF_RIGHTDOWN + Win32.MOUSEEVENTF_ABSOLUTE;
            rightDown.dx = 0; // 0,0 means current cursor position
            rightDown.dy = 0;
            rightDown.time = 0;
            rightDown.mouseData = 0;

            MOUSEINPUT rightUp = new MOUSEINPUT();
            rightUp.dwFlags = Win32.MOUSEEVENTF_RIGHTUP + Win32.MOUSEEVENTF_ABSOLUTE;
            rightUp.dx = 0;
            rightUp.dy = 0;
            rightUp.time = 0;
            rightUp.mouseData = 0;

            rightClick[0] = new INPUT();
            rightClick[0].type = Win32.INPUT_MOUSE;
            rightClick[0].mi = rightDown;

            rightClick[1] = new INPUT();
            rightClick[1].type = Win32.INPUT_MOUSE;
            rightClick[1].mi = rightUp;

            // SEND THE SPOOFED RIGHT-CLICK TO INVOKE THE MENU
            Win32.SendInput(2, rightClick, Marshal.SizeOf(rightClick[0]));
        }

        private void EnabledMenuItems()
        {
            if (string.IsNullOrEmpty(ImageRotateHelper.Properties.Settings.Default.FileFilter) || string.IsNullOrEmpty(ImageRotateHelper.Properties.Settings.Default.JpegDirectory))
                this.InitiateDirectoryMonitor();

            _MIStartMenu.Enabled = !_DirectoryMonitor.EnableRaisingEvents;
            _MIStopMenu.Enabled = _DirectoryMonitor.EnableRaisingEvents;
        }

        private void InitiateDirectoryMonitor()
        {
            _DirectoryMonitor = new System.IO.FileSystemWatcher(ImageRotateHelper.Properties.Settings.Default.JpegDirectory);
            _DirectoryMonitor.IncludeSubdirectories = true;
            _DirectoryMonitor.Filter = ImageRotateHelper.Properties.Settings.Default.FileFilter;
            _DirectoryMonitor.Created += new System.IO.FileSystemEventHandler(DirectoryMonitor_Created);
        }

        private void StartMonitoring()
        {
            if (_DirectoryMonitor == null)
                this.InitiateDirectoryMonitor();

            _DirectoryMonitor.EnableRaisingEvents = true;

            this.EnabledMenuItems();
        }

        private void StopMonitoring()
        {
            _DirectoryMonitor.EnableRaisingEvents = false;
            this.EnabledMenuItems();
        }

        #region '   Events  '

        private void DirectoryMonitor_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            //BitmapOrientationConverter d = new BitmapOrientationConverter();
            //d.Convert(e.FullPath);

            // WAIT A COUPLE OF SECOND TO MAKE SURE IMAGE HAS FINISHED WRITING.
            System.Threading.Thread.Sleep(1000);

            ExifMetaInfo emi = null;

            // DETERMINE IF IMAGE NEEDS TO BE ROTATED IN ITS OWN PROCESS
            try
            {
                emi = new ExifMetaInfo(e.FullPath);

                try
                {
                    // TODO: Log Error
                    _RotateImage.ProcessImage(e.FullPath, emi);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error was encountered when processing the image.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

            }
            catch (Exception)
            {
                // TODO: Log Error
                MessageBox.Show("An error was encountered when reading the Exif Metadata.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            // SIMULATE THE RIGHT MOUSE CLICK
            if (e.Button == MouseButtons.Left)
                this.SpoofRightClick();
        }

        private void MIExit_Click(object sender, EventArgs e)
        {
            // WE MUST MANUALLY TIDY UP AND REMOVE THE ICON BEFORE WE EXIT.
            // OTHERWISE IT WILL BE LEFT BEHIND UNTIL THE USER MOUSES OVER IT.
            _NotifyIcon.Visible = false;

            Application.Exit();
        }

        private void MISettings_Click(object sender, EventArgs e)
        {
            _SettingsWindow = new ApplicationSettings();

            _SettingsWindow.Icon = this.AppIcon;

            // IF WE ARE ALREADY SHOWING THE WINDOW SET FOCUS TO IT
            if (_SettingsWindow.Visible)
                _SettingsWindow.Focus();
            else
                _SettingsWindow.ShowDialog();

            // IF A CHANGE HAS BEEN MADE IN THE SETTING RESTART THE SERVICE
            if (_SettingsWindow.HasChanged)
            {
                this.StopMonitoring();
                _DirectoryMonitor.Dispose();
                this.InitiateDirectoryMonitor();
                this.StartMonitoring();
            }

            _SettingsWindow.Dispose();
        }

        private void MIStart_Click(object sender, EventArgs e)
        {
            this.StartMonitoring();
        }

        private void MIStop_Click(object sender, EventArgs e)
        {
            this.StopMonitoring();
        }

        private void MIQuickScan_Click(object sender, EventArgs e)
        {
            _QuickScanWindow = new QuickScan();

            _QuickScanWindow.Icon = this.AppIcon;

            // IF WE ARE ALREADY SHOWING THE WINDOW SET FOCUS TO IT
            if (_QuickScanWindow.Visible)
                _QuickScanWindow.Focus();
            else
                _QuickScanWindow.ShowDialog();

            _QuickScanWindow.Dispose();
        }

        #endregion

        #endregion
    }
}
