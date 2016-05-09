using System;
using System.IO;

using Hassium.CodeGen;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.SemanticAnalysis;
using Hassium.Runtime;

namespace Hassium
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            HassiumExecuter.FromFilePath(args[0]);
        }
    }
}
