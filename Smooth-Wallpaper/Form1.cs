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
        public Form1()
        {
            InitializeComponent();

           

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WallpaperManager w = new WallpaperManager();
            w.Run(pictureBox1);
        }


        private void Button2_Click(object sender, EventArgs e)
        {
        }

    }
}
