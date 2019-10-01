using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smooth_Wallpaper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

           


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
        }

        Core.Wallpaper wallpaper = new Core.Wallpaper();

        private async void Button1_Click(object sender, EventArgs e)
        {
            await foreach (var image in wallpaper.GetWallpaper(100))
            {
                var p = pictureBox1.Image;
                pictureBox1.Image = image;
                pictureBox1.Update();
                p?.Dispose();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
        }

    }
}
