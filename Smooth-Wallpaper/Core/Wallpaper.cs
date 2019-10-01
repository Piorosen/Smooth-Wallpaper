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
        public List<Paper> TimeLayer = new List<Paper>();


        public IEnumerable<Bitmap> GetWallpaper(int delay)
        {
            for (; ; )
            {
                var time = GetTodayTime();
                var paper = GetTimePaper(time);
                yield return GetTimeBitmap(paper, time, Color.FromArgb(255, 255, 255));
                Task.Delay(delay).Wait();
            }
        }

        protected List<Paper> GetTimePaper(ulong time)
        {
            List<Paper> result = new List<Paper>();

            foreach (var p in TimeLayer)
            {
                if (p.StartTime <= time && time < p.StartTime + p.Length)
                {
                    result.Add(p);
                }
            }

            return result;
        }

        protected ulong GetTodayTime()
        {
            var time = DateTime.Now.TimeOfDay;
            ulong p = Convert.ToUInt64(time.TotalMilliseconds);

            return p;
        }

        protected Bitmap GetTimeBitmap(List<Paper> paper, ulong time, Color baseColor)
        {
            var bound = Screen.AllScreens[0].Bounds;
            var image = new Bitmap(bound.Width, bound.Height);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.FillRectangle(new SolidBrush(baseColor), new Rectangle(0, 0, image.Width, image.Height));

                foreach (var layer in paper)
                foreach (var p in layer.Layer)
                {
                    var e = p.DrawBitmap(time);
                    g.DrawImage(e.Image, e.Position);
                }
            }

            return image;
        }


    }
}
