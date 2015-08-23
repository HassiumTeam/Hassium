using System;
using System.Collections;
using System.Collections.Generic;

namespace Hassium
{
    public class StackFrame
    {
        public LocalScope Scope { get; private set; }

        public Dictionary<string, object> Locals { get; private set; }

        public StackFrame(LocalScope scope)
        {
            this.Scope = scope;
            this.Locals = new Dictionary<string, object>();
        }
    }
}

