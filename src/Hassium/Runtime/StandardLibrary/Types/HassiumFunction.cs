using System;

using Hassium.Runtime;

namespace Hassium.Runtime.StandardLibrary.Types
{
    public delegate HassiumObject HassiumFunctionDelegate(VirtualMachine vm, params HassiumObject[] args);
    public class HassiumFunction: HassiumObject
    {
        private HassiumFunctionDelegate target;
        public int[] ParamLengths { get; private set; }

        public HassiumFunction(HassiumFunctionDelegate target, int paramLength)
        {
            this.target = target;
            ParamLengths = new int[] { paramLength };
            AddType("func");
        }
        public HassiumFunction(HassiumFunctionDelegate target, int[] paramLengths)
        {
            this.target = target;
            ParamLengths = paramLengths;
            AddType("func");
        }

        public override HassiumObject Invoke(VirtualMachine vm, HassiumObject[] args)
        {
            if (vm != null)
                vm.CallStack.Push(string.Format("func {0} ({1})", target.Method.Name, ParamLengths[ParamLengths.Length - 1]));
            if (ParamLengths[0] != -1)
            {
                foreach (int i in ParamLengths)
                    if (i == args.Length)
                    {
                        if (vm != null)
                        vm.CallStack.Pop();
                        return target(vm, args);
                    }
                throw new InternalException(string.Format("Expected argument length of {0}, got {1}", ParamLengths[0], args.Length));
            }
            else
            {
                if (vm != null)
                vm.CallStack.Pop();
                return target(vm, args);
            }
        }

        private HassiumString __tostring__ (HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}

