using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Diagnostics;
using System.Text;

namespace Hassium.Runtime.Util
{
    public class HassiumOS : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("OS");

        public HassiumOS()
        {
            AddType(TypeDefinition);

            AddAttribute("exec", exec, -1);
            AddAttribute("exit", exit, 0, 1);
            AddAttribute("exitcode", new HassiumProperty(get_exitcode, set_exitcode));
            AddAttribute("getarg", getvar, 1);
            AddAttribute("getargs", getvars, 0);
            AddAttribute("machinename", new HassiumProperty(get_machinename));
            AddAttribute("netversion", new HassiumProperty(get_netversion));
            AddAttribute("newline", new HassiumProperty(get_newline));
            AddAttribute("setarg", setvar, 2);
            AddAttribute("username", new HassiumProperty(get_username));
            AddAttribute("version", new HassiumProperty(get_version));
        }

        [DocStr(
            "@desc Starts a new process at the specified path with the given args and returns the OS.Process object.",
            "@param path The path of the executable to execute.",
            "@optional params args The arguments to start the process with.",
            "@returns A new OS.Process object that owns the started process."
            )]
        [FunctionAttribute("func exec (path : string, params args) : Process")]
        public HassiumProcess exec(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder arguments = new StringBuilder();

            for (int i = 1; i < args.Length; i++)
                arguments.AppendFormat("{0} ", args[i].ToString(vm, args[i], location).String);
            if (args.Length > 1)
                arguments.Remove(arguments.Length - 1, 1);
            var proc = new HassiumProcess(Process.Start(args[0].ToString(vm, args[0], location).String, arguments.ToString()));
            
            return proc;
        }

        [DocStr(
            "@desc Exits Hassium with the optionally specified exitcode, default 0.",
            "@optional code The int exitcode.",
            "@returns null."
            )]
        [FunctionAttribute("func exit () : null", "func exit (code : int) : null")]
        public HassiumNull exit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Environment.Exit(args.Length == 0 ? 0 : (int)args[0].ToInt(vm, args[0], location).Int);
            return Null;
        }

        [DocStr(
            "@desc Gets the exit code.",
            "@returns The exit code as int."
            )]
        [FunctionAttribute("exitcode { get; }")]
        public HassiumInt get_exitcode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Environment.ExitCode);
        }

        [DocStr(
            "@desc Sets the exit code.",
            "@returns null."
            )]
        [FunctionAttribute("exitcode { set; }")]
        public HassiumObject set_exitcode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Environment.ExitCode = (int)args[0].ToInt(vm, args[0], location).Int;
            return Null;
        }

        [DocStr(
            "@desc Gets the environment variable at the specified var.",
            "@returns The environment variable string at the var."
            )]
        [FunctionAttribute("func getvar (var : string) : string")]
        public HassiumString getvar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.GetEnvironmentVariable(args[0].ToString(vm, args[0], location).String));
        }

        [DocStr(
            "@desc Gets a new list containing all of the environment variables.",
            "@returns A new list of all the environment variables."
            )]
        [FunctionAttribute("func getvars () : list")]
        public HassiumList getvars(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList variables = new HassiumList(new HassiumObject[0]);

            foreach (var var_ in Environment.GetEnvironmentVariables())
                HassiumList.add(vm, variables, location, new HassiumString(var_.ToString()));

            return variables;
        }

        [DocStr(
            "@desc Gets the readonly name of this machine.",
            "@returns The machine name as string."
            )]
        [FunctionAttribute("machinename { get; }")]
        public HassiumString get_machinename(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.MachineName);
        }

        [DocStr(
            "@desc Gets the readonly .NET version.",
            "@returns The .NET version as string."
            )]
        [FunctionAttribute("netversion { get; }")]
        public HassiumString get_netversion(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.Version.ToString());
        }

        [DocStr(
            "@desc Gets the readonly string of the system newline separator.",
            "@returns The system's newline separator."
            )]
        [FunctionAttribute("newline { get; }")]
        public HassiumString get_newline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.NewLine);
        }

        [DocStr(
            "@desc Sets the specified environment variable name with the specified value.",
            "@param var The string variable name to set.",
            "@param val The string value.",
            "@returns null."
            )]
        [FunctionAttribute("func setvar (var : string, val : string) : null")]
        public HassiumNull setvar(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Environment.SetEnvironmentVariable(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);
            return Null;
        }

        [DocStr(
            "@desc Gets the readonly logged on username.",
            "@returns The string username."
            )]
        [FunctionAttribute("username")]
        public HassiumString get_username(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.UserName);
        }

        [DocStr(
            "@desc Gets the readonly OS version.",
            "@returns The OS version as string."
            )]
        [FunctionAttribute("version")]
        public HassiumString get_version(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.OSVersion.ToString());
        }
    }
}
