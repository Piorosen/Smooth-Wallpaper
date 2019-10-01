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

        public Bitmap Image { get; private set; }

        public Point Location { get; private set; } = new Point();
        public SizeF Scale = new SizeF(1.0F, 1.0F);

        public double Rotate = 0;

        public string Code { get; private set; } =
@"public Tuple<double, SizeF> ValueChange(double Rotate, SizeF Scale, ulong time)
{
    return new Tuple<double, SizeF>((time / 1000.0) * 90 % 360, Scale);
    // Scale = new SizeF(1, 1);
}

public Point PositionConvert(Point location, ulong time)
{
    return location;
}

public Bitmap ImageConvert(ulong time, SizeF scale, Bitmap bitmap)
{
    using (Graphics g = Graphics.FromImage(bitmap))
    {
        bitmap.SetResolution(g.DpiX * (1.0F / scale.Width), g.DpiY * (1.0F / scale.Height));
    }
    return bitmap;
}";


        public Element(Bitmap image, SizeF Scale, Point Location, string Name = "", string OriginCode = null)
        {
            if (OriginCode != null)
            {
                this.Code = OriginCode;
            }

            this.Name = Name;
            this.Image = image;
            this.Scale = Scale;
            this.Location = Location;
        }

        private static Bitmap RotateImage(Bitmap image, PointF offset, float angle)
        {
            //create a new empty bitmap to hold rotated image
            image.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(image))
            {
                //Put the rotation point in the center of the image
                g.TranslateTransform(offset.X, offset.Y);

                //rotate the image
                g.RotateTransform(angle);

                //move the image back
                g.TranslateTransform(-offset.X, -offset.Y);
                //draw passed in image onto graphics object
                g.DrawImage(image, new PointF(0, 0));
            }

            return image;

        }

        public Func<double, SizeF, ulong, Tuple<double, SizeF>> ValueChange;
        public Func<Point, ulong, Point> PositionConvert;
        public Func<ulong, SizeF, Bitmap, Bitmap> ImageConvert;


        private Bitmap GetImage(ulong time)
        {
            var image = ImageConvert(time, Scale, Image.Clone() as Bitmap);
            return RotateImage(image, new PointF(image.Width / 2, image.Height / 2), (float)Rotate);
        }

        public (Point Position, Bitmap Image) DrawBitmap(ulong time)
        {
            (this.Rotate, this.Scale) = ValueChange(Rotate, Scale, time);
            return (PositionConvert(Location, time), GetImage(time));
        }

    }
}
