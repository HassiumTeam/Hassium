using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Emit
{
    public class Scope
    {
        public Dictionary<string, int> Symbols { get; private set; }

        public Scope()
        {
            Symbols = new Dictionary<string, int>();
        }
    }
}
