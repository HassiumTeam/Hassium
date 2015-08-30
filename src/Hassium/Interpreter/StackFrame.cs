using System;
using System.Collections;
using System.Collections.Generic;

namespace Hassium
{
    public class StackFrame
    {
        public LocalScope Scope { get; private set; }

        public Dictionary<string, HassiumObject> Locals { get; private set; }

        public HassiumObject ReturnValue { get; set; }

        public StackFrame(LocalScope scope)
        {
            Scope = scope;
            Locals = new Dictionary<string, HassiumObject>();
        }
    }
}

