using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MarathiApp
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            label3.Text = "© 2009-" + DateTime.Now.Year + " Nitin Sawant, Mumbai";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.nitinsawant.com");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("iexplore.exe", "http://www.nitinsawant.com");
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("mailto:nitin@nitinsawant.com");
            this.Close();
        }

        private void about_Load(object sender, EventArgs e)
        {
            pictureBox2.Focus();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}