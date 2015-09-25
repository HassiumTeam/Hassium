using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.HassiumObjects;

namespace Hassium.Interpreter
{
    public class CallStack
    {
        private Stack<StackFrame> frames = new Stack<StackFrame>();
        
        public CallStack()
        {

        } 

        public StackFrame Peek()
        {
            return frames.Peek();
        }

        public HassiumObject GetVariable(string name)
        {
            return frames.First(x => x.Locals.ContainsKey(name)).Locals[name];
        }

        public void SetVariable(string name, HassiumObject value)
        {
            if (frames.Any(x => x.Locals.ContainsKey(name)))
                frames.First(x => x.Locals.ContainsKey(name)).Locals[name] = value;
            else Peek().Locals[name] = value;
        }

        public void FreeVariable(string name)
        {
            
        }

        public bool HasVariable(string name)
        {
            return Peek().Scope.Symbols.Contains(name) || frames.Any(x => x.Locals.ContainsKey(name));
        }
    }
}
