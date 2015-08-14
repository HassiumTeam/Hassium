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
            Debug.PrintTokens(tokens);
            Parser hassiumParser = new Parser(tokens);
            Console.Write(hassiumParser.EvaluateNode(hassiumParser.Parse()).ToString());
            //new HassiumExecutioner(new AST(tokens)).Execute();

            return "";
        }
    }

    public class StaticData
    {
        public static Dictionary<string, IFunction> Functions = new Dictionary<string, IFunction>();
        public static Dictionary<string, string> Vars = new Dictionary<string, string>();
    }
}

