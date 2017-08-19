using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime
{
    public class HassiumAttribNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("AttributeNotFoundException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "attribute", new HassiumProperty(get_attribute) },
            { INVOKE, new HassiumFunction(_new, 2) },
            { "message", new HassiumProperty(get_message) },
            { "object", new HassiumProperty(get_object) },
            { TOSTRING, new HassiumFunction(tostring, 0) },
        };

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || Attribs.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (Attribs[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in Attribs)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }


        public HassiumObject Object { get; private set; }
        public HassiumString Attribute { get; private set; }

        public HassiumAttribNotFoundException()
        {
            AddType(TypeDefinition);
            
        }

        [FunctionAttribute("func new (obj : object, attrib : string) : AttributeNotFoundException")]
        public static HassiumAttribNotFoundException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumAttribNotFoundException exception = new HassiumAttribNotFoundException();

            exception.Object = args[0];
            exception.Attribute = args[1].ToString(vm, args[1], location);

            return exception;
        }

        [FunctionAttribute("attribute { get; }")]
        public static HassiumObject get_attribute(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumAttribNotFoundException).Attribute;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var exception = (self as HassiumAttribNotFoundException);
            return new HassiumString(string.Format("Attribute Not Found: Could not find attribute '{0}' in object of type '{1}'", exception.Attribute.String, exception.Object.Type()));
        }

        [FunctionAttribute("object { get; }")]
        public static HassiumObject get_object(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumAttribNotFoundException).Object;
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
}
