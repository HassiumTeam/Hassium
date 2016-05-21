using System;

using Hassium.CodeGen;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary
{
    public class HassiumExtend: HassiumObject
    {
        public MethodBuilder Target { get; private set; }
        public HassiumClass Additions { get; private set; }

        public HassiumExtend(MethodBuilder target, HassiumClass additions)
        {
            Target = target;
            Additions = additions;
        }
    }
}