using System;
using Hassium.Functions;

namespace Hassium
{
    public class HassiumFunction : IFunction
    {
        private Interpreter interpreter;
        private FuncNode funcNode;
        private LocalScope localScope;

        public HassiumFunction(Interpreter interpreter, FuncNode funcNode, LocalScope localScope)
        {
            this.interpreter = interpreter;
            this.funcNode = funcNode;
            this.localScope = localScope;
        }

        public object Invoke(object[] args)
        {
            StackFrame stackFrame = new StackFrame(localScope);

            interpreter.CallStack.Push(stackFrame);
            for (int x = 0; x < funcNode.Parameters.Count; x++)
                stackFrame.Locals[funcNode.Parameters[x]] = args[x];

            interpreter.ExecuteStatement(funcNode.Body);

            object ret = interpreter.CallStack.Peek().ReturnValue;
            
            interpreter.CallStack.Pop();

            return ret;
        }

        public static Func<object> GetFuncVoid(object internalFunction)
        {
            return () => ((IFunction)internalFunction).Invoke(new object[] { });
        }


        public static Func<object, object> GetFunc1(object internalFunction)
        {
            return (arg1) => ((IFunction)internalFunction).Invoke(new[] { arg1 });
        }

        public static Func<object[], object> GetFunc1Arr(object internalFunction)
        {
            return ((IFunction)internalFunction).Invoke;
        }

        public static Func<object, object, object> GetFunc2(object internalFunction)
        {
            return (arg1, arg2) => ((IFunction)internalFunction).Invoke(new[] { arg1, arg2 });
        }

        public static Func<object, object, object, object> GetFunc3(object internalFunction)
        {
            return (arg1, arg2, arg3) => ((IFunction)internalFunction).Invoke(new[] { arg1, arg2, arg3 });
        }
    }
}

