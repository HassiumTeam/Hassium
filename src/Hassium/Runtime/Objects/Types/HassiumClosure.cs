using System;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumClosure: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = HassiumMethod.TypeDefinition;
        public HassiumMethod Method { get; private set; }
        public StackFrame.Frame Frame { get; private set; }

        public HassiumClosure(HassiumMethod method, StackFrame.Frame frame)
        {
            Method = method;
            Frame = frame;
            AddType(TypeDefinition);
        }

        public override HassiumObject Invoke(VirtualMachine vm, params HassiumObject[] args)
        {
            vm.StackFrame.Frames.Push(Frame);
            HassiumObject ret = Method.Invoke(vm, args);
            vm.StackFrame.PopFrame();
            return ret;
        }
    }
}

