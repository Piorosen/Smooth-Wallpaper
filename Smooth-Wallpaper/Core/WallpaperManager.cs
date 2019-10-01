﻿using Smooth_Wallpaper.Core.Library.Export;
using Smooth_Wallpaper.Core.Library.Import;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Smooth_Wallpaper.Core
{
    public class WallpaperManager
    {
        Wallpaper wallpaper = new Wallpaper();

        public bool Initialize(string xml)
        {
            XmlDocument document = new XmlDocument();
            // document.Load(xml);

            


            return true;
        }

        public void Export()
        {
            ExportManager export = new ExportManager();

            var p = new List<PaperInfo>();

            foreach (var l in wallpaper.TimeLayer)
            {
                var paper = new PaperInfo(l.StartTime, l.Length);
                foreach (var e in l.Layer)
                {
                    var elem = new ElementInfo(e.Scale, e.Location, e.
                    paper.Layer.Add()
                }
                p.Add(paper);
            }
            p.Add(new PaperInfo()
            {


            });

            export.Export(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"/s.xml", "asdf", p);
        }

        public void Run(Control control)
        {
            var p = new Core.Paper
            {
                StartTime = 0,
                Length = 60 * 60 * 24 * 1000,
                Layer = new List<Core.Element>()
                {
                    new Core.Element(new Bitmap(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/1.png")
                    , new Size(2,2), new Point(200, 100))
                    {
                        PositionConvert = (Point location, ulong time) =>
                        {
                            var ee = Math.Sin((double)time / 1000) * 200;
                            Console.WriteLine(new Point(location.X + (int)ee, 100));
                            return new Point(location.X + (int)ee, 100);
                        }

                    }
                }
            };

            wallpaper.AddWallPaper(p);


            foreach (var image in wallpaper.GetWallpaper(50))
            {
                var dis = control.BackgroundImage;
                control.BackgroundImage = image;
                dis?.Dispose();
            }
        }


    }
}
