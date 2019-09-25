using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                Time = 0,
                Layer = new List<Core.Element>()
                {
                    new Core.Element(new Bitmap(@"C:\Users\aoika\Desktop\git\Smooth-Wallpaper\Smooth-Wallpaper\1.png")
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

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (var image in wallpaper.GetWallpaper())
            {
                var p = pictureBox1.BackgroundImage;
                pictureBox1.BackgroundImage = image;
                pictureBox1.Update();
                p?.Dispose();
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            var bitmap = new Bitmap(1920, 1080);
            for (int i = 0; i < 100; i++)
            {
                bitmap = new Bitmap(1920, 1080);
                
            }
        }

    }
}
