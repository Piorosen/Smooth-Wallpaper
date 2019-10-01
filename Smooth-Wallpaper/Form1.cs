using Smooth_Wallpaper.Core;
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
        WallpaperManager w = new WallpaperManager();

        public Form1()
        {
            InitializeComponent();

            w.Initialize("");

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            w.Run(pictureBox1);
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            w.Export();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            w.Import(@"\\Mac\Home\Desktop\git\Smooth-Wallpaper\Smooth-Wallpaper\bin\asdf");
        }
    }
}
