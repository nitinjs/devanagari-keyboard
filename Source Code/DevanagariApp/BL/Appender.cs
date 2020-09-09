using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevanagariApp.BL
{
    public class Appender
    {
        public static bool AppendLine(string s)
        {
            try
            {
                string fileName = DateTime.Now.ToString("MM-dd-yyyy") + ".bin";
                string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;

                File.AppendAllLines(filePath, new List<string>() {
                DateTime.Now.ToString("hh:mm tt") + ": "+s
            });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool AppendLine(params string[] s)
        {
            try
            {
                foreach (string l in s)
                {
                    AppendLine(l);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
