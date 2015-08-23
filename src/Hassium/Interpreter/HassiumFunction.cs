using System;

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
            StackFrame stackFrame = new StackFrame(this.localScope);

            interpreter.CallStack.Push(stackFrame);
            for (int x = 0; x < this.funcNode.Parameters.Count; x++)
                stackFrame.Locals[this.funcNode.Parameters[x]] = args[x];

            interpreter.ExecuteStatement(funcNode.Body);
            interpreter.CallStack.Pop();

            return null;
        }
    }
}

