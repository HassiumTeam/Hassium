using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Runtime.Types
{
    public class HassiumNull : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("null");

        public HassiumNull()
        {
            AddType(TypeDefinition);
        }
    }
}
