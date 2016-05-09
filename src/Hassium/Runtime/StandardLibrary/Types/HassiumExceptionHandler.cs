using System;

using Hassium.CodeGen;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumExceptionHandler: HassiumObject
    {
        public MethodBuilder SourceMethod { get; private set; }
        public MethodBuilder HandlerMethod { get; private set; }
        public double Label { get; private set; }

        public HassiumExceptionHandler(MethodBuilder sourceMethod, MethodBuilder handlerMethod, double label)
        {
            SourceMethod = sourceMethod;
            HandlerMethod = handlerMethod;
            Label = label;
        }
    }
}

