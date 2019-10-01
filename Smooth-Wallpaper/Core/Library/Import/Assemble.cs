using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class Assemble
    {
        public string Integrate(List<PaperInfo> papers)
        {
            string result = string.Empty;

            foreach (var p in papers)
            {
                foreach (var e in p.Layer)
                {
                    result = $"{e.Code}\n\n";
                }
            }

            return CodeCombine(result);
        }

        private string CodeCombine(string code)
        {
            var front = "using System;\n" +
                        "using System.Collections.Generic;\n" +
                        "using System.Linq;\n" +
                        "using System.Text;\n" +
                        "using System.Threading.Tasks;\n" +
                        "using System.IO;\n" +
                        "using System.Drawing;\n\n" +
                        
                        "namespace Wallpaper.Dll\n" +
                        "{\n" +
                        "\tpublic class WallpaperCore\n" +
                        "\t{\n\n";


            var end = "\n\n\t}\n" +
                      "}\n";

            string result = front + code + end;

            return result;
        }
        

    }
}
