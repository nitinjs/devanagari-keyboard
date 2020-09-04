using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace Office2007Renderer
{
    [System.Drawing.ToolboxBitmapAttribute(typeof(System.Windows.Forms.Label))]
    public class ThemedLabel : System.Windows.Forms.Label
    {

        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        public ThemedLabel()
        {
            
        // Set the SystemEvents class to receive event notification when a user 
        // preference changes, the palette changes, or when display settings change.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            ToolStripManager.RendererChanged += new EventHandler(ToolStripManager_RendererChanged);

            InitColors();
        }

        private void InitColors()
        {
            try
            {
                Office2007Renderer renderer = (Office2007Renderer)ToolStripManager.Renderer;
                ProfessionalColorTable colorTable = (ProfessionalColorTable)renderer.ColorTable;
                base.ForeColor = colorTable.MenuItemText;
                base.BackColor = colorTable.MenuStripGradientEnd;
            }
            catch (Exception ex)
            {
                }
        }

        void ToolStripManager_RendererChanged(object sender, EventArgs e)
        {
            InitColors();
            this.Invalidate();
        }

    }

}
