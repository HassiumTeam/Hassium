using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime
{
    public class HassiumConversionFailedException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new ConversionFailedExceptionTypeDef();

        public HassiumObject DesiredType { get; set; }
        public HassiumObject Object { get; set; }

        public HassiumConversionFailedException()
        {
            AddType(TypeDefinition);
        }

        public class ConversionFailedExceptionTypeDef : HassiumTypeDefinition
        {
            public ConversionFailedExceptionTypeDef() : base("ConversionFailedException")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "desired", new HassiumProperty(get_desired) },
                    { INVOKE, new HassiumFunction(_new, 2) },
                    { "message", new HassiumProperty(get_message) },
                    { TOSTRING, new HassiumFunction(tostring, 0) }
                };
            }

            [FunctionAttribute("func new (obj : object, type : object) : ConversionFailedException")]
            public static HassiumConversionFailedException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumConversionFailedException exception = new HassiumConversionFailedException();

                exception.Object = args[0];
                if (args[1] is HassiumTypeDefinition)
                    exception.DesiredType = args[1] as HassiumTypeDefinition;
                else if (args[1] is HassiumTrait)
                    exception.DesiredType = args[1] as HassiumTrait;
                else
                    exception.Object = args[1];

                return exception;
            }

            [FunctionAttribute("desired { get; }")]
            public static HassiumObject get_desired(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumConversionFailedException).DesiredType;
            }

            [FunctionAttribute("message { get; }")]
            public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var exception = (self as HassiumConversionFailedException);
                return new HassiumString(string.Format("Conversion Failed: Could not convert object of type '{0}' to type '{1}'", exception.Object.Type(), exception.DesiredType));
            }

            [FunctionAttribute("object { get; }")]
            public static HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumConversionFailedException).Object;
            }

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
