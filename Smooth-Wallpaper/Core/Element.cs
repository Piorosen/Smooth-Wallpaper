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
        public SizeF Scale = new SizeF(1.0F, 1.0F);

        public double Rotate = 0;

        public Element(Bitmap image, Size Scale, Point Location)
        {
            this.Image = image;
            this.Scale = Scale;
            this.Location = Location;
        }

        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);
            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));
            return rotatedBmp;

        }

        public Action<Element, ulong> ValueChange = (element, time) =>
        {
            element.Rotate = (time / 1000.0) * 90 % 360;
        };

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


        private Bitmap GetImage(ulong time)
        {
            var image = ImageConvert(time, Scale, Rotate, Image);
            
            return RotateImage(image, new PointF(image.Width / 2, image.Height / 2), (float)Rotate);
        }

        public (Point Position, Bitmap Image) DrawBitmap(ulong time)
        {
            ValueChange(this, time);
            return (PositionConvert(Location, time), GetImage(time));
        }

    }
}
