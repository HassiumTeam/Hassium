using System;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Text
{
    public class HassiumTextModule: InternalModule
    {
        public HassiumTextModule() : base("Text")
        {
            AddAttribute("Encoding",        new HassiumEncoding());
            AddAttribute("StringBuilder",   new HassiumStringBuilder());
        }
    }
}

