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


            return true;
        }
        
        private Element SetFunction()
        {
            //var ins = instance.GetType();

            //var m1 = ins.GetMethod($"ValueChange_{key}", BindingFlags.Public | BindingFlags.Instance);
            //var m2 = ins.GetMethod($"PositionConvert_{key}", BindingFlags.Public | BindingFlags.Instance);
            //var m3 = ins.GetMethod($"ImageConvert_{key}", BindingFlags.Public | BindingFlags.Instance);

            //var ktype = keyValuePairs[key].GetType();

            //keyValuePairs[key].ValueChange = (Func<double, SizeF, ulong, Tuple<double, SizeF>>)
            //            Delegate.CreateDelegate(
            //                typeof(Func<double, SizeF, ulong, Tuple<double, SizeF>>),
            //                instance,
            //                m1);
            //keyValuePairs[key].PositionConvert = (Func<Point, ulong, Point>)
            //            Delegate.CreateDelegate(
            //                typeof(Func<Point, ulong, Point>),
            //                instance,
            //                m2);
            //keyValuePairs[key].ImageConvert = (Func<ulong, SizeF, Bitmap, Bitmap>)
            //            Delegate.CreateDelegate(
            //                typeof(Func<ulong, SizeF, Bitmap, Bitmap>),
            //                instance,
            //                m3);
        }

        private List<Paper> InfoToPaper(List<PaperInfo> Infos)
        {
            var result = new List<Paper>();

            foreach (var p in Infos)
            {
                var paper = new Paper();

                foreach (var e in p.Layer)
                {
                    var element = new Element(e.Image, e.Scale, e.Location, e.Name, e.OriginCode);



                    paper.Layer.Add(element);
                }

                result.Add(paper);
            }

            return result;
        }

        public void Import(string filename)
        {
            ImportManager im = new ImportManager();
            if (im.Load(filename, out object Core, out List<PaperInfo> Papers))
            {
                
            }
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
                    var elem = new ElementInfo(e.Scale, e.Location, e.Image, e.Code);
                    paper.Layer.Add(elem);
                }
                p.Add(paper);
            }
            var oo = Directory.GetCurrentDirectory();
            export.Export(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, "asdf", p);
        }

        public void Run(Control control)
        {
            


            foreach (var image in wallpaper.GetWallpaper(50))
            {
                var dis = control.BackgroundImage;
                control.BackgroundImage = image;
                dis?.Dispose();
            }
        }


    }
}
