/* Credit to contributer Zdimension, who added the lines in interpretBinaryOp for the
implementation of string concat amoung other additions and foreach loop*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;
using Hassium.HassiumObjects.Math;
using Hassium.Lexer;
using Hassium.Parser;
using Hassium.Parser.Ast;
using Hassium.Semantics;

namespace Hassium.Interpreter
{
    /// <summary>
    /// Interpreter.
    /// </summary>
    public class Interpreter
    {
        public Stack<StackFrame> CallStack = new Stack<StackFrame>();
        public Dictionary<string, HassiumObject> Globals = new Dictionary<string, HassiumObject>();

        public Dictionary<string, HassiumObject> Constants = new Dictionary<string, HassiumObject>
        {
            {"true", new HassiumBool(true)},
            {"false", new HassiumBool(false)},
            {"File", new HassiumFile()},
            {"Directory", new HassiumDirectory()},
            {"Path", new HassiumPath()},
            {"Math", new HassiumMath()},
            {"null", null},
        };

        public AstNode Code { get; set; }

        public SymbolTable SymbolTable { get; set; }



        private bool forceMain;


        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreter"/> class.
        /// </summary>
        public Interpreter(bool forcemain = true)
        {
            SymbolTable = new SymbolTable();
            forceMain = forcemain;
            LoadInternalFunctions();
        }

        public void LoadInternalFunctions()
        {
            foreach (var entry in GetFunctions())
                Globals.Add(entry.Key, entry.Value);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreter"/> class.
        /// </summary>
        /// <param name="symbolTable">Symbol table.</param>
        /// <param name="code">Code.</param>
        public Interpreter(SymbolTable symbolTable, AstNode code, bool forcemain = true)
        {
            this.Code = code;
            SymbolTable = symbolTable;
            forceMain = forcemain;
            LoadInternalFunctions();
        }

        public HassiumObject GetVariable(string name, AstNode node)
        {
            if (Constants.ContainsKey(name))
                return Constants[name];
            if (CallStack.Count > 0 && CallStack.Peek().Locals.ContainsKey(name))
                return CallStack.Peek().Locals[name];
            if (Globals.ContainsKey(name))
                return Globals[name];
            else throw new ParseException("The variable '" + name + "' doesn't exist.", node);
        }

        public bool HasVariable(string name, bool onlyglobal = false)
        {
            return onlyglobal
                ? Globals.ContainsKey(name) || Constants.ContainsKey(name)
                : Globals.ContainsKey(name) || Constants.ContainsKey(name) ||
                  (CallStack.Count > 0 && (CallStack.Peek().Scope.Symbols.Contains(name) || CallStack.Peek().Locals.ContainsKey(name)));
        }

        public void SetGlobalVariable(string name, HassiumObject value, AstNode node)
        {
            if (Constants.ContainsKey(name))
                throw new ParseException("Can't change the value of the internal constant '" + name + "'.", node);

            Globals[name] = value;
        }

        public void SetLocalVariable(string name, HassiumObject value, AstNode node)
        {
            if (Constants.ContainsKey(name))
                throw new ParseException("Can't change the value of the internal constant '" + name + "'.", node);

            if (CallStack.Count > 0)
                CallStack.Peek().Locals[name] = value;
        }

        public void SetVariable(string name, HassiumObject value, AstNode node, bool forceglobal = false, bool onlyexist = false)
        {
            if (!forceglobal && CallStack.Count > 0 && (!onlyexist || (CallStack.Peek().Scope.Symbols.Contains(name) || CallStack.Peek().Locals.ContainsKey(name))) && !Globals.ContainsKey(name))
                SetLocalVariable(name, value, node);
            else
                SetGlobalVariable(name, value, node);
        }

        public void FreeVariable(string name, AstNode node, bool forceglobal = false)
        {
            if(Constants.ContainsKey(name)) throw new ParseException("Can't delete internal constant '" + name + "'.", node);
            if (forceglobal)
            {
                if(!Globals.ContainsKey(name)) throw new ParseException("The global variable '" + name + "' doesn't exist.", node);
                Globals.Remove(name);
            }
            else
            {
                if(!HasVariable(name)) throw new ParseException("The variable '" + name + "' doesn't exist.", node);
                if (CallStack.Count > 0 && (CallStack.Peek().Scope.Symbols.Contains(name) || CallStack.Peek().Locals.ContainsKey(name)))
                    CallStack.Peek().Locals.Remove(name);
                else
                    Globals.Remove(name);
            }
        }

        private bool firstExecute = true;
        /// <summary>
        /// Execute this instance.
        /// </summary>
        public void Execute()
        {
            foreach (var node in Code.Children)
            {
                if (node is FuncNode && firstExecute)
                {
                    var fnode = ((FuncNode)node);
                    var scope = SymbolTable.ChildScopes[fnode.Name];
                    SetVariable(fnode.Name, new HassiumFunction(this, fnode, scope), node);
                }
            }

            if (!Globals.ContainsKey("main") && forceMain)
            {
                Console.WriteLine("Could not execute, no main entry point of program!");
                Environment.Exit(-1);
            }

            firstExecute = false;
            foreach (var node in Code.Children)
            {
                if (node is FuncNode)
                {
                    var fnode = ((FuncNode)node);
                    var scope = SymbolTable.ChildScopes[fnode.Name];
                    //If there is a main, let it be the main entry point of the program
                    if (fnode.Name == "main")
                    {
                        inFunc++;
                        new HassiumFunction(this, fnode, scope).Invoke(new HassiumObject[0]);
                        return;
                    }
                }
                else
                    ExecuteStatement(node);
            }
        }

        /*private void interpretBinaryAssign(BinOpNode node)
        {
            SetVariable(node.Left.ToString(), interpretBinaryOp(node, true), false, true);         
        }*/

        /// <summary>
        /// Interprets the binary op.
        /// </summary
        /// <returns>The binary op.</returns>
        /// <param name="node">Node.</param>
        private HassiumObject interpretBinaryOp(BinOpNode node)
        {
            var right = EvaluateNode(node.Right);
            if (node.BinOp == BinaryOperation.Assignment)
            {
                if (node.Left is ArrayGetNode)
                {
                    var call = (ArrayGetNode)(node.Left);

                    if (!call.Target.CanBeIndexed)
                        throw new ParseException("The [] operator only applies to objects of type Array, Dictionary or String.", node);

                    if(!call.Target.CanBeModified)
                        throw new ParseException("The specified target cannot be modified.", node);

                    var evaluated = EvaluateNode(call.Target);
                    if (evaluated is HassiumDictionary)
                    {
                        var theArray = ((HassiumDictionary) evaluated);
                        HassiumObject arid = null;

                        if (call.Arguments.Children.Count > 0)
                            arid = EvaluateNode(call.Arguments.Children[0]);

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
                        if(evaluated is HassiumString)
                        {
                            theArray = new HassiumArray(evaluated.ToString().ToCharArray().Cast<object>());
                        }
                        theArray = ((HassiumArray) evaluated);

                        int arid = -1;
                        bool append = false;

                        if (call.Arguments.Children.Count > 0)
                            arid = EvaluateNode(call.Arguments.Children[0]);
                        else
                            append = true;

                        var theValue = node.IsOpAssign
                            ? interpretBinaryOp(theArray[(int)arid], right, node.AssignOperation)
                            : right;

                        if(append)
                            theArray.Add(new[] {theValue});
                        else
                        {
                            if(arid >= theArray.Value.Length)
                                throw new IndexOutOfRangeException();

                            theArray[arid] = theValue;
                        }

                        SetVariable(call.Target.ToString(), theArray, call);
                    }
                    else
                    {
                        throw new ParseException("The [] operator only applies to objects of type Array, Dictionary or String.", node);
                    }
                    
                }
                else if (node.Left is MemberAccess)
                {
                    var accessor = (MemberAccess) node.Left;
                    var target = EvaluateNode(accessor.Left);
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
            var left = EvaluateNode(node.Left);
            return interpretBinaryOp(left, right, node.IsOpAssign ? node.AssignOperation : node.BinOp);
        }

        /// <summary>
        /// Interprets a binary op
        /// </summary>
        /// <param name="left">The left-hand parameter</param>
        /// <param name="right">The right-hand parameter</param>
        /// <param name="_op">The operation type</param>
        /// <returns>The result of the operation</returns>
        private HassiumObject interpretBinaryOp(object left, object right, BinaryOperation _op)
        {
            if (left is AstNode) left = EvaluateNode((AstNode) left);
            if (left is int) left = (double) (int) left; 
            if (right is AstNode) right = EvaluateNode((AstNode)right);
            if (right is int) right = (double)(int)right;
            switch (_op)
            {
                case BinaryOperation.Addition:
                    if (left is HassiumString || right is HassiumString)
                        return new HassiumString(left + right.ToString());
                    if (left is HassiumDate || right is HassiumDate)
                        return (HassiumDate) left + (HassiumDate) right;
                    if (left is HassiumKeyValuePair || right is HassiumKeyValuePair)
                        return new HassiumString(left + right.ToString());
                    return new HassiumNumber(Convert.ToDouble(left) + Convert.ToDouble(right));
                case BinaryOperation.Subtraction:
                    return new HassiumNumber(Convert.ToDouble(left) - Convert.ToDouble(right));
                case BinaryOperation.Division:
                    if(Convert.ToDouble(right) == 0.0) throw new DivideByZeroException("Cannot divide by zero");
                    return new HassiumNumber(Convert.ToDouble(left) / Convert.ToDouble(right));
                case BinaryOperation.Multiplication:
                    if ((left is HassiumString && right is HassiumNumber) ||
                        right is HassiumString && left is HassiumNumber)
                    {
                        if (left is HassiumString)
                            return new HassiumString(string.Concat(Enumerable.Repeat(left, Convert.ToInt32(right))));
                        else
                            return new HassiumString(string.Concat(Enumerable.Repeat(right, Convert.ToInt32(left))));
                    }
                    return new HassiumNumber(Convert.ToDouble(left) * Convert.ToDouble(right));
                case BinaryOperation.Equals:
                    //if (left is double || right is double) return ((double) left) == ((double) right);
                    return new HassiumBool(left.ToString() == right.ToString());
                case BinaryOperation.LogicalAnd:
                    return new HassiumBool(Convert.ToBoolean(left) && Convert.ToBoolean(right));
                case BinaryOperation.LogicalOr:
                    return new HassiumBool(Convert.ToBoolean(left) || Convert.ToBoolean(right));
                case BinaryOperation.NotEqualTo:
                    return new HassiumBool(left.GetHashCode() != right.GetHashCode());
                case BinaryOperation.LessThan:
                    return new HassiumBool(Convert.ToDouble(left) < Convert.ToDouble(right));
                case BinaryOperation.GreaterThan:
                    return new HassiumBool(Convert.ToDouble(left) > Convert.ToDouble(right));
                case BinaryOperation.GreaterOrEqual:
                    return new HassiumBool(Convert.ToDouble(left) >= Convert.ToDouble(right));
                case BinaryOperation.LesserOrEqual:
                    return new HassiumBool(Convert.ToDouble(left) <= Convert.ToDouble(right));
                case BinaryOperation.CombinedComparison:
                    if (new HassiumBool(interpretBinaryOp(left, right, BinaryOperation.GreaterThan)).Value) return new HassiumNumber(1);
                    if (new HassiumBool(interpretBinaryOp(left, right, BinaryOperation.LessThan))) return new HassiumNumber(-1);
                    return new HassiumNumber(0);
                case BinaryOperation.Xor:
                    return new HassiumNumber(Convert.ToInt32(left) ^ Convert.ToInt32(right));
                case BinaryOperation.BitwiseAnd:
                    return new HassiumNumber(Convert.ToInt32(left) & Convert.ToInt32(right));
                case BinaryOperation.BitwiseOr:
                    return new HassiumNumber(Convert.ToInt32(left) | Convert.ToInt32(right));
                case BinaryOperation.BitshiftLeft:
                    return new HassiumNumber(Convert.ToInt32(left) << Convert.ToInt32(right));
                case BinaryOperation.BitshiftRight:
                    return new HassiumNumber(Convert.ToInt32(left) >> Convert.ToInt32(right));
                case BinaryOperation.Modulus:
                    return new HassiumNumber(Convert.ToDouble(left) % Convert.ToDouble(right));

                case BinaryOperation.Pow:
                    return new HassiumNumber(Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right)));
                case BinaryOperation.Root:
                    return new HassiumNumber(Math.Pow(Convert.ToDouble(left), 1.0 / Convert.ToDouble(right)));
                    
                case BinaryOperation.NullCoalescing:
                    return HassiumObject.ToHassiumObject(left) ?? HassiumObject.ToHassiumObject(right);
            }
            // Raise error
            return new HassiumNumber(-1);
        }
        /// <summary>
        /// Interprets the unary op.
        /// </summary>
        /// <returns>The unary op.</returns>
        /// <param name="node">Node.</param>
        private HassiumObject interpretUnaryOp(UnaryOpNode node)
        {
            switch (node.UnOp)
            {
                case UnaryOperation.Not:
                    return !Convert.ToBoolean((object)EvaluateNode(node.Value));
                case UnaryOperation.Negate:
                    return -Convert.ToDouble((object)EvaluateNode(node.Value));
                case UnaryOperation.Complement:
                    return ~(int)Convert.ToDouble((object)EvaluateNode(node.Value));
            }
            //Raise error
            return -1;
        }

        private int inLoop;
        private bool continueLoop;
        private bool breakLoop;

        private int inFunc;
        private bool returnFunc;

        /// <summary>
        /// Executes the statement.
        /// </summary>
        /// <param name="node">Node.</param>
        public void ExecuteStatement(AstNode node)
        {
            while (true)
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
                    {
                        node = ifStmt.Body;
                        continue;
                    }
                    else
                    {
                        node = ifStmt.ElseBody;
                        continue;
                    }
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
                            if (breakLoop)
                            {
                                breakLoop = false;
                                break;
                            }
                        }
                    else
                        ExecuteStatement(whileStmt.ElseBody);
                    inLoop--;
                }
                else if (node is FuncNode && !firstExecute)
                {
                    var fnode = ((FuncNode) node);
                    var stackFrame = new StackFrame(SymbolTable.ChildScopes[fnode.Name]);
                    if (CallStack.Count > 0)
                    {
                        stackFrame.Scope.Symbols.AddRange(CallStack.Peek().Scope.Symbols);
                        CallStack.Peek().Locals.All(x =>
                        {
                            stackFrame.Locals.Add(x.Key, x.Value);
                            return true;
                        });
                    }
                    SetVariable(fnode.Name, new HassiumFunction(this, fnode, stackFrame), fnode);
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
                        if (breakLoop)
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
                    var needlestmt = forStmt.Needle;
                    var haystackstmt = EvaluateNode(forStmt.Haystack);

                    inLoop++;
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
                        foreach (var needle in (keyvname != "" ? theArray : (IEnumerable) (theArray.Value.Select(x => x.Value))))
                        {
                            if (keyvname != "") SetVariable(keyvname, ((HassiumKeyValuePair) needle).Key, forStmt);
                            SetVariable(valvname, keyvname != "" ? ((HassiumKeyValuePair) needle).Value : HassiumObject.ToHassiumObject(needle), forStmt);
                            ExecuteStatement(forStmt.Body);
                            if (continueLoop) continueLoop = false;
                            if (breakLoop)
                            {
                                breakLoop = false;
                                break;
                            }
                        }
                        if (keyvname != "") FreeVariable(keyvname, forStmt);
                        FreeVariable(valvname, forStmt);
                        inLoop--;
                    }
                    else if (haystackstmt is HassiumArray || haystackstmt is HassiumString)
                    {
                        HassiumArray theArray = null;
                        if (haystackstmt is HassiumString)
                        {
                            theArray = new HassiumArray(haystackstmt.ToString().ToCharArray().Cast<object>());
                        }
                        theArray = ((HassiumArray) haystackstmt);

                        var valvname = needlestmt.ToString();

                        SetVariable(valvname, null, forStmt);
                        foreach (var needle in theArray.Value)
                        {
                            SetVariable(valvname, HassiumObject.ToHassiumObject(needle), forStmt);
                            ExecuteStatement(forStmt.Body);
                            if (continueLoop) continueLoop = false;
                            if (breakLoop)
                            {
                                breakLoop = false;
                                break;
                            }
                        }
                        FreeVariable(valvname, forStmt);
                        inLoop--;
                    }
                    else
                    {
                        inLoop--;
                        throw new ParseException("Foreach can only be used with objects of type Array, Dictionary or String.", node);
                    }
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
                break;
            }
        }

        /// <summary>
        /// Evaluates the node.
        /// </summary>
        /// <returns>The node.</returns>
        /// <param name="node">Node.</param>
        public HassiumObject EvaluateNode(AstNode node)
        {
            if (node is NumberNode)
            {
                return new HassiumNumber(((NumberNode)node).Value);
            }
            else if (node is StringNode)
            {
                return new HassiumString(((StringNode)node).Value);
            }
            else if (node is MemberAccess)
            {
                var accessor = (MemberAccess)node;
                var target = EvaluateNode(accessor.Left);
                var attr = target.GetAttribute(accessor.Member);
                if (attr is InternalFunction && ((InternalFunction) attr).IsProperty)
                {
                    return ((InternalFunction) attr).Invoke(new HassiumObject[] {});
                }
                else
                {
                    return attr;
                }
            }
            else if(node is InstanceNode)
            {
                var inode = (InstanceNode) node;
                var fcall = (FunctionCallNode)inode.Target;
                var target = fcall.Target.ToString();

                var arguments = new HassiumObject[fcall.Arguments.Children.Count];
                for (var x = 0; x < fcall.Arguments.Children.Count; x++)
                {
                    arguments[x] = EvaluateNode(fcall.Arguments.Children[x]);
                }
                if(HasVariable(target, true))
                {
                    var theVar = Globals[target];

                    var iFunc = theVar as InternalFunction;
                    if(iFunc != null)
                    {
                        if(iFunc.IsConstructor)
                        {
                            return iFunc.Invoke(arguments);
                        }
                    }
                }
                throw new ParseException("No constructor found for " + target, node);
            }
            else if (node is BinOpNode)
            {
                var bnode = (BinOpNode) node;
                return interpretBinaryOp(bnode);
            }
            else if (node is UnaryOpNode)
            {
                return interpretUnaryOp((UnaryOpNode)node);
            }
            else if (node is ConditionalOpNode)
            {
                var ifStmt = (ConditionalOpNode)(node);
                if ((bool)(EvaluateNode(ifStmt.Predicate)))
                    return EvaluateNode(ifStmt.Body);
                else
                    return EvaluateNode(ifStmt.ElseBody);
            }
            else if(node is LambdaFuncNode)
            {
                var funcNode = (LambdaFuncNode)(node);
                var stackFrame = new StackFrame(SymbolTable.ChildScopes["lambda_" + funcNode.GetHashCode()]);
                if (CallStack.Count > 0)
                {
                    stackFrame.Scope.Symbols.AddRange(CallStack.Peek().Scope.Symbols);
                    CallStack.Peek().Locals.All(x =>
                    {
                        stackFrame.Locals.Add(x.Key, x.Value);
                        return true;
                    });
                }
                return new HassiumFunction(this, (FuncNode)funcNode, stackFrame);
            }
            else if (node is FunctionCallNode)
            {
                var call = (FunctionCallNode)node;

                IFunction target = null;


                bool dontEval = false;

                switch (call.Target.ToString())
                {
                    case "free":
                        dontEval = true;
                        target = new InternalFunction(args =>
                        {
                            FreeVariable(args[0].ToString(), node);
                            return null;
                        });
                        break;
                    default:
                        if ((!(call.Target is MemberAccess) && !HasVariable(call.Target.ToString())))
                        {
                            throw new ParseException("Attempt to run a non-valid function", node);
                        }
                        target = EvaluateNode(call.Target);
                        break;
                }

                

                if(target is InternalFunction && (target as InternalFunction).IsConstructor)
                    throw new ParseException("Attempt to run a constructor without the 'new' operator", node);

                var arguments = new HassiumObject[call.Arguments.Children.Count];
                for (var x = 0; x < call.Arguments.Children.Count; x++)
                {
                    arguments[x] = dontEval ? new HassiumString(call.Arguments.Children[x].ToString()) : EvaluateNode(call.Arguments.Children[x]);
                }
                inFunc++;
                HassiumObject ret = target.Invoke(arguments);
                if (returnFunc)
                    returnFunc = false;
                inFunc--;
                if (ret is HassiumArray) ret = ((Array)ret).Cast<HassiumObject>().Select((s, i) => new { s, i }).ToDictionary(x => HassiumObject.ToHassiumObject(x.i), x => HassiumObject.ToHassiumObject(x.s));
                return ret;
            }
            else if (node is IdentifierNode)
            {
                return GetVariable(((IdentifierNode)node).Identifier, node);
            }
            else if (node is ArrayInitializerNode)
            {
                var ainode = ((ArrayInitializerNode) node);
                var content = ainode.Value;
                if (ainode.IsDictionary)
                    return
                        content.Select(
                            pair =>
                                new KeyValuePair<HassiumObject, HassiumObject>(
                                    pair.Key is AstNode
                                        ? EvaluateNode((AstNode) (pair.Key))
                                        : HassiumObject.ToHassiumObject(pair.Key),
                                    pair.Value is AstNode
                                        ? EvaluateNode((AstNode) (pair.Value))
                                        : HassiumObject.ToHassiumObject(pair.Value)))
                            .ToDictionary(x => x.Key, x => x.Value);
                else
                    return new HassiumArray(content.Values.Select(x => x is AstNode ? EvaluateNode((AstNode)x) : x));
            }
            else if (node is MentalNode)
            {
                var mnode = ((MentalNode)node);
                if(!HasVariable(mnode.Name)) throw new ParseException("The operand of an increment or decrement operator must be a variable, property or indexer", mnode);
                var oldValue = GetVariable(mnode.Name, mnode);
                switch (mnode.OpType)
                {
                    case "++":
                        SetVariable(mnode.Name, Convert.ToDouble((object)GetVariable(mnode.Name, mnode)) + 1, mnode);
                        break;
                    case "--":
                        SetVariable(mnode.Name, Convert.ToDouble((object)GetVariable(mnode.Name, mnode)) - 1, mnode);
                        break;
                    default:
                        throw new ParseException("Unknown operation " + mnode.OpType, mnode);
                }
                return mnode.IsBefore ? GetVariable(mnode.Name, mnode) : oldValue;
            }
            else if (node is ArrayGetNode)
            {
                var call = (ArrayGetNode)(node);

                if(!call.Target.CanBeIndexed)
                    throw new ParseException("The [] operator only applies to objects of type Array, Dictionary or String.", node);

                var evaluated = EvaluateNode(call.Target);
                if (evaluated is HassiumDictionary)
                {
                    var theArray = ((HassiumDictionary) evaluated);
                    HassiumObject arid = null;

                    if (call.Arguments.Children.Count > 0)
                        arid = EvaluateNode(call.Arguments.Children[0]);

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
                    {
                        theArray = new HassiumArray(evaluated.ToString().ToCharArray().Cast<object>());
                    }
                    theArray = ((HassiumArray) evaluated);

                    int arid = -1;
                    bool append = false;

                    if (call.Arguments.Children.Count > 0)
                        arid = EvaluateNode(call.Arguments.Children[0]);
                    else
                        append = true;

                    if (append)
                        return theArray.Value.Last();
                    else
                    {
                        if (arid >= theArray.Value.Length)
                            throw new IndexOutOfRangeException();

                        return theArray[arid];
                    }
                }
                else
                {
                    throw new ParseException("The [] operator only applies to objects of type Array, Dictionary or String.", node);
                }
            }
            else if (node is ReturnNode)
            {
                if (inFunc == 0) throw new ParseException("'return' cannot be used outside a function", node);
                var returnStmt = (ReturnNode)(node);
                if(returnStmt.Value != null && !returnStmt.Value.ReturnsValue) throw new ParseException("This node type doesn't return a value.", returnStmt.Value);
                CallStack.Peek().ReturnValue = EvaluateNode(returnStmt.Value);
                returnFunc = true;
            }
            else if (node is ContinueNode)
            {
                if (inLoop == 0) throw new ParseException("'continue' cannot be used outside a loop", node);
                continueLoop = true;
            }
            else if (node is BreakNode)
            {
                if (inLoop == 0) throw new ParseException("'break' cannot be used outside a loop", node);
                breakLoop = true;
            }
            else
            {
                //Raise error
            }

            return 0;
        }
        /// <summary>
        /// Gets the functions.
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
                {
                    foreach (var myfunc in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                    {
                        var theattr1 = myfunc.GetCustomAttributes(typeof (IntFunc), true);
                        foreach (var theattr in theattr1.OfType<IntFunc>())
                        {
                            var rfunc = new InternalFunction(
                                (HassiumFunctionDelegate)
                                    Delegate.CreateDelegate(typeof (HassiumFunctionDelegate), myfunc), false, theattr.Constructor);

                            result.Add(theattr.Name, rfunc);
                            if (theattr.Alias != "") result.Add(theattr.Alias, rfunc);
                        }
                    }
                }
            }
            return result;
        }
    }
}

