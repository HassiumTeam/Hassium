using System;
using System.Diagnostics;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Util
{
    public class HassiumProcess: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Process");

        public Process Process { get; set; }

        public HassiumProcess()
        {
            AddType(TypeDefinition);
            AddAttribute("getProcessByID",      getProcessByID,     1);
            AddAttribute("getProcessByName",    getProcessByName,   1);
            AddAttribute("getProcessList",      getProcessList,     0);
            AddAttribute("isProcessRunning",    isProcessRunning,   1);
            AddAttribute("killProcess",         killProcess,        1);
            AddAttribute("start",               start,              1);
        }

        public HassiumProcess createFromProcess(Process proc)
        {
            AddType(TypeDefinition);
            HassiumProcess process = new HassiumProcess();
            process.Process = proc;
            process.AddAttribute("ID",     new HassiumProperty(process.get_ID));
            process.AddAttribute("kill",   process.kill,                     0);
            process.AddAttribute("name",   new HassiumProperty(process.get_Name));
            return process;
        }

        public HassiumProcess getProcessByID(VirtualMachine vm, HassiumObject[] args)
        {
            return createFromProcess(Process.GetProcessById((int)args[0].ToInt(vm).Int));
        }
        public HassiumProcess getProcessByName(VirtualMachine vm, HassiumObject[] args)
        {
            return createFromProcess(Process.GetProcessesByName(args[0].ToString(vm).String)[0]);
        }
        public HassiumList getProcessList(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (Process process in Process.GetProcesses())
                list.add(vm, createFromProcess(process));

            return list;
        }
        public HassiumInt get_ID(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Process.Id);
        }
        public HassiumBool isProcessRunning(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumBool(Process.GetProcessesByName(args[0].ToString(vm).String).Length != 0);
            else
            {
                Process process = ((HassiumProcess)args[0]).Process;
                foreach (Process proc in Process.GetProcesses())
                    if (proc.Id == process.Id)
                        return new HassiumBool(true);
                return new HassiumBool(false);
            }
        }
        public HassiumNull kill(VirtualMachine vm, HassiumObject[] args)
        {
            Process.Kill();
            return HassiumObject.Null;
        }
        public HassiumBool killProcess(VirtualMachine vm, HassiumObject[] args)
        {
            try
            {
                Process.GetProcessesByName(args[0].ToString(vm).String)[0].Kill();
                return new HassiumBool(true);
            }
            catch
            {
                return new HassiumBool(false);
            }
        }
        public HassiumString get_Name(VirtualMachine vm, HassiumObject[] args)
        {
            try
            {
                return new HassiumString(Process.ProcessName);
            }
            catch
            {
                return new HassiumString("");
            }
        }
        private HassiumNull start(VirtualMachine vm, HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 1:
                    if (args[0] is HassiumString)
                        Process.Start(args[0].ToString(vm).String);
                    else
                        Process.Start(((HassiumProcessContext)args[0]).StartInfo);
                    break;
                case 2:
                    Process.Start(args[0].ToString(vm).String, args[1].ToString(vm).String);
                    break;
            }

            return HassiumObject.Null;
        }

        public override HassiumString ToString(VirtualMachine vm, params HassiumObject[] args)
        {
            return get_Name(vm, args);
        }
    }
}
