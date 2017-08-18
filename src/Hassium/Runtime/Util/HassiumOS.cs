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
            AddAttribute("getarg", getarg, 1);
            AddAttribute("getargs", getargs, 0);
            AddAttribute("machinename", new HassiumProperty(get_machinename));
            AddAttribute("netversion", new HassiumProperty(get_netversion));
            AddAttribute("newline", new HassiumProperty(get_newline));
            AddAttribute("setarg", setarg, 2);
            AddAttribute("username", new HassiumProperty(get_username));
            AddAttribute("version", new HassiumProperty(get_version));
        }

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

        [FunctionAttribute("func exit () : null", "func exit (code : int) : null")]
        public HassiumNull exit(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Environment.Exit(args.Length == 0 ? 0 : (int)args[0].ToInt(vm, args[0], location).Int);
            return Null;
        }

        [FunctionAttribute("exitcode { get; }")]
        public HassiumInt get_exitcode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Environment.ExitCode);
        }

        [FunctionAttribute("exitcode { set; }")]
        public HassiumInt set_exitcode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Environment.ExitCode = (int)args[0].ToInt(vm, args[0], location).Int;
            return args[0].ToInt(vm, args[0], location);
        }

        [FunctionAttribute("func getarg (arg : string) : string")]
        public HassiumString getarg(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.GetEnvironmentVariable(args[0].ToString(vm, args[0], location).String));
        }

        [FunctionAttribute("func getargs () : list")]
        public HassiumList getargs(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList arguments = new HassiumList(new HassiumObject[0]);

            foreach (var arg in Environment.GetCommandLineArgs())
                HassiumList.add(vm, arguments, location, new HassiumString(arg));

            return arguments;
        }

        [FunctionAttribute("machinename { get; }")]
        public HassiumString get_machinename(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.MachineName);
        }

        [FunctionAttribute("netversion { get; }")]
        public HassiumString get_netversion(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.Version.ToString());
        }

        [FunctionAttribute("newline { get; }")]
        public HassiumString get_newline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.NewLine);
        }

        [FunctionAttribute("func setarg (arg : string, val : string) : null")]
        public HassiumNull setarg(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Environment.SetEnvironmentVariable(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);
            return Null;
        }

        [FunctionAttribute("username")]
        public HassiumString get_username(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.UserName);
        }

        [FunctionAttribute("version")]
        public HassiumString get_version(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Environment.OSVersion.ToString());
        }
    }
}
