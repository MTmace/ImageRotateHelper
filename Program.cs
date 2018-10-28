using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImageRotateHelper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            // INSTEAD OF RUNNING A FORM, WE RUN AN ApplicationContext
            Application.Run(new Code.RotateHelperApplicationContext());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("An error occurred while saving image", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

    }
}
