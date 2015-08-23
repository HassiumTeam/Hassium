using System;
using System.Collections.Generic;

namespace Hassium
{
    public class LocalScope
    {
        public List<string> Symbols { get; private set; }

        public LocalScope()
        {
            Symbols = new List<string>();
        }
    }
}

