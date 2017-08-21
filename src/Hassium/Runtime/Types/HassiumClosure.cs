using Hassium.Compiler;

using System.Collections.Generic;

namespace Hassium.Runtime.Types
{
    public class HassiumClosure : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = HassiumMethod.TypeDefinition;

        public HassiumMethod Method { get; private set; }
        public Dictionary<int, HassiumObject> Frame { get; private set; }

        public HassiumClosure(HassiumMethod method, Dictionary<int, HassiumObject> frame)
        {
            AddType(TypeDefinition);

            Method = method;
            Frame = frame;
        }

        public override HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            vm.StackFrame.Frames.Push(Frame);
            var ret = Method.Invoke(vm, location, args);
            vm.StackFrame.Frames.Pop();

            return ret;
        }
    }
}