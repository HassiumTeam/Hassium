using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.Parser.Ast;

namespace Hassium
{
    /// <summary>
    /// A Hassium function
    /// </summary>
    public class HassiumFunction: HassiumObject
    {
        public readonly Interpreter Interpreter;
        public readonly LocalScope LocalScope;
        public readonly FuncNode FuncNode;
        public StackFrame stackFrame;

        public HassiumFunction(Interpreter interpreter, FuncNode funcNode, LocalScope localScope)
        {
            this.Interpreter = interpreter;
            this.FuncNode = funcNode;
            this.LocalScope = localScope;
            this.stackFrame = null;
        }

        public HassiumFunction(Interpreter interpreter, FuncNode funcNode, StackFrame stackFrame)
        {
            this.Interpreter = interpreter;
            this.FuncNode = funcNode;
            this.LocalScope = stackFrame == null ? null : stackFrame.Scope;
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
            if(stackFrame == null || (stackFrame.Locals.Count == 0)) stackFrame = new StackFrame(LocalScope);

            Interpreter.CallStack.Push(stackFrame);
            for (int x = 0; x < FuncNode.Parameters.Count; x++)
                stackFrame.Locals[FuncNode.Parameters[x]] = args[x];

            Interpreter.ExecuteStatement(FuncNode.Body);

            HassiumObject ret = Interpreter.CallStack.Peek().ReturnValue;
            
            Interpreter.CallStack.Pop();

            if (ret is HassiumArray) ret = ((HassiumArray) ret).Cast<object>().Select((s, i) => new {s, i}).ToDictionary(x => (object)x.i, x => (object)x.s);

            stackFrame = new StackFrame(LocalScope);

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

