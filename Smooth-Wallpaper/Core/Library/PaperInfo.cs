﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class PaperInfo
    {

        public ulong StartTime { get; private set; }
        public ulong Length { get; private set; }

        public List<ElementInfo> Layer { get; private set; } = new List<ElementInfo>();
    }
}
