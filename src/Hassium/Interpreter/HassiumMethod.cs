using System;
using System.Linq;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;
using Hassium.Lexer;
using Hassium.Parser.Ast;
using Hassium.Semantics;

namespace Hassium.Interpreter
{
    public class HassiumMethod : HassiumObject
    {
        public HassiumObject SelfReference { get; set; }
        public Interpreter Interpreter;
        public LocalScope LocalScope;
        public FuncNode FuncNode;
        public StackFrame stackFrame;

        public string Name { get { return FuncNode.Name; } }

        public bool IsStatic { get { return !FuncNode.Parameters.Contains("this"); } }

        public bool IsConstructor { get { return Name == "new"; } }

        public HassiumMethod(Interpreter interpreter, FuncNode funcNode, LocalScope localScope, HassiumObject self)
        {
            SelfReference = self;
            Interpreter = interpreter;
            FuncNode = funcNode;
            LocalScope = localScope;
            stackFrame = null;
        }

        public HassiumMethod(Interpreter interpreter, FuncNode funcNode, StackFrame stackFrame, HassiumObject self)
        {
            SelfReference = self;
            Interpreter = interpreter;
            FuncNode = funcNode;
            LocalScope = stackFrame == null ? null : stackFrame.Scope;
            this.stackFrame = stackFrame;
        }

        public static implicit operator HassiumEventHandler(HassiumMethod mt)
        {
            return mt.Invoke;
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            if (stackFrame == null || (stackFrame.Locals.Count == 0 || FuncNode.Parameters.Any(x => stackFrame.Locals.ContainsKey(x))))
                stackFrame = new StackFrame(LocalScope, (IsStatic && !IsConstructor) ? null : SelfReference);
            if (!IsStatic || IsConstructor)
                stackFrame.Locals["this"] = SelfReference;

            Interpreter.inFunc++;
            var parms = FuncNode.Parameters;


            if (parms.Contains("this")) parms.Remove("this");

            if (parms.Count != args.Length)
                throw new Exception("Incorrect arguments for funtion " + Name);

            for (int x = 0; x < parms.Count; x++)
                stackFrame.Locals[parms[x]] = args[x];

            Interpreter.CallStack.Push(stackFrame);

            if (FuncNode.Body is FuncNode) ((FuncNode) FuncNode.Body).Body.Visit(Interpreter);
            else FuncNode.Body.Visit(Interpreter);

            HassiumObject ret = Interpreter.CallStack.Peek().ReturnValue;

            Interpreter.CallStack.Pop();

            if (ret is HassiumArray) ret = ((HassiumArray) ret).Cast<object>().Select((s, i) => new {s, i}).ToDictionary(x => (object)x.i, x => (object)x.s);
            Interpreter.inFunc--;
            return ret;
        }

        public override string ToString()
        {
            return string.Format("[HassiumMethod: SelfReference={0}]", SelfReference);
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{TResult}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject}"/></returns>
        public static Func<HassiumObject> GetFuncVoid(HassiumObject internalFunction)
        {
            return () => (internalFunction).Invoke();
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see typeref="Func"/> &lt;<see cref="HassiumObject"/>, <see cref="HassiumObject"/>&gt;
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject}"/></returns>
        public static Func<HassiumObject, HassiumObject> GetFunc1(HassiumObject internalFunction)
        {
            return arg1 => (internalFunction).Invoke();
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
            return (arg1, arg2) => (internalFunction).Invoke(arg1, arg2);
        }

        /// <summary>
        /// Converts an <see cref="IFunction"/> to a <see cref="Func{HassiumObject, HassiumObject, HassiumObject, HassiumObject}"/>
        /// </summary>
        /// <param name="internalFunction">The <see cref="IFunction"/> to convert</param>
        /// <returns>The resulting <see cref="Func{HassiumObject, HassiumObject, HassiumObject, HassiumObject}"/></returns>
        public static Func<HassiumObject, HassiumObject, HassiumObject, HassiumObject> GetFunc3(HassiumObject internalFunction)
        {
            return (arg1, arg2, arg3) => (internalFunction).Invoke(arg1, arg2, arg3);
        }
    }
}

