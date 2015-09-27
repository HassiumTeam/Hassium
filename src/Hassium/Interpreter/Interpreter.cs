// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//  * Redistributions of source code must retain the above copyright notice, this list
//    of conditions and the following disclaimer.
// 
//  * Redistributions in binary form must reproduce the above copyright notice, this
//    list of conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
// Neither the name of the copyright holder nor the names of its contributors may be
// used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Collections;
using Hassium.HassiumObjects.Conversion;
using Hassium.HassiumObjects.Drawing;
using Hassium.HassiumObjects.Interpreter;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Math;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Networking.HTTP;
using Hassium.HassiumObjects.Text;
using Hassium.HassiumObjects.Types;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.Parser.Ast;
using Hassium.Semantics;

namespace Hassium.Interpreter
{
    public delegate void ExitEventHandler(int code);

    /// <summary>
    ///     Interpreter.
    /// </summary>
    public class Interpreter : IVisitor
    {
        public CallStack CallStack = new CallStack();
        public Dictionary<string, HassiumObject> Globals = new Dictionary<string, HassiumObject>();
        public Dictionary<string, HassiumObject> Constants = new Dictionary<string, HassiumObject>
        {
            {"true", new HassiumBool(true)},
            {"false", new HassiumBool(false)},
            {"Convert", new HassiumConvert()},
            {"Console", new HassiumConsole()},
            {"Information", new HassiumInformation()},
            {"null", null}
        };

        public AstNode Code { get; set; }
        public SymbolTable SymbolTable { get; set; }

        public bool HandleErrors { get; set; }
        public bool ReturnFunc { get; set; }

        public int IsInFunction { get; set; }
        public int Exitcode = -1;

        private bool enforceMainEntryPoint;
        private bool isRepl { get; set; }
        private bool continueLoop { get; set; }
        private bool breakLoop { get; set; }
        private bool exit { get; set; }

        private int isInLoop { get; set; }
        private Stack<int> position = new Stack<int>(); 
        private Stack<int> nodePos = new Stack<int>(); 

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        public Interpreter(bool forcemain = true)
        {
            SymbolTable = new SymbolTable();
            enforceMainEntryPoint = forcemain;
            loadInternalFunctions();
            HandleErrors = true;
        }

        public Interpreter(SymbolTable symbolTable, AstNode code, bool forcemain = true)
        {
            Code = code;
            SymbolTable = symbolTable;
            enforceMainEntryPoint = forcemain;
            loadInternalFunctions();
            HandleErrors = true;
        }

         

        public void Execute(bool repl = false)
        {
            isRepl = repl;
            if (HandleErrors)
            {
                try
                {
                    foreach (var node in Code.Children)
                    {
                        if (exit) return;
                        node.Visit(this);
                    }

                    if (!Globals.ContainsKey("main`0") && enforceMainEntryPoint)
                    {
                        Console.WriteLine("Could not execute, no main entry point of program!");
                        Environment.Exit(-1);
                    }

                    if (enforceMainEntryPoint)
                        Globals["main`0"].Invoke();
                }
                catch (Exception ex)
                {
                    if (!HandleErrors) throw;
                    Console.WriteLine();
                    if (ex is ParseException)
                        printError(Program.options.Code, (ParseException) ex);
                    else
                        printError(Program.options.Code, new ParseException(ex.Message, nodePos.Peek()));
                        //Console.WriteLine("There has been an error. Message: " + ex.Message);

                    Console.WriteLine("\nStack Trace: \n" + ex.StackTrace);
                    Environment.Exit(-1);
                }
            }
            else
            {
                foreach (var node in Code.Children)
                {
                    if (exit) return;
                    node.Visit(this);
                }

                if (!Globals.ContainsKey("main`0") && enforceMainEntryPoint)
                {
                    Console.WriteLine("Could not execute, no main entry point of program!");
                    Environment.Exit(-1);
                }

                if (enforceMainEntryPoint)
                    Globals["main`0"].Invoke();
            }
        }

        public void SetVariable(string name, HassiumObject value, AstNode node, bool forceglobal = false,
            bool onlyexist = false)
        {
            validateVariableName(name, node);

            if (!forceglobal && CallStack.Any() &&(!onlyexist || CallStack.HasVariable(name)) && !Globals.ContainsKey(name))
                CallStack.SetVariable(name, value);
            else
                Globals[name] = value;
        }

        /// <summary>
        ///     Gets the functions.
        /// </summary>
        /// <returns>The functions.</returns>
        /// <param name="path">Path.</param>
        public static Dictionary<string, InternalFunction> GetFunctions(string path = "")
        {
            var result = new Dictionary<string, InternalFunction>();

            var testAss = path == "" ? Assembly.GetExecutingAssembly() : Assembly.LoadFrom(path);

            foreach (var type in testAss.GetTypes())
            {
                if (type.GetInterface(typeof (ILibrary).FullName) != null)
                    foreach (var myfunc in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                    {
                        var theattr1 = myfunc.GetCustomAttributes(typeof (IntFunc), true);
                        foreach (var theattr in theattr1.OfType<IntFunc>())
                        {
                            foreach (int argNumber in theattr.Arguments)
                            {
                                var rfunc = new InternalFunction(
                                    (HassiumFunctionDelegate)
                                    Delegate.CreateDelegate(typeof (HassiumFunctionDelegate), myfunc),
                                    argNumber, false, theattr.Constructor);
                                result.Add(theattr.Name + "`" + (argNumber == -1 ? "i" : argNumber.ToString()), rfunc);
                                if (theattr.Alias != "")
                                    result.Add(theattr.Alias + "`" + (argNumber == -1 ? "i" : argNumber.ToString()),
                                        rfunc);
                            }
                        }
                    }
            }
            return result;
        }

        private IFunction getFunction(string name, int parm, AstNode node)
        {
            if (hasVariable(name + "`" + parm))
                return getVariable(name + "`" + parm, node);
            if (hasVariable(name + "`i"))
                return getVariable(name + "`i", node);
            if (hasVariable(name))
                return getVariable(name, node);

            if(Globals.Any(x => x.Key.StartsWith(name) && x.Key.Contains("`")) || CallStack.HasVariable(name, true))
                throw new ParseException("Incorrect arguments for function " + name, node);

            throw new ParseException("The function " + name + " doesn't exist", node);
        }

        private static void printError(string str, ParseException e)
        {
            var idx = e.Position;
            if (idx == -1)
            {
                Console.WriteLine("Error at position <unknown>: " + e.Message);
                return;
            }
            var line = str.Substring(0, idx).Split('\n').Length;
            var _x = str.Split('\n').Take(line);
            var res = _x.Last();
            string trimd = res.Trim();
            _x = _x.Take(line - 1);
            var column = idx - (string.Join("\n", _x).Length + (_x.Any() ? 1 : 0)) + 1;
            Console.WriteLine("Error at position " + idx + ", line " + line
                              + " column " + column + ": " +
                              e.Message);
            Console.WriteLine("   " + trimd);
            Console.WriteLine(new string(' ', 2 + (column - (res.Length - trimd.Length))) + '^');
        }

        private void loadInternalFunctions()
        {
            foreach (var entry in GetFunctions())
                Globals.Add(entry.Key, entry.Value);
        }


        private void freeVariable(string name, AstNode node, bool forceglobal = false)
        {
            if (Constants.ContainsKey(name))
                throw new ParseException("Can't delete internal constant '" + name + "'.", node);
            if (forceglobal)
            {
                if (!Globals.ContainsKey(name))
                    throw new ParseException("The global variable '" + name + "' doesn't exist.", node);
                Globals.Remove(name);
            }
            else
            {
                if (!hasVariable(name)) throw new ParseException("The variable '" + name + "' doesn't exist.", node);
                if (CallStack.HasVariable(name))
                    CallStack.FreeVariable(name);
                else
                    Globals.Remove(name);
            }
        }

        private HassiumObject interpretBinaryOp(BinOpNode node)
        {
            var right = (HassiumObject) node.Right.Visit(this);
            if (node.BinOp == BinaryOperation.Assignment)
            {
                if (node.Left is ArrayGetNode)
                {
                    var call = (ArrayGetNode) (node.Left);

                    if (!call.Target.CanBeIndexed)
                        throw new ParseException(
                            "The [] operator only applies to objects of type Array, Dictionary or String.", node);

                    if (!call.Target.CanBeModified)
                        throw new ParseException("The specified target cannot be modified.", node);

                    var evaluated = call.Target.Visit(this);
                    if (evaluated is HassiumDictionary)
                    {
                        var theArray = ((HassiumDictionary) evaluated);
                        HassiumObject arid = null;

                        if (call.Arguments.Children.Count > 0)
                            arid = (HassiumObject) call.Arguments.Children[0].Visit(this);

                        var theValue = (node.IsOpAssign && arid != null)
                            ? interpretBinaryOp(theArray[arid], right, node.AssignOperation)
                            : right;

                        if (arid == null)
                            theArray.Value.Add(new HassiumKeyValuePair(theArray.Value.Count, theValue));
                        else
                        {
                            if (theArray.Value.Any(cur => cur.Key.ToString() == arid.ToString()))
                            {
                                foreach (var cur in theArray.Value.Where(cur => cur.Key.ToString() == arid.ToString()))
                                {
                                    theArray.Value[cur.Key].Key = theValue;
                                    break;
                                }
                            }
                            else
                            {
                                theArray[arid] = theValue;
                            }
                        }

                        SetVariable(call.Target.ToString(), theArray, call);
                    }
                    else if (evaluated is HassiumArray || evaluated is HassiumString)
                    {
                        HassiumArray theArray = null;
                        if (evaluated is HassiumString)
                            theArray = new HassiumArray(evaluated.ToString().ToCharArray().Cast<object>());

                        theArray = ((HassiumArray) evaluated);

                        int arid = -1;
                        bool append = false;

                        if (call.Arguments.Children.Count > 0)
                            arid = (HassiumObject) call.Arguments.Children[0].Visit(this);
                        else
                            append = true;

                        var theValue = node.IsOpAssign
                            ? interpretBinaryOp(theArray[arid], right, node.AssignOperation)
                            : right;

                        if (append)
                            theArray.Add(new[] {theValue});
                        else
                        {
                            if (arid >= theArray.Value.Length)
                                throw new ParseException("The index is out of the bounds of the array", call);

                            theArray[arid] = theValue;
                        }

                        SetVariable(call.Target.ToString(), theArray, call);
                    }
                    else
                    {
                        throw new ParseException(
                            "The [] operator only applies to objects of type Array, Dictionary or String.", node);
                    }
                }
                else if (node.Left is MemberAccessNode)
                {
                    var accessor = (MemberAccessNode) node.Left;
                    var target = (HassiumObject) accessor.Left.Visit(this);
                    target.SetAttribute(accessor.Member, right);
                }
                else
                {
                    if (!(node.Left is IdentifierNode))
                        throw new ParseException("Not a valid identifier", node);
                    SetVariable(node.Left.ToString(),
                        node.IsOpAssign
                            ? interpretBinaryOp(new BinOpNode(node.Position, node.AssignOperation, node.Left, node.Right))
                            : right, node);
                }
                return right;
            }
            var left = node.Left.Visit(this);
            if (node.BinOp == BinaryOperation.Is)
            {
                var target = right;
                Type ttype = null;
                if (target is HassiumClass) ttype = target.GetType();
                return left.GetType() == ttype;
            }
            return interpretBinaryOp(left, right, node.IsOpAssign ? node.AssignOperation : node.BinOp, node.Position);
        }

        private bool hasFunction(string name, int parm)
        {
            return hasVariable(name + "`" + parm) || hasVariable(name + "`i") || hasVariable(name);
        }

        /// <summary>
        ///     Interprets a binary op
        /// </summary>
        /// <param name="left">The left-hand parameter</param>
        /// <param name="right">The right-hand parameter</param>
        /// <param name="_op">The operation type</param>
        /// <param name="pos">position</param>
        /// <returns>The result of the operation</returns>
        private HassiumObject interpretBinaryOp(object left, object right, BinaryOperation _op, int pos = -1)
        {
            if (left == null && (_op != BinaryOperation.NullCoalescing && _op != BinaryOperation.Equals && _op != BinaryOperation.NotEqualTo))
                throw new ParseException("Left operand can't be null", pos);
            if (left is AstNode) left = ((AstNode) left).Visit(this);
            if (left is int) left = (double) (int) left;
            if (right is AstNode) right = ((AstNode) right).Visit(this);
            if (right is int) right = (double) (int) right;
            switch (_op)
            {
                case BinaryOperation.Addition:
                    if (right == null) return (HassiumObject)left;
                    if (left == null) return (HassiumObject)right;
                    if (left is HassiumString || right is HassiumString)
                        return new HassiumString(left + right.ToString());
                    if (left is HassiumDate || right is HassiumDate)
                        return (HassiumDate) left + (HassiumDate) right;
                    if (left is HassiumKeyValuePair || right is HassiumKeyValuePair)
                        return new HassiumString(left + right.ToString());
                    if (left is HassiumInt && right is HassiumInt)
                        return new HassiumInt(Convert.ToInt32(left) + Convert.ToInt32(right));
                    if (left is HassiumEvent && right is HassiumMethod)
                    {
                        var ev = (HassiumEvent) left;
                        ev.AddHandler((HassiumMethod) right);
                        return ev;
                    }
                    return new HassiumDouble(Convert.ToDouble(left) + Convert.ToDouble(right));

                case BinaryOperation.Subtraction:
                    if (left is HassiumInt && right is HassiumInt)
                        return new HassiumInt(Convert.ToInt32(left) - Convert.ToInt32(right));
                    if (left is HassiumEvent && right is HassiumMethod)
                    {
                        var ev = (HassiumEvent) left;
                        ev.RemoveHandler((HassiumMethod) right);
                        return ev;
                    }
                    return new HassiumDouble(Convert.ToDouble(left) - Convert.ToDouble(right));

                case BinaryOperation.Division:
                    if (Convert.ToDouble(right) == 0.0) throw new ParseException("Cannot divide by zero", pos);
                    if (left is HassiumInt && right is HassiumInt)
                        return new HassiumInt(Convert.ToInt32(left) / Convert.ToInt32(right));
                    return new HassiumDouble(Convert.ToDouble(left) / Convert.ToDouble(right));

                case BinaryOperation.Multiplication:
                    if ((left is HassiumString && right is HassiumInt) ||
                        right is HassiumString && left is HassiumInt)
                    {
                        if (left is HassiumString)
                            return new HassiumString(string.Concat(Enumerable.Repeat(left, Convert.ToInt32(right))));
                        else
                            return new HassiumString(string.Concat(Enumerable.Repeat(right, Convert.ToInt32(left))));
                    }
                    if (left is HassiumInt && right is HassiumInt)
                        return new HassiumInt(Convert.ToInt32(left) * Convert.ToInt32(right));
                    return new HassiumDouble(Convert.ToDouble(left) * Convert.ToDouble(right));
                case BinaryOperation.Equals:
                    if (left == null && right == null) return true;
                    if (left == null) return right == null;
                    if (right == null) return left == null;
                    if ((left is HassiumInt || left is HassiumDouble) && (right is HassiumInt || right is HassiumDouble))
                        return new HassiumBool(((HassiumObject)left).HDouble().Value == ((HassiumObject)right).HDouble().Value);
                    return new HassiumBool(left.ToString() == right.ToString());
                case BinaryOperation.LogicalAnd:
                    return new HassiumBool(Convert.ToBoolean(left) && Convert.ToBoolean(right));
                case BinaryOperation.LogicalOr:
                    return new HassiumBool(Convert.ToBoolean(left) || Convert.ToBoolean(right));
                case BinaryOperation.NotEqualTo:
                    if((left is HassiumInt || left is HassiumDouble) && (right is HassiumInt || right is HassiumDouble))
                        return new HassiumBool(((HassiumObject)left).HDouble().Value != ((HassiumObject)right).HDouble().Value);
                    return new HassiumBool(left.ToString() != right.ToString());
                case BinaryOperation.LessThan:
                    return new HassiumBool(Convert.ToDouble(left) < Convert.ToDouble(right));
                case BinaryOperation.GreaterThan:
                    return new HassiumBool(Convert.ToDouble(left) > Convert.ToDouble(right));
                case BinaryOperation.GreaterOrEqual:
                    return new HassiumBool(Convert.ToDouble(left) >= Convert.ToDouble(right));
                case BinaryOperation.LesserOrEqual:
                    return new HassiumBool(Convert.ToDouble(left) <= Convert.ToDouble(right));
                case BinaryOperation.CombinedComparison:
                    if (new HassiumBool(interpretBinaryOp(left, right, BinaryOperation.GreaterThan)))
                        return new HassiumInt(1);
                    return new HassiumBool(interpretBinaryOp(left, right, BinaryOperation.LessThan))
                        ? new HassiumInt(-1)
                        : new HassiumInt(0);
                case BinaryOperation.Xor:
                    return new HassiumInt(Convert.ToInt32(left) ^ Convert.ToInt32(right));
                case BinaryOperation.BitwiseAnd:
                    return new HassiumInt(Convert.ToInt32(left) & Convert.ToInt32(right));
                case BinaryOperation.BitwiseOr:
                    return new HassiumInt(Convert.ToInt32(left) | Convert.ToInt32(right));
                case BinaryOperation.BitshiftLeft:
                    return new HassiumInt(Convert.ToInt32(left) << Convert.ToInt32(right));
                case BinaryOperation.BitshiftRight:
                    return new HassiumInt(Convert.ToInt32(left) >> Convert.ToInt32(right));
                case BinaryOperation.Modulus:
                    return new HassiumDouble(Convert.ToDouble(left) % Convert.ToDouble(right));

                case BinaryOperation.Pow:
                    if (left is HassiumInt && right is HassiumInt)
                        return new HassiumInt((int) Math.Pow(Convert.ToInt32(left), Convert.ToInt32(right)));
                    return new HassiumDouble(Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right)));
                case BinaryOperation.Root:
                    return new HassiumDouble(Math.Pow(Convert.ToDouble(left), 1.0 / Convert.ToDouble(right)));

                case BinaryOperation.NullCoalescing:
                    return HassiumObject.ToHassiumObject(left) ?? HassiumObject.ToHassiumObject(right);
            }
            // Raise error
            throw new ParseException("The operation " + _op + " is not implemented yet.", pos);
        }

        /// <summary>
        ///     Interprets the unary op.
        /// </summary>
        /// <returns>The unary op.</returns>
        /// <param name="node">Node.</param>
        private HassiumObject interpretUnaryOp(UnaryOpNode node)
        {
            var value = node.Value.Visit(this);
            switch (node.UnOp)
            {
                case UnaryOperation.Not:
                    return !Convert.ToBoolean(value);
                case UnaryOperation.Negate:
                    if (value is int) return -(int) value;
                    return -Convert.ToDouble(value);
                case UnaryOperation.Complement:
                    return ~(int) Convert.ToDouble(value);
            }
            //Raise error
            throw new ParseException("The operation " + node.UnOp + " is not implemented yet.", node.Position);
        }

        private HassiumObject getVariable(string name, AstNode node)
        {
            if (Constants.ContainsKey(name))
                return Constants[name];
            if (CallStack.HasVariable(name, true)) return CallStack.GetVariable(name, true);
            if (Globals.ContainsKey(name))
                return Globals[name];
            if (Globals.Any(x => x.Key.StartsWith(name) && x.Key.Replace(name, "").StartsWith("`")))
                return Globals.First(x => x.Key.StartsWith(name) && x.Key.Replace(name, "").StartsWith("`")).Value;

            else throw new ParseException("The variable '" + name + "' doesn't exist.", node);
        }

        private bool hasVariable(string name, bool onlyglobal = false)
        {
            return onlyglobal
                ? Globals.ContainsKey(name) || Constants.ContainsKey(name)
                    : Globals.ContainsKey(name) || Constants.ContainsKey(name) ||
                (CallStack.HasVariable(name));
        }

        private void validateVariableName(string name, AstNode node)
        {
            if (name.Contains("`")) name = name.Substring(0, name.IndexOf('`'));

            if (Constants.ContainsKey(name))
                throw new ParseException("Can't change the value of the internal constant '" + name + "'.", node);

            var intfuncs = new List<string> {"eval", "exit", "free"};
            if(intfuncs.Contains(name))
                throw new ParseException("Can't redefine the internal function '" + name + "'.", node);
        }
            
        public object Accept(Expression expr)
        {
            visitSubnodes(expr);
            return null;
        }

        public object Accept(ArgListNode node)
        {
            return node.Children.Select(x => (HassiumObject) x.Visit(this)).ToArray();
        }

        public object Accept(ArrayGetNode node)
        {
            var call = node;

            if (!call.Target.CanBeIndexed)
                throw new ParseException(
                    "The [] operator only applies to objects of type Array, Dictionary or String.", node);

            var evaluated = (HassiumObject) call.Target.Visit(this);
            if (evaluated is HassiumDictionary)
            {
                var theArray = ((HassiumDictionary) evaluated);
                HassiumObject arid = null;

                if (call.Arguments.Children.Count > 0)
                    arid = (HassiumObject) call.Arguments.Children[0].Visit(this);

                if (arid == null)
                    return theArray.Value.Last().Value;
                else
                {
                    return theArray.Value.Any(cur => cur.Key.ToString() == arid.ToString())
                        ? theArray.Value.First(cur => cur.Key.ToString() == arid.ToString()).Value
                        : theArray[arid];
                }
            }
            else if (evaluated is HassiumArray || evaluated is HassiumString)
            {
                HassiumArray theArray = null;
                if (evaluated is HassiumString)
                    theArray = new HassiumArray(evaluated.ToString().ToCharArray().Cast<object>());
                else theArray = ((HassiumArray) evaluated);

                int arid = -1;
                bool append = false;

                if (call.Arguments.Children.Count > 0)
                    arid = ((HassiumObject) call.Arguments.Children[0].Visit(this)).HDouble().ValueInt;
                else
                    append = true;

                int count = (HassiumObject) call.Count.Visit(this);

                if (append)
                    return theArray.Value.Last();
                else
                {
                    if (arid >= theArray.Value.Length || arid + count > theArray.Value.Length)
                        throw new ParseException("The index is out of the bounds of the array", call);

                    var r = theArray.Value.Skip(arid).Take(count).ToArray();
                    return r.Length == 1 ? r[0] : r.ToArray();
                }
            }
            else
            {
                throw new ParseException(
                    "The [] operator only applies to objects of type Array, Dictionary or String.", node);
            }
        }

        public object Accept(ArrayIndexerNode node)
        {
            return null;
        }

        public object Accept(ArrayInitializerNode node)
        {
            var ainode = node;
            var content = ainode.Value;
            if (ainode.IsDictionary)
                return new HassiumDictionary(content.Select(
                    pair =>
                        new KeyValuePair<HassiumObject, HassiumObject>(
                            pair.Key is AstNode
                                ? (HassiumObject) ((AstNode) (pair.Key)).Visit(this)
                                : HassiumObject.ToHassiumObject(pair.Key),
                            pair.Value is AstNode
                                ? (HassiumObject) ((AstNode) (pair.Value)).Visit(this)
                                : HassiumObject.ToHassiumObject(pair.Value)))
                    .ToDictionary(x => x.Key, x => x.Value));
            else
                return
                    new HassiumArray(
                        content.Values.Select(x => x is AstNode ? (HassiumObject) ((AstNode) x).Visit(this) : x));
        }

        public object Accept(BinOpNode node)
        {
            var bnode = node;
            var res = interpretBinaryOp(bnode);
            if (isRepl) ConsoleFunctions.PrintLn(new[] {res});
            return res;
        }

        public object Accept(BreakNode node)
        {
            if (isInLoop == 0) throw new ParseException("'break' cannot be used outside a loop", node);
            breakLoop = true;
            return null;
        }

        public object Accept(CaseNode node)
        {
            node.Body.Visit(this);
            return null;
        }

        private bool gotoposition;
        private int positiontogo = -1;

        public object Accept(GotoNode node)
        {
            if (!CallStack.Any())
                throw new ParseException("'goto' cannot be used in global scope", node);

            if(!CallStack.Peek().Labels.ContainsKey(node.Name))
                throw new ParseException("Unknown label " + node.Name, node.Position);

            gotoposition = true;
            positiontogo = CallStack.Peek().Labels[node.Name];
            return null; 
        }

        public object Accept(ClassNode node)
        {
            var cnode = node;
            if (!Globals.ContainsKey(cnode.Name))
                Globals.Add(cnode.Name, new HassiumClass(cnode, this));
            return null;
        }

        public object Accept(LabelNode node)
        {
            if (!CallStack.Any())
                throw new ParseException("A label cannot be created in global scope.", node);
            CallStack.Peek().Labels.Add(node.Name, position.Peek());
            return null;
        }

        public object Accept(CodeBlock node)
        {
            visitSubnodes(node);
            return null;
        }

        public object Accept(ConditionalOpNode node)
        {
            var ifStmt = node;
            if ((HassiumBool) (ifStmt.Predicate.Visit(this)))
            { // Don't remove these braces, otherwise the universe will explode
                if (ifStmt.Body != null) return ifStmt.Body.Visit(this);
            }
            else
            {
                if (ifStmt.ElseBody != null) return ifStmt.ElseBody.Visit(this);
            }

            return null;
        }

        public object Accept(ContinueNode node)
        {
            if (isInLoop == 0) throw new ParseException("'continue' cannot be used outside a loop", node);
            continueLoop = true;

            return null;
        }

        public object Accept(ForEachNode node)
        {
            var forStmt = node;
            var needlestmt = forStmt.Needle;
            var haystackstmt = forStmt.Haystack.Visit(this);

            isInLoop++;
            if (haystackstmt is HassiumDictionary)
            {
                var theArray = ((HassiumDictionary) haystackstmt);

                var keyvname = "";
                var valvname = "";
                if (needlestmt is ArrayInitializerNode)
                {
                    keyvname = ((ArrayInitializerNode) needlestmt).Value[0].ToString();
                    valvname = ((ArrayInitializerNode) needlestmt).Value[1].ToString();
                }
                else
                {
                    valvname = needlestmt.ToString();
                }
                if (keyvname != "") SetVariable(keyvname, null, forStmt);
                SetVariable(valvname, null, forStmt);
                foreach (var needle in (keyvname == "" ? (IEnumerable) (theArray.Value.Select(x => x.Value)) : theArray))
                {
                    if (keyvname != "") SetVariable(keyvname, ((HassiumKeyValuePair) needle).Key, forStmt);
                    SetVariable(valvname,
                        keyvname != "" ? ((HassiumKeyValuePair) needle).Value : HassiumObject.ToHassiumObject(needle),
                        forStmt);
                    forStmt.Body.Visit(this);
                    if (continueLoop) continueLoop = false;
                    if (breakLoop)
                    {
                        breakLoop = false;
                        break;
                    }
                }
                if (keyvname != "") freeVariable(keyvname, forStmt);
                freeVariable(valvname, forStmt);
                isInLoop--;
            }
            else if (haystackstmt is HassiumArray || haystackstmt is HassiumString)
            {
                HassiumArray theArray = null;
                if (haystackstmt is HassiumString)
                    theArray = new HassiumArray(haystackstmt.ToString().ToCharArray().Cast<object>());
                else theArray = ((HassiumArray) haystackstmt);

                var valvname = needlestmt.ToString();

                SetVariable(valvname, null, forStmt);
                foreach (var needle in theArray.Value)
                {
                    SetVariable(valvname, HassiumObject.ToHassiumObject(needle), forStmt);
                    forStmt.Body.Visit(this);
                    if (continueLoop) continueLoop = false;
                    if (breakLoop)
                    {
                        breakLoop = false;
                        break;
                    }
                }
                freeVariable(valvname, forStmt);
                isInLoop--;
            }
            else
            {
                isInLoop--;
                throw new ParseException("Foreach can only be used with objects of type Array, Dictionary or String.", node);
            }
            return null;
        }

        public object Accept(ForNode node)
        {
            var forStmt = node;
            isInLoop++;
            forStmt.Left.Visit(this);
            while ((HassiumBool) (forStmt.Predicate.Visit(this)))
            {
                forStmt.Body.Visit(this);
                if (continueLoop) continueLoop = false;

                if (breakLoop)
                {
                    breakLoop = false;
                    break;
                }
                forStmt.Right.Visit(this);
            }
            isInLoop--;
            return null;
        }

        public object Accept(FuncNode node)
        {
            var fnode = node;
            var stackFrame = new StackFrame(SymbolTable.ChildScopes[fnode.Name + "`" + fnode.Parameters.Count]);
            if (CallStack.Any())
            {
                stackFrame.Scope.Symbols.AddRange(CallStack.Peek().Scope.Symbols);
                CallStack.Peek().Locals.All(x =>
                {
                    stackFrame.Locals.Add(x.Key, x.Value);
                    return true;
                });
            }
            var hfunc = new HassiumMethod(this, fnode, stackFrame, null);
            SetVariable(fnode.Name + "`" + fnode.Parameters.Count, hfunc, fnode);
            return hfunc;
        }


        public object Accept(FunctionCallNode node)
        {
            var call = node;

            IFunction target = null;


            bool dontEval = false;

            switch (call.Target.ToString())
            {
                case "free":
                    dontEval = true;
                    break;
                case "exit":
                case "eval":
                    // internal interpreter functions
                    break;
                default:
                    /*if (((call.Target is IdentifierNode) &&
                         !hasFunction(call.Target.ToString(), call.Arguments.Children.Count)))
                        throw new ParseException("The function " + call.Target + " doesn't exist", node);*/

                    if (call.Target is MemberAccessNode)
                    {
                        var man = (MemberAccessNode) call.Target;
                        var targ = (HassiumObject) man.Left.Visit(this);
                        if (targ.Attributes.ContainsKey(man.Member + "`" + call.Arguments.Children.Count))
                            target = targ.GetAttribute(man.Member + "`" + call.Arguments.Children.Count, node.Position);
                        else if (targ.Attributes.ContainsKey(man.Member))
                            target = targ.GetAttribute(man.Member, node.Position);
                        else if(man.Member == "toString")
                            target = new InternalFunction(x => targ.ToString(), 0);
                        else
                            throw new ParseException(
                                "The function " + man.Member + " doesn't exist for the object " + man.Left, node);
                    }
                    else if(call.Target is IdentifierNode)
                        target = getFunction(call.Target.ToString(), call.Arguments.Children.Count, node);
                    else
                        target = (HassiumObject) call.Target.Visit(this);
                    break;
            }

            if (Program.options.Secure)
            {
                var forbidden = new List<string> {"system", "runtimecall", "input"};
                if (forbidden.Contains(call.Target.ToString()))
                    throw new ParseException("The " + call.Target + "() function is disabled for security reasons.", node);
            }


            if (target is InternalFunction && (target as InternalFunction).IsConstructor)
                throw new ParseException("Attempt to run a constructor without the 'new' operator", node);

            if (target is HassiumMethod)
            {
                var th = target as HassiumMethod;
                if (!th.IsStatic)
                if (call.Target is MemberAccessNode)
                {
                    var man = (MemberAccessNode)call.Target;
                    if (!((HassiumObject)man.Left.Visit(this)).IsInstance)
                        throw new ParseException("Non-static method can only be used with instance of class", call);
                }
            }

            bool notGood = false;
            switch(call.Target.ToString())
            {
                case "free":
                case "eval":
                    notGood = call.Arguments.Children.Count != 1;
                    break;
                case "exit":
                    notGood = call.Arguments.Children.Count != 1 && call.Arguments.Children.Count != 0;
                    break;
            }
            if(notGood)
                throw new ParseException("Incorrect arguments for function " + call.Target, node);

            var arguments =
                call.Arguments.Children.Select(
                    x => dontEval ? new HassiumString(x.ToString()) : (HassiumObject) x.Visit(this)).ToArray();

            switch (call.Target.ToString())
            {
                case "free":
                    freeVariable(arguments[0].ToString(), node);
                    return null;
                case "exit":
                    exit = true;
                    Exitcode = arguments.Length == 0 ? 0 : arguments[0].HInt().Value;
                    return null;
                case "eval":
                    var code = arguments[0].ToString();
                    var tokens = new Lexer.Lexer(code).Tokenize();
                    var hassiumParser = new Parser.Parser(tokens, code);
                    var ast = hassiumParser.Parse();
                    var interpret = new Interpreter(new SemanticAnalyser(ast).Analyse(), ast, false)
                    {
                        Globals = Globals,
                        CallStack = CallStack,
                        HandleErrors = false
                    };
                    try
                    {
                        interpret.Execute();
                    }
                    catch (Exception e)
                    {
                        if (e is ParseException)
                        {
                            if (call.Arguments.Children.Count > 0 && call.Arguments.Children[0] is StringNode)
                                throw new ParseException(e.Message, call.Arguments.Children[0].Position + ((ParseException)e).Position - 1);
                            throw new ParseException(e.Message, node);
                        }
                        else throw;
                    }
                    return null;
            }


            HassiumObject ret = target.Invoke(arguments);
            if (ReturnFunc)
                ReturnFunc = false;
            //if (ret is HassiumArray) ret = ((Array)ret).Cast<HassiumObject>().Select((s, i) => new { s, i }).ToDictionary(x => HassiumObject.ToHassiumObject(x.i), x => HassiumObject.ToHassiumObject(x.s));
            return ret;
        }

        public object Accept(IdentifierNode node)
        {
            return getVariable(node.Identifier, node);
        }

        public object Accept(IfNode node)
        {
            var ifStmt = node;
            if ((HassiumBool) (ifStmt.Predicate.Visit(this)))
                ifStmt.Body.Visit(this);
            else
                ifStmt.ElseBody.Visit(this);
            return null;
        }

        public object Accept(InstanceNode node)
        {
            var inode = node;
            var fcall = (FunctionCallNode) inode.Target;
            var arguments = (HassiumObject[]) fcall.Arguments.Visit(this);

            HassiumObject theVar = null;
            if (fcall.Target is MemberAccessNode)
                theVar = (HassiumObject) fcall.Target.Visit(this);
            else theVar = (HassiumObject) getFunction(fcall.Target.ToString(), fcall.Arguments.Children.Count, node);

            if (theVar is InternalFunction)
            {
                var iFunc = (InternalFunction) theVar;
                if (iFunc.IsConstructor)
                {
                    var ret = iFunc.Invoke(arguments);
                    ret.IsInstance = true;
                    return ret;
                }
            }
            else if (theVar is HassiumClass)
            {
                var iCl = ((HassiumClass) theVar).Clone();
                return iCl.Instanciate(arguments, fcall.Position);
            }

            throw new ParseException("No constructor found for " + fcall.Target, node);
        }

        public object Accept(LambdaFuncNode node)
        {
            var funcNode = node;
            var stackFrame = new StackFrame(SymbolTable.ChildScopes["lambda_" + funcNode.GetHashCode()]);
            if (CallStack.Any())
            {
                stackFrame.Scope.Symbols.AddRange(CallStack.Peek().Scope.Symbols);
                CallStack.Peek().Locals.All(x =>
                {
                    stackFrame.Locals.Add(x.Key, x.Value);
                    return true;
                });
            }
            return new HassiumMethod(this, (FuncNode) funcNode, stackFrame, null);
        }

        public object Accept(MemberAccessNode node)
        {
            var accessor = node;
            var target = (HassiumObject) accessor.Left.Visit(this);
            var attr = target.GetAttribute(accessor.Member, node.Position + 1);
            if (attr is InternalFunction && ((InternalFunction) attr).IsProperty)
                return ((InternalFunction) attr).Invoke();
            else
                return attr;
        }

        public object Accept(IncDecNode node)
        {
            var mnode = node;
            var tg = (HassiumObject) mnode.Target.Visit(this);
            switch (mnode.OpType)
            {
                case "++":
                    new BinOpNode(node.Position, BinaryOperation.Assignment, mnode.Target,
                        new BinOpNode(node.Position, BinaryOperation.Addition, mnode.Target,
                            new NumberNode(node.Position, 1, true))).Visit(this);
                    break;
                case "--":
                    new BinOpNode(node.Position, BinaryOperation.Assignment, mnode.Target,
                       new BinOpNode(node.Position, BinaryOperation.Subtraction, mnode.Target,
                           new NumberNode(node.Position, 1, true))).Visit(this);
                    break;
                default:
                    throw new ParseException("Unknown operation " + mnode.OpType, mnode);
            }
            return mnode.IsBefore ? mnode.Target.Visit(this) : tg;
        }

        public object Accept(NumberNode node)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (node.IsInt)
                return new HassiumInt(Convert.ToInt32(node.Value));
            return new HassiumDouble(node.Value);
        }

        public object Accept(PropertyNode node)
        {
            var prop = new HassiumProperty(node.Name, x => GetPropVal(node, x[0]), x => SetPropVal(node, x[1], x[0]),
                node.SetNode == null);
            SetVariable(node.Name, prop, node);
            return prop;
        }

        private HassiumObject GetPropVal(PropertyNode node, HassiumObject self)
        {
            var funcnode = new HassiumMethod(this,
                new FuncNode(node.GetNode.Position, "__getprop__" + node.Name + "`1", new List<string> {"this"},
                    node.GetNode.Body), SymbolTable.ChildScopes["__getprop__" + node.Name + "`1"], self);
            return funcnode.Invoke();
        }

        private HassiumObject SetPropVal(PropertyNode node, HassiumObject value, HassiumObject self)
        {
            if (node.SetNode == null)
                throw new ParseException("The property is read-only, it cannot be modified.", node);
            var funcnode = new HassiumMethod(this,
                new FuncNode(node.SetNode.Position, "__setprop__" + node.Name + "`2", new List<string> {"this", "value"},
                    node.SetNode.Body), SymbolTable.ChildScopes["__setprop__" + node.Name + "`2"], self);
            funcnode.Invoke(value);
            return null;
        }

        public object Accept(ReturnNode node)
        {
            if (IsInFunction == 0) throw new ParseException("'return' cannot be used outside a function", node);
            var returnStmt = node;
            if (returnStmt.Value != null && !returnStmt.Value.ReturnsValue)
                throw new ParseException("This node type doesn't return a value.", returnStmt.Value);
            
            
            if (returnStmt.Value == null)
            {
                CallStack.Peek().ReturnValue = null;
                ReturnFunc = true;
                return null;
            }
            else
            {
                var ret = returnStmt.Value.Visit(this);
                ReturnFunc = true;
                CallStack.Peek().ReturnValue = (HassiumObject) ret;
                return ret;
            }
        }

        public object Accept(StatementNode node)
        {
            return node.Visit(this);
        }

        public object Accept(StringNode node)
        {
            return new HassiumString(node.Value);
        }

        public object Accept(SwitchNode node)
        {
            var pred = node.Predicate.Visit(this);
            if (node.Body.Any(x => x.Values.Any(y => y.Visit(this).ToString() == pred.ToString())))
            {
                var cnode = node.Body.First(x => x.Values.Any(y => y.Visit(this).ToString() == pred.ToString()));
                cnode.Visit(this);
            }
            else if (node.DefaultBody != null)
                    node.DefaultBody.Visit(this);
            return null;
        }

        public object Accept(ThreadNode node)
        {
            var threadStmt = node;
            new Thread(() => threadStmt.Node.Visit(this)).Start();
            return null;
        }

        public object Accept(UseNode node)
        {
            if (node.IsModule)
            {
                string mname = node.Path.ToLower();
                if (Program.options.Secure)
                {
                    var forbidden = new List<string> {"io", "net", "network", "drawing"};
                    if (forbidden.Contains(mname))
                        throw new ParseException("The module " + mname + " cannot be imported for security reasons.", node);
                }
                switch (mname)
                {
                    case "io":
                        Constants.Add("File", new HassiumFile());
                        Constants.Add("Directory", new HassiumDirectory());
                        Constants.Add("Path", new HassiumPath());
                        Constants.Add("IO", new HassiumIO());
                        Constants.Add("StreamWriter",
                            new InternalFunction(
                                x =>
                                    new HassiumStreamWriter(x[0] is HassiumStream
                                        ? new StreamWriter(((HassiumStream) x[0]).Value)
                                        : new StreamWriter(x[0].ToString())), 1, false, true));
                        Constants.Add("StreamReader",
                            new InternalFunction(
                                x =>
                                    new HassiumStreamReader(x[0] is HassiumStream
                                        ? new StreamReader(((HassiumStream) x[0]).Value)
                                        : new StreamReader(x[0].ToString())), 1, false, true));
                        Constants.Add("FileStream",
                            new InternalFunction(
                                x => new HassiumFileStream(new FileStream(x[0].ToString(), FileMode.OpenOrCreate)), 1,
                                false, true));
                        Constants.Add("BinaryWriter",
                            new InternalFunction(
                                x => new HassiumBinaryWriter(new BinaryWriter(((HassiumStream) x[0]).Value)), 1, false,
                                true));
                        Constants.Add("BinaryReader",
                            new InternalFunction(
                                x => new HassiumBinaryReader(new BinaryReader(((HassiumStream) x[0]).Value)), 1, false,
                                true));
                        break;
                    case "math":
                        Constants.Add("Math", new HassiumMath());
                        break;
                    case "debug":
                        Constants.Add("Debug", new HassiumDebug());
                        Constants.Add("Interpreter", new HassiumInterpreter());
                        break;
                    case "collections":
                        Constants.Add("Stack",
                            new InternalFunction(
                                x =>
                                    new HassiumStack(x.Length == 0
                                        ? new Stack<HassiumObject>()
                                        : new Stack<HassiumObject>(x[0].HInt().Value)), new[] {0, 1}, false, true));
                        Constants.Add("Dictionary",
                            new InternalFunction(
                                x => new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>()), 0, false,
                                true));
                        break;
                    case "net":
                    case "network":
                        Constants.Add("WebClient",
                            new InternalFunction(x => new HassiumWebClient(new WebClient()), 0, false, true));
                        Constants.Add("TcpClient",
                            new InternalFunction(x => new HassiumTcpClient(new TcpClient()), 0, false, true));
                        Constants.Add("NetworkStream",
                            new InternalFunction(
                                x => new HassiumNetworkStream(new NetworkStream(((HassiumSocket)x[0]).Value)), 1, false,
                                true));
                        Constants.Add("HttpListener",
                            new InternalFunction(x => new HassiumHttpListener(new HttpListener()), 0, false, true));
                        Constants.Add("Dns", new HassiumDns());
                        Constants.Add("WebUtility", new HassiumWebUtility());
                        Constants.Add("Socket",
                            new InternalFunction(
                                x =>
                                    new HassiumSocket(new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                                        ProtocolType.Tcp)), 0, false, true));
                        break;
                    case "text":
                        Constants.Add("StringBuilder",
                            new InternalFunction(x => new HassiumStringBuilder(new StringBuilder()), 0, false, true));
                        Constants.Add("Encoding",
                            new InternalFunction(x => new HassiumEncoding(x[0].HString()), 1, false, true));
                        if (!Program.options.Secure)
                        {
                            Constants.Add("TextWriter",
                                new InternalFunction(x => new HassiumTextWriter(File.CreateText(x[0].ToString())), 1,
                                    false, true));
                            Constants.Add("TextReader",
                                new InternalFunction(x => new HassiumTextReader(File.OpenText(x[0].ToString())), 1,
                                    false, true));
                        }
                        break;
                    case "drawing":
                        Constants.Add("Color",
                            new InternalFunction(x => new HassiumColor(x), new[] {1, 3, 4, 5}, false, true));
                        Constants.Add("Bitmap",
                            new InternalFunction(x => new HassiumBitmap(x), new[] {1, 2}, false, true));
                        Constants.Add("Image",
                            new InternalFunction(x => new HassiumImage(x[0].HString()), 1, false, true));
                        break;
                    default:
                        throw new Exception("Unknown Module: " + node.Path);
                }
            }
            else if (node.IsLibrary)
            {
                foreach (var entry in GetFunctions(node.Path))
                    Globals.Add(entry.Key, entry.Value);
            }
            else
            {
                Interpreter inter = new Interpreter(false);

                string code = File.ReadAllText(node.Path);
                Parser.Parser hassiumParser = new Parser.Parser(new Lexer.Lexer(code).Tokenize(), code);
                AstNode ast = hassiumParser.Parse();
                inter.SymbolTable = new SemanticAnalyser(ast).Analyse();
                inter.Code = ast;
                inter.Execute();

                if (node.Global)
                    foreach (var entry in inter.Globals)
                    {
                        if (Globals.ContainsKey(entry.Key))
                            Globals.Remove(entry.Key);
                        Globals.Add(entry.Key, entry.Value);
                    }
                else
                {
                    var modu = new HassiumModule(node.Name);
                    foreach (var entry in inter.Globals)
                        modu.SetAttribute(entry.Key, entry.Value);
                    
                    SetVariable(node.Name, modu, node);
                }
            }
            return null;
        }

        public object Accept(TryNode node)
        {
            var tryStmt = node;
            try
            {
                tryStmt.Body.Visit(this);
            }
            catch
            {
                tryStmt.CatchBody.Visit(this);
            }
            finally
            {
                if (tryStmt.FinallyBody != null)
                    tryStmt.FinallyBody.Visit(this);
            }
            return null;
        }

        public object Accept(UnaryOpNode node)
        {
            return interpretUnaryOp(node);
        }

        public object Accept(UncheckedNode node)
        {
            unchecked
            {
                node.Node.Visit(this);
            }
            return null;
        }

        public object Accept(WhileNode node)
        {
            var whileStmt = node;
            isInLoop++;
            int counter = 0;
            while ((HassiumBool) whileStmt.Predicate.Visit(this))
            {
                counter++;
                whileStmt.Body.Visit(this);
                if (continueLoop) continueLoop = false;
                if (breakLoop)
                {
                    breakLoop = false;
                    break;
                }
            }

            if (counter == 0)
            {
                if (whileStmt.ElseBody != null)
                    whileStmt.ElseBody.Visit(this);
            }
            isInLoop--;
            return null;
        }

        public object Accept(DoNode node)
        {
            var doStmt = node;
            isInLoop++;
            do
            {
                doStmt.DoBody.Visit(this);
                if (continueLoop)
                    continueLoop = false;
                if (breakLoop)
                {
                    breakLoop = false;
                    break;
                }
            } while ((HassiumBool) doStmt.Predicate.Visit(this));

            isInLoop--;
            return null;
        }

        private void visitSubnodes(AstNode node)
        {
            position.Push(-1);
            nodePos.Push(-1);
            for (int index = 0; index < node.Children.Count; index++)
            {
                position.Pop();
                position.Push(index);
                
                var nd = node.Children[index];
                nodePos.Pop();
                nodePos.Push(nd.Position);
                nd.Visit(this);
                if(gotoposition)
                {
                    index = positiontogo;
                    gotoposition = false;
                    positiontogo = -1;
                    continue;
                }
                if (continueLoop || breakLoop || ReturnFunc || exit) break;
            }
            position.Pop();
            nodePos.Pop();
        }
    }
}