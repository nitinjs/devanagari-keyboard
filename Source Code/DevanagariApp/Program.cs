using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DevanagariApp
{
    static class Program
    {
        public static String ApplicationTitle;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region OLD
            try
            {
                Application.SetCompatibleTextRenderingDefault(false);
                Version ver = new Version(Application.ProductVersion);
                frmMain Kb = new frmMain();
                ApplicationTitle = String.Format(Kb.Text);
                if (ClassProcessUtils.ThisProcessIsAlreadyRunning())
                {
                    ClassProcessUtils.SetFocusToPreviousInstance(ApplicationTitle);
                    Kb.Dispose();
                }
                else
                {
                    //Application.EnableVisualStyles();
                    //Application.Run(Kb);

                    Application.EnableVisualStyles();
                    Kb.Visible = false;
                    Application.Run(Kb);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Contact nitin@nitinsawant.com\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //frmMain frm = new frmMain();
            //frm.Visible = false;
            //Application.Run(frm);
        }
    }
}