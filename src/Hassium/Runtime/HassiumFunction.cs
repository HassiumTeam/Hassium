using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;

namespace Hassium.Runtime
{
    public delegate HassiumObject HassiumFunctionDelegate(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args);

    public class HassiumFunction : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = HassiumMethod.TypeDefinition;
        public HassiumFunctionDelegate Target { get; private set; }
        public int[] ParameterLengths { get; private set; }

        public HassiumFunction(HassiumFunctionDelegate target, int paramLength)
        {
            AddType(TypeDefinition);
            Target = target;
            ParameterLengths = new int[] { paramLength };
        }
        public HassiumFunction(HassiumFunctionDelegate target, params int[] paramLengths)
        {
            AddType(TypeDefinition);
            Target = target;
            ParameterLengths = paramLengths;
        }

        public override HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var a = Target.Method.GetCustomAttributes(typeof(FunctionAttribute), false);

            if (ParameterLengths.Length == 0)
                ParameterLengths = new int[] { -1 };

            if (a.Length > 0)
            {
                var reps = (a[0] as FunctionAttribute).SourceRepresentations;
                if (reps.Count > 1 && ParameterLengths[0] != -1)
                    vm.PushCallStack(string.Format("{0}\t[{1}]", reps[new List<int>(ParameterLengths).IndexOf(args.Length)], location));
                else if (reps.Count == 0)
                    vm.PushCallStack(string.Format("{0}\t[{1}]", reps[0]));
            }

            if (ParameterLengths[0] != -1)
            {
                foreach (int len in ParameterLengths)
                    if (len == args.Length)
                        return Target(vm, Parent, location, args);
                vm.RaiseException(HassiumArgLengthException.ArgLengthExceptionTypeDef._new(vm, null, location, this, new HassiumInt(ParameterLengths[0]), new HassiumInt(args.Length)));
                return Null;
            }

            return Target(vm, Parent, location, args);
        }

        public string GetTopSourceRep()
        {

            var a = Target.Method.GetCustomAttributes(typeof(FunctionAttribute), false);

            if (a.Length > 0)
                return (a[0] as FunctionAttribute).SourceRepresentation;
            return string.Empty;
        }
    }
}
