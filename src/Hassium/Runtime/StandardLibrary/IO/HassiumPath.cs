using System;
using System.IO;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.IO
{
    public class HassiumPath: HassiumObject
    {
        public HassiumPath()
        {
            Attributes.Add("getTempFile",                   new HassiumFunction(getTempFile, 0));
            Attributes.Add("getTempPath",                   new HassiumFunction(getTempPath, 0));
            Attributes.Add("parseDirectoryName",            new HassiumFunction(parseDirectoryName, 1));
            Attributes.Add("parseExtension",                new HassiumFunction(parseExtension, 1));
            Attributes.Add("parseFileName",                 new HassiumFunction(parseFileName, 1));
            Attributes.Add("parseFileNameWithoutExtension", new HassiumFunction(parseFileNameWithoutExtension, 1));
            Attributes.Add("parseRoot",                     new HassiumFunction(parseRoot, 1));
            AddType("Path");
        }

        private HassiumString getTempFile(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempFileName());
        }
        private HassiumString getTempPath(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempPath());
        }
        private HassiumString parseDirectoryName(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Path.GetDirectoryName(HassiumString.Create(args[0]).Value));
        }
        private HassiumString parseExtension(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Path.GetExtension(HassiumString.Create(args[0]).Value));
        }
        private HassiumString parseFileName(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileName(HassiumString.Create(args[0]).Value));
        }
        private HassiumString parseFileNameWithoutExtension(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileNameWithoutExtension(HassiumString.Create(args[0]).Value));
        }
        private HassiumString parseRoot(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Path.GetPathRoot(HassiumString.Create(args[0]).Value));
        }
    }
}

