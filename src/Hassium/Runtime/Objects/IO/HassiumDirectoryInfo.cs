using System;
using System.IO;

using Hassium.Runtime.Objects.Types;
using Hassium.Runtime.Objects.Util;

namespace Hassium.Runtime.Objects.IO
{
    public class HassiumDirectoryInfo: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("DirectoryInfo");

        public DirectoryInfo DirectoryInfo { get; set; }

        public HassiumDirectoryInfo()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 1);
        }

        public HassiumDirectoryInfo _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumDirectoryInfo directoryInfo = new HassiumDirectoryInfo();
            directoryInfo.DirectoryInfo = new DirectoryInfo(args[0].ToString(vm).String);
            directoryInfo.AddAttribute("creationTime", new HassiumProperty(directoryInfo.get_creationTime, directoryInfo.set_creationTime));
            directoryInfo.AddAttribute("extension", new HassiumProperty(directoryInfo.get_extension));
            directoryInfo.AddAttribute("getDirectories", directoryInfo.getDirectories, 0);
            directoryInfo.AddAttribute("getFiles", directoryInfo.getFiles, 0);
            directoryInfo.AddAttribute("move", directoryInfo.move, 1);
            directoryInfo.AddAttribute("name", new HassiumProperty(directoryInfo.get_name));
            directoryInfo.AddAttribute("parent", new HassiumProperty(directoryInfo.get_parent));
            directoryInfo.AddAttribute("root", new HassiumProperty(directoryInfo.get_root));
            return directoryInfo;
        }
        public HassiumDateTime get_creationTime(VirtualMachine vm, params HassiumObject[] args)
        {
            var ret = new HassiumDateTime();
            ret.DateTime = DirectoryInfo.CreationTime;
            HassiumDateTime.AddAttributes(ret);
            return ret;
        }
        public HassiumNull set_creationTime(VirtualMachine vm, params HassiumObject[] args)
        {
            DirectoryInfo.CreationTime = ((HassiumDateTime)args[0]).DateTime;
            return HassiumObject.Null;
        }
        public HassiumString get_extension(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(DirectoryInfo.Extension);
        }
        public HassiumList getDirectories(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            var directories = DirectoryInfo.GetDirectories();
            foreach (var directory in directories)
                result.add(vm, new HassiumString(directory.ToString()));
            return result;
        }
        public HassiumList getFiles(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            var files = DirectoryInfo.GetFiles();
            foreach (var file in files)
                result.add(vm, new HassiumString(file.ToString()));
            return result;
        }
        public HassiumNull move(VirtualMachine vm, params HassiumObject[] args)
        {
            DirectoryInfo.MoveTo(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }
        public HassiumString get_name(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(DirectoryInfo.Name);
        }
        public HassiumDirectoryInfo get_parent(VirtualMachine vm, params HassiumObject[] args)
        {
            return _new(vm, new HassiumString(DirectoryInfo.Parent.ToString()));
        }
        public HassiumDirectoryInfo get_root(VirtualMachine vm, params HassiumObject[] args)
        {
            return _new(vm, new HassiumString(DirectoryInfo.Root.ToString()));
        }
    }
}

