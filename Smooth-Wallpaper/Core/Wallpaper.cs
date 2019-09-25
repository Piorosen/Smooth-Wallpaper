using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smooth_Wallpaper.Core
{
    public class Wallpaper
    {
        protected List<Paper> TimeLayer = new List<Paper>();

        public void AddWallPaper(Paper layer)
        {
            TimeLayer.Add(layer);
            TimeLayer.Sort((a, b) => a.Time.CompareTo(b.Time));
        }

        public Bitmap GetWallpaper()
        {
            var time = GetTodayTime();
            var paper = GetTimePaper(time);

            return GetTimeBitmap(paper, time, Color.FromArgb(255,255,255));
        }

        protected Paper GetTimePaper(ulong time)
        {
            Paper result = TimeLayer.Count != 0 ? TimeLayer[0] : new Paper();

            foreach (var p in TimeLayer)
            {
                if (p.Time > time)
                {
                    break;
                }
                result = p;
            }

            return result;
        }

        protected ulong GetTodayTime()
        {
            var time = DateTime.Now.TimeOfDay;
            ulong p = Convert.ToUInt64(time.TotalSeconds);

            return p;
        }

        protected Bitmap GetTimeBitmap(Paper paper, ulong time, Color baseColor)
        {
            var bound = Screen.AllScreens[1].Bounds;
            var image = new Bitmap(bound.Width, bound.Height);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.FillRectangle(new SolidBrush(baseColor), new Rectangle(0, 0, image.Width, image.Height));
                
                foreach (var p in paper.Layer)
                {
                    g.DrawImage(p.GetImage(time), p.GetLocation(time));
                }
            }

            return image;
        }


    }
}
