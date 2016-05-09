using System;

namespace Hassium.Runtime.StandardLibrary.Text
{
    public class HassiumTextModule: InternalModule
    {
        public HassiumTextModule() : base("Text")
        {
            Attributes.Add("Encoding",      new HassiumEncoding());
            Attributes.Add("StringBuilder", new HassiumStringBuilder());
        }
    }
}

