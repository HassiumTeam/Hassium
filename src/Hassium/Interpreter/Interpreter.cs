using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace Hassium
{
    public class Interpreter
    {
        public static Dictionary<string, object> variables = new Dictionary<string, object>();
        private AstNode code;

        public Interpreter(AstNode code)
        {
            //variables = new Dictionary<string, object>();
            this.code = code;
            foreach (Dictionary<string, InternalFunction> entries in GetFunctions())
                foreach (KeyValuePair<string, InternalFunction> entry in entries)
                    variables.Add(entry.Key, entry.Value);
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
                    return Convert.ToDouble((evaluateNode(node.Left))) * Convert.ToDouble((evaluateNode(node.Right)));
                case BinaryOperation.Assignment:
                    if (!(node.Left is IdentifierNode))
                        throw new Exception("Not a valid identifier");
                    object right = evaluateNode(node.Right);
                    if (variables.ContainsKey(node.Left.ToString()))
                        variables.Remove(node.Left.ToString());
                    variables.Add(node.Left.ToString(), right);
                    return right.ToString();
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
            }
            //Raise error
            return -1;
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
            else if (node is IdentifierNode)
            {
                if (variables.ContainsKey(node.ToString()))
                    return variables[node.ToString()];
                else
                    throw new Exception("Undefined variable: " + node.ToString());
            }
            else if (node is FunctionCallNode)
            {
                FunctionCallNode call = node as FunctionCallNode;
                IFunction target = evaluateNode(call.Target) as IFunction;
                if (target == null)
                    throw new Exception("Attempt to run a non-valid function!");
                object[] arguments = new object[call.Arguments.Children.Count];
                for (int x = 0; x < call.Arguments.Children.Count; x++)
                    arguments[x] = evaluateNode(call.Arguments.Children[x]);
                return target.Invoke(arguments);
            }
            else
            {
                //Raise error
            }

            return 0;
        }

        private List<Dictionary<string, InternalFunction>> GetFunctions(string path = "")
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

