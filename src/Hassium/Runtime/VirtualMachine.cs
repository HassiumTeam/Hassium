using System;
using System.Collections.Generic;

using Hassium.CodeGen;
using Hassium.Runtime.StandardLibrary;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime
{
    public class VirtualMachine
    {
        public StackFrame StackFrame { get { return stackFrame; } }
        public Stack<HassiumObject> Stack { get { return stack; } }
        public Stack<string> CallStack { get { return callStack; } }
        public Stack<HassiumExceptionHandler> Handlers { get { return handlers; } }
        public SourceLocation CurrentSourceLocation { get { return sourceLocation; } }
        public HassiumModule CurrentlyExecutingModule { get { return module; } }

        private StackFrame stackFrame;
        private Stack<HassiumObject> stack;
        private Stack<string> callStack = new Stack<string>();
        private Stack<HassiumExceptionHandler> handlers = new Stack<HassiumExceptionHandler>();

        private Dictionary<double, HassiumObject> globals;
        private HassiumModule module;
        private SourceLocation sourceLocation;
        private Stack<MethodBuilder> methodStack = new Stack<MethodBuilder>();
        private Dictionary<MethodBuilder, int> exceptionReturns = new Dictionary<MethodBuilder, int>(); 

        public void Execute(HassiumModule module)
        {
            globals = new Dictionary<double, HassiumObject>();
            stack = new Stack<HassiumObject>();
            stackFrame = new StackFrame();
            this.module = module;
            gatherGlobals(module.ConstantPool);

            callStack.Push("func main ()");
            stackFrame.EnterFrame();
            ExecuteMethod((MethodBuilder)module.Attributes["main"]);
            stackFrame.PopFrame();
            callStack.Pop();
        }

        public HassiumObject ExecuteMethod(MethodBuilder method)
        {
            methodStack.Push(method);
            gatherLabels(method);
            for (int position = 0; position < method.Instructions.Count; position++)
            {
                if (exceptionReturns.ContainsKey(method))
                {
                    position = exceptionReturns[method];
                    exceptionReturns.Remove(method);
                }

                HassiumObject left, right, value, list, index, location;
                double argument = method.Instructions[position].Argument;
                int argumentInt = Convert.ToInt32(argument);
                sourceLocation = method.Instructions[position].SourceLocation;
                string attribute;
     //          Console.WriteLine("{0}\t{1}\t\t{2}", method.Instructions[position].InstructionType, argument, method.Name);
       
                try
                {
                    switch (method.Instructions[position].InstructionType)
                    {
                        case InstructionType.Binary_Operation:
                            right = stack.Pop();
                            left = stack.Pop();
                            executeBinaryOperation(left, right, argumentInt);
                            break;
                        case InstructionType.Build_Closure:
                            stack.Push(new HassiumClosure((MethodBuilder)stack.Pop(), StackFrame.Frames.Peek()));
                            break;
                        case InstructionType.Call:
                            HassiumObject target = stack.Pop();
                            HassiumObject[] args = new HassiumObject[argumentInt];
                            for (int i = 0; i < args.Length; i++)
                                args[i] = stack.Pop();
                            stack.Push(target.Invoke(this, args));
                            break;
                        case InstructionType.Create_List:
                            HassiumObject[] listElements = new HassiumObject[argumentInt];
                            for (int i = argumentInt - 1; i >= 0; i--)
                                listElements[i] = stack.Pop();
                            stack.Push(new HassiumList(listElements));
                            break;
                        case InstructionType.Create_Tuple:
                            HassiumObject[] tupleElements = new HassiumObject[argumentInt];
                            for (int i = argumentInt - 1; i >= 0; i--)
                                tupleElements[i] = stack.Pop();
                            stack.Push(new HassiumTuple(tupleElements));
                            break;
                        case InstructionType.Dup:
                            stack.Push(stack.Peek());
                            break;
                        case InstructionType.Enumerable_Full:
                            stack.Push(stack.Pop().EnumerableFull(this));
                            break;
                        case InstructionType.Enumerable_Next:
                            stack.Push(stack.Pop().EnumerableNext(this));
                            break;
                        case InstructionType.Enumerable_Reset:
                            stack.Pop().EnumerableReset(this);
                            break;
                        case InstructionType.Jump:
                            position = method.Labels[argument];
                            break;
                        case InstructionType.Jump_If_True:
                            if (((HassiumBool)stack.Pop()).Value)
                                position = method.Labels[argument];
                            break;
                        case InstructionType.Jump_If_False:
                            if (!((HassiumBool)stack.Pop()).Value)
                                position = method.Labels[argument];
                            break;
                        case InstructionType.Key_Value_Pair:
                            HassiumObject value_ = stack.Pop();
                            HassiumObject key = stack.Pop();
                            stack.Push(new HassiumKeyValuePair(key, value_));
                            break;
                        case InstructionType.Load_Attribute:
                            attribute = module.ConstantPool[argumentInt].ToString(this);
                            location = stack.Pop();
                            HassiumObject attrib = null;
                            try
                            {
                                attrib = location.Attributes[attribute];
                            }
                            catch (KeyNotFoundException)
                            {
                                throw new InternalException(location + " does not contain a definition for " + attribute);
                            }
                            if (attrib is HassiumProperty)
                                stack.Push(((HassiumProperty)attrib).GetValue(this, new HassiumObject[] { }));
                            else if (attrib is UserDefinedProperty)
                                stack.Push(ExecuteMethod(((UserDefinedProperty)attrib).GetMethod));
                            else
                                stack.Push(attrib);
                            break;
                        case InstructionType.Load_Global:
                            try
                            {
                                stack.Push(globals[argument]);
                            }
                            catch (KeyNotFoundException)
                            {
                                throw new InternalException("Cannot find global identifier!");
                            }
                            break;
                        case InstructionType.Load_Global_Variable:
                            stack.Push(module.Globals[argumentInt]);
                            break;
                        case InstructionType.Load_List_Element:
                            index = stack.Pop();
                            list = stack.Pop();
                            stack.Push(list.Index(this, index));
                            break;
                        case InstructionType.Load_Local:
                            stack.Push(stackFrame.GetVariable(argumentInt));
                            break;
                        case InstructionType.Pop:
                            stack.Pop();
                            break;
                        case InstructionType.Pop_Handler:
                            handlers.Pop();
                            break;
                        case InstructionType.Push:
                            stack.Push(new HassiumInt(argumentInt));
                            break;
                        case InstructionType.Push_Bool:
                            stack.Push(new HassiumBool(argument == 1));
                            break;
                        case InstructionType.Push_Handler:
                            HassiumExceptionHandler handler = (HassiumExceptionHandler)module.ConstantPool[argumentInt];
                            handler.Frame = StackFrame.Frames.Peek();
                            handlers.Push(handler);
                            break;
                        case InstructionType.Push_Object:
                            stack.Push(module.ConstantPool[argumentInt]);
                            break;
                        case InstructionType.Raise:
                            RaiseException(stack.Pop(), method, ref position);
                            break;
                        case InstructionType.Return:
                            methodStack.Pop();
                            return stack.Pop();
                        case InstructionType.Self_Reference:
                            stack.Push(method.Parent);
                            break;
                        case InstructionType.Store_Attribute:
                            location = stack.Pop();
                            attribute = module.ConstantPool[argumentInt].ToString(this);
                            if (location is HassiumProperty)
                            {
                                HassiumProperty builtinProp = location as HassiumProperty;
                                builtinProp.Invoke(this, new HassiumObject[] { stack.Pop() });
                            }
                            else if (location is UserDefinedProperty)
                            {
                                UserDefinedProperty userProp = location as UserDefinedProperty;
                                userProp.SetMethod.Invoke(this, new HassiumObject[] { stack.Pop() });
                            }
                            else
                                try
                                {
                                    location.Attributes[attribute] = stack.Pop();
                                }
                                catch (KeyNotFoundException)
                                {
                                    throw new InternalException(location + " does not contain a definition for " + attribute);
                                }
                            break;
                        case InstructionType.Store_Global_Variable:
                            module.Globals[argumentInt] = stack.Pop();
                            break;
                        case InstructionType.Store_List_Element:
                            index = stack.Pop();
                            list = stack.Pop();
                            value = stack.Pop();
                            stack.Push(list.StoreIndex(this, index, value));
                            break;
                        case InstructionType.Store_Local:
                            value = stack.Pop();
                            if (stackFrame.Contains(argumentInt))
                                stackFrame.Modify(argumentInt, value);
                            else
                                stackFrame.Add(argumentInt, value);
                            break;
                        case InstructionType.Unary_Operation:
                            executeUnaryOperation(stack.Pop(), argumentInt);
                            break;
                    }
                }
                catch (InternalException ex)
                {
                    RaiseException(new HassiumString(ex.Message), method, ref position);
                }
                catch (DivideByZeroException)
                {
                    RaiseException(new HassiumString("Divide by zero!"), method, ref position);
                }
            }
            methodStack.Pop();
            return HassiumObject.Null;
        }

        private void executeBinaryOperation(HassiumObject left, HassiumObject right, int argument)
        {
            switch (argument)
            {
                case 0:
                    stack.Push(left.Add(this, right));
                    break;
                case 1:
                    stack.Push(left.Sub(this, right));
                    break;
                case 2:
                    stack.Push(left.Mul(this, right));
                    break;
                case 3:
                    stack.Push(left.Div(this, right));
                    break;
                case 4:
                    stack.Push(new HassiumInt((long)Math.Floor((double)left.Div(this, right).Value)));
                    break;
                case 5:
                    stack.Push(left.Mod(this, right));
                    break;
                case 6:
                    stack.Push(left.XOR(this, right));
                    break;
                case 7:
                    stack.Push(left.OR(this, right));
                    break;
                case 8:
                    stack.Push(left.Xand(this, right));
                    break;
                case 9:
                    stack.Push(left.Equals(this, right));
                    break;
                case 10:
                    stack.Push(left.NotEquals(this, right));
                    break;
                case 11:
                    stack.Push(left.GreaterThan(this, right));
                    break;
                case 12:
                    stack.Push(left.GreaterThanOrEqual(this, right));
                    break;
                case 13:
                    stack.Push(left.LesserThan(this, right));
                    break;
                case 14:
                    stack.Push(left.LesserThanOrEqual(this, right));
                    break;
                case 15:
                    stack.Push(new HassiumBool(HassiumBool.Create(left).Value || HassiumBool.Create(right).Value));
                    break;
                case 16:
                    stack.Push(new HassiumBool(HassiumBool.Create(left).Value && HassiumBool.Create(right).Value));
                    break;
                case 17:
                    stack.Push(new HassiumDouble(Math.Pow(HassiumDouble.Create(left).Value, HassiumDouble.Create(right).Value)));
                    break;
                case 18:
                    stack.Push(left.BitShiftLeft(this, right));
                    break;
                case 19:
                    stack.Push(left.BitShiftRight(this, right));
                    break;
                case 20:
                    stack.Push(left is HassiumNull ? right : left);
                    break;
                case 21:
                    stack.Push(left.Contains(this, right));
                    break;
            }
        }

        public void RaiseException(HassiumObject message, MethodBuilder method, ref int pos)
        {
            if (handlers.Count == 0)
                throw new RuntimeException(message.ToString(this), sourceLocation);
            else
            {
                HassiumExceptionHandler handler = handlers.Peek() as HassiumExceptionHandler;
                handler.Invoke(this, new HassiumObject[] { message });
                //pos = handler.SourceMethod.Labels[handler.Label];
                exceptionReturns.Add(handler.SourceMethod, handler.SourceMethod.Labels[handler.Label]);
            }
        }

        private void executeUnaryOperation(HassiumObject target, int argument)
        {
            switch (argument)
            {
                case 0:
                    stack.Push(target.Not(this));
                    break;
                case 1:
                    stack.Push(target.BitwiseComplement(this));
                    break;
                case 2:
                    stack.Push(target.Negate(this));
                    break;
            }
        }

        private void gatherLabels(MethodBuilder method)
        {
            method.Labels = new Dictionary<double, int>();
            for (int i = 0; i < method.Instructions.Count; i++)
            {
      //          Console.WriteLine(method.Instructions[i].InstructionType + "\t" + method.Instructions[i].Argument);
                if (method.Instructions[i].InstructionType == InstructionType.Label)
                    method.Labels.Add(method.Instructions[i].Argument, i);
            }
        }

        private void gatherGlobals(List<HassiumObject> constantPool)
        {
            for (int i = 0; i < constantPool.Count; i++)
                if (GlobalFunctions.FunctionList.ContainsKey(constantPool[i].ToString(this)))
                    globals.Add(Convert.ToDouble(i), GlobalFunctions.FunctionList[constantPool[i].ToString(this)]);
                else if (module.Attributes.ContainsKey(constantPool[i].ToString(this)))
                    globals.Add(i, module.Attributes[constantPool[i].ToString(this)]);
            List<int> keys = new List<int>(module.Globals.Keys);
            foreach (int key in keys)
                module.Globals[key] = module.Globals[key].Invoke(this, new HassiumObject[0]);
        }
    }
}