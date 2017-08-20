using System;
using System.Collections.Generic;
using System.Text;

using Hassium.Compiler;
using Hassium.Compiler.Parser.Ast;
using Hassium.Compiler.Emit;
using Hassium.Runtime.Types;

using Iodine.Util;

namespace Hassium.Runtime
{
    public class VirtualMachine : ICloneable
    {
        public LinkedStack<string> CallStack { get; private set; }
        public HassiumMethod CurrentMethod { get; private set; }
        public HassiumModule CurrentModule { get; private set; }
        public SourceLocation CurrentSourceLocation { get; private set; }
        public Dictionary<HassiumMethod, int> ExceptionReturns { get; set; }
        public Dictionary<string, HassiumObject> Globals { get; private set; }
        public Stack<HassiumExceptionHandler> Handlers { get; set; }
        public LinkedStack<HassiumObject> Stack { get; set; }
        public StackFrame StackFrame { get; set; }

        public StackFrame.Frame GlobalFrame { get; set; }

        private HassiumObject lastValuePopped = HassiumObject.Null;

        public VirtualMachine()
        {

        }

        public VirtualMachine(HassiumModule module)
        {
            CallStack = new LinkedStack<string>();
            CurrentModule = module;
            ExceptionReturns = new Dictionary<HassiumMethod, int>();
            Globals = new Dictionary<string, HassiumObject>();
            Handlers = new Stack<HassiumExceptionHandler>();
            Stack = new LinkedStack<HassiumObject>();
            StackFrame = new StackFrame();
            ImportGlobals();
        }

        public void Execute(HassiumModule module, HassiumList args, StackFrame.Frame frame = null)
        {
            CallStack = new LinkedStack<string>();
            CurrentModule = module;
            ExceptionReturns = new Dictionary<HassiumMethod, int>();
            Globals = new Dictionary<string, HassiumObject>();
            Handlers = new Stack<HassiumExceptionHandler>();
            Stack = new LinkedStack<HassiumObject>();
            StackFrame = new StackFrame();
            ImportGlobals();

            var globalClass = module.GetAttribute("__global__");
            (globalClass.GetAttribute("__init__") as HassiumMethod).Invoke(this, new SourceLocation("", 0, 0));
            GlobalFrame = StackFrame.Frames.Peek();
            if (globalClass.ContainsAttribute("main"))
            {
                var mainMethod = (globalClass.GetAttribute("main") as HassiumMethod);
                if (mainMethod.Parameters.Count > 0)
                    mainMethod.Invoke(this, mainMethod.SourceLocation, args);
                else
                    mainMethod.Invoke(this, mainMethod.SourceLocation);
            }
        }

        public HassiumObject ExecuteMethod(HassiumMethod method)
        {
            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            for (int pos = 0; pos < method.Instructions.Count; pos++)
            {
                try
                {
                  //  watch.Start();
                    if (ExceptionReturns.ContainsKey(method))
                    {
                        pos = ExceptionReturns[method];
                        ExceptionReturns.Remove(method);
                    }

                    HassiumObject left, right, val, list;
                    HassiumObject[] elements;
                    string attrib;

                    var inst = method.Instructions[pos];
                    int arg = inst.Arg;

                    CurrentSourceLocation = method.Instructions[pos].SourceLocation;
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
                            var initials = new Dictionary<HassiumObject, HassiumObject>();
                            for (int i = 0; i < arg; i++)
                                initials.Add(Stack.Pop(), Stack.Pop());
                            Stack.Push(new HassiumDictionary(initials));
                            break;
                        case InstructionType.BuildList:
                            elements = new HassiumObject[arg];
                            for (int i = elements.Length - 1; i >= 0; i--)
                                elements[i] = Stack.Pop();
                            Stack.Push(new HassiumList(elements));
                            break;
                        case InstructionType.BuildThread:
                            Stack.Push(new HassiumThread(this, CurrentSourceLocation, inst.Object as HassiumMethod, StackFrame.Frames.Peek()));
                            break;
                        case InstructionType.BuildTuple:
                            HassiumObject[] tupleElements = new HassiumObject[arg];
                            for (int i = arg - 1; i >= 0; i--)
                                tupleElements[i] = Stack.Pop();
                            Stack.Push(new HassiumTuple(tupleElements));
                            break;
                        case InstructionType.Call:
                            val = Stack.Pop();
                            elements = new HassiumObject[arg];
                            for (int i = elements.Length - 1; i >= 0; i--)
                                elements[i] = Stack.Pop();
                            Stack.Push(val.Invoke(this, CurrentSourceLocation, elements));
                            break;
                        case InstructionType.Duplicate:
                            Stack.Push(Stack.Peek());
                            break;
                        case InstructionType.EnforcedAssignment:
                            val = Stack.Pop();
                            HassiumObject type = inst.Object.Invoke(this, CurrentSourceLocation);
                            if (type is HassiumTrait)
                            {
                                if (!(type as HassiumTrait).Is(this, CurrentSourceLocation, val).Bool)
                                    RaiseException(HassiumConversionFailedException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, val, type));
                            }
                            else
                            {
                                type = type is HassiumTypeDefinition ? type : type.Type();
                                if (!val.Types.Contains(type as HassiumTypeDefinition))
                                    RaiseException(HassiumConversionFailedException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, val, type));
                            }
                            if (StackFrame.Contains(arg))
                                StackFrame.Modify(arg, val);
                            else
                                StackFrame.Add(arg, val);
                            Stack.Push(val);
                            break;
                        case InstructionType.Iter:
                            val = Stack.Pop();
                            Stack.Push(val.Iter(this, val, CurrentSourceLocation));
                            break;
                        case InstructionType.IterableFull:
                            val = Stack.Pop();
                            Stack.Push(val.IterableFull(this, val, CurrentSourceLocation));
                            break;
                        case InstructionType.IterableNext:
                            val = Stack.Pop();
                            Stack.Push(val.IterableNext(this, val, CurrentSourceLocation));
                            break;
                        case InstructionType.Jump:
                            pos = method.Labels[arg];
                            break;
                        case InstructionType.JumpIfFalse:
                            val = Stack.Pop();
                            if (!val.ToBool(this, val, CurrentSourceLocation).Bool)
                                pos = method.Labels[arg];
                            break;
                        case InstructionType.JumpIfTrue:
                            val = Stack.Pop();
                            if (val.ToBool(this, val, CurrentSourceLocation).Bool)
                                pos = method.Labels[arg];
                            break;
                        case InstructionType.LoadAttribute:
                            val = Stack.Pop();
                            try
                            {
                                var attribute = val.GetAttribute(inst.Constant);
                                if (attribute.IsPrivate)
                                {
                                    RaiseException(HassiumPrivateAttribException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, new HassiumString(inst.Constant), val));
                                    return HassiumObject.Null;
                                }
                                if (attribute is HassiumProperty)
                                    Stack.Push((attribute as HassiumProperty).Get.Invoke(this, CurrentSourceLocation));
                                else
                                    Stack.Push(attribute);
                            }
                            catch (KeyNotFoundException)
                            {
                                RaiseException(HassiumAttribNotFoundException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, val, new HassiumString(inst.Constant)));
                            }
                            break;
                        case InstructionType.LoadGlobal:
                            attrib = inst.Constant;
                            if (method.Module.GetAttribute("__global__").ContainsAttribute(attrib))
                                Stack.Push(method.Module.GetAttribute("__global__").GetAttribute(attrib));
                            else if (Globals.ContainsKey(attrib))
                                Stack.Push(Globals[attrib]);
                            else if (method.Parent != null)
                            {
                                if (method.Parent.ContainsAttribute(attrib))
                                    Stack.Push(method.Parent.GetAttribute(attrib));
                                else
                                    RaiseException(HassiumAttribNotFoundException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, method.Parent, new HassiumString(attrib)));
                            }
                            else
                                RaiseException(HassiumAttribNotFoundException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, CurrentModule, new HassiumString(attrib)));
                            break;
                        case InstructionType.LoadGlobalVariable:
                            try
                            {
                                Stack.Push(CurrentModule.Globals[arg]);
                            }
                            catch (KeyNotFoundException)
                            {
                                RaiseException(HassiumAttribNotFoundException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, CurrentModule, new HassiumString(arg.ToString())));
                            }
                            break;
                        case InstructionType.LoadIterableElement:
                            list = Stack.Pop();
                            Stack.Push(list.Index(this, list, CurrentSourceLocation, Stack.Pop()));
                            break;
                        case InstructionType.LoadLocal:
                            if (GlobalFrame.ContainsVariable(arg))
                            {
                                if (StackFrame.Contains(arg))
                                    Stack.Push(StackFrame.GetVariable(CurrentSourceLocation, this, arg));
                                else
                                    Stack.Push(GlobalFrame.GetVariable(arg));
                            }
                            else
                                Stack.Push(StackFrame.GetVariable(CurrentSourceLocation, this, arg));
                            break;
                        case InstructionType.Pop:
                            lastValuePopped = Stack.Pop();
                            break;
                        case InstructionType.PopHandler:
                            //Handlers.Pop();
                            break;
                        case InstructionType.Push:
                            Stack.Push(new HassiumInt(arg));
                            break;
                        case InstructionType.PushConstant:
                            Stack.Push(new HassiumString(inst.Constant));
                            break;
                        case InstructionType.PushHandler:
                            var handler = inst.Object as HassiumExceptionHandler;
                            handler.Frame = StackFrame.Frames.Peek();
                            Handlers.Push(handler);
                            break;
                        case InstructionType.PushObject:
                            Stack.Push(inst.Object);
                            break;
                        case InstructionType.Raise:
                            RaiseException(Stack.Pop());
                            break;
                        case InstructionType.Return:
                            return Stack.Pop();
                        case InstructionType.SelfReference:
                            Stack.Push(method.Parent);
                            break;
                        case InstructionType.SetInitialAttribute:
                            val = Stack.Pop();
                            attrib = val.ToString(this, val, CurrentSourceLocation).String;
                            val = Stack.Pop();
                            var obj = Stack.Peek();
                            if (obj.ContainsAttribute(attrib))
                                obj.RemoveAttribute(attrib);
                            obj.AddAttribute(attrib, val);
                            break;
                        case InstructionType.StartThread:
                            val = Stack.Pop();
                            (val as HassiumThread).start(this, val, CurrentSourceLocation);
                            break;
                        case InstructionType.StoreAttribute:
                            val = Stack.Pop();
                            attrib = inst.Constant;
                            if (val.IsPrivate)
                            {
                                RaiseException(HassiumAttribNotFoundException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, new HassiumString(inst.Constant), Stack.Pop()));
                                return HassiumObject.Null;
                            }

                            if (val.ContainsAttribute(attrib))
                            {
                                if (val.GetAttribute(attrib) is HassiumProperty)
                                {
                                    if (((HassiumProperty)val.GetAttribute(attrib)).Set == null)
                                    {
                                        RaiseException(HassiumKeyNotFoundException.Attribs[HassiumObject.INVOKE].Invoke(this, CurrentSourceLocation, val, new HassiumString(string.Format("{0} { set; }", attrib))));
                                        return null;
                                    }
                                    ((HassiumProperty)val.GetAttribute(attrib)).Set.Invoke(this, CurrentSourceLocation, Stack.Pop());
                                    break;
                                }
                                else
                                    val.RemoveAttribute(attrib);
                            }
                            val.AddAttribute(attrib, Stack.Pop());
                            break;
                        case InstructionType.StoreGlobal:
                            val = Stack.Pop();
                            attrib = inst.Constant;
                            if (Globals.ContainsKey(attrib))
                                Globals.Remove(attrib);
                            Globals.Add(attrib, val);
                            break;
                        case InstructionType.StoreGlobalVariable:
                            CurrentModule.Globals[arg] = Stack.Pop();
                            break;
                        case InstructionType.StoreIterableElement:
                            val = Stack.Pop();
                            Stack.Push(val.StoreIndex(this, val, CurrentSourceLocation, Stack.Pop(), Stack.Pop()));
                            break;
                        case InstructionType.StoreLocal:
                            val = Stack.Pop();
                            if (StackFrame.Contains(arg))
                                StackFrame.Modify(arg, val);
                            else
                                StackFrame.Add(arg, val);
                            break;
                        case InstructionType.Swap:
                            val = Stack.Pop();
                            int index = (int)val.ToInt(this, val, CurrentSourceLocation).Int;
                            val = StackFrame.GetVariable(CurrentSourceLocation, this, index);
                            StackFrame.Modify(index, StackFrame.GetVariable(CurrentSourceLocation, this, arg));
                            StackFrame.Modify(arg, val);
                            Stack.Push(val);
                            break;
                        case InstructionType.UnaryOperation:
                            interpretUnaryOperation(Stack.Pop(), arg);
                            break;
                    }
                    //Console.WriteLine(method.Instructions[pos].ToString() + "\t" + method.Name);
                }
                catch (Exception ex)
                {
                    RaiseException(new HassiumString(ex.ToString()));
                }
                //watch.Reset();
            }
            return lastValuePopped;
        }

        private void interpretBinaryOperation(HassiumObject left, HassiumObject right, int op)
        {
            switch (op)
            {
                case (int)BinaryOperation.Addition:
                    Stack.Push(left.Add(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.BitshiftLeft:
                    Stack.Push(left.BitshiftLeft(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.BitshiftRight:
                    Stack.Push(left.BitshiftRight(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.BitwiseAnd:
                    Stack.Push(left.BitwiseAnd(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.BitwiseOr:
                    Stack.Push(left.BitwiseOr(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.BitwiseXor:
                    Stack.Push(left.Xor(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.Division:
                    Stack.Push(left.Divide(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.EqualTo:
                    Stack.Push(left.EqualTo(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.GreaterThan:
                    Stack.Push(left.GreaterThan(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.GreaterThanOrEqual:
                    Stack.Push(left.GreaterThanOrEqual(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.IntegerDivision:
                    Stack.Push(left.IntegerDivision(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.Is:
                    Stack.Push(Is(left, right));
                    break;
                case (int)BinaryOperation.LesserThan:
                    Stack.Push(left.LesserThan(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.LesserThanOrEqual:
                    Stack.Push(left.LesserThanOrEqual(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.LogicalAnd:
                    Stack.Push(left.LogicalAnd(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.LogicalOr:
                    Stack.Push(left.LogicalOr(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.Modulus:
                    Stack.Push(left.Modulus(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.Multiplication:
                    Stack.Push(left.Multiply(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.NotEqualTo:
                    Stack.Push(left.NotEqualTo(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.NullCoalescing:
                    if (left == HassiumObject.Null || left == null)
                        Stack.Push(right);
                    else
                        Stack.Push(left);
                    break;
                case (int)BinaryOperation.Power:
                    Stack.Push(left.Power(this, left, CurrentSourceLocation, right));
                    break;
                case (int)BinaryOperation.Subtraction:
                    Stack.Push(left.Subtract(this, left, CurrentSourceLocation, right));
                    break;
            }
        }

        public HassiumBool Is(HassiumObject left, HassiumObject right)
        {
            if (right is HassiumTypeDefinition)
                return new HassiumBool(left.Types.Contains(right as HassiumTypeDefinition));
            else if (right is HassiumTrait)
                return (right as HassiumTrait).Is(this, CurrentSourceLocation, left);
            else
                return new HassiumBool(left.Types.Contains(right.Type()));
        }

        private void interpretUnaryOperation(HassiumObject target, int op)
        {
            switch (op)
            {
                case (int)UnaryOperation.BitwiseNot:
                    Stack.Push(target.BitwiseNot(this, target, CurrentSourceLocation));
                    break;
                case (int)UnaryOperation.LogicalNot:
                    Stack.Push(target.LogicalNot(this, target, CurrentSourceLocation));
                    break;
                case (int)UnaryOperation.Negate:
                    Stack.Push(target.Negate(this, target, CurrentSourceLocation));
                    break;
            }
        }

        public void RaiseException(HassiumObject message)
        {
            if (Handlers.Count == 0)
            {
                var callStack = UnwindCallStack();
                Console.Write("Unhandled Exception: ");
                if (message.ContainsAttribute("message"))
                    Console.WriteLine(message.GetAttribute("message").Invoke(this, CurrentSourceLocation).ToString(this, message.GetAttribute("message"), CurrentSourceLocation).String);
                else
                    Console.WriteLine(message.ToString(this, message, CurrentSourceLocation).String);
                Console.WriteLine(callStack);
                Environment.Exit(0);
                return;
            }
            var handler = Handlers.Pop();
            handler.Invoke(this, CurrentSourceLocation, message);
            if (!ExceptionReturns.ContainsKey(handler.Caller))
                ExceptionReturns.Add(handler.Caller, handler.Caller.Labels[handler.Label]);
        }

        public void ImportGlobals()
        {
            foreach (var pair in GlobalFunctions.Functions)
                if (!Globals.ContainsKey(pair.Key))
                    Globals.Add(pair.Key, pair.Value);

            /*foreach (string constant in CurrentModule.ConstantPool.Values)
            {
                if (GlobalFunctions.Functions.ContainsKey(constant) && !Globals.ContainsKey(constant))
                    Globals.Add(constant, GlobalFunctions.Functions[constant]);
                else if (CurrentModule.ContainsAttribute(constant))
                    Globals.Add(constant, CurrentModule.GetAttribute(constant));
            }
            */
            
            foreach (var pair in InternalModule.InternalModules["Types"].GetAttributes())
                if (!Globals.ContainsKey(pair.Key))
                    Globals.Add(pair.Key, pair.Value);

            foreach (var pair in CurrentModule.GetAttribute("__global__").GetAttributes())
            {
                if (Globals.ContainsKey(pair.Key))
                    Globals.Remove(pair.Key);
                Globals.Add(pair.Key, pair.Value);
            }

        }
        public void PushCallStack(string val)
        {
            CallStack.Push(val);
        }

        public string PopCallStack()
        {
            var ret = CallStack.Pop();
            return ret;
        }

        public string UnwindCallStack()
        {
            StringBuilder sb = new StringBuilder();
            if (CallStack.Count <= 0)
                return sb.ToString();
            sb.AppendLine("At:");
            var firstLine = CallStack.Peek();
            firstLine = firstLine.Substring(0, firstLine.IndexOf("\t"));
            firstLine = string.Format("{0}\t[{1}]", firstLine, CurrentSourceLocation);
            sb.AppendLine(firstLine);
            int len = CallStack.Count;
            for (int i = 0; i < len; i++)
                sb.AppendLine(CallStack.Pop());

            CallStack = new LinkedStack<string>();
            return sb.ToString();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
