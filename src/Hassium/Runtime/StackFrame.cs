using System;
using System.Collections.Generic;

using Hassium.Compiler;

namespace Hassium.Runtime
{
    public class StackFrame
    {
        public class Frame : ICloneable
        {
            public Dictionary<int, HassiumObject> variables = new Dictionary<int, HassiumObject>();
            public void Add(int index, HassiumObject value)
            {
                variables.Add(index, value);
            }
            public bool ContainsVariable(int index)
            {
                return variables.ContainsKey(index);
            }
            public void Modify(int index, HassiumObject value)
            {
                variables[index] = value;
            }
            public HassiumObject GetVariable(int index)
            {
                return variables[index];
            }
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        public Stack<Frame> Frames;
        public Dictionary<int, HassiumObject> Locals { get { return Frames.Peek().variables; } }
        public StackFrame()
        {
            Frames = new Stack<Frame>();
        }

        public void PushFrame()
        {
            Frames.Push(new Frame());
        }
        public Frame PopFrame()
        {
            return Frames.Pop();
        }
        public void Add(int index, HassiumObject value = null)
        {
            if (Frames.Peek().ContainsVariable(index))
                Frames.Peek().variables.Remove(index);
            Frames.Peek().Add(index, value);
        }
        public bool Contains(int index)
        {
            foreach (Frame frame in Frames)
                if (frame.ContainsVariable(index))
                    return true;
            return false;
        }
        public void Modify(int index, HassiumObject value)
        {
            Frames.Peek().Modify(index, value);
        }
        public HassiumObject GetVariable(SourceLocation location, VirtualMachine vm, int index)
        {
            if (Frames.Peek().ContainsVariable(index))
                return Frames.Peek().GetVariable(index);
            vm.RaiseException(HassiumVariableNotFoundException.Attribs[HassiumObject.INVOKE].Invoke(vm, location));
            return HassiumObject.Null;
        }
    }
}
