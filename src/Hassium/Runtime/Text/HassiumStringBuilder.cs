using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime.Text
{
    public class HassiumStringBuilder : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("StringBuilder");

        public StringBuilder StringBuilder { get; private set; }

        public HassiumStringBuilder()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 0, 1);
        }

        [FunctionAttribute("func new () : StringBuilder", "func new (obj : object) : StringBuilder")]
        public static HassiumStringBuilder _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumStringBuilder sb = new HassiumStringBuilder();

            sb.StringBuilder = args.Length == 0 ? new StringBuilder() : new StringBuilder(args[0].ToString(vm, location).String);
            sb.AddAttribute("append", sb.append, 1);
            sb.AddAttribute("appendf", sb.appendf, -1);
            sb.AddAttribute("appendline", sb.appendline, 1);
            sb.AddAttribute("clear", sb.clear, 0);
            sb.AddAttribute("insert", sb.insert, 2);
            sb.AddAttribute("length", new HassiumProperty(sb.get_length));
            sb.AddAttribute("replace", sb.replace, 2);
            sb.AddAttribute(TOSTRING, sb.ToString, 0);
            return sb;
        }

        [FunctionAttribute("func append (obj : object) : StringBuilder")]
        public HassiumStringBuilder append(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder.Append(args[0].ToString(vm, location).String);

            return this;
        }

        [FunctionAttribute("func appendf (fmt : string, params obj) : StringBuilder")]
        public HassiumStringBuilder appendf(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder.Append(GlobalFunctions.format(vm, location, args).ToString(vm, location).String);

            return this;
        }

        [FunctionAttribute("func appendline (obj : object) : StringBuilder")]
        public HassiumStringBuilder appendline(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder.AppendLine(args[0].ToString(vm, location).String);

            return this;
        }

        [FunctionAttribute("func clear () : StringBuilder")]
        public HassiumStringBuilder clear(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder.Clear();

            return this;
        }

        [FunctionAttribute("func insert (index : int, obj : object) : StringBuilder")]
        public HassiumStringBuilder insert(VirtualMachine vm, SourceLocation location, params HassiumObject[] argS)
        {
            StringBuilder.Insert((int)argS[0].ToInt(vm, location).Int, argS[1].ToString(vm, location).String);

            return this;
        }

        [FunctionAttribute("length { get; }")]
        public HassiumInt get_length(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(StringBuilder.Length);
        }

        [FunctionAttribute("func replace (obj1 : object, obj2 : object) : StringBuilder")]
        public HassiumStringBuilder replace(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder.Replace(args[0].ToString(vm, location).String, args[1].ToString(vm, location).String);

            return this;
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(StringBuilder.ToString());
        }
    }
}
