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
    /// <summary>
    /// Class that contains engine for running hassium code from source.
    /// </summary>
    public class HassiumEngine
    {
        /// <summary>
        /// Interpreter instance.
        /// </summary>
        public Interpreter.Interpreter Interpreter { get; set; }

        /// <summary>
        /// Initializes new engine with parameter to force the need for a main() function.
        /// </summary>
        /// <param name="forceMain"></param>
        public HassiumEngine(bool forceMain = false)
        {
            this.Interpreter = new Interpreter.Interpreter(forceMain);
        }
        /// <summary>
        /// Initializes new engine with parameter of an existing Interpreter.
        /// </summary>
        /// <param name="interpreter"></param>
        public HassiumEngine(Interpreter.Interpreter interpreter)
        {
            this.Interpreter = interpreter;
        }

        /// <summary>
        /// Executes source code.
        /// </summary>
        /// <param name="source">Source code to execute</param>
        public void RunString(string source)
        {
            var ast = new Parser.Parser(new Lexer.Lexer(source).Tokenize(), source).Parse();
            var table = new Semantics.SemanticAnalyser(ast).Analyse();
            Interpreter.Code = ast;
            Interpreter.SymbolTable = table;
            Interpreter.Execute();
        }

        /// <summary>
        /// Adds a global attribute to the current Hassium instance.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hassiumObject"></param>
        public void AddGlobalAttribute(string name, HassiumObject hassiumObject)
        {
            Interpreter.Globals.Add(name, hassiumObject);
        }

        /// <summary>
        /// Returns true if the attribute is found in the global attributes.
        /// </summary>
        /// <param name="name">Attribute name to check.</param>
        /// <returns></returns>
        public bool CheckGlobalAttribute(string name)
        {
            return (Interpreter.Globals.ContainsKey(name)) ? true : false;
        }

        /// <summary>
        /// Changes the value of a global attribute
        /// </summary>
        /// <param name="name">Attribute name.</param>
        /// <param name="hassiumObject">Value.</param>
        public void SetGlobalAttribute(string name, HassiumObject hassiumObject)
        {
            Interpreter.Globals[name] = hassiumObject;
        }
    }
}
