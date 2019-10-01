using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smooth_Wallpaper.Core
{
    public class WallpaperManager
    {
        public async void Run(Control control)
        {
            Wallpaper wallpaper = new Wallpaper();

            var p = new Core.Paper
            {
                StartTime = 0,
                EndTime = 60 * 60 * 24 * 1000,
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


            await foreach (var image in wallpaper.GetWallpaper(50))
            {
                var dis = control.BackgroundImage;
                control.BackgroundImage = image;
                dis?.Dispose();
            }
        }


    }
}
