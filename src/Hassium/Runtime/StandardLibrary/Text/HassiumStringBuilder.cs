using System;
using System.Text;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Text
{
    public class HassiumStringBuilder: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("StringBuilder");

        public StringBuilder StringBuilder { get; set; } 
        public HassiumStringBuilder()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, new int[] { 0, 1 }));
            AddType(HassiumStringBuilder.TypeDefinition);
        }

        private HassiumStringBuilder _new (VirtualMachine vm, HassiumObject[] args)
        {
            HassiumStringBuilder hassiumStringBuilder = new HassiumStringBuilder();
            if (args.Length == 0)
                hassiumStringBuilder.StringBuilder = new StringBuilder();
            else
                hassiumStringBuilder.StringBuilder = new StringBuilder(HassiumString.Create(args[0]).Value);
            hassiumStringBuilder.Attributes.Add("append",       new HassiumFunction(hassiumStringBuilder.append, 1));
            hassiumStringBuilder.Attributes.Add("appendLine",   new HassiumFunction(hassiumStringBuilder.appendLine, 1));
            hassiumStringBuilder.Attributes.Add("insert",       new HassiumFunction(hassiumStringBuilder.insert, 2));
            hassiumStringBuilder.Attributes.Add(HassiumObject.TOSTRING_FUNCTION,    new HassiumFunction(hassiumStringBuilder.toString, 0));
            hassiumStringBuilder.Attributes.Add(HassiumObject.ADD_FUNCTION,         new HassiumFunction(hassiumStringBuilder.__add__, 1));
            hassiumStringBuilder.Attributes.Add(HassiumObject.INDEX_FUNCTION,       new HassiumFunction(hassiumStringBuilder.__index__, 1));
            hassiumStringBuilder.Attributes.Add(HassiumObject.STORE_INDEX_FUNCTION, new HassiumFunction(hassiumStringBuilder.__storeindex__, 2));

            return hassiumStringBuilder;
        }

        public HassiumStringBuilder append(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder.Append(args[0].ToString(vm));
            return this;
        }
        public HassiumStringBuilder appendLine(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder.AppendLine(args[0].ToString(vm));
            return this;
        }
        public HassiumStringBuilder insert(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder.Insert((int)HassiumInt.Create(args[0]).Value, args[1].ToString(vm));
            return this;
        }
        public HassiumString toString(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(StringBuilder.ToString());
        }

        public HassiumStringBuilder __add__ (VirtualMachine vm, HassiumObject[] args)
        {
            return append(vm, args);
        }
        public HassiumChar __index__ (VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumChar(StringBuilder[(int)HassiumInt.Create(args[0]).Value]);
        }
        public HassiumStringBuilder __storeindex__ (VirtualMachine vm, HassiumObject[] args)
        {
            return insert(vm, args);
        }
    }
}