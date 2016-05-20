using System;
using System.Collections.Generic;

using Hassium.CodeGen;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary
{
    public class HassiumMultiFunc: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("MultiFunc");
        public List<MethodBuilder> Lambdas { get; private set; }

        public HassiumMultiFunc(List<MethodBuilder> methods)
        {
            Lambdas = methods;
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(__invoke__, -1));
        }

        private HassiumObject __invoke__ (VirtualMachine vm, HassiumObject[] args)
        {
            foreach (MethodBuilder method in Lambdas)
            {
                if (method.Parameters.Count == args.Length)
                    return method.Invoke(vm, args);
            }
            throw new InternalException("Unknown parameter length " + args.Length);
        }
    }
}

