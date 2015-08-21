/* Credit to contributer Zdimension, who added the lines in interpretBinaryOp for the
implementation of string concat amoung other additions and foreach loop*/

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hassium
{
    public class Interpreter
    {
        private static Dictionary<string, object> variables = new Dictionary<string, object>();

        private static Dictionary<string, object> constants = new Dictionary<string, object>
        {
            {"true", true},
            {"false", false},
            {"null", null }
        };

        private static Dictionary<string, IFunction> ext_methods = new Dictionary<string, IFunction>();

        private static Dictionary<string, AstNode[]> methods = new Dictionary<string, AstNode[]>();
        private AstNode code;

        public Interpreter(AstNode code)
        {
            //variables = new Dictionary<string, object>();
            this.code = code;
            foreach (var entries in GetFunctions())
                foreach (var entry in entries)
                    SetFunction(entry.Key, entry.Value);
        }

        public void Execute()
        {
            foreach (AstNode node in this.code.Children)
            {
                executeStatement(node);
            }
        }

        private object interpretBinaryOp(BinOpNode node)
        {
            switch (node.BinOp)
            {
                case BinaryOperation.Addition:
                    if (evaluateNode(node.Left) is string || evaluateNode(node.Right) is string)
                        return evaluateNode(node.Left) + evaluateNode(node.Right).ToString();
                    return Convert.ToDouble((evaluateNode(node.Left))) + Convert.ToDouble((evaluateNode(node.Right)));
                case BinaryOperation.Subtraction:
                    return Convert.ToDouble((evaluateNode(node.Left))) - Convert.ToDouble((evaluateNode(node.Right)));
                case BinaryOperation.Division:
                    return Convert.ToDouble((evaluateNode(node.Left))) / Convert.ToDouble((evaluateNode(node.Right)));
                case BinaryOperation.Multiplication:
                    if ((evaluateNode(node.Left) is string && evaluateNode(node.Right) is double) ||
                        evaluateNode(node.Right) is string && evaluateNode(node.Left) is double)
                    {
                        var p1 = evaluateNode(node.Left);
                        var p2 = evaluateNode(node.Right);
                        if (p1 is string) return string.Concat(Enumerable.Repeat(p1, Convert.ToInt32(p2)));
                        else return string.Concat(Enumerable.Repeat(p2, Convert.ToInt32(p1)));
                    }
                    return Convert.ToDouble((evaluateNode(node.Left))) * Convert.ToDouble((evaluateNode(node.Right)));
                case BinaryOperation.Assignment:
                    if (!(node.Left is IdentifierNode))
                        throw new Exception("Not a valid identifier");
                    var right = evaluateNode(node.Right);
                    SetVar(node.Left.ToString(), right);
                    return right;
                case BinaryOperation.Equals:
                    return evaluateNode(node.Left).GetHashCode() == evaluateNode(node.Right).GetHashCode();
                case BinaryOperation.And:
                    return (bool)(evaluateNode(node.Left)) && (bool)(evaluateNode(node.Right));
                case BinaryOperation.Or:
                    return (bool)(evaluateNode(node.Left)) || (bool)(evaluateNode(node.Right));
                case BinaryOperation.NotEqualTo:
                    return evaluateNode(node.Left).GetHashCode() != evaluateNode(node.Right).GetHashCode();
                case BinaryOperation.LessThan:
                    return Convert.ToDouble(evaluateNode(node.Left)) < Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.GreaterThan:
                    return Convert.ToDouble(evaluateNode(node.Left)) > Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.GreaterOrEqual:
                    return Convert.ToDouble(evaluateNode(node.Left)) >= Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.LesserOrEqual:
                    return Convert.ToDouble(evaluateNode(node.Left)) <= Convert.ToDouble(evaluateNode(node.Right));
                case BinaryOperation.Xor:
                    return (bool)(evaluateNode(node.Left)) ^ (bool)(evaluateNode(node.Right));
                case BinaryOperation.BitshiftLeft:
                    return (byte)(evaluateNode(node.Left)) << (byte)(evaluateNode(node.Right));
                case BinaryOperation.BitshiftRight:
                    return (byte)(evaluateNode(node.Left)) >> (byte)(evaluateNode(node.Right));
                case BinaryOperation.Modulus:
                    return (double)(evaluateNode(node.Left)) % (double)(evaluateNode(node.Right));
            }
            // Raise error
            return -1;
        }

        private object interpretUnaryOp(UnaryOpNode node)
        {
            switch (node.UnOp)
            {
                case UnaryOperation.Not:
                    return !(bool)((evaluateNode(node.Value)));
                case UnaryOperation.Negate:
                    return -(double)((evaluateNode(node.Value)));
                case UnaryOperation.Complement:
                    return ~(int)(double)((evaluateNode(node.Value)));
            }
            //Raise error
            return -1;
        }

        public static void SetVar(string varName, object val)
        {
            if(constants.ContainsKey(varName)) throw new ArgumentException("'" + varName + "' is an internal constant and can not be used as an assignment target.");
            if (!variables.ContainsKey(varName)) variables.Add(varName, val);
            else variables[varName] = val;
        }

        public static void SetFunction(string funcName, IFunction func)
        {
            if (constants.ContainsKey(funcName)) throw new ArgumentException("'" + funcName + "' is an internal constant and can not be used as an assignment target.");
            if (variables.ContainsKey(funcName)) throw new ArgumentException("A variable with the name '" + funcName + "' already exists.");
            if (ext_methods.ContainsKey(funcName) || methods.ContainsKey(funcName)) throw new ArgumentException("A function with the name '" + funcName + "' already exists.");
            else ext_methods.Add(funcName, func);
        }

        public static void SetFunction(string funcName, AstNode[] func)
        {
            if (constants.ContainsKey(funcName)) throw new ArgumentException("'" + funcName + "' is an internal constant and can not be used as an assignment target.");
            if (variables.ContainsKey(funcName)) throw new ArgumentException("A variable with the name '" + funcName + "' already exists.");
            if (ext_methods.ContainsKey(funcName) || methods.ContainsKey(funcName)) throw new ArgumentException("A function with the name '" + funcName + "' already exists.");
            else methods.Add(funcName, func);
        }

        public static object GetVar(string varName)
        {
            if (constants.ContainsKey(varName)) return constants[varName];
            if (variables.ContainsKey(varName)) return variables[varName];
            else throw new ArgumentException("The variable '" + varName + "' doesn't exist.");
        }

        public static object GetFunction(string varName)
        {
            if (methods.ContainsKey(varName)) return methods[varName];
            if (ext_methods.ContainsKey(varName)) return ext_methods[varName];
            else throw new ArgumentException("The function '" + varName + "' doesn't exist.");
        }

        public static bool HasVar(string varName)
        {
            return constants.ContainsKey(varName) || variables.ContainsKey(varName);
        }

        public static bool HasFunction(string varName)
        {
            return methods.ContainsKey(varName) || ext_methods.ContainsKey(varName);
        }

        public static void FreeVar(string varName)
        {
            if (constants.ContainsKey(varName)) throw new ArgumentException("'" + varName + "' is an internal constant and can not be removed.");
            if (!variables.ContainsKey(varName)) throw new ArgumentException("The variable '" + varName + "' doesn't exist.");
            else variables.Remove(varName);
        }

        private void executeStatement(AstNode node)
        {
            if (node is CodeBlock)
                foreach (AstNode anode in node.Children)
                    executeStatement(anode);
            else if (node is IfNode)
            {
                IfNode ifStmt = (IfNode)(node);
                if ((bool)(evaluateNode(ifStmt.Predicate)))
                    executeStatement(ifStmt.Body);
                else
                    executeStatement(ifStmt.ElseBody);
            }
            else if (node is WhileNode)
            {
                WhileNode whileStmt = (WhileNode)(node);
                if ((bool)(evaluateNode(whileStmt.Predicate)))
                    while ((bool)(evaluateNode(whileStmt.Predicate)))
                    {
                        executeStatement(whileStmt.Body);
                    }
                else
                    executeStatement(whileStmt.ElseBody);
            }
            else if (node is ForNode)
            {
                ForNode forStmt = (ForNode)(node);
                executeStatement(forStmt.Left);
                while (((bool)(evaluateNode(forStmt.Predicate))))
                {
                    executeStatement(forStmt.Body);
                    executeStatement(forStmt.Right);
                }
            }
            else if (node is ForEachNode)
            {
                ForEachNode forStmt = (ForEachNode)(node);
                var needlestmt = forStmt.Needle.ToString();
                var haystack = evaluateNode(forStmt.Haystack);
                if((haystack as IEnumerable) == null) throw new ArgumentException("'" + haystack + "' is not an array and therefore can not be used in foreach.");
                
                foreach(var needle in (IEnumerable)haystack)
                {
                    SetVar(needlestmt, needle);
                    executeStatement(forStmt.Body);
                }
                FreeVar(needlestmt);
            }
            else if (node is TryNode)
            {
                TryNode tryStmt = (TryNode)(node);
                try
                {
                    executeStatement(tryStmt.Body);
                }
                catch
                {
                    executeStatement(tryStmt.CatchBody);
                }
            }
            else if (node is ThreadNode)
            {
                ThreadNode threadStmt = (ThreadNode)(node);
                Task.Factory.StartNew(() => executeStatement(threadStmt.Node));
            }
            else if (node is FuncNode)
            {
                FuncNode funcStmt = (FuncNode)(node);
                SetFunction(funcStmt.Name, new AstNode[2] { funcStmt.Arguments, funcStmt.Body } );
            }
            else
            {
                evaluateNode(node);
            }
        }

        private object evaluateNode(AstNode node)
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
                return interpretBinaryOp((BinOpNode)node);
            }
            else if (node is UnaryOpNode)
            {
                return interpretUnaryOp((UnaryOpNode)node);
            }
            else if (node is FunctionCallNode)
            {
                FunctionCallNode call = node as FunctionCallNode;
                if (methods.ContainsKey(call.Target.ToString()))
                {
                    executeStatement(methods[call.Target.ToString()][1]);
                    return null;
                }

                IFunction target = evaluateNode(call.Target) as IFunction;

                if (target == null)
                    throw new Exception("Attempt to run a non-valid function!");

                object[] arguments = new object[call.Arguments.Children.Count];
                for (int x = 0; x < call.Arguments.Children.Count; x++)
                {
                    arguments[x] = evaluateNode(call.Arguments.Children[x]);
                    if (arguments[x] is double && (((double) (arguments[x])) % 1 == 0))
                        arguments[x] = (int) (double) arguments[x];
                }
                return target.Invoke(arguments);
            }
            else if (node is IdentifierNode)
            {
                return HasFunction(node.ToString()) ? GetFunction(node.ToString()) : GetVar(node.ToString());
            }
            else if (node is ArrayGetNode)
            {
                var call = (ArrayGetNode)node;
                var arrname = call.Target.ToString();
                Array monarr = (Array)GetVar(arrname);
                var arguments = new object[call.Arguments.Children.Count];
                for (var x = 0; x < call.Arguments.Children.Count; x++)
                    arguments[x] = evaluateNode(call.Arguments.Children[x]);

                var arid = (int)double.Parse(string.Join("", arguments));
                if (arid < 0 || arid >= monarr.Length) throw new ArgumentOutOfRangeException();
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
            List<Dictionary<string, InternalFunction>> result = new List<Dictionary<string, InternalFunction>>();
            Assembly testAss;

            if (path != "")
                testAss = Assembly.LoadFrom(path);
            else
                testAss = Assembly.GetExecutingAssembly();

            foreach(Type type in testAss.GetTypes())
            {
                if (type.GetInterface (typeof (ILibrary).FullName) != null)
                {
                    ILibrary ilib = (ILibrary)Activator.CreateInstance(type);
                    result.Add(ilib.GetFunctions());
                }
            }
            return result;
        }

    }
}

