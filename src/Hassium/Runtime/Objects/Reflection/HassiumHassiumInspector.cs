using System;
using System.Collections.Generic;

using Hassium.Compiler.CodeGen;
using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Reflection
{
    public class HassiumHassiumInspector: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("HassiumInspector");

        public HassiumObject HassiumObject { get; set; }

        public HassiumHassiumInspector()
        {
            AddType(TypeDefinition);
            AddAttribute("compileModuleFromSource", compileModuleFromSource,    1);
            AddAttribute("getCurrentModule",        getCurrentModule,           0);
            AddAttribute(HassiumObject.INVOKE,      _new,                    0, 1);
        }

        public HassiumModule compileModuleFromSource(VirtualMachine vm, params HassiumObject[] args)
        {
            return Compiler.CodeGen.Compiler.CompileModuleFromSource(args[0].ToString(vm).String);
        }
        public HassiumModule getCurrentModule(VirtualMachine vm, params HassiumObject[] args)
        {
            return vm.CurrentModule;
        }
        public HassiumHassiumInspector _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumHassiumInspector hassiumInspector = new HassiumHassiumInspector();
            hassiumInspector.HassiumObject = args[0];
            hassiumInspector.AddAttribute("getObjectsByType",   hassiumInspector.getObjectsByType,   1);
            hassiumInspector.AddAttribute("getImports",         hassiumInspector.getImports,         0);
            hassiumInspector.AddAttribute("getParent",          hassiumInspector.getParent,          0);

            return hassiumInspector;
        }

        public HassiumList getObjectsByType(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (HassiumObject obj in HassiumObject.Attributes.Values)
                if (obj.Types.Contains(args[0].Type()))
                    result.add(vm, obj);
            return result;
        }
        public HassiumList getImports(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            if (HassiumObject is HassiumModule)
            {
                foreach (string import in ((HassiumModule)HassiumObject).Imports)
                    result.add(vm, new HassiumString(import));
            }
            return result;
        }
        public HassiumObject getParent(VirtualMachine vm, params HassiumObject[] args)
        {
            return HassiumObject.Parent;
        }
    }
}
