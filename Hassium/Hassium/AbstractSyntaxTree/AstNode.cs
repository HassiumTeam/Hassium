using System;
using System.Collections.Generic;

namespace Hassium
{
    public abstract class AstNode
    {
        public List<AstNode> Children {
            private set;
            get;
        }

        public AstNode ()
        {
            this.Children = new List <AstNode> ();
        }
    }
}

