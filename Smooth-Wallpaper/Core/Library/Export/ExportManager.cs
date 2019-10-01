using Smooth_Wallpaper.Core.Library.Import;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smooth_Wallpaper.Core.Library.Export
{
    public class ExportManager
    {/// <WallPaper>
     ///     <Paper Start="" Length="">
     ///         <Element>
     ///             <Location X="" Y="">
     ///             </Location>
     ///             <Size Width="" Height="">
     ///             </Size>
     ///             <Image>Path</Image>
     ///             
     ///             <ImageConvert>Code Path</ImageConvert>
     ///             <PositionConvert>Code Path</PositionConvert>
     ///             <ValueChange>Code Path</ValueChange>
     ///             or
     ///             <Code>Code</code>
     ///         </Element>
     ///     </Paper>
     /// </WallPaper>
        public bool Export(string filename, string type, List<PaperInfo> papers)
        {
            string directory = Path.Combine(Path.GetDirectoryName(filename), type);
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }

            Directory.CreateDirectory(directory);


            using (var writer = XmlWriter.Create(filename))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("WallPaper");

                foreach (var p in papers)
                {
                    writer.WriteStartElement("Paper");
                    writer.WriteAttributeString(nameof(p.StartTime), p.StartTime.ToString());
                    writer.WriteAttributeString(nameof(p.Length), p.Length.ToString());

                    foreach (var e in p.Layer)
                    {
                        writer.WriteStartElement("Element");

                        writer.WriteStartElement(nameof(e.Location));
                        writer.WriteAttributeString(nameof(e.Location.X), e.Location.X.ToString());
                        writer.WriteAttributeString(nameof(e.Location.Y), e.Location.Y.ToString());
                        writer.WriteEndElement();

                        writer.WriteStartElement(nameof(e.Scale));
                        writer.WriteAttributeString(nameof(e.Scale.Width), e.Scale.Width.ToString());
                        writer.WriteAttributeString(nameof(e.Scale.Height), e.Scale.Height.ToString());
                        writer.WriteEndElement();

                        writer.WriteElementString(nameof(e.Image), SaveBitmap(e, directory));
                        writer.WriteElementString(nameof(e.Code), SaveCode(e, directory));

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return true;
        }

        string SaveBitmap(ElementInfo e, string dir)
        {
            string type = Path.GetFileName(dir);

            string directory = Path.Combine(dir, "Image");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            directory = Path.Combine(directory, $"{e.Name}.png");

            e.Image.Save(directory);
            
            return $"./{type}/Image/{e.Name}.png";
        }

        string SaveCode(ElementInfo e, string dir)
        {
            string type = Path.GetFileName(dir);

            string directory = Path.Combine(dir, "Code");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            directory = Path.Combine(directory, $"{e.Name}.cs");

            using (StreamWriter sw = new StreamWriter(directory))
            {
                sw.WriteLine(e.OriginCode);
            }

            return $"./{type}/Code/{e.Name}.cs";
        }
    }
}
