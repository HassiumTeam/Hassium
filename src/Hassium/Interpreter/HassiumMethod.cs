using System.Linq;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.Interpreter
{
    public class HassiumMethod : HassiumObject
    {
        public HassiumObject SelfReference { private set; get;}
        private HassiumFunction function;

        public string Name { get { return function.FuncNode.Name; } }

        public bool IsStatic { get { return !function.FuncNode.Parameters.Contains("this"); } }

        public bool IsConstructor { get { return Name == "new"; } }

        public HassiumMethod(HassiumFunction function, HassiumObject self)
        {
            SelfReference = self;
            this.function = function;
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            StackFrame stackFrame = function.stackFrame;
            if (stackFrame == null || (stackFrame.Locals.Count == 0))
                stackFrame = new StackFrame(function.LocalScope, (IsStatic && !IsConstructor) ? null : SelfReference);
            else if (!IsStatic || IsConstructor)
                stackFrame.Locals["this"] = SelfReference;

            function.Interpreter.inFunc++;
            var parms = function.FuncNode.Parameters;
            if (parms.Contains("this")) parms.Remove("this");
            for (int x = 0; x < parms.Count; x++)
                stackFrame.Locals[parms[x]] = args[x];

            function.Interpreter.CallStack.Push(stackFrame);

            function.FuncNode.Body.Visit(function.Interpreter);

            HassiumObject ret = function.Interpreter.CallStack.Peek().ReturnValue;

            function.Interpreter.CallStack.Pop();

            if (ret is HassiumArray) ret = ((HassiumArray) ret).Cast<object>().Select((s, i) => new {s, i}).ToDictionary(x => (object)x.i, x => (object)x.s);
            function.Interpreter.inFunc--;
            return ret;
        }

        public override string ToString()
        {
            return string.Format("[HassiumMethod: SelfReference={0}]", SelfReference);
        }
    }
}

