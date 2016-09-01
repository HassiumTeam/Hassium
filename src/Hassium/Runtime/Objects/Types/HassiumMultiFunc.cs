using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumMultiFunc: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("multiFunc");

        public List<HassiumMethod> Methods { get; private set; }

        public HassiumMultiFunc(List<HassiumMethod> methods)
        {
            Methods = methods;
            AddType(TypeDefinition);
        }

        public override HassiumObject Invoke(VirtualMachine vm, params HassiumObject[] args)
        {
            foreach (var method in Methods)
            {
                if (method.Parameters.Count == args.Length)
                {
                    int i = 0;
                    foreach (var param in method.Parameters)
                    {
                        var arg = args[i++];
                        if (param.Key.IsEnforced)
                        if (!arg.Types.Contains((HassiumTypeDefinition)vm.Globals[param.Key.Type]))
                            goto nextMethod;
                    }
                    return method.Invoke(vm, args);
                }
                nextMethod:;
            }
            throw new InternalException(vm, "No suitable methods found with length {0}", args.Length);
        }
    }
}

