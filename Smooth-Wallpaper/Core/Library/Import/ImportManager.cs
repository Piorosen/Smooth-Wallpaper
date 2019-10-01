﻿using System;
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
        public bool Load(string filename)
        {
            string read = string.Empty;

            using (var sr = new StreamReader(filename))
            {
                read = sr.ReadToEnd();
            }


            XmlLoader loader = new XmlLoader();
            var papers = loader.Load(read);

            Assemble asm = new Assemble();
            var integrate = asm.Integrate(papers);

            Build build = new Build();
            var error = build.Compile(integrate);

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

            if (LoadDll(@$"{Directory.GetCurrentDirectory()}\core.dll", out Type[] value))
            {
                Console.WriteLine("!!!");
            }


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