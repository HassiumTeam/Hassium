using System;
using System.Collections.Generic;

using Hassium.Compiler;

namespace Hassium.Runtime
{
    public class StackFrame
    {
        public Stack<Dictionary<int, HassiumObject>> Frames;
        public Dictionary<int, HassiumObject> Locals { get { return Frames.Peek(); } }
        public StackFrame()
        {
            Frames = new Stack<Dictionary<int, HassiumObject>>();
        }

        public void PushFrame()
        {
            Frames.Push(new Dictionary<int, HassiumObject>());
        }
        public Dictionary<int, HassiumObject> PopFrame()
        {
            return Frames.Pop();
        }
        public void Add(int index, HassiumObject value = null)
        {
            if (Frames.Peek().ContainsKey(index))
                Frames.Peek().Remove(index);
            Frames.Peek().Add(index, value);
        }
        public bool Contains(int index)
        {
            foreach (var frame in Frames)
                if (frame.ContainsKey(index))
                    return true;
            return false;
        }
        public void Modify(int index, HassiumObject value)
        {
            Frames.Peek()[index] = value;
        }
        public HassiumObject GetVariable(SourceLocation location, VirtualMachine vm, int index)
        {
            if (Frames.Peek().ContainsKey(index))
                return Frames.Peek()[index];
            vm.RaiseException(HassiumVariableNotFoundException.VariableNotFoundExceptionTypeDef._new(vm, null, location));
            return HassiumObject.Null;
        }
    }
}
