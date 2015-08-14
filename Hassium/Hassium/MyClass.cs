using System;
using System.Collections.Generic;

namespace Hassium
{
    public class HassiumInterpreter
    {
        public HassiumInterpreter()
        {
            StaticData.Functions.Add("print", new PrintFunction());
        }

        public string Execute(string code)
        {
            List<Token> tokens = new Lexer(code).Tokenize();
            new HassiumExecutioner(new AST(tokens)).Execute();

            Debug.PrintTokens(tokens);

            return "";
        }
    }

    public class StaticData
    {
        public static Dictionary<string, IFunction> Functions = new Dictionary<string, IFunction>();
        public static Dictionary<string, string> Vars = new Dictionary<string, string>();
    }
}

