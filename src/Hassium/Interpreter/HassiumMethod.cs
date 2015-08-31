using System;
using System.Linq;
using Hassium.HassiumObjects;

namespace Hassium
{
    public class HassiumMethod : HassiumObject
    {
        public HassiumObject SelfReference { private set; get;}
        private HassiumFunction function;

        public HassiumMethod(HassiumFunction function, HassiumObject self)
        {
            this.SelfReference = self;
            this.function = function;
        }

        public override HassiumObject Invoke(HassiumObject[] args)
        {
            StackFrame stackFrame = function.stackFrame;
            if (stackFrame == null || (stackFrame.Locals.Count == 0))
                stackFrame = new StackFrame(function.LocalScope, SelfReference);
            else
                stackFrame.Locals["this"] = SelfReference;
            
            function.Interpreter.CallStack.Push(stackFrame);
            for (int x = 0; x < function.FuncNode.Parameters.Count; x++)
                stackFrame.Locals[function.FuncNode.Parameters[x]] = args[x];

            function.Interpreter.ExecuteStatement(function.FuncNode.Body);

            HassiumObject ret = function.Interpreter.CallStack.Peek().ReturnValue;

            function.Interpreter.CallStack.Pop();

            if (ret is HassiumArray) ret = ((HassiumArray) ret).Cast<object>().Select((s, i) => new {s, i}).ToDictionary(x => (object)x.i, x => (object)x.s);

            return ret;
        }

        public override string ToString()
        {
            return string.Format("[HassiumMethod: SelfReference={0}]", SelfReference);
        }
    }
}

