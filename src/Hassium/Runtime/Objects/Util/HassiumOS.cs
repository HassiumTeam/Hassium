using System;
using System.Collections.Generic;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Util
{
    public class HassiumOS: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("OS");

        public HassiumOS()
        {
            AddType(TypeDefinition);
            AddAttribute("exit",                    exit,                       1);
            AddAttribute("exitCode",                new HassiumProperty(get_exitCode, set_exitCode));
            AddAttribute("getCommandLineArgs",      getCommandLineArgs,         0);
            AddAttribute("getEnvironmentVariable",  getEnvironmentVariable,     1);
            AddAttribute("getEnvironmentVariables", getEnvironmentVariables,    0);
            AddAttribute("machineName",             new HassiumProperty(get_machineName));
            AddAttribute("newLine",                 new HassiumProperty(get_newLine));
            AddAttribute("OSVersion",               new HassiumProperty(get_OSVersion));
            AddAttribute("processorCount",          new HassiumProperty(get_processorCount));
            AddAttribute("userName",                new HassiumProperty(get_userName));
            AddAttribute("version",                 new HassiumProperty(get_version));
        }

        public HassiumObject exit(VirtualMachine vm, params HassiumObject[] args)
        {
            Environment.Exit((int)args[0].ToInt(vm).Int);
            return args[0];
        }
        public HassiumInt get_exitCode(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Environment.ExitCode);
        }
        public HassiumNull set_exitCode(VirtualMachine vm, params HassiumObject[] args)
        {
            Environment.ExitCode = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        public HassiumList getCommandLineArgs(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (string arg in Environment.GetCommandLineArgs())
                result.add(vm, new HassiumString(arg));
            return result;
        }
        public HassiumString getEnvironmentVariable(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Environment.GetEnvironmentVariable(args[0].ToString(vm).String));
        }
        public HassiumDictionary getEnvironmentVariables(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumDictionary result = new HassiumDictionary(new List<HassiumKeyValuePair>());
            var dictionary = Environment.GetEnvironmentVariables();
            foreach (var variable in (Dictionary<string, string>)dictionary)
                result.Dictionary.Add(new HassiumString(variable.Key), new HassiumString(variable.Value));
            return result;
        }
        public HassiumString get_machineName(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Environment.MachineName);
        }
        public HassiumString get_newLine(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Environment.NewLine);
        }
        public HassiumString get_OSVersion(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Environment.OSVersion.ToString());
        }
        public HassiumInt get_processorCount(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Environment.ProcessorCount);
        }
        public HassiumString get_userName(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Environment.UserName);
        }
        public HassiumString get_version(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Environment.Version.ToString());
        }
    }
}

