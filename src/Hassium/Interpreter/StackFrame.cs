using System.Collections.Generic;
using Hassium.HassiumObjects;
using Hassium.Semantics;

namespace Hassium.Interpreter
{
    public class StackFrame
    {
        public HassiumObject Self { get; private set; }

        public LocalScope Scope { get; private set; }

        public Dictionary<string, HassiumObject> Locals { get; private set; }

        public HassiumObject ReturnValue { get; set; }

        public StackFrame(LocalScope scope, HassiumObject self = null)
        {
            Scope = scope;
            Locals = new Dictionary<string, HassiumObject>();
            if (self != null)
                Locals["this"] = self;
        }
    }
}

