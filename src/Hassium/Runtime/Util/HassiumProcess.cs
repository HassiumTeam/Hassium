using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Diagnostics;

namespace Hassium.Runtime.Util
{
    public class HassiumProcess : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new ProcessTypeDef();

        public Process Process { get; set; }
        public ProcessStartInfo StartInfo { get; set; }

        public HassiumProcess()
        {
            AddType(TypeDefinition);
        }

        public HassiumProcess(Process process)
        {
            AddType(TypeDefinition);
            Process = process;
            StartInfo = process.StartInfo;
        }
        public HassiumProcess(Process process, ProcessStartInfo startInfo)
        {
            AddType(TypeDefinition);
            Process = process;
            StartInfo = startInfo;
        }

        [DocStr(
            "@desc A class representing a separate process being hosted by this process.",
            "@returns Process."
            )]
        public class ProcessTypeDef : HassiumTypeDefinition
        {
            public ProcessTypeDef() : base("Process")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "args", new HassiumProperty(get_args, set_args)  },
                    { "createwindow", new HassiumProperty(get_createwindow, set_createwindow)  },
                    { INVOKE, new HassiumFunction(_new, 2) },
                    { "path", new HassiumProperty(get_path, set_path)  },
                    { "shellexecute", new HassiumProperty(get_shellexecute, set_shellexecute)  },
                    { "start", new HassiumFunction(start, 0)  },
                    { "stop", new HassiumFunction(stop, 0)  },
                    { "username", new HassiumProperty(get_username, set_username)  }
                };
            }

            [DocStr(
                "@desc Constructs a new Process object using the specified process path and the given argument string.",
                "@param path The path to the executable.",
                "@param args The argument string.",
                "@returns The new Process object."
                )]
            [FunctionAttribute("func new (path : string, args : string")]
            public static HassiumProcess _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumProcess process = new HassiumProcess();

                process.StartInfo = new ProcessStartInfo(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);
                process.Process = new Process();
                process.Process.StartInfo = process.StartInfo;

                return process;
            }

            [DocStr(
                "@desc Gets the mutable argument string.",
                "@returns The argument string."
                )]
            [FunctionAttribute("args { get; }")]
            public static HassiumString get_args(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                return new HassiumString(StartInfo.Arguments);
            }

            [DocStr(
                "@desc Sets the mutable argument string.",
                "@returns null."
                )]
            [FunctionAttribute("args { set; }")]
            public static HassiumNull set_args(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                StartInfo.Arguments = args[0].ToString(vm, args[0], location).String;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable boolean indicating if a window will be created.",
                "@returns true if a new window will be created, otherwise false."
                )]
            [FunctionAttribute("createwindow { get; }")]
            public static HassiumBool get_createwindow(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                return new HassiumBool(!StartInfo.CreateNoWindow);
            }

            [DocStr(
                "@desc Sets the mutable boolean indicating if a window will be created.",
                "@returns null."
                )]
            [FunctionAttribute("createwindow { set; }")]
            public static HassiumNull set_createwindow(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                StartInfo.CreateNoWindow = !args[0].ToBool(vm, args[0], location).Bool;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable string to the executable path.",
                "@returns The path to the executable as string."
                )]
            [FunctionAttribute("path { get; }")]
            public static HassiumString get_path(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                return new HassiumString(StartInfo.FileName);
            }

            [DocStr(
                "@desc Sets the mutable path string to the executable.",
                "@returns null."
                )]
            [FunctionAttribute("path { set; }")]
            public static HassiumNull set_path(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                StartInfo.FileName = args[0].ToString(vm, args[0], location).String;
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable boolean indicating if the process will use a shell execute.",
                "@returns true if the process will use shell execute, otherwise false."
                )]
            [FunctionAttribute("shellexecute { get; }")]
            public static HassiumBool get_shellexecute(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                return new HassiumBool(StartInfo.UseShellExecute);
            }

            [DocStr(
                "@desc Sets the mutable boolean indicating if the process wil use a shell execute.",
                "@returns null."
                )]
            [FunctionAttribute("shellexecute { set; }")]
            public static HassiumNull set_shellexecute(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                StartInfo.UseShellExecute = args[0].ToBool(vm, args[0], location).Bool;
                return Null;
            }

            [DocStr(
                "@desc Starts the process.",
                "@returns null."
                )]
            [FunctionAttribute("func start () : null")]
            public static HassiumNull start(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Process = (self as HassiumProcess).Process;
                var StartInfo = (self as HassiumProcess).StartInfo;

                Process.StartInfo = StartInfo;
                Process.Start();

                return Null;
            }

            [DocStr(
                "@desc Stops the process.",
                "@returns null."
                )]
            [FunctionAttribute("func stop () : null")]
            public static HassiumNull stop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumProcess).Process.Kill();
                return Null;
            }

            [DocStr(
                "@desc Gets the mutable string username for the process to execute with.",
                "@returns The string username."
                )]
            [FunctionAttribute("username { get; }")]
            public static HassiumString get_username(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                return new HassiumString(StartInfo.UserName);
            }

            [DocStr(
                "@desc Sets the mutable string username for the process to execute with.",
                "@returns null."
                )]
            [FunctionAttribute("username { set; }")]
            public static HassiumNull set_username(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StartInfo = (self as HassiumProcess).StartInfo;
                StartInfo.UserName = args[0].ToString(vm, args[0], location).String;
                return Null;
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
