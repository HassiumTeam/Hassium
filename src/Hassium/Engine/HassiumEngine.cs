using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.Interpreter;
using Hassium.HassiumObjects;
using Hassium.Functions;

namespace Hassium.Engine
{
    public class HassiumEngine
    {
        public Interpreter.Interpreter Interpreter { get; set; }
        public HassiumEngine(bool forceMain = false)
        {
            this.Interpreter = new Interpreter.Interpreter(forceMain);
        }

        public HassiumEngine(Interpreter.Interpreter interpreter)
        {
            this.Interpreter = interpreter;
        }

        public void RunString(string source)
        {
            var ast = new Parser.Parser(new Lexer.Lexer(source).Tokenize(), source).Parse();
            var table = new Semantics.SemanticAnalyser(ast).Analyse();
            Interpreter.Code = ast;
            Interpreter.SymbolTable = table;
            Interpreter.Execute();
        }

        public void AddGlobalAttribute(string name, HassiumObject hassiumObject)
        {
            Interpreter.Globals.Add(name, hassiumObject);
        }

        public bool CheckGlobalAttribute(string name)
        {
            return (Interpreter.Globals.ContainsKey(name)) ? true : false;
        }

        public void SetGlobalAttribute(string name, HassiumObject hassiumObject)
        {
            Interpreter.Globals[name] = hassiumObject;
        }
    }
}
