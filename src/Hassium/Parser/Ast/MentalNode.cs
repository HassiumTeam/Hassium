using System;

namespace Hassium
{
    public class MentalNode: AstNode
    {
        public string OpType { get; private set; }

        public string Name { get; private set; }

        public MentalNode(string type, string name)
        {
            this.OpType = type;
            this.Name = name;
        }
    }
}

