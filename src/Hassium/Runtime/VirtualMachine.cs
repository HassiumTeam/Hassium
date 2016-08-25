using System;
using System.Collections.Generic;
using System.IO;

using Hassium.Compiler;
using Hassium.Compiler.CodeGen;
using Hassium.Compiler.Parser.Ast;
using Hassium.Runtime.Objects;
using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime
{
    public class VirtualMachine
    {
        public Stack<string> CallStack { get; private set; }
        public HassiumMethod CurrentMethod { get; private set; }
        public HassiumModule CurrentModule { get; private set; }
        public SourceLocation CurrentSourceLocation { get; private set; }
        public Dictionary<HassiumMethod, int> ExceptionReturns { get; private set; }
        public Dictionary<string, HassiumObject> Globals { get; private set; }
        public Stack<HassiumExceptionHandler> Handlers { get; private set; }
        public Stack<HassiumObject> Stack { get; private set; }
        public StackFrame StackFrame { get; private set; }

        public void Execute(HassiumModule module, string[] args)
        {
            Stack = new Stack<HassiumObject>();
            StackFrame = new StackFrame();
            CallStack = new Stack<string>();
            Handlers = new Stack<HassiumExceptionHandler>();
            ExceptionReturns = new Dictionary<HassiumMethod, int>();
            Globals = new Dictionary<string, HassiumObject>() { { "true", new HassiumBool(true) }, { "false", new HassiumBool(false) } };
            CurrentModule = module;
            importGlobals();
            importInitials();
            importArgs(args);

            StackFrame.PushFrame();
            CallStack.Push(((HassiumMethod)module.Attributes["main"]).SourceRepresentation);
            ExecuteMethod((HassiumMethod)module.Attributes["main"]);
            CallStack.Pop();
            StackFrame.PopFrame();
        }

        public HassiumObject ExecuteMethod(HassiumMethod method)
        {
            for (int pos = 0; pos < method.Instructions.Count; pos++)
            {
                if (ExceptionReturns.ContainsKey(method))
                {
                    pos = ExceptionReturns[method];
                    ExceptionReturns.Remove(method);
                }

                HassiumObject left, right, val, list;
                HassiumObject[] elements;
                string attrib;

                int arg = method.Instructions[pos].Argument;
                CurrentSourceLocation = method.Instructions[pos].SourceLocation;
                //Console.WriteLine(method.Instructions[pos].ToString());
                try
                {
                    switch (method.Instructions[pos].InstructionType)
                    {
                        case InstructionType.BinaryOperation:
                            right = Stack.Pop();
                            left = Stack.Pop();
                            interpretBinaryOperation(left, right, arg);
                            break;
                        case InstructionType.BuildClosure:
                            Stack.Push(new HassiumClosure(Stack.Pop() as HassiumMethod, StackFrame.Frames.Peek()));
                            break;
                        case InstructionType.BuildDictionary:
                            List<HassiumKeyValuePair> pairs = new List<HassiumKeyValuePair>();
                            for (int i = 0; i < arg; i++)
                                pairs.Add(Stack.Pop() as HassiumKeyValuePair);
                            Stack.Push(new HassiumDictionary(pairs));
                            break;
                        case InstructionType.BuildKeyValuePair:
                            Stack.Push(new HassiumKeyValuePair(Stack.Pop(), Stack.Pop()));
                            break;
                        case InstructionType.BuildList:
                            elements = new HassiumObject[arg];
                            for (int i = elements.Length - 1; i >= 0; i--)
                                elements[i] = Stack.Pop();
                            Stack.Push(new HassiumList(elements));
                            break;
                        case InstructionType.BuildTuple:
                            HassiumObject[] tupleElements = new HassiumObject[arg];
                            for (int i = 0; i < arg; i++)
                                tupleElements[i] = Stack.Pop();
                            Stack.Push(new HassiumTuple(tupleElements));
                            break;
                        case InstructionType.Call:
                            val = Stack.Pop();
                            elements = new HassiumObject[arg];
                            for (int i = elements.Length - 1; i >= 0; i--)
                                elements[i] = Stack.Pop();
                            Stack.Push(val.Invoke(this, elements));
                            break;
                        case InstructionType.Dereference:
                            Stack.Push(((HassiumPointer)Stack.Pop()).Dereference());
                            break;
                        case InstructionType.Duplicate:
                            Stack.Push(Stack.Peek());
                            break;
                        case InstructionType.EnforcedAssignment:
                            var type = Globals[Stack.Pop().ToString(this).String].Type();
                            val = Stack.Pop();
                            if (!val.Types.Contains(type))
                                throw new InternalException(this, "Expcted assignment type {0}, got {1}!", type, val.Type());
                            if (StackFrame.Contains(arg))
                                StackFrame.Modify(arg, val);
                            else
                                StackFrame.Add(arg, val);
                            break;
                        case InstructionType.Iter:
                            Stack.Push(Stack.Pop().Iter(this));
                            break;
                        case InstructionType.IterableFull:
                            Stack.Push(Stack.Pop().IterableFull(this));
                            break;
                        case InstructionType.IterableNext:
                            Stack.Push(Stack.Pop().IterableNext(this));
                            break;
                        case InstructionType.Jump:
                            pos = method.Labels[arg];
                            break;
                        case InstructionType.JumpIfFalse:
                            if (!Stack.Pop().ToBool(this).Bool)
                                pos = method.Labels[arg];
                            break;
                        case InstructionType.JumpIfTrue:
                            if (Stack.Pop().ToBool(this).Bool)
                                pos = method.Labels[arg];
                            break;
                        case InstructionType.LoadAttribute:
                            val = Stack.Pop();
                            try
                            {
                                var attribute = val.Attributes[CurrentModule.ConstantPool[arg]];
                                if (attribute is HassiumProperty)
                                    Stack.Push(((HassiumProperty)attribute).Get.Invoke(this));
                                else
                                    Stack.Push(attribute);
                            }
                            catch (KeyNotFoundException)
                            {
                                throw new InternalException(this, InternalException.ATTRIBUTE_NOT_FOUND, CurrentModule.ConstantPool[arg], val.Type());
                            }
                            break;
                        case InstructionType.LoadGlobal:
                            attrib = CurrentModule.ConstantPool[arg];
                            if (Globals.ContainsKey(attrib))
                                Stack.Push(Globals[attrib]);
                            else if (method.Parent != null)
                            {
                                if (method.Parent.Attributes.ContainsKey(attrib))
                                    Stack.Push(method.Parent.Attributes[attrib]);
                                else
                                    throw new InternalException(this, InternalException.VARIABLE_ERROR, attrib);
                            }
                            else
                                throw new InternalException(this, InternalException.VARIABLE_ERROR, attrib);
                            break;
                        case InstructionType.LoadGlobalVariable:
                            try
                            {
                                Stack.Push(CurrentModule.Globals[arg]);
                            }
                            catch (KeyNotFoundException)
                            {
                                throw new InternalException(this, InternalException.VARIABLE_ERROR, arg);
                            }
                            break;
                        case InstructionType.LoadListElement:
                            list = Stack.Pop();
                            Stack.Push(list.Index(this, Stack.Pop()));
                            break;
                        case InstructionType.LoadLocal:
                            Stack.Push(StackFrame.GetVariable(this, arg));
                            break;
                        case InstructionType.Pop:
                            Stack.Pop();
                            break;
                        case InstructionType.PopHandler:
                            Handlers.Pop();
                            break;
                        case InstructionType.Push:
                            Stack.Push(new HassiumInt(arg));
                            break;
                        case InstructionType.PushConstant:
                            Stack.Push(new HassiumString(CurrentModule.ConstantPool[arg]));
                            break;
                        case InstructionType.PushHandler:
                            var handler = CurrentModule.ObjectPool[arg] as HassiumExceptionHandler;
                            handler.Frame = StackFrame.Frames.Peek();
                            Handlers.Push(handler);
                            break;
                        case InstructionType.PushObject:
                            Stack.Push(CurrentModule.ObjectPool[arg]);
                            break;
                        case InstructionType.Raise:
                            RaiseException(Stack.Pop(), method, ref pos);
                            break;
                        case InstructionType.Reference:
                            Stack.Push(new HassiumPointer(StackFrame.Frames.Peek(), arg));
                            break;
                        case InstructionType.Return:
                            return Stack.Pop();
                        case InstructionType.SelfReference:
                            Stack.Push(method.Parent);
                            break;
                        case InstructionType.SetInitialAttribute:
                            attrib = Stack.Pop().ToString(this).String;
                            val = Stack.Pop();
                            var obj = Stack.Pop();
                            if (obj.Attributes.ContainsKey(attrib))
                                obj.Attributes.Remove(attrib);
                            obj.Attributes.Add(attrib, val);
                            Stack.Push(obj);
                            break;
                        case InstructionType.StoreAttribute:
                            val = Stack.Pop();
                            attrib = CurrentModule.ConstantPool[arg];
                            try
                            {
                                if (val.Attributes.ContainsKey(attrib))
                                {
                                    if (val.Attributes[attrib] is HassiumProperty)
                                    {
                                        if (((HassiumProperty)val.Attributes[attrib]).Set == null)
                                            throw new InternalException(this, InternalException.ATTRIBUTE_NOT_FOUND, string.Format("set_{0}", attrib), val.Type());
                                        ((HassiumProperty)val.Attributes[attrib]).Set.Invoke(this, Stack.Pop());
                                        break;
                                    }
                                    else
                                        val.Attributes.Remove(attrib);
                                }
                                val.Attributes.Add(attrib, Stack.Pop());
                            }
                            catch (KeyNotFoundException)
                            {
                                throw new InternalException(this, InternalException.VARIABLE_ERROR, val.Type());
                            }
                            break;
                        case InstructionType.StoreGlobalVariable:
                            CurrentModule.Globals[arg] = Stack.Pop();
                            break;
                        case InstructionType.StoreListElement:
                            Stack.Push(Stack.Pop().StoreIndex(this, Stack.Pop(), Stack.Pop()));
                            break;
                        case InstructionType.StoreLocal:
                            val = Stack.Pop();
                            if (StackFrame.Contains(arg))
                                StackFrame.Modify(arg, val);
                            else
                                StackFrame.Add(arg, val);
                            break;
                        case InstructionType.StoreReference:
                            Stack.Push(((HassiumPointer)Stack.Pop()).StoreReference(Stack.Pop()));
                            break;
                        case InstructionType.UnaryOperation:
                            interpretUnaryOperation(Stack.Pop(), arg);
                            break;
                    }
                }
                catch (DivideByZeroException ex)
                {
                    RaiseException(new HassiumString(ex.Message), method, ref pos);
                }
                catch (FileNotFoundException ex)
                {
                    RaiseException(new HassiumString(ex.Message), method, ref pos);
                }
                catch (IOException ex)
                {
                    RaiseException(new HassiumString(ex.Message), method, ref pos);
                }
                catch (InternalException ex)
                {
                    RaiseException(new HassiumString(ex.Message), method, ref pos);
                }
                catch (Exception ex)
                {
                    RaiseException(new HassiumString(ex.ToString()), method, ref pos);
                }
            }
            return HassiumObject.Null;
        }

        public void RaiseException(HassiumObject message, HassiumMethod method, ref int pos)
        {
            if (Handlers.Count == 0)
                throw new InternalException(this, message.ToString(this).String);
            var handler = Handlers.Peek();
            handler.Invoke(this, message);
            ExceptionReturns.Add(handler.Caller, handler.Caller.Labels[handler.Label]);
        }

        private void interpretBinaryOperation(HassiumObject left, HassiumObject right, int op)
        {
            switch (op)
            {
                case (int)BinaryOperation.Addition:
                    Stack.Push(left.Add(this, right));
                    break;
                case (int)BinaryOperation.BitshiftLeft:
                    Stack.Push(left.BitshiftLeft(this, right));
                    break;
                case (int)BinaryOperation.BitshiftRight:
                    Stack.Push(left.BitshiftRight(this, right));
                    break;
                case (int)BinaryOperation.BitwiseAnd:
                    Stack.Push(left.BitwiseAnd(this, right));
                    break;
                case (int)BinaryOperation.BitwiseOr:
                    Stack.Push(left.BitwiseOr(this, right));
                    break;
                case (int)BinaryOperation.BitwiseXor:
                    Stack.Push(left.BitwiseXor(this, right));
                    break;
                case (int)BinaryOperation.Division:
                    Stack.Push(left.Divide(this, right));
                    break;
                case (int)BinaryOperation.EqualTo:
                    Stack.Push(left.EqualTo(this, right));
                    break;
                case (int)BinaryOperation.GreaterThan:
                    Stack.Push(left.GreaterThan(this, right));
                    break;
                case (int)BinaryOperation.GreaterThanOrEqual:
                    Stack.Push(left.GreaterThanOrEqual(this, right));
                    break;
                case (int)BinaryOperation.IntegerDivision:
                    Stack.Push(left.IntegerDivision(this, right));
                    break;
                case (int)BinaryOperation.Is:
                    if (right is HassiumTrait)
                        Stack.Push(((HassiumTrait)right).Is(this, left));
                    else
                        Stack.Push(new HassiumBool(left.Types.Contains(right.Type())));
                    break;
                case (int)BinaryOperation.LesserThan:
                    Stack.Push(left.LesserThan(this, right));
                    break;
                case (int)BinaryOperation.LesserThanOrEqual:
                    Stack.Push(left.LesserThanOrEqual(this, right));
                    break;
                case (int)BinaryOperation.LogicalAnd:
                    Stack.Push(left.LogicalAnd(this, right));
                    break;
                case (int)BinaryOperation.LogicalOr:
                    Stack.Push(left.LogicalOr(this, right));
                    break;
                case (int)BinaryOperation.Modulus:
                    Stack.Push(left.Modulus(this, right));
                    break;
                case (int)BinaryOperation.Multiplication:
                    Stack.Push(left.Multiply(this, right));
                    break;
                case (int)BinaryOperation.NotEqualTo:
                    Stack.Push(left.NotEqualTo(this, right));
                    break;
                case (int)BinaryOperation.NullCoalescing:
                    if (left == HassiumObject.Null || left == null)
                        Stack.Push(right);
                    else
                        Stack.Push(left);
                    break;
                case (int)BinaryOperation.Power:
                    Stack.Push(left.Power(this, right));
                    break;
                case (int)BinaryOperation.Subraction:
                    Stack.Push(left.Subtract(this, right));
                    break;
            }
        }

        private void interpretUnaryOperation(HassiumObject target, int op)
        {
            switch (op)
            {
                case (int)UnaryOperation.BitwiseNot:
                    Stack.Push(target.BitwiseNot(this));
                    break;
                case (int)UnaryOperation.LogicalNot:
                    Stack.Push(target.LogicalNot(this));
                    break;
                case (int)UnaryOperation.Negate:
                    Stack.Push(target.Negate(this));
                    break;
            }
        }

        private void importArgs(string[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (string arg in args)
                list.add(this, new HassiumString(arg));
            Globals.Add("args", list);
        }

        private void importGlobals()
        {
            foreach (string constant in CurrentModule.ConstantPool.Values)
            {
                    if (GlobalFunctions.Functions.ContainsKey(constant))
                        Globals.Add(constant, GlobalFunctions.Functions[constant]);
                    else if (CurrentModule.Attributes.ContainsKey(constant))
                        Globals.Add(constant, CurrentModule.Attributes[constant]);
            }
            foreach (var pair in InternalModule.InternalModules["Types"].Attributes)
                Globals.Add(pair.Key, pair.Value);
        }

        private void importInitials()
        {
            foreach (var pair in CurrentModule.InitialVariables)
                CurrentModule.Globals.Add(pair.Key, pair.Value.Invoke(this));
        }
    }
}