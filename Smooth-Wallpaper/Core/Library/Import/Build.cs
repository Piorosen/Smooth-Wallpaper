﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.IO;

namespace Smooth_Wallpaper.Core.Library.Import
{
    public class Build
    {
        public CompilerErrorCollection Compile(string code)
        {
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("c#");
            CompilerParameters compilerParameters = new CompilerParameters();

            //.GenerateExecutable 이값을 'false'로 하면 dll로 출력됨
            compilerParameters.GenerateExecutable = false;
            compilerParameters.OutputAssembly = @$"{Directory.GetCurrentDirectory()}\core.dll";

            CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, code);

            if (compilerResults.Errors.Count > 0)
            {
                return compilerResults.Errors;
            }
            else
            {
                return new CompilerErrorCollection();
            }
        }


    }
}
