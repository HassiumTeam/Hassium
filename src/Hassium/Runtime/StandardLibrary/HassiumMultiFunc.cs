using System;
using System.Collections.Generic;

using Hassium.CodeGen;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary
{
    public class HassiumMultiFunc: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("multifunc");
        public List<MethodBuilder> Lambdas { get; private set; }

        public HassiumMultiFunc(List<MethodBuilder> methods)
        {
            Lambdas = methods;
            Attributes.Add("add", new HassiumFunction(add, -1));
            Attributes.Add(HassiumObject.ADD_FUNCTION,      new HassiumFunction(__add__, 1));
            Attributes.Add(HassiumObject.INVOKE_FUNCTION,   new HassiumFunction(__invoke__, -1));
        }

        private HassiumNull add(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args)
                Lambdas.Add((MethodBuilder)obj);
            return HassiumObject.Null;
        }

        private HassiumMultiFunc __add__ (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumMultiFunc multiFunc = this.Clone() as HassiumMultiFunc;
            multiFunc.Lambdas.Add((MethodBuilder)args[0]);
            return multiFunc;
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