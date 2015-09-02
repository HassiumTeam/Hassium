using System.Collections.Generic;

namespace Hassium.Semantics
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

