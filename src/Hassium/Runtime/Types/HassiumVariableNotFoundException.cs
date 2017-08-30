using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime
{
    public class HassiumVariableNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new VariableNotFoundExceptionTypeDef();

        public HassiumVariableNotFoundException()
        {
            AddType(TypeDefinition);
        }

        public class VariableNotFoundExceptionTypeDef : HassiumTypeDefinition
        {
            public VariableNotFoundExceptionTypeDef() : base("VariableNotFoundException")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { INVOKE, new HassiumFunction(_new, 0) },
                    { "message", new HassiumProperty(get_message) },
                    { TOSTRING, new HassiumFunction(tostring, 0) }
                };
            }

            [DocStr(
                "@desc Constructs a new VariableNotFoundException.",
                "@returns The new VariableNotFoundException object."
                )]
            [FunctionAttribute("func new () : VariableNotFoundException")]
            public static HassiumVariableNotFoundException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumVariableNotFoundException exception = new HassiumVariableNotFoundException();


                return exception;
            }

            [DocStr(
                "@desc Gets the readonly string message of the exception.",
                "@returns The exception message string."
            )]
            [FunctionAttribute("message { get; }")]
            public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(string.Format("Variable Not Found: variable was not found inside the stack frmae"));
            }

            [DocStr(
                "@desc Returns the string value of the exception, including the message and callstack.",
                "@returns The string value of the exception."
            )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(get_message(vm, self, location).String);
                sb.Append(vm.UnwindCallStack());

                return new HassiumString(sb.ToString());
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
