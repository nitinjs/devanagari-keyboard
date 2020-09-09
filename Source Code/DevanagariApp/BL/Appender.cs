using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevanagariApp.BL
{
    public static class Appender
    {
        public static int ThreashHold = 607;//procrastination threashold
        public static string TempFile = AppDomain.CurrentDomain.BaseDirectory + System.Guid.NewGuid().ToString() + ".bin";
        public static string TodaysFile = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("MM-dd-yyyy") + ".bin";

        public static bool AppendLine(bool isTemp, string s)
        {
            try
            {
                string filePath = TodaysFile;
                if (isTemp)
                {
                    filePath = TempFile;
                }

                File.AppendAllLines(filePath, new List<string>() {
                   (isTemp?"": DateTime.Now.ToString("hh:mm:ss.fff tt") + ": ")+s
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// https://stackoverflow.com/a/3709300/223752
        /// </summary>
        /// <param name="isTemp"></param>
        /// <returns></returns>
        public static string ReadAllText(bool isTemp = false)
        {
            try
            {
                //return File.ReadAllText(TodaysFile);
                using (FileStream stream = File.Open(isTemp ? TempFile : TodaysFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                        //while (!reader.EndOfStream)
                        //{

                        //}
                    }
                }
            }
            catch
            {
                return "Error!";
            }
        }

        public static bool AppendLine(bool isTemp, params string[] s)
        {
            try
            {
                string filePath = TodaysFile;
                if (isTemp)
                {
                    filePath = TempFile;
                }

                var lst = s.ToList();
                lst.ForEach(x =>
                {
                    x = (isTemp ? "" : ("\n\n\n" + DateTime.Now.ToString("hh:mm:ss.fff tt") + ": ")) + x;
                });
                File.AppendAllLines(filePath, lst);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Procrastinate(this TimeSpan t, string s)
        {
            bool isTemp = !(t.Milliseconds >= ThreashHold);
            if (isTemp)
            {
                Appender.AppendLine(isTemp, s);
            }
            else
            {
                if (!File.Exists(TempFile))
                {
                    File.Create(TempFile);
                }

                string data = ReadAllText(true);
                Appender.AppendLine(isTemp, data + s + "\n\nStarted at:" + frmMain.prevTime.ToString("hh:mm:ss.fff tt") + "\nEnded at: " + DateTime.Now.ToString("hh:mm:ss.fff tt") + "\nProcrastination Duration: " + t.Humanize());
                TempFile = AppDomain.CurrentDomain.BaseDirectory + System.Guid.NewGuid().ToString() + ".bin";
                File.AppendAllText(TempFile, "\n******************\n");
            }
        }
    }
}
