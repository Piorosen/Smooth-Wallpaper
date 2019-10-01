using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core
{
    public class Element
    {
        public string Name { get; private set; }

        private Bitmap Image;

        private Point Location = new Point();
        private SizeF Scale = new SizeF(1.0F, 1.0F);

        public double Rotate = 0;

        public Element(Bitmap image, Size Scale, Point Location)
        {
            this.Image = image;
            this.Scale = Scale;
            this.Location = Location;
        }

        public Func<Point, ulong, Point> PositionConvert = (location, time) =>
        {
            return location;
        };

        public Func<ulong, SizeF, double, Bitmap, Bitmap> ImageConvert = (time, scale, rot, bitmap) =>
        {
            Bitmap result = bitmap.Clone() as Bitmap;
            using (Graphics g = Graphics.FromImage(result))
            {
                bitmap.SetResolution(g.DpiX * (1.0F / scale.Width), g.DpiY * (1.0F / scale.Height));
            }
            return bitmap;
        };

        private Point GetLocation(ulong time)
        {
            return PositionConvert(Location, time);
        }

        private Bitmap GetImage(ulong time)
        {
            return ImageConvert(time, Scale, Rotate, Image);
        }

        public (Point Position, Bitmap Image) DrawBitmap(ulong time)
        {
            return (GetLocation(time), GetImage(time));
        }

    }
}
