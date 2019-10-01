﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class ElementInfo
    {
        public string Name { get; private set; }

        public SizeF Scale { get; private set; }
        public Point Location { get; private set; }

        public Bitmap Image { get; private set; }
        public string Code { get; private set; }

        public string OriginCode { get; private set; }

        private static int _count = 0;
        private static int Count
        {
            get
            {
                return _count++;
            }
        }

        public ElementInfo(SizeF Scale, Point Location, string Image, string Code)
        {
            Name = Count.ToString();

            this.Scale = Scale;
            this.Location = Location;
            this.Image = new Bitmap(Image);

            using (StreamReader sr = new StreamReader(Code))
            {
                this.Code = sr.ReadToEnd();
                this.OriginCode = Code;
            }

            /// public Action<Element, ulong> ValueChange
            /// public Func<Point, ulong, Point> PositionConvert
            /// public Func<ulong, SizeF, double, Bitmap, Bitmap> ImageConvert

            var change = new List<string>
            {
                "ValueChange",
                "PositionConvert",
                "ImageConvert"
            };

            foreach (var n in change)
            {
                this.Code = this.Code.Replace(n, $"{n}_{Name}");
            }
        }

        public ElementInfo(SizeF Scale, Point Location, Bitmap Image, string Code)
        {
            Name = Count.ToString();

            this.Scale = Scale;
            this.Location = Location;
            this.Code = Code;
            this.OriginCode = Code;

            this.Image = Image;

        }
    }
}
