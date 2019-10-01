using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class PaperInfo
    {
        public ulong StartTime;
        public ulong Length;

        public List<ElementInfo> Layer = new List<ElementInfo>();
    }
}
