using System;
using System.Collections.Generic;
using System.Diagnostics;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Util
{
    public class HassiumProcessContext: HassiumObject
    {
        public static HassiumProcessContext Create(HassiumObject obj)
        {
            if (!(obj is HassiumProcessContext))
                throw new InternalException(string.Format("Cannot convert from {0} to HassiumProcessContext!", obj.GetType().Name));
            return (HassiumProcessContext)obj;
        }

        public ProcessStartInfo StartInfo { get; set; }
        public HassiumProcessContext()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, new int[] { 0, 1, 2 }));
        }

        private HassiumProcessContext _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumProcessContext hassiumProcessContext = new HassiumProcessContext();

            switch (args.Length)
            {
                case 0:
                    hassiumProcessContext.StartInfo = new ProcessStartInfo();
                    break;
                case 1:
                    hassiumProcessContext.StartInfo = new ProcessStartInfo(HassiumString.Create(args[0]).Value);
                    break;
                case 2:
                    hassiumProcessContext.StartInfo = new ProcessStartInfo(HassiumString.Create(args[0]).Value, HassiumString.Create(args[1]).Value);
                    break;
            }
            hassiumProcessContext.Attributes.Add("arguments",               new HassiumProperty(hassiumProcessContext.get_Arguments, hassiumProcessContext.set_Arguments));
            hassiumProcessContext.Attributes.Add("createNoWindow",          new HassiumProperty(hassiumProcessContext.get_CreateNoWindow, hassiumProcessContext.set_CreateNoWindow));
            hassiumProcessContext.Attributes.Add("environmentVariables",    new HassiumProperty(hassiumProcessContext.get_EnvironmentVariables));
            hassiumProcessContext.Attributes.Add("filePath",                new HassiumProperty(hassiumProcessContext.get_FilePath, hassiumProcessContext.set_FilePath));
            hassiumProcessContext.Attributes.Add("redirectStandardError",   new HassiumProperty(hassiumProcessContext.get_RedirectStandardError, hassiumProcessContext.set_RedirectStandardError));
            hassiumProcessContext.Attributes.Add("redirectStandardInput",   new HassiumProperty(hassiumProcessContext.get_RedirectStandardInput, hassiumProcessContext.set_RedirectStandardInput));
            hassiumProcessContext.Attributes.Add("redirectStandardOutput",  new HassiumProperty(hassiumProcessContext.get_RedirectStandardOutput, hassiumProcessContext.set_RedirectStandardOutput));

            return hassiumProcessContext;
        }

        public HassiumString get_Arguments(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(StartInfo.Arguments);
        }
        public HassiumNull set_Arguments(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.Arguments = HassiumString.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        public HassiumBool get_CreateNoWindow(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.CreateNoWindow);
        }
        public HassiumNull set_CreateNoWindow(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.CreateNoWindow = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        public HassiumList get_EnvironmentVariables(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            var variables = StartInfo.EnvironmentVariables;
            foreach (KeyValuePair<string, string> entry in variables)
                list.Value.Add(new HassiumKeyValuePair(new HassiumString(entry.Key), new HassiumString(entry.Value)));

            return list;
        }
        public HassiumString get_FilePath(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(StartInfo.FileName);
        }
        public HassiumNull set_FilePath(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.FileName = HassiumString.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        public HassiumBool get_RedirectStandardError(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.RedirectStandardError);
        }
        public HassiumNull set_RedirectStandardError(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.RedirectStandardError = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        public HassiumBool get_RedirectStandardInput(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.RedirectStandardInput);
        }
        public HassiumNull set_RedirectStandardInput(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.RedirectStandardInput = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        public HassiumBool get_RedirectStandardOutput(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.RedirectStandardOutput);
        }
        public HassiumNull set_RedirectStandardOutput(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.RedirectStandardOutput = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }
        public HassiumBool get_UseShellExecute(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StartInfo.UseShellExecute);
        }
        public HassiumNull set_UseShellExecute(VirtualMachine vm, HassiumObject[] args)
        {
            StartInfo.UseShellExecute = HassiumBool.Create(args[0]).Value;
            return HassiumObject.Null;
        }
    }
}