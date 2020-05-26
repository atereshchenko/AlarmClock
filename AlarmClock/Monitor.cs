using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    class Monitor
    {
        /// <summary>
        /// Pixels on width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Pixels on height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Monitor()
        {
            this.Height = SystemInformation.PrimaryMonitorSize.Height;
            this.Width = SystemInformation.PrimaryMonitorSize.Width;
        }

        /// <summary>
        /// Сonstructor with parameters
        /// </summary>
        /// <param name="x">Pixels on width</param>
        /// <param name="y">Pixels on height</param>
        public Monitor(int x, int y)
        {
            this.Height = y;
            this.Width = x;
        }
        /// <summary>
        /// Print resolution monitor
        /// </summary>
        public string Resolution()
        {
            return $"{Width}*{Height}";
        }
    }
}
