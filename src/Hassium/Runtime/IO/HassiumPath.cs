using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
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
            AddAttribute("getappdata", getappdata, 0);
            AddAttribute("getdocuments", getdocuments, 0);
            AddAttribute("gethome", gethome, 0);
            AddAttribute("getstartup", getstartup, 0);
            AddAttribute("parsedir", parsedir, 1);
            AddAttribute("parseext", parseext, 1);
            AddAttribute("parsefilename", parsefilename, 1);
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

        [FunctionAttribute("func getappdata () : string")]
        public HassiumString getappdata(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        [FunctionAttribute("func getdocuments () : string")]
        public HassiumString getdocuments(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        [FunctionAttribute("func gethome () : string")]
        public HassiumString gethome(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                    Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            return new HassiumString(homePath);
        }

        [FunctionAttribute("func getstartup () : string")]
        public HassiumString getstartup(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
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

        [FunctionAttribute("func parsefilename (path : string) : string")]
        public HassiumString parsefilename(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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
