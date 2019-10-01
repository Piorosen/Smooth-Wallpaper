using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class ImportManager
    {
        bool Build(string currentDir, out CompilerErrorCollection error, out List<PaperInfo> papers)
        {
            string read = string.Empty;

            using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "export.xml")))
            {
                read = sr.ReadToEnd();
            }


            XmlLoader loader = new XmlLoader();
            papers = loader.Load(read);

            Assemble asm = new Assemble();
            var integrate = asm.Integrate(papers);

            Build build = new Build();
            error = build.Compile(integrate, currentDir);

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

            return result;
        }

        bool FindType(Type[] value, string Name, out Type result)
        {
            result = null;

            foreach (var find in value)
            {
                if (find.FullName == Name)
                {
                    result = find;
                    break;
                }
            }

            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        
        public bool Load(string dir, out object Core)
        {
            Core = null;

            var currentDirectory = Directory.GetCurrentDirectory();
            
            Directory.SetCurrentDirectory(dir);

            var keyValuePairs = new Dictionary<string, Element>();

            if (Build(currentDirectory, out CompilerErrorCollection error, out List<PaperInfo> papers))
            {
                if (LoadDll(@$"{currentDirectory}\core.dll", out Type[] value))
                {
                    if (FindType(value, "Wallpaper.Dll.WallpaperCore", out Type result))
                    {
                        Core = Activator.CreateInstance(result);
                        return true;
                    }
                }
            }

            Directory.SetCurrentDirectory(currentDirectory);
            return false;
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