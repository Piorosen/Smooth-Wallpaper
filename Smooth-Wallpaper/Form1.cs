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
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Core.Wallpaper wallpaper = new Core.Wallpaper();

            var p = new Core.Paper
            {
                Time = 0,
                Layer = new List<Core.Element>()
                {
                    new Core.Element(new Bitmap(@"C:\Users\aoika\source\repos\Smooth-Wallpaper\Smooth-Wallpaper\1.png")
                    , new Size(1,1), new Point(100, 100))
                }
            };



            wallpaper.AddWallPaper(p);
            pictureBox1.Image = wallpaper.GetWallpaper();
        }
    }
}
