using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core.Library.Import
{
    /// <WallPaper>
    ///     <Paper Start="" End="">
    ///         <Element>
    ///             <Location>
    ///                 <X></X>
    ///                 <Y></Y>
    ///             </Location>
    ///             <Size>
    ///                 <Width></Width>
    ///                 <Height></Height>
    ///             </Size>
    ///             <Image>Path</Image>
    ///             <ImageConvert>Code Path</ImageConvert>
    ///             <PositionConvert>Code Path</PositionConvert>
    ///             <ValueChange>Code Path</ValueChange>
    ///             or
    ///             <Code>Code</code>
    ///         </Element>
    ///     </Paper>
    /// </WallPaper>
    public class ElementInfo
    {
        public Size Scale { get; private set; }
        public Point Location { get; private set; }

        public Bitmap Image { get; private set; }
        public string Code { get; private set; }

        public ElementInfo(Size Scale, Point Location, string Image, string Code)
        {
            this.Scale = Scale;
            this.Location = Location;
            this.Image = new Bitmap(Image);

            using (StreamReader sr = new StreamReader(Code))
            {
                this.Code = sr.ReadToEnd();
            }
        }
    }
}
