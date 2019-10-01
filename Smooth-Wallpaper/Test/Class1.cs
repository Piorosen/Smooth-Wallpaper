using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Wallpaper.Dll
{
    public class WallpaperCore
    {

        Tuple<double, SizeF> ValueChange_0(double Rotate, SizeF Scale, ulong time)
        {
            return new Tuple<double, SizeF>((time / 1000.0) * 90 % 360, Scale);
            // Scale = new SizeF(1, 1);
        }

        Point PositionConvert_0(Point location, ulong time)
        {
            return location;
        }

        Bitmap ImageConvert_0(ulong time, SizeF scale, Bitmap bitmap)
        {
            Bitmap result = bitmap.Clone() as Bitmap;
            using (Graphics g = Graphics.FromImage(result))
            {
                bitmap.SetResolution(g.DpiX * (1.0F / scale.Width), g.DpiY * (1.0F / scale.Height));
            }
            return bitmap;
        }




    }
}
