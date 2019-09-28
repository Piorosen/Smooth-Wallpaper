using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core
{
    public class Paper
    {
        /// <summary>
        /// Second
        /// </summary>
        public ulong StartTime;
        /// <summary>
        /// Second
        /// </summary>
        public ulong EndTime;

        public List<Element> Layer = new List<Element>();
    }
}
