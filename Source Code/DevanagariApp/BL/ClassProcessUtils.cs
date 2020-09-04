using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DevanagariApp
{
    class ClassProcessUtils
    {
        private static Mutex mutex = null;
        /// Determine if the current process is already running
        public static bool ThisProcessIsAlreadyRunning()
        {
            // Only want to call this method once, at startup.
            Debug.Assert(mutex == null);
            // createdNew needs to be false in .Net 2.0, otherwise, if another instance of
            // this program is running, the Mutex constructor will block, and then throw 
            // an exception if the other instance is shut down.
            bool createdNew = false;
            mutex = new Mutex(false, Application.ProductName, out createdNew);
            Debug.Assert(mutex != null);
            return !createdNew;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsWindowEnabled(IntPtr hWnd);

        /// Set focus to the previous instance of the specified program.
        public static void SetFocusToPreviousInstance(string windowCaption)
        {
            // Look for previous instance of this program.
            IntPtr hWnd = FindWindow(null, windowCaption);

            // If a previous instance of this program was found...
            if (hWnd != null)
            {
                // Is it displaying a popup window?
                IntPtr hPopupWnd = GetLastActivePopup(hWnd);

                // If so, set focus to the popup window. Otherwise set focus
                // to the program's main window.
                if (hPopupWnd != null && IsWindowEnabled(hPopupWnd))
                {
                    hWnd = hPopupWnd;
                }

                SetForegroundWindow(hWnd);

                // If program is minimized, restore it.
                if (IsIconic(hWnd))
                {
                    ShowWindow(hWnd, SW_RESTORE);
                }
            }
        }
    }
}


