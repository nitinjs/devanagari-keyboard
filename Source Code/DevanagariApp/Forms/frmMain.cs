//
//   Nitin Sawant
//   http://www.nitinsawant.com
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Media;
using System.IO;
using DevanagariApp.BL;
using DevanagariApp.Forms;

namespace DevanagariApp
{
    public partial class frmMain : Form
    {
        string enableChar;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
        string preChar = "";
        // private Boolean visible = true;
        //private int numThemeFlag = 1;
        UserActivityHook hk;
        SoundPlayer sp;
        private frmCheck chk;
        private frmSplash splash;
        public static String strLastActiveWindow;
        private Point mouse_offset;
        private bool swarFlag = true;
        public static int EnableFlag = 1;
        public static frmProcrastination procrastination;
        public frmMain()
        {
            InitializeComponent();
            hk = new UserActivityHook(true, true);
            hk.KeyPress += new KeyPressEventHandler(hk_KeyPress);
            hk.KeyDown += new KeyEventHandler(hk_KeyDown);
            hk.OnMouseActivity += Hk_OnMouseActivity;
            sp = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "No Sound.wav");
            chk = new frmCheck();
            splash = new frmSplash();
            splash.ShowDialog();
            procrastination = new frmProcrastination();
            this.Visible = true;
        }

        public static DateTime prevTime = DateTime.Now;
        private void Hk_OnMouseActivity(object sender, MouseEventArgs e)
        {
            TimeSpan proCrastination = DateTime.Now - prevTime;
            if (tsProcrastinationEnabled.Checked)
            {
                if (e.Clicks > 0)
                {
                    proCrastination.Procrastinate("clicks: " + e.Clicks);
                    //Appender.AppendLine("Clicks " + e.Clicks + ", Procrastination " + proCrastination.Humanize());
                }
                else
                {
                    proCrastination.Procrastinate("");
                    //Appender.AppendLine("Mouse event, Procrastination " + proCrastination.Humanize());
                }
            }
            prevTime = DateTime.Now;
        }

        void hk_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyData.ToString());
            //LControlKey
            //M
            if ((e.KeyData.ToString() == "M") && (enableChar == "LControlKey"))
            {
                this.enabledToolStripMenuItem_Click(sender, new EventArgs());
                sp.Play();

                ntfTray.BalloonTipText = "सध्याची भाषा: " + (enabledToolStripMenuItem.Checked ? "मराठी" : "इंग्रजी");
                ntfTray.ShowBalloonTip(1);
            }
            enableChar = e.KeyData.ToString();

            TimeSpan proCrastination = DateTime.Now - prevTime;
            if (tsProcrastinationEnabled.Checked)
            {
                proCrastination.Procrastinate(enableChar.ToString());
            }
            //Appender.AppendLine(enableChar + ", Procrastination " + proCrastination.Humanize());
            prevTime = DateTime.Now;
        }

        private int ToASCII(char c)
        {
            int code = (int)c;
            return (code);
        }
        void hk_KeyPress(object sender, KeyPressEventArgs e)
        {
            // MessageBox.Show(e.KeyChar.ToString());
            enableChar = "";
            if (e.KeyChar == (char)12)//L key event
            {
                if (enabledToolStripMenuItem.Checked == true)
                {
                    enabledToolStripMenuItem.Checked = false;
                    EnableFlag = 0;
                    ntfTray.Text = "Marathi Keyboard (Disabled)";
                    this.ntfTray.Icon = this.ntfTemp.Icon;
                }
                else
                {
                    enabledToolStripMenuItem.Checked = true;
                    EnableFlag = 1;
                    ntfTray.Text = "Marathi Keyboard (Enabled)";
                    this.ntfTray.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfTray.Icon")));
                }
                e.Handled = true;
            }
            if (EnableFlag == 1)
            {
                if (e.KeyChar == '$')
                {
                    this.SendMessage("ृ");
                    this.swarFlag = true;
                    e.Handled = true;

                }
                if (e.KeyChar == (char)Keys.Escape)
                {
                    if (swarFlag == false)
                        this.swarFlag = true;
                    else
                        this.swarFlag = false;
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.Back)
                {
                    if (txtTemp.Text.Length > 0)
                        txtTemp.Text = txtTemp.Text.Substring(0, txtTemp.Text.Length - 1);
                    else
                        txtTemp.Text = "";
                    if (swarFlag == false)
                        this.swarFlag = true;
                    else
                        this.swarFlag = false;
                    //sp.Play();
                }
                if (e.KeyChar == (char)Keys.A)
                {
                    if (preChar == "अ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("्");
                        this.swarFlag = true;
                        preChar = "्";
                    }
                    else
                    {
                        this.SendMessage("अ");
                        preChar = "अ";
                    }
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('a'))
                {
                    if (preChar == "आ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ा");
                        this.swarFlag = true;
                        preChar = "ा";
                    }
                    else
                    {
                        this.SendMessage("आ");
                        this.swarFlag = false;
                        preChar = "आ";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('b'))
                {
                    this.SendMessage("ब");
                    this.swarFlag = false;
                    preChar = "ब";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.B)
                {
                    this.SendMessage("भ");
                    this.swarFlag = false;
                    preChar = "भ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('c'))
                {
                    this.SendMessage("च");
                    this.swarFlag = false;
                    preChar = "च";
                    sp.Play();
                    e.Handled = true;
                }

                if (e.KeyChar == (char)Keys.C)
                {
                    this.SendMessage("छ");
                    this.swarFlag = false;
                    preChar = "छ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('d'))
                {
                    this.SendMessage("द");
                    this.swarFlag = false;
                    preChar = "द";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D)
                {
                    this.SendMessage("ड");
                    this.swarFlag = false;
                    preChar = "ड";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('e'))
                {
                    if (preChar == "ए")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("े");
                        preChar = "े";
                        this.swarFlag = true;
                    }
                    else
                    {
                        this.SendMessage("ए");
                        this.swarFlag = false;
                        preChar = "ए";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.E)
                {
                    if (preChar == "ऐ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ै");
                        this.swarFlag = true;
                        preChar = "ै";
                    }
                    else
                    {
                        this.SendMessage("ऐ");
                        this.swarFlag = false;
                        preChar = "ऐ";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('f'))
                {
                    this.SendMessage("फ");
                    this.swarFlag = false;
                    preChar = "फ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('g'))
                {
                    this.SendMessage("ग");
                    this.swarFlag = false;
                    preChar = "ग";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.G)
                {
                    this.SendMessage("घ");
                    this.swarFlag = false;
                    preChar = "घ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII(':'))
                {
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ः");
                        this.swarFlag = true;
                        preChar = "ः";
                        sp.Play();
                        e.Handled = true;
                    }
                }
                if (e.KeyChar == (char)ToASCII('h'))
                {
                    this.SendMessage("ह");
                    this.swarFlag = false;
                    preChar = "ह";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.H)
                {
                    switch (preChar)
                    {
                        case "ट":
                            {
                                this.SendOneBS();
                                this.SendMessage("ठ");
                                preChar = "ठ";
                            }
                            break;
                        case "त":
                            {
                                this.SendOneBS();
                                this.SendMessage("थ");
                                preChar = "थ";
                            }
                            break;
                        case "ड":
                            {
                                this.SendOneBS();
                                this.SendMessage("ढ");
                                preChar = "ढ";
                            }
                            break;
                        case "न":
                            {
                                this.SendOneBS();
                                this.SendMessage("ण");
                                preChar = "ण";
                            }
                            break;
                        case "द":
                            {
                                this.SendOneBS();
                                this.SendMessage("ध");
                                preChar = "ध";
                            }
                            break;
                        case "ग":
                            {
                                this.SendOneBS();
                                this.SendMessage("घ");
                                preChar = "घ";
                            }
                            break;
                        case "क":
                            {
                                this.SendOneBS();
                                this.SendMessage("ख");
                                preChar = "ख";
                            }
                            break;
                        case "ज":
                            {
                                this.SendOneBS();
                                this.SendMessage("झ");
                                preChar = "झ";
                            }
                            break;
                        case "स":
                            {
                                this.SendOneBS();
                                this.SendMessage("श");
                                preChar = "श";
                            }
                            break;
                        case "प":
                            {
                                this.SendOneBS();
                                this.SendMessage("फ");
                                preChar = "फ";
                            }
                            break;
                        case "ब":
                            {
                                this.SendOneBS();
                                this.SendMessage("भ");
                                preChar = "भ";
                            }
                            break;
                        case "च":
                            {
                                this.SendOneBS();
                                this.SendMessage("छ");
                                preChar = "छ";
                            }
                            break;
                    }
                    preChar = "";
                    this.swarFlag = false;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('i'))
                {
                    if (preChar == "इ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ि");
                        this.swarFlag = true;
                        preChar = "ि";
                    }
                    else
                    {
                        this.SendMessage("इ");
                        this.swarFlag = false;
                        preChar = "इ";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.I)
                {
                    if (preChar == "ई")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ी");
                        preChar = "ी";
                        this.swarFlag = true;
                    }
                    else
                    {
                        this.SendMessage("ई");
                        this.swarFlag = false;
                        preChar = "ई";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('j'))
                {
                    this.SendMessage("ज");
                    this.swarFlag = false;
                    preChar = "ज";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('k'))
                {
                    this.SendMessage("क");
                    this.swarFlag = false;
                    preChar = "क";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.K)
                {
                    this.SendMessage("ख");
                    this.swarFlag = false;
                    preChar = "ख";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('l'))
                {
                    this.SendMessage("ल");
                    this.swarFlag = false;
                    preChar = "ल";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.L)
                {
                    this.SendMessage("ळ");
                    this.swarFlag = false;
                    preChar = "ळ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('m'))
                {
                    this.SendMessage("म");
                    this.swarFlag = false;
                    preChar = "म";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.N)
                {
                    if (preChar == "" || preChar == " ")
                    {
                        this.SendMessage("अं");
                        preChar = "अं";
                    }
                    else
                    {
                        if (preChar == "अं")
                        {
                            this.SendMessage("अं");
                            preChar = "अं";
                        }
                        else
                        {
                            this.SendMessage("ं");
                            preChar = "ं";
                        }
                    }
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('n'))
                {
                    this.SendMessage("न");
                    this.swarFlag = false;
                    preChar = "न";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('o'))
                {
                    if (preChar == "ओ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ो");
                        this.swarFlag = true;
                        preChar = "ो";
                    }
                    else
                    {
                        this.SendMessage("ओ");
                        this.swarFlag = false;
                        preChar = "ओ";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.O)
                {
                    if (preChar == "औ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ौ");
                        this.swarFlag = true;
                        preChar = "ौ";
                    }
                    else
                    {
                        this.SendMessage("औ");
                        this.swarFlag = false;
                        preChar = "औ";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('p'))
                {
                    this.SendMessage("प");
                    this.swarFlag = false;
                    preChar = "प";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('q'))
                {
                    this.SendMessage("क");
                    this.swarFlag = false;
                    preChar = "क";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('r'))
                {
                    this.SendMessage("र");
                    this.swarFlag = false;
                    preChar = "र";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('s'))
                {
                    this.SendMessage("स");
                    this.swarFlag = false;
                    preChar = "स";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.S)
                {
                    this.SendMessage("ष");
                    this.swarFlag = false;
                    preChar = "ष";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('t'))
                {
                    this.SendMessage("त");
                    this.swarFlag = false;
                    preChar = "त";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.T)
                {
                    this.SendMessage("ट");
                    this.swarFlag = false;
                    preChar = "ट";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('u'))
                {
                    if (preChar == "उ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ु");
                        this.swarFlag = true;
                        preChar = "ु";
                    }
                    else
                    {
                        this.SendMessage("उ");
                        this.swarFlag = false;
                        preChar = "उ";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.U)
                {
                    if (preChar == "ऊ")
                        swarFlag = true;
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ू");
                        this.swarFlag = true;
                        preChar = "ू";
                    }
                    else
                    {
                        this.SendMessage("ऊ");
                        this.swarFlag = false;
                        preChar = "ऊ";
                    }
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('v'))
                {
                    this.SendMessage("व");
                    this.swarFlag = false;
                    preChar = "व";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('w'))
                {
                    this.SendMessage("व");
                    this.swarFlag = false;
                    preChar = "व";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('x'))
                {
                    this.SendMessage("क्ष");
                    this.swarFlag = false;
                    preChar = "क्ष";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.X)
                {
                    this.SendMessage("क्स");
                    this.swarFlag = false;
                    preChar = "क्स";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('y'))
                {
                    this.SendMessage("य");
                    this.swarFlag = false;
                    preChar = "य";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('z'))
                {
                    this.SendMessage("झ");
                    this.swarFlag = false;
                    preChar = "झ";
                    sp.Play();
                    e.Handled = true;
                }
                //if (e.KeyChar == (char)Keys.Enter)
                //{
                //    this.SendMessage("{ENTER}");
                //strData.Insert(strData.Length,str.Text);
                //   this.swarFlag = true;
                //   sp.Play();
                //    e.Handled = true;
                //}
                if (e.KeyChar == (char)Keys.Space)
                {
                    //strLastActiveWindow = chk.ActiveWindow;
                    //MessageBox.Show(strLastActiveWindow);
                    //int iHandle = NativeWin32.FindWindow(null, strLastActiveWindow);
                    //
                    //NativeWin32.SetForegroundWindow(iHandle);
                    //try
                    //{
                    //    System.Windows.Forms.SendKeys.Send(" ");
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    //this.SendMessage(" ");
                    //preChar = " ";
                    this.swarFlag = true;
                    sp.Play();
                    //e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D9)
                {
                    this.SendMessage("९");
                    preChar = "९";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D8)
                {
                    this.SendMessage("८");
                    preChar = "८";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D7)
                {
                    this.SendMessage("७");
                    preChar = "६";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D6)
                {
                    this.SendMessage("६");
                    preChar = "६";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D5)
                {
                    this.SendMessage("५");
                    preChar = "५";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D4)
                {
                    this.SendMessage("४");
                    preChar = "४";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D3)
                {
                    this.SendMessage("३");
                    preChar = "३";
                    this.swarFlag = true;
                    //
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D2)
                {
                    this.SendMessage("२");
                    preChar = "२";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D1)
                {
                    this.SendMessage("१");
                    preChar = "१";
                    this.swarFlag = true;
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.D0)
                {
                    this.SendMessage("०");
                    this.swarFlag = true;
                    preChar = "०";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('`'))
                {
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ॅ");
                        this.swarFlag = true;
                        preChar = "ॅ";
                    }
                    else
                    {
                        this.SendMessage("ऍ");
                        this.swarFlag = false;
                        preChar = "ऍ";
                    }
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('~'))
                {
                    if (this.swarFlag == false)
                    {
                        this.SendMessage("ॉ");
                        this.swarFlag = true;
                        preChar = "ॉ";
                    }
                    else
                    {
                        this.SendMessage("ऑ");
                        this.swarFlag = false;
                        preChar = "ऑ";
                    }
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('|'))
                {
                    this.SendMessage("ऽ");
                    this.swarFlag = false;
                    preChar = "ऽ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('Q'))
                {
                    this.SendMessage("ॐ");
                    this.swarFlag = false;
                    preChar = "ॐ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('R'))
                {
                    this.SendMessage("ऋ");
                    this.swarFlag = false;
                    preChar = "ऋ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('Z'))
                {
                    this.SendMessage("ज्ञ");
                    this.swarFlag = false;
                    preChar = "ज्ञ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('P'))
                {
                    this.SendMessage("त्र");
                    this.swarFlag = false;
                    preChar = "त्र";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('W'))
                {
                    this.SendMessage("श्र");
                    this.swarFlag = false;
                    preChar = "श्र";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('Y'))
                {
                    this.SendMessage("ञ");
                    this.swarFlag = false;
                    preChar = "ञ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('V'))
                {
                    this.SendMessage("व्ह");
                    this.swarFlag = false;
                    preChar = "व्ह";
                    sp.Play();
                    e.Handled = true;
                }
                //NOT MATCHING
                if (e.KeyChar == (char)ToASCII('F'))
                {
                    this.SendMessage("ॉ");
                    this.swarFlag = false;
                    preChar = "ॉ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('J'))
                {
                    this.SendMessage("ॅ");
                    this.swarFlag = false;
                    preChar = "ॅ";
                    sp.Play();
                    e.Handled = true;
                }
                if (e.KeyChar == (char)ToASCII('M'))
                {
                    this.SendMessage("ँ");
                    this.swarFlag = false;
                    preChar = "ँ";
                    sp.Play();
                    e.Handled = true;
                }
                //MessageBox.Show(ToASCII(e.KeyChar).ToString());
            }
            else
            {
                try
                {
                    sp.Play();
                }
                catch (Exception ex)
                {
                    sp.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "No Sound.wav";
                    sp.Play();
                }
            }
        }

        private void Akshar_Click(object sender, EventArgs e)
        {
            Button str = (Button)sender;
            //Thread.Sleep(200);
            txtTemp.SelectedText = str.Text;
            //SendMessage("{" + str.Text + "}");
            sp.Play();
        }

        private void Swar_Click(object sender, EventArgs e)
        {
            if (this.swarFlag == true)
            {
                Button str = (Button)sender;
                txtTemp.SelectedText = str.Text;
                this.swarFlag = true;
            }
            this.swarFlag = true;
            sp.Play();
        }



        private void sendBS()
        {
            strLastActiveWindow = chk.ActiveWindow;
            //MessageBox.Show(strLastActiveWindow);
            int iHandle = NativeWin32.FindWindow(null, strLastActiveWindow);

            NativeWin32.SetForegroundWindow(iHandle);

            for (int i = 2; i < txtTemp.Text.Length * 2; i++)
            {
                try
                {
                    System.Windows.Forms.SendKeys.SendWait("{BS}");
                }
                catch (Exception ex)
                { }
            }
        }

        private void SendOneBS()
        {
            strLastActiveWindow = chk.ActiveWindow;
            //MessageBox.Show(strLastActiveWindow);
            int iHandle = NativeWin32.FindWindow(null, strLastActiveWindow);

            NativeWin32.SetForegroundWindow(iHandle);

            System.Windows.Forms.SendKeys.SendWait("{BS}");
        }

        private void SendMessage()
        {
            //this.sendBS();
            char[] chrs = txtTemp.Text.ToCharArray();
            foreach (char c in chrs)
            {
                this.SendMessage(c.ToString());
            }
        }

        private void SendMessage(string p)
        {
            strLastActiveWindow = chk.ActiveWindow;//chk.ActiveWindow;
            //MessageBox.Show(strLastActiveWindow);
            int iHandle = NativeWin32.FindWindow(null, strLastActiveWindow);
            NativeWin32.SetForegroundWindow(iHandle);
            try
            {
                System.Windows.Forms.SendKeys.SendWait(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Declare external functions.
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        private string GetPreviousWindowText()
        {
            int chars = 256;
            StringBuilder buff = new StringBuilder(chars);

            // Obtain the handle of the active window.
            IntPtr handle = GetForegroundWindow();

            // Update the controls.
            if (GetWindowText(handle, buff, chars) > 0)
            {
                return (buff.ToString());
            }
            else
                return ("Keyboard");
        }

        private void btnSwar_MouseDown(object sender, MouseEventArgs e)
        {
            this.swarFlag = false;
        }

        private void button48_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "्";
            }
        }

        private void button47_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ा";
            }
        }

        private void button46_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ि";
            }
        }

        private void button45_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ी";
            }
        }

        private void button37_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ु";
            }
        }

        private void button36_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ू";
            }
        }

        private void button44_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "े";
            }
        }

        private void button43_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ै";
            }
        }

        private void button42_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ो";
            }
        }

        private void button41_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ौ";
            }
        }

        private void button40_MouseDown(object sender, MouseEventArgs e)
        {
            //if (this.swarFlag == false)
            //{
            txtTemp.SelectedText = "ं";
            //}
        }

        private void button39_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ः";
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            txtTemp.Text = " ";
            //this.SendMessage(" ");
            txtTemp.SelectionStart = txtTemp.Text.Length;
            this.swarFlag = true;
            //
            sp.Play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ntfTray.ShowBalloonTip(1);
            this.loadSettings();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            //frmSplash frm = new frmSplash();
            //frm.ShowDialog();
            //chk.Show();
            //this.Focus();
            //hideToolStripMenuItem_Click(sender, e);
        }

        private void loadSettings()
        {
            //Application.UserAppDataRegistry.SetValue("Theme", 2);
            //mSkin.Style = SkinSoft.OSSkin.SkinStyle.Office2007Black;

            try
            {
                int xCoord = 0, yCoord = 0;
                xCoord = Convert.ToInt32(Application.UserAppDataRegistry.GetValue("LocationX"));
                yCoord = Convert.ToInt32(Application.UserAppDataRegistry.GetValue("LocationY"));
                if (xCoord < 0)
                {
                    xCoord = 0;
                }
                if (yCoord < 0)
                {
                    yCoord = 0;
                }

                if (xCoord == 0 && yCoord == 0)
                {
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    //MessageBox.Show(xCoord.ToString() + " " + yCoord.ToString());
                    Point p = new Point(xCoord, yCoord);
                    this.Location = p;
                }
                tsProcrastinationEnabled.Checked = Convert.ToBoolean(Application.UserAppDataRegistry.GetValue("ProcrastinationEnabled"));
                procrastination.nmThreashold.Value = Convert.ToInt32(Application.UserAppDataRegistry.GetValue("ProcrastinationThreshold"));
                procrastination.chkIsVerbose.Checked = Convert.ToBoolean(Application.UserAppDataRegistry.GetValue("IsVerbose"));
            }
            catch (Exception ex)
            {
                Point p = new Point(0, 0);
                this.Location = p;
            }
            try
            {
                if (!File.Exists(sp.SoundLocation))
                    sp.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "No Sound.wav";
                else
                {
                    string sl =Convert.ToString(Application.UserAppDataRegistry.GetValue("SoundLocation"));
                    if (string.IsNullOrWhiteSpace(sl))
                    {
                        Application.UserAppDataRegistry.SetValue("SoundLocation", sp.SoundLocation);
                    }
                    else
                    {
                        sp.SoundLocation = sl;
                    }
                }
                if (sp.SoundLocation == AppDomain.CurrentDomain.BaseDirectory + "Click.wav")
                {
                    onToolStripMenuItem.Checked = true;
                }
                else
                {
                    offToolStripMenuItem.Checked = true;
                }
            }
            catch (Exception ex)
            {
                this.WindowState = FormWindowState.Normal;
            }
            //try
            //{
            //    AutoStart as1 = new AutoStart();
            //    autostartToolStripMenuItem.Checked = as1.Check();
            //}
            //catch (Exception ex)
            //{ }
            //hideToolStripMenuItem.Text = "&Hide";
            // this.Visible = true;
            //try
            //{
            //    visible = Convert.ToBoolean(Application.UserAppDataRegistry.GetValue("visible"));
            //    if (!visible)
            //    {
            //        hideToolStripMenuItem.Text = "&Hide";
            //        this.Visible = true;
            //    }
            //    else
            //    {
            //        hideToolStripMenuItem.Text = "&Show";
            //        this.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Application.UserAppDataRegistry.SetValue("visible", "true");
            //    this.Visible = true;
            //}
        }
        private void SaveSettings()
        {
            //Application.UserAppDataRegistry.SetValue("Theme", numThemeFlag);
            Application.UserAppDataRegistry.SetValue("LocationX", this.Location.X);
            Application.UserAppDataRegistry.SetValue("LocationY", this.Location.Y);
            if (!File.Exists(sp.SoundLocation))
                sp.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "No Sound.wav";
            Application.UserAppDataRegistry.SetValue("SoundLocation", sp.SoundLocation);
            Application.UserAppDataRegistry.SetValue("ProcrastinationEnabled", tsProcrastinationEnabled.Checked);
            Application.UserAppDataRegistry.SetValue("ProcrastinationThreshold", procrastination.nmThreashold.Value);
            Application.UserAppDataRegistry.SetValue("IsVerbose", procrastination.chkIsVerbose.Checked);
            //Application.UserAppDataRegistry.SetValue("visible", this.Visible.ToString());
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.NoMove2D;
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                Location = mousePos;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ntfTray.Visible = false;
            //this.Hide();
            this.SaveSettings();
            if (MessageBox.Show("Click 'Yes' to exit the app\n'No' to run in background.", "Devnagari Keyboard", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                hideToolStripMenuItem_Click(sender, e);
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTemp.Text = " ";
            txtTemp.SelectionStart = txtTemp.Text.Length;
            //
        }

        private void button66_Click(object sender, EventArgs e)
        {
            if (txtTemp.Text.Length > 0)
                txtTemp.Text = txtTemp.Text.Substring(0, txtTemp.Text.Length - 1);
            else
                txtTemp.Text = "";
            if (swarFlag == false)
                this.swarFlag = true;
            else
                this.swarFlag = false;
            sp.Play();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.SendMessage(this.btnEnter.Tag.ToString());
            sp.Play();
        }


        private void button64_Click(object sender, EventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ँ";
                this.swarFlag = true;
            }
            else
            {
                txtTemp.SelectedText = "अँ";
                this.swarFlag = false;
            }
            //
            sp.Play();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ॅ";
                this.swarFlag = true;
            }
            else
            {
                txtTemp.SelectedText = "ऍ";
                this.swarFlag = false;
            }
        }

        private void txtTemp_TextChanged(object sender, EventArgs e)
        {
            if (txtTemp.Text != "")
            {
                this.sendBS();
                this.SendMessage();
                //txtTemp.Text = "";
            }
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enabledToolStripMenuItem.Checked == true)
            {
                enabledToolStripMenuItem.Checked = false;
                EnableFlag = 0;
                ntfTray.Text = "Marathi Keyboard (Disabled)";
                this.ntfTray.Icon = this.ntfTemp.Icon;
            }
            else
            {
                enabledToolStripMenuItem.Checked = true;
                EnableFlag = 1;
                ntfTray.Text = "Marathi Keyboard (Enabled)";
                this.ntfTray.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfTray.Icon")));
            }
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.onToolStripMenuItem.Checked = true;
            this.offToolStripMenuItem.Checked = false;
            sp.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Click.wav";
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.onToolStripMenuItem.Checked = false;
            this.offToolStripMenuItem.Checked = true;
            sp.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "No Sound.wav";
        }

        private void ntfTray_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                hideToolStripMenuItem_Click(new object(), new EventArgs());
            }
            catch (Exception ex)
            {

            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Visible)
                {
                    hideToolStripMenuItem.Text = "&Hide";
                    this.Visible = true;
                    // this.WindowState = FormWindowState.Normal;
                    // this.ShowInTaskbar = true;
                    this.Focus();
                }
                else
                {
                    hideToolStripMenuItem.Text = "S&how";
                    // this.WindowState = FormWindowState.Minimized;
                    this.Visible = false;
                    ntfTray.BalloonTipText = "मराठी-इंग्रजी भाषा बदलण्यासाठी Ctrl+M प्रेस करा.";
                    ntfTray.ShowBalloonTip(1);
                    //this.ShowInTaskbar = false;
                }
                ntfTray.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        //private void Form1_SizeChanged(object sender, System.EventArgs e)
        //{
        //    if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
        //        {
        //            this.Visible = false;
        //            this.ShowInTaskbar = false;
        //            hideToolStripMenuItem.Text = "S&how";
        //        }
        //}

        //private void autostartToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        AutoStart as1 = new AutoStart();
        //        if (autostartToolStripMenuItem.Checked)
        //        {
        //            as1.RunAtStart(false);
        //            autostartToolStripMenuItem.Checked = false;
        //        }
        //        else
        //        {
        //            as1.RunAtStart(true);
        //            autostartToolStripMenuItem.Checked = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void ctxMarathi_Opening(object sender, CancelEventArgs e)
        {
            this.Activate();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            if (this.swarFlag == false)
            {
                txtTemp.SelectedText = "ॉ";
                this.swarFlag = true;
            }
            else
            {
                txtTemp.SelectedText = "ऑ";
                this.swarFlag = false;
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible)
                {
                    hideToolStripMenuItem.Text = "S&how";
                    // this.WindowState = FormWindowState.Minimized;
                    this.Visible = false;
                    ntfTray.BalloonTipText = "Press 'Ctrl + M' to switch between English - Marathi";
                    ntfTray.ShowBalloonTip(1);
                    //this.ShowInTaskbar = false;
                }
            }
            catch (Exception ex)
            {

            }
            new frmHelp().ShowDialog();
            try
            {
                if (!this.Visible)
                {
                    hideToolStripMenuItem.Text = "&Hide";
                    this.Visible = true;
                    // this.WindowState = FormWindowState.Normal;
                    // this.ShowInTaskbar = true;
                    this.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ntfTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void aboutToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmAbout abt = new frmAbout();
            if (!this.Visible)
            {
                abt.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                abt.StartPosition = FormStartPosition.CenterParent;
            }
            abt.ShowDialog();
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isHidden = false;
            if (this.Visible)
            {
                this.Hide();
                isHidden = true;
            }
            procrastination.ShowDialog();

            if (isHidden)
                this.Show();
        }

        private void tsProcrastinationEnabled_Click(object sender, EventArgs e)
        {
            tsProcrastinationEnabled.Checked = !tsProcrastinationEnabled.Checked;
        }
    }
}