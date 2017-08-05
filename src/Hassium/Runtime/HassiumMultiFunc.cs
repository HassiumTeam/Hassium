using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;

namespace Hassium.Runtime
{
    public class HassiumMultiFunc : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = HassiumMethod.TypeDefinition;

        public List<HassiumMethod> Methods { get; private set; }

        public HassiumMultiFunc()
        {
            AddType(TypeDefinition);
            Methods = new List<HassiumMethod>();
            AddAttribute(INVOKE, Invoke);
        }

        public override HassiumObject Invoke(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            List<HassiumMethod> lengthMatchingMethods = new List<HassiumMethod>();

            foreach (var method in Methods)
                if (method.Parameters.Count == args.Length)
                    lengthMatchingMethods.Add(method);

            if (lengthMatchingMethods.Count == 0)
            {
                vm.RaiseException(HassiumArgumentLengthException._new(vm, location, this, new HassiumInt(Methods[0].Parameters.Count), new HassiumInt(args.Length)));
                return Null;
            }
            else if (lengthMatchingMethods.Count == 1)
                return lengthMatchingMethods[0].Invoke(vm, location, args);
            else
            {
                foreach (var method in lengthMatchingMethods)
                {
                    bool foundMatch = true;
                    int i = 0;
                    foreach (var param in method.Parameters)
                    {
                        var arg = args[i++];
                        if (param.Key.FunctionParameterType == Compiler.Parser.FunctionParameterType.Enforced)
                        {
                            if (!vm.Is(arg, vm.ExecuteMethod(param.Key.EnforcedType)).Bool)
                            {
                                foundMatch = false;
                                break;
                            }
                        }
                    }
                    if (foundMatch)
                        return method.Invoke(vm, location, args);
                }
                vm.RaiseException(HassiumArgumentLengthException._new(vm, location, this, new HassiumInt(Methods[0].Parameters.Count), new HassiumInt(args.Length)));
                return Null;
            }
        }
    }
}
