using System;

namespace Hassium
{
    public class Token
    {
        public string Operator { get; private set; }
        public string Value { get; private set; }

        public Token(string operate, string value)
        {
            this.Operator = operate;
            this.Value = value;
        }
    }
}

