using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class XmlLoader
    {
        /// <WallPaper>
        ///     <Paper Start="" Length="">
        ///         <Element>
        ///             <Location X="" Y="">
        ///             </Location>
        ///             <Size Width="" Height="">
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
        
        public List<PaperInfo> Load(string xml)
        {
            var result = new List<PaperInfo>();

            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            var papers = document["WallPaper"].ChildNodes;

            foreach (var p in papers)
            {
                result.Add(PaperLoad(p as XmlNode));
            }

            return result;
        }

        private PaperInfo PaperLoad(XmlNode node)
        {
            ulong startTime = ulong.Parse(node.Attributes["Start"].Value);
            ulong length = ulong.Parse(node.Attributes["Length"].Value);

            PaperInfo result = new PaperInfo(startTime, length);

            foreach (var e in node.ChildNodes)
            {
                result.Layer.Add(ElementLoad(e as XmlNode));
            }
            return result;
        }

        private ElementInfo ElementLoad(XmlNode node)
        {
            Point Location = new Point();
            SizeF Scale = new SizeF();
            string Image = string.Empty;
            string Code = string.Empty;

            Location.X = int.Parse(node[nameof(Location)].Attributes[nameof(Location.X)].Value);
            Location.Y = int.Parse(node[nameof(Location)].Attributes[nameof(Location.Y)].Value);

            Scale.Width = float.Parse(node[nameof(Location)].Attributes[nameof(Scale.Width)].Value);
            Scale.Height = float.Parse(node[nameof(Location)].Attributes[nameof(Scale.Height)].Value);

            Image = node[nameof(Image)].InnerText;
            Code = node[nameof(Code)].InnerText;

            return new ElementInfo(Scale, Location, Image, Code);
        }

    }
}
