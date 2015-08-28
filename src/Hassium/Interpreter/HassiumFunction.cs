using System;
using Hassium.Functions;
using Hassium.Parser.Ast;

namespace Hassium
{
    /// <summary>
    /// A Hassium function
    /// </summary>
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

        public HassiumFunction(Interpreter interpreter, LambdaFuncNode funcNode, LocalScope localScope)
        {
            this.interpreter = interpreter;
            this.funcNode = new FuncNode("", funcNode.Parameters, funcNode.Body);
            this.localScope = localScope;
        }

        /// <summary>
        /// Invokes the function
        /// </summary>
        /// <param name="args">The list of arguments</param>
        /// <returns>The return value</returns>
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

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{Object}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{Object}"/></returns>
        public static Func<object> GetFuncVoid(object internalFunction)
        {
            return () => ((IFunction)internalFunction).Invoke(new object[] { });
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see typeref="Func"/> &lt;<see cref="Object"/>, <see cref="Object"/>&gt;
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{Object, Object}"/></returns>
        public static Func<object, object> GetFunc1(object internalFunction)
        {
            return (arg1) => ((IFunction)internalFunction).Invoke(new[] { arg1 });
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="T:Func{Object[], Object}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{Object}"/></returns>
        public static Func<object[], object> GetFunc1Arr(object internalFunction)
        {
            return ((IFunction)internalFunction).Invoke;
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{Object, Object, Object}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{Object, Object, Object}"/></returns>
        public static Func<object, object, object> GetFunc2(object internalFunction)
        {
            return (arg1, arg2) => ((IFunction)internalFunction).Invoke(new[] { arg1, arg2 });
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{Object, Object, Object, Object}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{Object, Object, Object, Object}"/></returns>
        public static Func<object, object, object, object> GetFunc3(object internalFunction)
        {
            return (arg1, arg2, arg3) => ((IFunction)internalFunction).Invoke(new[] { arg1, arg2, arg3 });
        }
    }
}

