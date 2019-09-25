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
                    , new Size(2,2), new Point(100, 100))
                }
            };

            wallpaper.AddWallPaper(p);
        }

        Core.Wallpaper wallpaper = new Core.Wallpaper();

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = wallpaper.GetWallpaper();
        }
    }
}
