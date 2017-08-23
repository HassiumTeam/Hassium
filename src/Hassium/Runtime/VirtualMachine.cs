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
        public LinkedStack<string> CallStack;
        public HassiumMethod CurrentMethod;
        public HassiumModule CurrentModule;
        public SourceLocation CurrentSourceLocation { get { return inst.SourceLocation; } }
        public Dictionary<HassiumMethod, int> ExceptionReturns;
        public Dictionary<string, HassiumObject> Globals;
        public Stack<HassiumExceptionHandler> Handlers;
        public LinkedStack<HassiumObject> Stack;
        public StackFrame StackFrame;

        public Dictionary<int, HassiumObject> GlobalFrame;

        private HassiumObject lastValuePopped = HassiumObject.Null;
        private HassiumInstruction inst;

        public VirtualMachine()
        {

        }

        public VirtualMachine(HassiumModule module)
        {
            CallStack = new LinkedStack<string>();
            CurrentModule = module;
            ExceptionReturns = new Dictionary<HassiumMethod, int>();
            Globals = module.BoundAttributes["__global__"].BoundAttributes;
            Handlers = new Stack<HassiumExceptionHandler>();
            Stack = new LinkedStack<HassiumObject>();
            StackFrame = new StackFrame();
            ImportGlobals();
        }

        public void Execute(HassiumModule module, HassiumList args, Dictionary<int, HassiumObject> frame = null)
        {
            CallStack = new LinkedStack<string>();
            CurrentModule = module;
            ExceptionReturns = new Dictionary<HassiumMethod, int>();
            Globals = module.BoundAttributes["__global__"].BoundAttributes;
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
            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch
            int count = method.Instructions.Count;
            for (int pos = 0; pos < count; pos++)
            {
                try
                {
                    //watch.Start();
                    if (ExceptionReturns.ContainsKey(method))
                    {
                        pos = ExceptionReturns[method];
                        ExceptionReturns.Remove(method);
                    }

                    HassiumObject left, right, val, list;
                    HassiumObject[] elements;
                    string attrib;
                    int arg;
                    SourceLocation loc;

                    inst = method.Instructions[pos];

                    switch (inst.InstructionType)
                    {
                        case InstructionType.BinaryOperation:
                            right = Stack.Pop();
                            left = Stack.Pop();
                            interpretBinaryOperation(left, right, inst.Arg);
                            break;
                        case InstructionType.BuildClosure:
                            Stack.Push(new HassiumClosure(Stack.Pop() as HassiumMethod, StackFrame.Frames.Peek()));
                            break;
                        case InstructionType.BuildDictionary:
                            arg = inst.Arg;
                            var initials = new Dictionary<HassiumObject, HassiumObject>();
                            for (int i = 0; i < arg; i++)
                                initials.Add(Stack.Pop(), Stack.Pop());
                            Stack.Push(new HassiumDictionary(initials));
                            break;
                        case InstructionType.BuildList:
                            elements = new HassiumObject[inst.Arg];
                            for (int i = elements.Length - 1; i >= 0; i--)
                                elements[i] = Stack.Pop();
                            Stack.Push(new HassiumList(elements));
                            break;
                        case InstructionType.BuildThread:
                            Stack.Push(new HassiumThread(this, inst.SourceLocation, inst.Object as HassiumMethod, StackFrame.Frames.Peek()));
                            break;
                        case InstructionType.BuildTuple:
                            arg = inst.Arg;
                            HassiumObject[] tupleElements = new HassiumObject[arg];
                            for (int i = arg - 1; i >= 0; i--)
                                tupleElements[i] = Stack.Pop();
                            Stack.Push(new HassiumTuple(tupleElements));
                            break;
                        case InstructionType.Call:
                            val = Stack.Pop();
                            elements = new HassiumObject[inst.Arg];
                            for (int i = elements.Length - 1; i >= 0; i--)
                                elements[i] = Stack.Pop();
                            Stack.Push(val.Invoke(this, inst.SourceLocation, elements));
                            break;
                        case InstructionType.Duplicate:
                            Stack.Push(Stack.Peek());
                            break;
                        case InstructionType.EnforcedAssignment:
                            loc = inst.SourceLocation;
                            val = Stack.Pop();
                            HassiumObject type = inst.Object.Invoke(this, inst.SourceLocation);
                            if (type is HassiumTrait)
                            {
                                if (!(type as HassiumTrait).Is(this, loc, val).Bool)
                                    RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(this, null, loc, val, type));
                            }
                            else
                            {
                                type = type is HassiumTypeDefinition ? type : type.Type();
                                if (!val.Types.Contains(type as HassiumTypeDefinition))
                                    RaiseException(HassiumConversionFailedException.ConversionFailedExceptionTypeDef._new(this, null, loc, val, type));
                            }
                            arg = inst.Arg;
                            if (StackFrame.Contains(arg))
                                StackFrame.Modify(arg, val);
                            else
                                StackFrame.Add(arg, val);
                            Stack.Push(val);
                            break;
                        case InstructionType.Iter:
                            val = Stack.Pop();
                            Stack.Push(val.Iter(this, val, inst.SourceLocation));
                            break;
                        case InstructionType.IterableFull:
                            val = Stack.Pop();
                            Stack.Push(val.IterableFull(this, val, inst.SourceLocation));
                            break;
                        case InstructionType.IterableNext:
                            val = Stack.Pop();
                            Stack.Push(val.IterableNext(this, val, inst.SourceLocation));
                            break;
                        case InstructionType.Jump:
                            pos = method.Labels[inst.Arg];
                            break;
                        case InstructionType.JumpIfFalse:
                            val = Stack.Pop();
                            if (!(val as HassiumBool).Bool)
                                pos = method.Labels[inst.Arg];
                            break;
                        case InstructionType.JumpIfTrue:
                            val = Stack.Pop();
                            if ((val as HassiumBool).Bool)
                                pos = method.Labels[inst.Arg];
                            break;
                        case InstructionType.LoadAttribute:
                            loc = inst.SourceLocation;
                            val = Stack.Pop();
                            try
                            {
                                var attribute = val.GetAttribute(inst.Constant);
                                if (attribute.IsPrivate)
                                {
                                    RaiseException(HassiumPrivateAttribException.PrivateAttribExceptionTypeDef._new(this, null, loc, new HassiumString(inst.Constant), val));
                                    return HassiumObject.Null;
                                }

                                if (attribute is HassiumProperty)
                                    Stack.Push((attribute as HassiumProperty).Get.Invoke(this, inst.SourceLocation));
                                else
                                    Stack.Push(attribute);
                            }
                            catch (KeyNotFoundException)
                            {
                                RaiseException(HassiumAttribNotFoundException.AttribNotFoundExceptionTypeDef._new(this, null, loc, val, new HassiumString(inst.Constant)));
                            }
                            break;
                        case InstructionType.LoadGlobal:
                            loc = inst.SourceLocation;
                            attrib = inst.Constant;
                            if (Globals.ContainsKey(attrib))
                                Stack.Push(Globals[attrib]);
                            else if (method.Parent != null)
                            {
                                if (method.Parent.ContainsAttribute(attrib))
                                    Stack.Push(method.Parent.GetAttribute(attrib));
                                else
                                    RaiseException(HassiumAttribNotFoundException.AttribNotFoundExceptionTypeDef._new(this, null, loc, method.Parent, new HassiumString(attrib)));
                            }
                            else
                                RaiseException(HassiumAttribNotFoundException.AttribNotFoundExceptionTypeDef._new(this, null, loc, CurrentModule, new HassiumString(attrib)));
                            break;
                        case InstructionType.LoadGlobalVariable:
                            try
                            {
                                Stack.Push(CurrentModule.Globals[inst.Arg]);
                            }
                            catch (KeyNotFoundException)
                            {
                                RaiseException(HassiumAttribNotFoundException.AttribNotFoundExceptionTypeDef._new(this, null, inst.SourceLocation, CurrentModule, new HassiumString(inst.Arg.ToString())));
                            }
                            break;
                        case InstructionType.LoadIterableElement:
                            list = Stack.Pop();
                            Stack.Push(list.Index(this, list, inst.SourceLocation, Stack.Pop()));
                            break;
                        case InstructionType.LoadLocal:
                           /* if (GlobalFrame.ContainsVariable(arg))
                            {
                                if (StackFrame.Contains(arg))
                                    Stack.Push(StackFrame.GetVariable(CurrentSourceLocation, this, arg));
                                else
                                    Stack.Push(GlobalFrame.GetVariable(arg));
                            }
                            else*/
                                Stack.Push(StackFrame.GetVariable(inst.SourceLocation, this, inst.Arg));
                            break;
                        case InstructionType.Pop:
                            lastValuePopped = Stack.Pop();
                            break;
                        case InstructionType.PopHandler:
                            //Handlers.Pop();
                            break;
                        case InstructionType.Push:
                            Stack.Push(new HassiumInt(inst.Arg));
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
                            attrib = val.ToString(this, val, inst.SourceLocation).String;
                            val = Stack.Pop();
                            var obj = Stack.Peek();
                            if (obj.ContainsAttribute(attrib))
                                obj.RemoveAttribute(attrib);
                            obj.AddAttribute(attrib, val);
                            break;
                        case InstructionType.StartThread:
                            val = Stack.Pop();
                            HassiumThread.ThreadTypeDef.start(this, val, inst.SourceLocation);
                            break;
                        case InstructionType.StoreAttribute:
                            val = Stack.Pop();
                            attrib = inst.Constant;
                            if (val.IsPrivate)
                            {
                                RaiseException(HassiumAttribNotFoundException.AttribNotFoundExceptionTypeDef._new(this, null, inst.SourceLocation, new HassiumString(inst.Constant), Stack.Pop()));
                                return HassiumObject.Null;
                            }

                            if (val.ContainsAttribute(attrib))
                            {
                                if (val.GetAttribute(attrib) is HassiumProperty)
                                {
                                    if (((HassiumProperty)val.GetAttribute(attrib)).Set == null)
                                    {
                                        RaiseException(HassiumKeyNotFoundException.KeyNotFoundExceptionTypeDef._new(this, null, inst.SourceLocation, val, new HassiumString(string.Format("{0} { set; }", attrib))));
                                        return null;
                                    }
                                    ((HassiumProperty)val.GetAttribute(attrib)).Set.Invoke(this, inst.SourceLocation, Stack.Pop());
                                    break;
                                }
                                else
                                    val.RemoveAttribute(attrib);
                            }
                            val.AddAttribute(attrib, Stack.Pop().SetSelfReference(val));
                            break;
                        case InstructionType.StoreGlobal:
                            val = Stack.Pop();
                            attrib = inst.Constant;
                            if (Globals.ContainsKey(attrib))
                                Globals.Remove(attrib);
                            Globals.Add(attrib, val);
                            break;
                        case InstructionType.StoreGlobalVariable:
                            CurrentModule.Globals[inst.Arg] = Stack.Pop();
                            break;
                        case InstructionType.StoreIterableElement:
                            val = Stack.Pop();
                            Stack.Push(val.StoreIndex(this, val, inst.SourceLocation, Stack.Pop(), Stack.Pop()));
                            break;
                        case InstructionType.StoreLocal:
                            arg = inst.Arg;
                            val = Stack.Pop();
                            if (StackFrame.Contains(arg))
                                StackFrame.Modify(arg, val);
                            else
                                StackFrame.Add(arg, val);
                            break;
                        case InstructionType.Swap:
                            loc = inst.SourceLocation;
                            val = Stack.Pop();
                            arg = inst.Arg;
                            int index = (int)val.ToInt(this, val, loc).Int;
                            val = StackFrame.GetVariable(loc, this, index);
                            StackFrame.Modify(index, StackFrame.GetVariable(loc, this, arg));
                            StackFrame.Modify(arg, val);
                            Stack.Push(val);
                            break;
                        case InstructionType.UnaryOperation:
                            interpretUnaryOperation(Stack.Pop(), inst.Arg);
                            break;
                    }
                    //Console.WriteLine(method.Instructions[pos].ToString() + "\t" + watch.ElapsedTicks);
                    //watch.Reset();
                }
                catch (Exception ex)
                {
                    RaiseException(new HassiumString(ex.ToString()));
                }
            }
            return lastValuePopped;
        }

        private void interpretBinaryOperation(HassiumObject left, HassiumObject right, int op)
        {
            var loc = inst.SourceLocation;
            switch (op)
            {
                case (int)BinaryOperation.Addition:
                    Stack.Push(left.Add(this, left, loc, right));
                    break;
                case (int)BinaryOperation.BitshiftLeft:
                    Stack.Push(left.BitshiftLeft(this, left, loc, right));
                    break;
                case (int)BinaryOperation.BitshiftRight:
                    Stack.Push(left.BitshiftRight(this, left, loc, right));
                    break;
                case (int)BinaryOperation.BitwiseAnd:
                    Stack.Push(left.BitwiseAnd(this, left, loc, right));
                    break;
                case (int)BinaryOperation.BitwiseOr:
                    Stack.Push(left.BitwiseOr(this, left, loc, right));
                    break;
                case (int)BinaryOperation.BitwiseXor:
                    Stack.Push(left.Xor(this, left, loc, right));
                    break;
                case (int)BinaryOperation.Division:
                    Stack.Push(left.Divide(this, left, loc, right));
                    break;
                case (int)BinaryOperation.EqualTo:
                    Stack.Push(left.EqualTo(this, left, loc, right));
                    break;
                case (int)BinaryOperation.GreaterThan:
                    Stack.Push(left.GreaterThan(this, left, loc, right));
                    break;
                case (int)BinaryOperation.GreaterThanOrEqual:
                    Stack.Push(left.GreaterThanOrEqual(this, left, loc, right));
                    break;
                case (int)BinaryOperation.IntegerDivision:
                    Stack.Push(left.IntegerDivision(this, left, loc, right));
                    break;
                case (int)BinaryOperation.Is:
                    Stack.Push(Is(left, right));
                    break;
                case (int)BinaryOperation.LesserThan:
                    Stack.Push(left.LesserThan(this, left, loc, right));
                    break;
                case (int)BinaryOperation.LesserThanOrEqual:
                    Stack.Push(left.LesserThanOrEqual(this, left, loc, right));
                    break;
                case (int)BinaryOperation.LogicalAnd:
                    Stack.Push(left.LogicalAnd(this, left, loc, right));
                    break;
                case (int)BinaryOperation.LogicalOr:
                    Stack.Push(left.LogicalOr(this, left, loc, right));
                    break;
                case (int)BinaryOperation.Modulus:
                    Stack.Push(left.Modulus(this, left, loc, right));
                    break;
                case (int)BinaryOperation.Multiplication:
                    Stack.Push(left.Multiply(this, left, loc, right));
                    break;
                case (int)BinaryOperation.NotEqualTo:
                    Stack.Push(left.NotEqualTo(this, left, loc, right));
                    break;
                case (int)BinaryOperation.NullCoalescing:
                    if (left == HassiumObject.Null || left == null)
                        Stack.Push(right);
                    else
                        Stack.Push(left);
                    break;
                case (int)BinaryOperation.Power:
                    Stack.Push(left.Power(this, left, loc, right));
                    break;
                case (int)BinaryOperation.Subtraction:
                    Stack.Push(left.Subtract(this, left, loc, right));
                    break;
            }
        }

        public HassiumBool Is(HassiumObject left, HassiumObject right)
        {
            if (right is HassiumTypeDefinition)
                return new HassiumBool(left.Types.Contains(right as HassiumTypeDefinition));
            else if (right is HassiumTrait)
                return (right as HassiumTrait).Is(this, inst.SourceLocation, left);
            else
                return new HassiumBool(left.Types.Contains(right.Type()));
        }

        private void interpretUnaryOperation(HassiumObject target, int op)
        {
            var loc = inst.SourceLocation;
            switch (op)
            {
                case (int)UnaryOperation.BitwiseNot:
                    Stack.Push(target.BitwiseNot(this, target, loc));
                    break;
                case (int)UnaryOperation.LogicalNot:
                    Stack.Push(target.LogicalNot(this, target, loc));
                    break;
                case (int)UnaryOperation.Negate:
                    Stack.Push(target.Negate(this, target, loc));
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
                    Console.WriteLine(message.GetAttribute("message").Invoke(this, inst.SourceLocation).ToString(this, message.GetAttribute("message"), inst.SourceLocation).String);
                else
                    Console.WriteLine(message.ToString(this, message, inst.SourceLocation).String);
                Console.WriteLine(callStack);
                Environment.Exit(0);
                return;
            }
            var handler = Handlers.Pop();
            handler.Invoke(this, inst.SourceLocation, message);
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
            firstLine = string.Format("{0}\t[{1}]", firstLine, inst.SourceLocation);
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