using System;
using System.Collections.Generic;

namespace Hassium
{
    public class SymbolTable
    {
        public List<string> Symbols { get; private set; }

        public Dictionary<string, LocalScope> ChildScopes { get; private set; }

        public SymbolTable()
        {
            Symbols = new List<string>();
            ChildScopes = new Dictionary<string, LocalScope>();
        }
    }
}

