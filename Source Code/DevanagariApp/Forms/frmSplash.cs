using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DevanagariApp
{
    public partial class frmSplash : Form
    {
        private int i = 0;
        public frmSplash()
        {
            InitializeComponent();
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Files\\");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (i == 100)
                this.Close();
        }
    }
}