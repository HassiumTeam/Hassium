using Hassium.Compiler;
using Hassium.Runtime;
using Hassium.Runtime.Types;

using System.IO;

namespace Hassium.Runtime.IO
{
    public class HassiumPath : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Path");

        public HassiumPath()
        {
            AddType(TypeDefinition);

            AddAttribute("combine", combine, -1);
            AddAttribute("parsedir", parsedir, 1);
            AddAttribute("parseext", parseext, 1);
            AddAttribute("parsename", parsename, 1);
            AddAttribute("parseroot", parseroot, 1);
        }

        [FunctionAttribute("func combine (params paths) : string")]
        public HassiumString combine(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            string[] paths = new string[args.Length];
            for (int i = 0; i < paths.Length; i++)
                paths[i] = args[i].ToString(vm, args[i], location).String;
            return new HassiumString(Path.Combine(paths));
        }

        [FunctionAttribute("func parsedir (path : string) : string")]
        public HassiumString parsedir(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetDirectoryName(args[0].ToString(vm, args[0], location).String));
        }

        [FunctionAttribute("func parseext (path : string) : string")]
        public HassiumString parseext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetExtension(args[0].ToString(vm, args[0], location).String));
        }

        [FunctionAttribute("func parsename (path : string) : string")]
        public HassiumString parsename(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileName(args[0].ToString(vm, args[0], location).String));
        }

        [FunctionAttribute("func parseroot (path : string) : string")]
        public HassiumString parseroot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetPathRoot(args[0].ToString(vm, args[0], location).String));
        }
    }
}
