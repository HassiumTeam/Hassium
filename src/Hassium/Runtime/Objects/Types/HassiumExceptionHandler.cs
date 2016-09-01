using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumExceptionHandler: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("catch");

        public HassiumMethod Caller { get; private set; }
        public HassiumMethod Handler { get; private set; }
        public int Label { get; private set; }
        public StackFrame.Frame Frame { get; set; }

        public HassiumExceptionHandler(HassiumMethod caller, HassiumMethod handler, int label)
        {
            Caller = caller;
            Handler = handler;
            Label = label;
            AddType(TypeDefinition);
        }

        public override HassiumObject Invoke(VirtualMachine vm, params HassiumObject[] args)
        {
            vm.StackFrame.Frames.Push(Frame);
            var ret = Handler.Invoke(vm, args);
            vm.StackFrame.PopFrame();
            return ret;
        }
    }
}

