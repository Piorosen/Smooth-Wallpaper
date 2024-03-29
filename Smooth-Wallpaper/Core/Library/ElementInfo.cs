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
        public string Code
        {
            get
            {
                string result = OriginCode;

                var change = new List<string>
                {
                    "ValueChange",
                    "PositionConvert",
                    "ImageConvert"
                };

                foreach (var n in change)
                {
                    result = result.Replace(n, $"{n}_{Name}");
                }
                return result;
            }
        }

        public string OriginCode { get; private set; }

        private static int _count = 0;
        private static int Count
        {
            get
            {
                return _count++;
            }
        }

        public ElementInfo SetName(string name)
        {
            ElementInfo a = new ElementInfo(this.Scale, this.Location, this.Image, this.OriginCode)
            {
                Name = name
            };
            return a;
        }

        public ElementInfo(SizeF Scale, Point Location, string Image, string Code, string name = null)
        {
            Name = name;
            if (name == null)
            {
                Name = Count.ToString();
            }
            

            this.Scale = Scale;
            this.Location = Location;
            this.Image = new Bitmap(Image);

            using (StreamReader sr = new StreamReader(Code))
            {
                this.OriginCode = sr.ReadToEnd();
            }
        }

        public ElementInfo(SizeF Scale, Point Location, Bitmap Image, string Code, string name = null)
        {
            Name = name;
            if (name == null)
            {
                Name = Count.ToString();
            }

            this.Scale = Scale;
            this.Location = Location;
            this.OriginCode = Code;

            this.Image = Image;

        }
    }
}
