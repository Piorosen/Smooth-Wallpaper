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

        void ValueChange_0(ref double Rotate, ref SizeF Scale, ulong time)
        {
            Rotate = (time / 1000.0) * 90 % 360;
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
