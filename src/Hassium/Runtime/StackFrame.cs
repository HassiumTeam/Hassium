using System;
using System.Collections.Generic;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime
{
    public class StackFrame
    {
        public class Frame
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
        }
        public Stack<Frame> Frames;
        public Dictionary<int, HassiumObject> Locals { get { return Frames.Peek().variables; } }
        public StackFrame()
        {
            Frames = new Stack<Frame>();
        }

        public void EnterFrame()
        {
            Frames.Push(new Frame());
        }
        public void PopFrame()
        {
            Frames.Pop();
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
        public HassiumObject GetVariable(int index)
        {
            if (Frames.Peek().ContainsVariable(index))
                return Frames.Peek().GetVariable(index);
            throw new InternalException("Variable was not found inside the stack frame! Index " + index);
        }
    }
}