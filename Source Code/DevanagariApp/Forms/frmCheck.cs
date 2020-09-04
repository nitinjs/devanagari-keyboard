using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DevanagariApp
{
    public partial class frmCheck : Form
    {
        public frmCheck()
        {
            InitializeComponent();
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        public string ActiveWindow;

        // Declare external functions.
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private delegate void myDelegate();

        private void getActiveForm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new myDelegate(getActiveForm),new object[]{});
                return;
            }

            int chars = 256;
            StringBuilder buff = new StringBuilder(chars);

            // Obtain the handle of the active window.
            IntPtr handle = GetForegroundWindow();
            
            // Update the controls.
            if (GetWindowText(handle, buff, chars) > 0)
            {
                if (buff.ToString() != "Marathi Keyboard")
                {
                    this.ActiveWindow = buff.ToString();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.getActiveForm();
        }
    }
}