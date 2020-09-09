using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevanagariApp.BL
{
    public static class Extensions
    {
        public static string Humanize(this TimeSpan t)
        {
            return string.Format("{0:D2}m:{1:D2}s:{2:D2}ms",
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
        }
    }
}
