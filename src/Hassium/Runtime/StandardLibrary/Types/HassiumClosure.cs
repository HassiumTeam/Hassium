using System;

using Hassium.CodeGen;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public class HassiumClosure: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("lambda");
        public MethodBuilder Method { get; private set; }
        public StackFrame.Frame Frame { get; private set; }
        public HassiumClosure(MethodBuilder method, StackFrame.Frame frame)
        {
            Method = method;
            Frame = frame;
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(__invoke__, -1));
            AddType(HassiumClosure.TypeDefinition);
        }

        public HassiumObject __invoke__ (VirtualMachine vm, HassiumObject[] args)
        {
            vm.StackFrame.Frames.Push(Frame);
            HassiumObject ret = Method.Invoke(vm, args);
            vm.StackFrame.PopFrame();

            return ret;
        }
    }
}

