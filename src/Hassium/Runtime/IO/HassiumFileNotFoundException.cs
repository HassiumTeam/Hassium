﻿using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumFileNotFoundException : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new FileNotFoundExceptionTypeDef();

        public HassiumString Path { get; set; }

        public HassiumFileNotFoundException()
        {
            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class representing an exception that is thrown when a file is not found.",
            "@returns FileNotFoundException."
            )]
        public class FileNotFoundExceptionTypeDef : HassiumTypeDefinition
        {
            public FileNotFoundExceptionTypeDef() : base("FileNotFoundException")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { INVOKE, new HassiumFunction(_new, 1) },
                    { "message", new HassiumProperty(get_message) },
                    { "path", new HassiumProperty(get_path) },
                    { TOSTRING, new HassiumFunction(tostring) }
                };
            }

            [DocStr(
                "@desc Constructs a new FileNotFoundException object using the specified path.",
                "@param path The path to the file that was not found.",
                "@returns The new FileNotFoundException."
                )]
            [FunctionAttribute("func new (path : string) : FileNotFoundException")]
            public static HassiumFileNotFoundException _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumFileNotFoundException exception = new HassiumFileNotFoundException();

                exception.Path = args[0].ToString(vm, args[0], location);

                return exception;
            }

            [DocStr(
                "@desc Gets the readonly string message of the exception.",
                "@returns The exception message string."
                )]
            [FunctionAttribute("message { get; }")]
            public static HassiumString get_message(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(string.Format("File not found: '{0}' does not exist!", (self as HassiumFileNotFoundException).Path.String));
            }

            [DocStr(
                "@desc Gets the readonly string of the file that was not found.",
                "@returns The file path as a string."
                )]
            [FunctionAttribute("path { get; }")]
            public static HassiumObject get_path(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumFileNotFoundException).Path;
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
