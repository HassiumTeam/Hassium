/* Credit to contributer Zdimension, who added the lines in interpretBinaryOp for the
implementation of string concat amoung other additions and foreach loop*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hassium.Functions;
using Hassium.Parser.Ast;

namespace Hassium
{
    public class Interpreter
    {
        public Stack<StackFrame> CallStack = new Stack<StackFrame>();
        public static Dictionary<string, object> Globals = new Dictionary<string, object>();

        public static Dictionary<string, object> Constants = new Dictionary<string, object>
        {
            {"true", true},
            {"false", false},
            {"null", null},
        };

        private SymbolTable table;
        private AstNode code;

        public Interpreter(SymbolTable symbolTable, AstNode code)
        {
            this.code = code;
            table = symbolTable;
            foreach (var entry in GetFunctions())
                Globals.Add(entry.Key, entry.Value);
        }

        public object GetVariable(string name)
        {
            if (Constants.ContainsKey(name))
                return Constants[name];
            if (CallStack.Count > 0 && (CallStack.Peek().Scope.Symbols.Contains(name) || CallStack.Peek().Locals.ContainsKey(name)))
                return CallStack.Peek().Locals[name];
            if (Globals.ContainsKey(name))
                return Globals[name];
            else throw new ArgumentException("The variable '" + name + "' doesn't exist.");
        }

        public bool HasVariable(string name, bool onlyglobal = false)
        {
            return onlyglobal
                ? Globals.ContainsKey(name) || Constants.ContainsKey(name)
                : Globals.ContainsKey(name) || Constants.ContainsKey(name) ||
                  (CallStack.Count > 0 && (CallStack.Peek().Scope.Symbols.Contains(name) || CallStack.Peek().Locals.ContainsKey(name)));
        }

        public void SetGlobalVariable(string name, object value)
        {
            if (Constants.ContainsKey(name))
                throw new ArgumentException("Can't change the value of the internal constant '" + name + "'.");

            Globals[name] = value;
        }

        public void SetLocalVariable(string name, object value)
        {
            if (Constants.ContainsKey(name))
                throw new ArgumentException("Can't change the value of the internal constant '" + name + "'.");

            if (CallStack.Count > 0)
                CallStack.Peek().Locals[name] = value;
        }

        public void SetVariable(string name, object value, bool forceglobal = false, bool onlyexist = false)
        {
            if (!forceglobal && CallStack.Count > 0 && (!onlyexist || (CallStack.Peek().Scope.Symbols.Contains(name) || CallStack.Peek().Locals.ContainsKey(name))))
                SetLocalVariable(name, value);
            else
                SetGlobalVariable(name, value);
        }

        public void FreeVariable(string name, bool forceglobal = false)
        {
            if(Constants.ContainsKey(name)) throw new ArgumentException("Can't delete internal constant '" + name + "'.");
            if (forceglobal)
            {
                if(!Globals.ContainsKey(name)) throw new ArgumentException("The global variable '" + name + "' doesn't exist.");
                Globals.Remove(name);
            }
            else
            {
                if(!HasVariable(name)) throw new ArgumentException("The variable '" + name + "' doesn't exist.");
                if (CallStack.Count > 0 && (CallStack.Peek().Scope.Symbols.Contains(name) || CallStack.Peek().Locals.ContainsKey(name)))
                    CallStack.Peek().Locals.Remove(name);
                else
                    Globals.Remove(name);
            }
        }

        public void Execute()
        {
            foreach (var node in code.Children)
            {
                if (node is FuncNode)
                {
                    var fnode = ((FuncNode)node);
                    var scope = table.ChildScopes[fnode.Name];
                    SetVariable(fnode.Name, new HassiumFunction(this, fnode, scope));
                }
            }
            foreach (var node in code.Children)
            {
                ExecuteStatement(node);
            }
        }

        private void interpretBinaryAssign(BinOpNode node)
        {
            SetVariable(node.Left.ToString(), interpretBinaryOp(node, true), false, true);         
        }

        private object interpretBinaryOp(BinOpNode node, bool isAssign = false)
        {
            switch (isAssign ? node.AssignOperation : node.BinOp)
            {
                case BinaryOperation.Addition:
                    if (EvaluateNode(node.Left) is string || EvaluateNode(node.Right) is string)
                        return EvaluateNode(node.Left) + EvaluateNode(node.Right).ToString();
                    return Convert.ToDouble((EvaluateNode(node.Left))) + Convert.ToDouble((EvaluateNode(node.Right)));
                case BinaryOperation.Subtraction:
                    return Convert.ToDouble((EvaluateNode(node.Left))) - Convert.ToDouble((EvaluateNode(node.Right)));
                case BinaryOperation.Division:
                    return Convert.ToDouble((EvaluateNode(node.Left))) / Convert.ToDouble((EvaluateNode(node.Right)));
                case BinaryOperation.Multiplication:
                    if ((EvaluateNode(node.Left) is string && EvaluateNode(node.Right) is double) ||
                        EvaluateNode(node.Right) is string && EvaluateNode(node.Left) is double)
                    {
                        var p1 = EvaluateNode(node.Left);
                        var p2 = EvaluateNode(node.Right);
                        if (p1 is string)
                            return string.Concat(Enumerable.Repeat(p1, Convert.ToInt32(p2)));
                        else
                            return string.Concat(Enumerable.Repeat(p2, Convert.ToInt32(p1)));
                    }
                    return Convert.ToDouble((EvaluateNode(node.Left))) * Convert.ToDouble((EvaluateNode(node.Right)));
                case BinaryOperation.Assignment:
                    if (!(node.Left is IdentifierNode))
                        throw new Exception("Not a valid identifier");
                    var right = EvaluateNode(node.Right);
                    SetVariable(node.Left.ToString(), right);
                    return right;
                case BinaryOperation.Equals:
                    return EvaluateNode(node.Left).GetHashCode() == EvaluateNode(node.Right).GetHashCode();
                case BinaryOperation.And:
                    return Convert.ToBoolean(EvaluateNode(node.Left)) && Convert.ToBoolean(EvaluateNode(node.Right));
                case BinaryOperation.Or:
                    return Convert.ToBoolean(EvaluateNode(node.Left)) || Convert.ToBoolean(EvaluateNode(node.Right));
                case BinaryOperation.NotEqualTo:
                    return EvaluateNode(node.Left).GetHashCode() != EvaluateNode(node.Right).GetHashCode();
                case BinaryOperation.LessThan:
                    return Convert.ToDouble(EvaluateNode(node.Left)) < Convert.ToDouble(EvaluateNode(node.Right));
                case BinaryOperation.GreaterThan:
                    return Convert.ToDouble(EvaluateNode(node.Left)) > Convert.ToDouble(EvaluateNode(node.Right));
                case BinaryOperation.GreaterOrEqual:
                    return Convert.ToDouble(EvaluateNode(node.Left)) >= Convert.ToDouble(EvaluateNode(node.Right));
                case BinaryOperation.LesserOrEqual:
                    return Convert.ToDouble(EvaluateNode(node.Left)) <= Convert.ToDouble(EvaluateNode(node.Right));
                case BinaryOperation.Xor:
                    return Convert.ToBoolean(EvaluateNode(node.Left)) ^ Convert.ToBoolean(EvaluateNode(node.Right));
                case BinaryOperation.BitshiftLeft:
                    return Convert.ToInt32(EvaluateNode(node.Left)) << Convert.ToInt32(EvaluateNode(node.Right));
                case BinaryOperation.BitshiftRight:
                    return Convert.ToInt32(EvaluateNode(node.Left)) >> Convert.ToInt32(EvaluateNode(node.Right));
                case BinaryOperation.Modulus:
                    return Convert.ToDouble(EvaluateNode(node.Left)) % Convert.ToDouble(EvaluateNode(node.Right));

                case BinaryOperation.Pow:
                    return Math.Pow(Convert.ToDouble(EvaluateNode(node.Left)), Convert.ToDouble(EvaluateNode(node.Right)));
                case BinaryOperation.Root:
                    return Math.Pow(Convert.ToDouble(EvaluateNode(node.Left)), 1.0 / Convert.ToDouble(EvaluateNode(node.Right)));
            }
            // Raise error
            return -1;
        }

        private object interpretUnaryOp(UnaryOpNode node)
        {
            switch (node.UnOp)
            {
                case UnaryOperation.Not:
                    return !Convert.ToBoolean((EvaluateNode(node.Value)));
                case UnaryOperation.Negate:
                    return -Convert.ToDouble((EvaluateNode(node.Value)));
                case UnaryOperation.Complement:
                    return ~(int)Convert.ToDouble((EvaluateNode(node.Value)));
            }
            //Raise error
            return -1;
        }

        private int inLoop = 0;
        private bool continueLoop = false;
        private bool breakLoop = false;

        private int inFunc = 0;
        private bool returnFunc = false;

        public void ExecuteStatement(AstNode node)
        {
            if (CallStack.Count > 0 && CallStack.Peek().ReturnValue != null)
                return;
            if (node is CodeBlock)
            {
                foreach (var anode in node.Children)
                {
                    ExecuteStatement(anode);
                    if (continueLoop || breakLoop || returnFunc) return;
                }
            }
            else if (node is IfNode)
            {
                var ifStmt = (IfNode) (node);
                if ((bool) (EvaluateNode(ifStmt.Predicate)))
                    ExecuteStatement(ifStmt.Body);
                else
                    ExecuteStatement(ifStmt.ElseBody);
            }
            else if (node is WhileNode)
            {
                var whileStmt = (WhileNode) (node);
                inLoop++;
                if ((bool) (EvaluateNode(whileStmt.Predicate)))
                    while ((bool) EvaluateNode(whileStmt.Predicate))
                    {
                        ExecuteStatement(whileStmt.Body);
                        if (continueLoop) continueLoop = false;
                        if(breakLoop)
                        {
                            breakLoop = false;
                            break;
                        }
                    }
                else
                    ExecuteStatement(whileStmt.ElseBody);
                inLoop--;
            }
            else if (node is ForNode)
            {
                var forStmt = (ForNode) (node);
                inLoop++;
                ExecuteStatement(forStmt.Left);
                while ((bool) EvaluateNode(forStmt.Predicate))
                {
                    ExecuteStatement(forStmt.Body);
                    if (continueLoop) continueLoop = false;
                    if(breakLoop)
                    {
                        breakLoop = false;
                        break;
                    }
                    ExecuteStatement(forStmt.Right);
                }
                inLoop--;
            }
            else if (node is ForEachNode)
            {
                var forStmt = (ForEachNode) (node);
                var needlestmt = forStmt.Needle.ToString();
                var haystack = EvaluateNode(forStmt.Haystack);
                inLoop++;
                SetVariable(needlestmt, null);
                if ((haystack as IEnumerable) == null)
                    throw new ArgumentException("'" + haystack +
                                                "' is not an array and therefore can not be used in foreach.");

                foreach (var needle in (IEnumerable) haystack)
                {
                    SetVariable(needlestmt, needle);
                    ExecuteStatement(forStmt.Body);
                    if (continueLoop) continueLoop = false;
                    if(breakLoop)
                    {
                        breakLoop = false;
                        break;
                    }
                }
                FreeVariable(needlestmt);
                inLoop--;
            }
            else if (node is TryNode)
            {
                var tryStmt = (TryNode) (node);
                try
                {
                    ExecuteStatement(tryStmt.Body);
                }
                catch
                {
                    ExecuteStatement(tryStmt.CatchBody);
                }
                finally
                {
                    ExecuteStatement(tryStmt.FinallyBody);
                }
            }
            else if (node is ThreadNode)
            {
                var threadStmt = (ThreadNode) (node);
                Task.Factory.StartNew(() => ExecuteStatement(threadStmt.Node));
            }
            else
            {
                EvaluateNode(node);
                if (continueLoop || breakLoop || returnFunc) return;
            }
        }

        public object EvaluateNode(AstNode node)
        {
            if (node is NumberNode)
            {
                return ((NumberNode)node).Value;
            }
            else if (node is StringNode)
            {
                return ((StringNode)node).Value;
            }
            else if (node is BinOpNode)
            {
                if (((BinOpNode)node).IsAssign)
                {
                    interpretBinaryAssign((BinOpNode)node);
                    return null;
                }
                return interpretBinaryOp((BinOpNode)node);
            }
            else if (node is UnaryOpNode)
            {
                return interpretUnaryOp((UnaryOpNode)node);
            }
            else if (node is FunctionCallNode)
            {
                var call = (FunctionCallNode) node;

                var target = EvaluateNode(call.Target) as IFunction;

                if (target == null)
                    throw new Exception("Attempt to run a non-valid function!");

                var arguments = new object[call.Arguments.Children.Count];
                for (var x = 0; x < call.Arguments.Children.Count; x++)
                {
                    arguments[x] = EvaluateNode(call.Arguments.Children[x]);
                    if (arguments[x] is double && (((double)(arguments[x])) % 1 == 0))
                        arguments[x] = (int)(double)arguments[x];
                }
                inFunc++;
                var rval = target.Invoke(arguments);
                if (returnFunc) returnFunc = false;
                inFunc--;
                return rval;
            }
            else if (node is IdentifierNode)
            {
                return GetVariable(((IdentifierNode)node).Identifier);
            }
            else if(node is ArrayInitializerNode)
            {
                return ((ArrayInitializerNode) node).Value.Select(x => EvaluateNode((AstNode)x)).ToArray();
            }
            else if (node is ArrayGetNode)
            {
                var call = (ArrayGetNode)node;
                Array theArray = null;
                var evaluated = EvaluateNode(call.Target);
                if (evaluated is string) theArray = evaluated.ToString().ToArray();
                else if (evaluated is Array) theArray = (Array) evaluated;
                else
                {
                    throw new Exception("The [] operator only applies to objects of type Array or String.");
                }
                var arguments = new object[call.Arguments.Children.Count];
                for (var x = 0; x < call.Arguments.Children.Count; x++)
                    arguments[x] = EvaluateNode(call.Arguments.Children[x]);

                var arid = (int)double.Parse(string.Join("", arguments));
                if (arid < 0 || arid >= theArray.Length)
                    throw new ArgumentOutOfRangeException();
                var retvalue = theArray.GetValue(arid);
                if (retvalue is AstNode) return EvaluateNode((AstNode) retvalue);
                else return retvalue;
            }
            else if (node is ReturnNode)
            {
                if (inFunc == 0) throw new Exception("'return' cannot be used outside a function");
                var returnStmt = (ReturnNode)(node);
                CallStack.Peek().ReturnValue = EvaluateNode(returnStmt.Value);
                returnFunc = true;
            }
            else if (node is ContinueNode)
            {
                if (inLoop == 0) throw new Exception("'continue' cannot be used outside a loop");
                continueLoop = true;
            }
            else if (node is BreakNode)
            {
                if (inLoop == 0) throw new Exception("'break' cannot be used outside a loop");
                breakLoop = true;
            }
            else
            {
                //Raise error
            }

            return 0;
        }

        public static Dictionary<string, InternalFunction> GetFunctions(string path = "")
        {
            var result = new Dictionary<string, InternalFunction>();

            var testAss = path == "" ? Assembly.GetExecutingAssembly() : Assembly.LoadFrom(path);

            foreach (var type in testAss.GetTypes())
            {
                if (type.GetInterface(typeof(ILibrary).FullName) != null)
                {
                    if (type.GetMethod("GetFunctions") != null) // TODO: DEPRECATED
                    {
                        var method = type.GetMethod("GetFunctions");
                        var rdict =
                            (Dictionary<string, InternalFunction>)
                                (method.Invoke(Activator.CreateInstance(type, null), null));
                        foreach (var entry in rdict)
                            result.Add(entry.Key, entry.Value);
                    }
                    else
                    {
                        foreach (var myfunc in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                        {
                            var theattr1 = myfunc.GetCustomAttributes(typeof (IntFunc), true);
                            foreach (var theattr in theattr1.OfType<IntFunc>())
                            {
                                var rfunc = new InternalFunction(
                                    (HassiumFunctionDelegate)
                                        Delegate.CreateDelegate(typeof (HassiumFunctionDelegate), myfunc));
                                result.Add(theattr.Name, rfunc);
                                if(theattr.Alias != "") result.Add(theattr.Alias, rfunc);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

