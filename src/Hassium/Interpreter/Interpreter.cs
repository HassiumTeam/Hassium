/* Credit to contributer Zdimension, who added the lines in interpretBinaryOp for the
implementation of string concat amoung other additions and foreach loop*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hassium
{
    public class Interpreter
    {
        public Stack<StackFrame> CallStack = new Stack<StackFrame>();
        public static Dictionary<string, object> Globals = new Dictionary<string, object>();
        private SymbolTable table;
        private AstNode code;

        public Interpreter(SymbolTable symbolTable, AstNode code)
        {
            //Globals = new Dictionary<string, object>();
            this.code = code;
            table = symbolTable;
            foreach (var entry in GetFunctions().SelectMany(entries => entries))
                Globals.Add(entry.Key, entry.Value);
        }

        public void Execute()
        {
            foreach (var node in code.Children)
            {
                if (node is FuncNode)
                {
                    var fnode = ((FuncNode)node);
                    var scope = table.ChildScopes[fnode.Name];
                    Globals[fnode.Name] = new HassiumFunction(this, fnode, scope);
                }
            }
            foreach (var node in code.Children)
            {
                ExecuteStatement(node);
            }
        }

        private void interpretBinaryAssign(BinOpNode node)
        {
            object right = interpretBinaryOp(node, true);
            if (CallStack.Count > 0 && CallStack.Peek().Scope.Symbols.Contains(node.Left.ToString()))
                CallStack.Peek().Locals[node.Left.ToString()] = right;
            else
                Globals[node.Left.ToString()] = right;
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

                    if (CallStack.Count > 0 && CallStack.Peek().Scope.Symbols.Contains(node.Left.ToString()))
                        CallStack.Peek().Locals[node.Left.ToString()] = right;
                    else
                        Globals[node.Left.ToString()] = right;
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

        public void ExecuteStatement(AstNode node)
        {
            if (CallStack.Count > 0 && CallStack.Peek().ReturnValue != null)
                return;
            if (node is CodeBlock)
                foreach (var anode in node.Children)
                    ExecuteStatement(anode);
            else if (node is IfNode)
            {
                var ifStmt = (IfNode)(node);
                if ((bool)(EvaluateNode(ifStmt.Predicate)))
                    ExecuteStatement(ifStmt.Body);
                else
                    ExecuteStatement(ifStmt.ElseBody);
            }
            else if (node is WhileNode)
            {
                var whileStmt = (WhileNode)(node);
                if ((bool)(EvaluateNode(whileStmt.Predicate)))
                    while ((bool)EvaluateNode(whileStmt.Predicate))
                    {
                        ExecuteStatement(whileStmt.Body);
                    }
                else
                    ExecuteStatement(whileStmt.ElseBody);
            }
            else if (node is ForNode)
            {
                var forStmt = (ForNode)(node);
                ExecuteStatement(forStmt.Left);
                while ((bool)EvaluateNode(forStmt.Predicate))
                {
                    ExecuteStatement(forStmt.Body);
                    ExecuteStatement(forStmt.Right);
                }
            }
            else if (node is ForEachNode)
            {
                var forStmt = (ForEachNode)(node);
                var needlestmt = forStmt.Needle.ToString();
                var haystack = EvaluateNode(forStmt.Haystack);
                if (!Globals.ContainsKey(needlestmt))
                    Globals.Add(needlestmt, null);
                if ((haystack as IEnumerable) == null)
                    throw new ArgumentException("'" + haystack + "' is not an array and therefore can not be used in foreach.");
                    
                foreach (var needle in (IEnumerable)haystack)
                {
                    Globals[needlestmt] = needle;
                    ExecuteStatement(forStmt.Body);
                }
                Globals.Remove(needlestmt);
            }
            else if (node is TryNode)
            {
                var tryStmt = (TryNode)(node);
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
                var threadStmt = (ThreadNode)(node);
                Task.Factory.StartNew(() => ExecuteStatement(threadStmt.Node));
            }
            else if (node is ReturnNode)
            {
                var returnStmt = (ReturnNode)(node);
                CallStack.Peek().ReturnValue = EvaluateNode(returnStmt.Value);
            }
            else
            {
                EvaluateNode(node);
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
                return target.Invoke(arguments);
            }
            else if (node is IdentifierNode)
            {
                var name = ((IdentifierNode)node).Identifier;
                if (CallStack.Count > 0 && CallStack.Peek().Scope.Symbols.Contains(name))
                    return CallStack.Peek().Locals[name];
                else
                    return Globals[name];
            }
            else if (node is ArrayGetNode)
            {
                var call = (ArrayGetNode)node;
                var arrname = call.Target.ToString();
                Array monarr = null;
                if (Globals.ContainsKey(arrname))
                    monarr = (Array)Globals[arrname];
                else
                    throw new Exception("Undefined variable: " + node);
                var arguments = new object[call.Arguments.Children.Count];
                for (var x = 0; x < call.Arguments.Children.Count; x++)
                    arguments[x] = EvaluateNode(call.Arguments.Children[x]);

                var arid = (int)double.Parse(string.Join("", arguments));
                if (arid < 0 || arid >= monarr.Length)
                    throw new ArgumentOutOfRangeException();
                return monarr.GetValue(arid);
            }
            else
            {
                //Raise error
            }

            return 0;
        }

        public static List<Dictionary<string, InternalFunction>> GetFunctions(string path = "")
        {
            var result = new List<Dictionary<string, InternalFunction>>();

            var testAss = path == "" ? Assembly.GetExecutingAssembly() : Assembly.LoadFrom(path);

            foreach (var type in testAss.GetTypes())
            {
                if (type.GetInterface(typeof(ILibrary).FullName) != null)
                {
                    var ilib = (ILibrary)Activator.CreateInstance(type);
                    result.Add(ilib.GetFunctions());
                }
            }
            return result;
        }
    }
}

