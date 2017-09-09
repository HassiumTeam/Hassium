using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;
using System.IO;

namespace Hassium.Runtime.IO
{
    public class HassiumPath : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new PathTypeDef();

        public HassiumPath()
        {
            AddType(TypeDefinition);

        }

        [DocStr(
            "@desc A class containing methods for interacting with path strings.",
            "@returns Path."
            )]
        public class PathTypeDef : HassiumTypeDefinition
        {
            public PathTypeDef() : base("Path")
            {
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

            [DocStr(
                "@desc Combines the given file paths together and returns the file string.",
                "@optional params paths The list of paths to combine.",
                "@returns The resulting path string."
            )]
            [FunctionAttribute("func combine (params paths) : string")]
            public HassiumString combine(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                string[] paths = new string[args.Length];
                for (int i = 0; i < paths.Length; i++)
                    paths[i] = args[i].ToString(vm, args[i], location).String;
                return new HassiumString(Path.Combine(paths));
            }

            [DocStr(
                "@desc Gets the path to the ApplicationData folder.",
                "@returns The ApplicationData folder."
            )]
            [FunctionAttribute("func getappdata () : string")]
            public HassiumString getappdata(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }

            [DocStr(
                "@desc Gets the path to the MyDocuments folder.",
                "@returns The MyDocuments folder."
            )]
            [FunctionAttribute("func getdocuments () : string")]
            public HassiumString getdocuments(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            }

            [DocStr(
                "@desc Gets the home folder of the currently logged in user.",
                "@returns The home folder."
            )]
            [FunctionAttribute("func gethome () : string")]
            public HassiumString gethome(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                    Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                return new HassiumString(homePath);
            }

            [DocStr(
                "@desc Gets the startup folder of the currently logged in user.",
                "@returns The startup folder."
            )]
            [FunctionAttribute("func getstartup () : string")]
            public HassiumString getstartup(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
            }

            [DocStr(
                "@desc Parses the directory name of the specified path string and returns it.",
                "@param path The path to parse.",
                "@returns The directory name of the path."
            )]
            [FunctionAttribute("func parsedir (path : string) : string")]
            public HassiumString parsedir(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Path.GetDirectoryName(args[0].ToString(vm, args[0], location).String));
            } 

            [DocStr(
                "@desc Parses The extension of the specified path string and returns it.",
                "@param path The path to parse.",
                "@returns The extension of the file path."
            )]
            [FunctionAttribute("func parseext (path : string) : string")]
            public HassiumString parseext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Path.GetExtension(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Parses the file name of the specified path string and returns it.",
                "@param path The path to parse.",
                "@returns The file name of the file path."
            )]
            [FunctionAttribute("func parsefilename (path : string) : string")]
            public HassiumString parsefilename(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Path.GetFileName(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Parses the root directory of the specified path string and returns it.",
                "@params path The path to parse.",
                "@returns The root directory of the file path."
            )]
            [FunctionAttribute("func parseroot (path : string) : string")]
            public HassiumString parseroot(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Path.GetPathRoot(args[0].ToString(vm, args[0], location).String));
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
