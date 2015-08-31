using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;
using Hassium.Parser.Ast;

namespace Hassium
{
    /// <summary>
    /// A Hassium function
    /// </summary>
    public class HassiumFunction: HassiumObject
    {
        private Interpreter interpreter;
        private FuncNode funcNode;
        private LocalScope localScope;
        public StackFrame stackFrame;

        public HassiumFunction(Interpreter interpreter, FuncNode funcNode, LocalScope localScope)
        {
            this.interpreter = interpreter;
            this.funcNode = funcNode;
            this.localScope = localScope;
            this.stackFrame = null;
        }

        public HassiumFunction(Interpreter interpreter, FuncNode funcNode, StackFrame stackFrame)
        {
            this.interpreter = interpreter;
            this.funcNode = funcNode;
            this.localScope = stackFrame == null ? null : stackFrame.Scope;
            this.stackFrame = stackFrame;
        }

        public override string ToString()
        {
            return ((object)this).ToString();
        }

        /// <summary>
        /// Invokes the function
        /// </summary>
        /// <param name="args">The list of arguments</param>
        /// <returns>The return value</returns>
        public override HassiumObject Invoke(HassiumObject[] args)
        {
            if(stackFrame == null || (stackFrame.Locals.Count == 0)) stackFrame = new StackFrame(localScope);

            interpreter.CallStack.Push(stackFrame);
            for (int x = 0; x < funcNode.Parameters.Count; x++)
                stackFrame.Locals[funcNode.Parameters[x]] = args[x];

            interpreter.ExecuteStatement(funcNode.Body);

            HassiumObject ret = interpreter.CallStack.Peek().ReturnValue;
            
            interpreter.CallStack.Pop();

            if (ret is HassiumArray) ret = ((HassiumArray) ret).Cast<object>().Select((s, i) => new {s, i}).ToDictionary(x => (object)x.i, x => (object)x.s);

            stackFrame = new StackFrame(localScope);

            return ret;
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{HassiumObject}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject}"/></returns>
        public static Func<HassiumObject> GetFuncVoid(HassiumObject internalFunction)
        {
            return () => (internalFunction).Invoke(new HassiumObject[0]);
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see typeref="Func"/> &lt;<see cref="HassiumObject"/>, <see cref="HassiumObject"/>&gt;
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject}"/></returns>
        public static Func<HassiumObject, HassiumObject> GetFunc1(HassiumObject internalFunction)
        {
            return (arg1) => (internalFunction).Invoke(new HassiumObject[0]);
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="T:Func{HassiumArray, HassiumObject}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject}"/></returns>
        public static Func<HassiumObject[], HassiumObject> GetFunc1Arr(HassiumObject internalFunction)
        {
            return (internalFunction).Invoke;
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{HassiumObject, HassiumObject, HassiumObject}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject, HassiumObject}"/></returns>
        public static Func<HassiumObject, HassiumObject, HassiumObject> GetFunc2(HassiumObject internalFunction)
        {
            return (arg1, arg2) => (internalFunction).Invoke(new[] { arg1, arg2 });
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{HassiumObject, HassiumObject, HassiumObject, HassiumObject}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject, HassiumObject, HassiumObject}"/></returns>
        public static Func<HassiumObject, HassiumObject, HassiumObject, HassiumObject> GetFunc3(HassiumObject internalFunction)
        {
            return (arg1, arg2, arg3) => (internalFunction).Invoke(new[] { arg1, arg2, arg3 });
        }
    }
}

