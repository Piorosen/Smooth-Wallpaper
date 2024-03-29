﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class XmlLoader
    {
        
        
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
            ulong startTime = ulong.Parse(node.Attributes["StartTime"].Value);
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

            string name = string.Empty;

            name = node.Attributes["Id"].Value;
            Location.X = int.Parse(node[nameof(Location)].Attributes[nameof(Location.X)].Value);
            Location.Y = int.Parse(node[nameof(Location)].Attributes[nameof(Location.Y)].Value);

            Scale.Width = float.Parse(node[nameof(Scale)].Attributes[nameof(Scale.Width)].Value);
            Scale.Height = float.Parse(node[nameof(Scale)].Attributes[nameof(Scale.Height)].Value);

            Image = node[nameof(Image)].InnerText;
            Code = node[nameof(Code)].InnerText;

            return new ElementInfo(Scale, Location, Image, Code).SetName(name);
        }

    }
}
