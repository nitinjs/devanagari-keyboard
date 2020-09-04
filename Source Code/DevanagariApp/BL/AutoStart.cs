using Microsoft.Win32;
using System.Windows.Forms;
namespace DevanagariApp
{
    class AutoStart
    {
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\LocalMachine\\Run", true);
        public AutoStart()
        {
            rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\LocalMachine\\Run", true);
        }
        public void RunAtStart(bool val)
        {
            if (val)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue("Marathi Keyboard", Application.ExecutablePath.ToString());
                MessageBox.Show(Application.ExecutablePath.ToString());
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue("Marathi Keyboard", false);
            }
        }
        public bool Check()
        {
            if (rkApp.GetValue("Marathi Keyboard") == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                return false;
            }

            else
            {
                // The value exists, the application is set to run at startup
                return true;
            }
        }
    }
}
