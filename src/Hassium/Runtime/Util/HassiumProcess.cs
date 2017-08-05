using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Diagnostics;

namespace Hassium.Runtime.Util
{
    public class HassiumProcess : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Process");

        public Process Process { get; set; }
        public ProcessStartInfo StartInfo { get; set; }

        public HassiumProcess()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new);
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

        public static void ImportAttribs(HassiumProcess proc)
        {
            proc.AddAttribute("args", new HassiumProperty(proc.get_args, proc.set_args));
            proc.AddAttribute("createwindow", new HassiumProperty(proc.get_createwindow, proc.set_createwindow));
            proc.AddAttribute("path", new HassiumProperty(proc.get_path, proc.set_path));
            proc.AddAttribute("shellexecute", new HassiumProperty(proc.get_shellexecute, proc.set_shellexecute));
            proc.AddAttribute("start", proc.start, 0);
            proc.AddAttribute("stop", proc.stop, 0);
            proc.AddAttribute("username", new HassiumProperty(proc.get_username, proc.set_username));
        }

        [FunctionAttribute("func new (path : string, args : string")]
        public static HassiumProcess _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumProcess process = new HassiumProcess();

            process.StartInfo = new ProcessStartInfo(args[0].ToString(vm, location).String, args[1].ToString(vm, location).String);
            process.Process = new Process();
            process.Process.StartInfo = process.StartInfo;
            ImportAttribs(process);

            return process;
        }

        [FunctionAttribute("args { get; }")]
        public HassiumString get_args(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(StartInfo.Arguments);
        }

        [FunctionAttribute("args { set; }")]
        public HassiumNull set_args(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StartInfo.Arguments = args[0].ToString(vm, location).String;
            return Null;
        }

        [FunctionAttribute("createwindow { get; }")]
        public HassiumBool get_createwindow(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.CreateNoWindow);
        }

        [FunctionAttribute("createwindow { set; }")]
        public HassiumNull set_createwindow(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StartInfo.CreateNoWindow = args[0].ToBool(vm, location).Bool;
            return Null;
        }

        [FunctionAttribute("path { get; }")]
        public HassiumString get_path(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(StartInfo.FileName);
        }

        [FunctionAttribute("path { set; }")]
        public HassiumNull set_path(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StartInfo.FileName = args[0].ToString(vm, location).String;
            return Null;
        }

        [FunctionAttribute("shellexecute { get; }")]
        public HassiumBool get_shellexecute(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.UseShellExecute);
        }

        [FunctionAttribute("shellexecute { set; }")]
        public HassiumNull set_shellexecute(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StartInfo.UseShellExecute = args[0].ToBool(vm, location).Bool;
            return Null;
        }

        [FunctionAttribute("func start () : null")]
        public HassiumNull start(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Process.StartInfo = StartInfo;
            Process.Start();

            return Null;
        }

        [FunctionAttribute("func stop () : null")]
        public HassiumNull stop(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Process.Kill();
            return Null;
        }

        [FunctionAttribute("username { get; }")]
        public HassiumString get_username(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(StartInfo.UserName);
        }

        [FunctionAttribute("username { set; }")]
        public HassiumNull set_username(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StartInfo.UserName = args[0].ToString(vm, location).String;
            return Null;
        }
    }
}
