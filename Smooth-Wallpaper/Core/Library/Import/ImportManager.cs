using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class ImportManager
    {
        public bool Load(string dir)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            
            Directory.SetCurrentDirectory(dir);

            string read = string.Empty;

            using (var sr = new StreamReader(Path.Combine(dir, "export.xml")))
            {
                read = sr.ReadToEnd();
            }


            XmlLoader loader = new XmlLoader();
            var papers = loader.Load(read);

            Assemble asm = new Assemble();
            var integrate = asm.Integrate(papers);

            Build build = new Build();
            var error = build.Compile(integrate, currentDirectory);

            bool result = false;
            if (error.Count > 0)
            {
                result = false;
                foreach (var e in error)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                result = true;
                Console.WriteLine("Success");
            }

            Dictionary<string, Element> keyValuePairs = new Dictionary<string, Element>();

            foreach (var p in papers)
            {
                foreach (var e in p.Layer)
                {
                    keyValuePairs[e.Name] = new Element(e.Image, e.Scale, e.Location, e.Name, e.OriginCode);
                }
            }

            if (LoadDll(@$"{currentDirectory}\core.dll", out Type[] value))
            {
                Type type = null;

                foreach (var find in value)
                {
                    if (find.FullName == "Wallpaper.Dll.WallpaperCore")
                    {
                        type = find;
                        break;
                    }
                }

                if (type == null)
                {
                    result = false;
                }

                foreach (var key in keyValuePairs.Keys)
                {
                    
                }
            }

            Directory.SetCurrentDirectory(currentDirectory);
            return result;
        }

        bool LoadDll(string file, out Type[] result)
        {
            result = new Type[0];

            if (File.Exists(file) != true)
            {
                return false;
            }

            Assembly asm = Assembly.LoadFrom(file);

            if (asm == null)
            {
                return false;
            }

            result = asm.GetExportedTypes();

            return true;
        }

    }
}