using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumDirectoryNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("DirectoryNotFoundException");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { INVOKE, new HassiumFunction(_new, 1) },
            { "message", new HassiumProperty(get_message) },
            { "path", new HassiumProperty(get_path) },
            { TOSTRING, new HassiumFunction(tostring, 0) }
        };

        public HassiumString Path { get; set; }

        public HassiumDirectoryNotFoundException()
        {
            AddType(TypeDefinition);
        }

        [FunctionAttribute("func new (path : str) : DirectoryNotFoundException")]
        public static HassiumDirectoryNotFoundException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumDirectoryNotFoundException exception = new HassiumDirectoryNotFoundException();

            exception.Path = args[0].ToString(vm, args[0], location);

            return exception;
        }

        [FunctionAttribute("message { get; }")]
        public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(string.Format("Directory Not Found: '{0}' does not exist", (self as HassiumDirectoryNotFoundException).Path.String));
        }

        [FunctionAttribute("path { get; }")]
        public static HassiumString get_path(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return (self as HassiumDirectoryNotFoundException).Path;
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(get_message(vm, self, location).String);
            sb.Append(vm.UnwindCallStack());

            return new HassiumString(sb.ToString());
        }

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
    }
}
