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
        public Bitmap Image;

        public Point Location = new Point();
        public SizeF Scale = new SizeF(1, 1);

        public double Rotate = 0;

        public Element(Bitmap image, Size Scale, Point Location)
        {
            this.Image = image;
            this.Scale = Scale;
            this.Location = Location;
        }

        public Func<Point, ulong, Point> PositionConvert = (location, time) => location;
        public Func<ulong, SizeF, double, Bitmap, Bitmap> ImageConvert = (time, scale, rot, bitmap) => bitmap;

        public Point GetLocation(ulong time)
        {
            return PositionConvert(Location, time);
        }

        public Bitmap GetImage(ulong time)
        {
            return ImageConvert(time, Scale, Rotate, Image);
        }

    }
}
