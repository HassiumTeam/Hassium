using System;
using System.Text;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Text
{
    public class HassiumStringBuilder: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("StringBuilder");

        public StringBuilder StringBuilder { get; set; }

        public HassiumStringBuilder()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 0, 1);
        }

        public HassiumStringBuilder _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumStringBuilder stringBuilder = new HassiumStringBuilder();
            stringBuilder.StringBuilder = args.Length == 0 ? new StringBuilder() : new StringBuilder(args[0].ToString(vm).String);
            stringBuilder.AddAttribute("append",        stringBuilder.append,           1);
            stringBuilder.AddAttribute("appendFormat",  stringBuilder.appendFormat,    -1);
            stringBuilder.AddAttribute("appendLine",    stringBuilder.appendLine,    0, 1);
            stringBuilder.AddAttribute("clear",         stringBuilder.clear,            0);
            stringBuilder.AddAttribute("insert",        stringBuilder.insert,           2);
            stringBuilder.AddAttribute("length",        new HassiumProperty(stringBuilder.get_length));
            stringBuilder.AddAttribute("replace",       stringBuilder.replace,          2);
            stringBuilder.AddAttribute(HassiumObject.TOSTRING, stringBuilder.ToString,  0);

            return stringBuilder;
        }

        public HassiumStringBuilder append(VirtualMachine vm, params HassiumObject[] args)
        {
            StringBuilder.Append(args[0].ToString(vm).String);
            return this;
        }
        public HassiumStringBuilder appendFormat(VirtualMachine vm, params HassiumObject[] args)
        {
            string[] strings = new string[args.Length - 1];
            for (int i = 1; i < args.Length; i++)
                strings[i] = args[i].ToString(vm).String;
            StringBuilder.AppendFormat(args[0].ToString(vm).String, strings);
            return this;
        }
        public HassiumStringBuilder appendLine(VirtualMachine vm, params HassiumObject[] args)
        {
            StringBuilder.AppendLine(args.Length == 0 ? string.Empty : args[0].ToString(vm).String);
            return this;
        }
        public HassiumStringBuilder clear(VirtualMachine vm, params HassiumObject[] args)
        {
            StringBuilder.Clear();
            return this;
        }
        public HassiumStringBuilder insert(VirtualMachine vm, params HassiumObject[] args)
        {
            StringBuilder.Insert((int)args[0].ToInt(vm).Int, args[1].ToString(vm).String);
            return this;
        }
        public HassiumInt get_length(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(StringBuilder.Length);
        }
        public HassiumStringBuilder replace(VirtualMachine vm, params HassiumObject[] args)
        {
            StringBuilder.Replace(args[0].ToString(vm).String, args[1].ToString(vm).String);
            return this;
        }       
        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(StringBuilder.ToString());
        }
    }
}

