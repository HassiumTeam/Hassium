using System;
using System.Collections.Generic;
using System.Diagnostics;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Util
{
    public class HassiumProcessContext: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("ProcessContext");

        public ProcessStartInfo StartInfo { get; set; }
        public HassiumProcessContext()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, new int[] { 0, 1, 2 });
        }

        private HassiumProcessContext _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumProcessContext processContext = new HassiumProcessContext();

            switch (args.Length)
            {
                case 0:
                    processContext.StartInfo = new ProcessStartInfo();
                    break;
                case 1:
                    processContext.StartInfo = new ProcessStartInfo(args[0].ToString(vm).String);
                    break;
                case 2:
                    processContext.StartInfo = new ProcessStartInfo(args[0].ToString(vm).String, args[1].ToString(vm).String);
                    break;
            }
            processContext.Attributes.Add("arguments",               new HassiumProperty(processContext.get_arguments, processContext.set_arguments));
            processContext.Attributes.Add("createNoWindow",          new HassiumProperty(processContext.get_createNoWindow, processContext.set_createNoWindow));
            processContext.Attributes.Add("environmentVariables",    new HassiumProperty(processContext.get_environmentVariables));
            processContext.Attributes.Add("filePath",                new HassiumProperty(processContext.get_filePath, processContext.set_filePath));
            processContext.Attributes.Add("redirectStandardError",   new HassiumProperty(processContext.get_redirectStandardError, processContext.set_redirectStandardError));
            processContext.Attributes.Add("redirectStandardInput",   new HassiumProperty(processContext.get_redirectStandardInput, processContext.set_redirectStandardInput));
            processContext.Attributes.Add("redirectStandardOutput",  new HassiumProperty(processContext.get_redirectStandardOutput, processContext.set_redirectStandardOutput));

            return processContext;
        }

        public HassiumString get_arguments(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(StartInfo.Arguments);
        }
        public HassiumNull set_arguments(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.Arguments = args[0].ToString(vm).String;
            return HassiumObject.Null;
        }
        public HassiumBool get_createNoWindow(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.CreateNoWindow);
        }
        public HassiumNull set_createNoWindow(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.CreateNoWindow = args[0].ToBool(vm).Bool;
            return HassiumObject.Null;
        }
        public HassiumList get_environmentVariables(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            var variables = StartInfo.EnvironmentVariables;
            foreach (KeyValuePair<string, string> entry in variables)
                list.add(vm, new HassiumKeyValuePair(new HassiumString(entry.Key), new HassiumString(entry.Value)));

            return list;
        }
        public HassiumString get_filePath(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(StartInfo.FileName);
        }
        public HassiumNull set_filePath(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.FileName = args[0].ToString(vm).String;
            return HassiumObject.Null;
        }
        public HassiumBool get_redirectStandardError(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.RedirectStandardError);
        }
        public HassiumNull set_redirectStandardError(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.RedirectStandardError = args[0].ToBool(vm).Bool;
            return HassiumObject.Null;
        }
        public HassiumBool get_redirectStandardInput(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.RedirectStandardInput);
        }
        public HassiumNull set_redirectStandardInput(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.RedirectStandardInput = args[0].ToBool(vm).Bool;
            return HassiumObject.Null;
        }
        public HassiumBool get_redirectStandardOutput(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.RedirectStandardOutput);
        }
        public HassiumNull set_redirectStandardOutput(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.RedirectStandardOutput = args[0].ToBool(vm).Bool;
            return HassiumObject.Null;
        }
        public HassiumBool get_useShellExecute(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.UseShellExecute);
        }
        public HassiumNull set_useShellExecute(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.UseShellExecute = args[0].ToBool(vm).Bool;
            return HassiumObject.Null;
        }
    }
}
