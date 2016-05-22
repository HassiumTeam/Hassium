using System;
using System.Collections.Generic;

using Hassium.CodeGen;
using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Reflection
{
    public class HassiumAssembly: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Assembly");
        private HassiumAssembly create(HassiumObject module)
        {
            HassiumAssembly hassiumAssembly = new HassiumAssembly();

            hassiumAssembly.Module = module;
            hassiumAssembly.Attributes.Add("disassemble",   new HassiumFunction(hassiumAssembly.disassemble, 1));
            hassiumAssembly.Attributes.Add("getAttributes", new HassiumFunction(hassiumAssembly.getAttributes, 0));
            hassiumAssembly.Attributes.Add("getClasses",    new HassiumFunction(hassiumAssembly.getClasses, 0));
            hassiumAssembly.Attributes.Add("getFunctions",  new HassiumFunction(hassiumAssembly.getFunctions, 0));

            return hassiumAssembly;
        }

        public HassiumObject Module { get; set; }
        public HassiumAssembly()
        {
            Attributes.Add("fromFile",        new HassiumFunction(fromFile, 1));
            Attributes.Add("fromObject",      new HassiumFunction(fromObject, 1));
            Attributes.Add("getCurrentAssembly",    new HassiumFunction(getCurrentAssembly, 0));
            AddType(HassiumAssembly.TypeDefinition);
        }
        
        public HassiumList disassemble(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            List<Instruction> instructions = ((MethodBuilder)Module.Attributes[HassiumString.Create(args[0]).Value]).Instructions;
            foreach (Instruction instruction in instructions)
                list.Value.Add(new HassiumKeyValuePair(new HassiumString(instruction.InstructionType.ToString()), new HassiumDouble(instruction.Argument)));

            return list;
        }
        public HassiumList getAttributes(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (HassiumObject obj in Module.Attributes.Values)
                list.Value.Add(obj);
            return list;
        }             
        private HassiumAssembly fromObject(VirtualMachine vm, HassiumObject[] args)
        {
            return create(args[0]);
        }                                         
        private HassiumAssembly fromFile(VirtualMachine vm, HassiumObject[] args)
        {
            string path = HassiumString.Create(args[0]).Value;
            HassiumObject module;
            if (path.EndsWith(".dll"))
                module = HassiumCompiler.LoadModulesFromDLL(path)[0];
            else
                module = HassiumExecuter.FromFilePath(path, null, false);

            return create(module);
        }
        public HassiumList getClasses(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            foreach (HassiumObject obj in Module.Attributes.Values)
                if (obj is HassiumClass)
                    list.Value.Add(obj);
            return list;
        }
        private HassiumAssembly getCurrentAssembly(VirtualMachine vm, HassiumObject[] args)
        {
            return create(vm.CurrentlyExecutingModule);
        }
        public HassiumList getFunctions(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            foreach (HassiumObject obj in Module.Attributes.Values)
                if (obj is HassiumFunction)
                    list.Value.Add(obj);
            return list;
        }
    }
}