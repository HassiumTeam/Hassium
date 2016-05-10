using System;

using Hassium.CodeGen;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumExceptionHandler: HassiumObject
    {
        public MethodBuilder SourceMethod { get; private set; }
        public MethodBuilder HandlerMethod { get; private set; }
        public double Label { get; private set; }
        public StackFrame.Frame Frame { get; set; }

        public HassiumExceptionHandler(MethodBuilder sourceMethod, MethodBuilder handlerMethod, double label)
        {
            SourceMethod = sourceMethod;
            HandlerMethod = handlerMethod;
            Label = label;
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(__invoke__, -1));
        }

        private HassiumObject __invoke__ (VirtualMachine vm, HassiumObject[] args)
        {
            vm.StackFrame.Frames.Push(Frame);
            var ret = HandlerMethod.Invoke(vm, args);
            vm.StackFrame.PopFrame();
            return ret;
        }
    }
}

