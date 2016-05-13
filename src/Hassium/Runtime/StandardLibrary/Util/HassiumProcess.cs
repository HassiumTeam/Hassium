using System;
using System.Diagnostics;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Util
{
    public class HassiumProcess: HassiumObject
    {
        public Process Process { get; set; }

        public static HassiumProcess Create(HassiumObject obj)
        {
            if (!(obj is HassiumProcess))
                throw new InternalException(string.Format("Cannot convert from {0} to HassiumProcess!", obj.GetType().Name));
            return (HassiumProcess)obj;
        }

        public HassiumProcess()
        {
            Attributes.Add("getProcessByID",    new HassiumFunction(getProcessByID, 1));
            Attributes.Add("getProcessByName",  new HassiumFunction(getProcessByName, 1));
            Attributes.Add("getProcessList",    new HassiumFunction(getProcessList, 0));
            Attributes.Add("isProcessRunning",  new HassiumFunction(isProcessRunning, 1));
            Attributes.Add("killProcess",       new HassiumFunction(killProcess, 1));
            Attributes.Add("start",             new HassiumFunction(start, 1));
        }

        private HassiumProcess createFromProcess(Process process)
        {
            HassiumProcess hassiumProcess = new HassiumProcess();
            hassiumProcess.Process = process;
            hassiumProcess.Attributes.Add("ID",     new HassiumProperty(hassiumProcess.get_ID));
            hassiumProcess.Attributes.Add("kill",   new HassiumFunction(hassiumProcess.kill, 0));
            hassiumProcess.Attributes.Add("name",   new HassiumProperty(hassiumProcess.get_Name));
            hassiumProcess.Attributes.Add(HassiumObject.TOSTRING_FUNCTION, new HassiumFunction(hassiumProcess.toString, 0));

            return hassiumProcess;
        }

        private HassiumProcess getProcessByID(VirtualMachine vm, HassiumObject[] args)
        {
            return createFromProcess(Process.GetProcessById((int)HassiumInt.Create(args[0]).Value));
        }
        private HassiumProcess getProcessByName(VirtualMachine vm, HassiumObject[] args)
        {
            return createFromProcess(Process.GetProcessesByName(HassiumString.Create(args[0]).Value)[0]);
        }
        private HassiumList getProcessList(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (Process process in Process.GetProcesses())
                list.Value.Add(createFromProcess(process));

            return list;
        }
        public HassiumInt get_ID(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Process.Id);
        }
        private HassiumBool isProcessRunning(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumString)
                return new HassiumBool(Process.GetProcessesByName(HassiumString.Create(args[0]).Value).Length != 0);
            else
            {
                Process process = HassiumProcess.Create(args[0]).Process;
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
        private HassiumBool killProcess(VirtualMachine vm, HassiumObject[] args)
        {
            try
            {
                Process.GetProcessesByName(HassiumString.Create(args[0]).Value)[0].Kill();
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
                        Process.Start(HassiumString.Create(args[0]).Value);
                    else
                        Process.Start(HassiumProcessContext.Create(args[0]).StartInfo);
                    break;
                case 2:
                    Process.Start(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value);
                    break;
            }

            return HassiumObject.Null;
        }

        public HassiumString toString(VirtualMachine vm, HassiumObject[] args)
        {
            return get_Name(vm, args);
        }
    }
}

