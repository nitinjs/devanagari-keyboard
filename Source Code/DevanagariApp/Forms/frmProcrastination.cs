using DevanagariApp.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevanagariApp.Forms
{
    public partial class frmProcrastination : Form
    {
        public frmProcrastination()
        {
            InitializeComponent();
        }

        private void frmProcrastination_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Appender.TodaysFile))
            {
                File.Create(Appender.TodaysFile);
            }

            txtLog.Text = Appender.ReadAllText(false);

            //Scroll to end
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void nmThreashold_ValueChanged(object sender, EventArgs e)
        {
            if (nmThreashold.Value <= 1)
            {
                nmThreashold.Value = 1;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(Appender.TodaysFile);
                File.Create(AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("MM-dd-yyyy") + ".bin");
                frmProcrastination_Load(null, null);
            }
            catch
            {
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            tmrMain.Interval = Convert.ToInt32(nmThreashold.Value) * 1000;
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
    }
}
