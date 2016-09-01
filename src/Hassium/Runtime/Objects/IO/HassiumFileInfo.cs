using System;
using System.IO;

using Hassium.Runtime.Objects.Types;
using Hassium.Runtime.Objects.Util;

namespace Hassium.Runtime.Objects.IO
{
    public class HassiumFileInfo: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("FileInfo");

        public FileInfo FileInfo { get; set; }

        public HassiumFileInfo()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 1);
        }

        public HassiumFileInfo _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumFileInfo fileInfo = new HassiumFileInfo();
            fileInfo.FileInfo = new FileInfo(args[0].ToString(vm).String);
            fileInfo.AddAttribute("creationTime", new HassiumProperty(fileInfo.get_creationTime));
            fileInfo.AddAttribute("directory", new HassiumProperty(fileInfo.get_directory));
            fileInfo.AddAttribute("extension", new HassiumProperty(fileInfo.get_extension));
            fileInfo.AddAttribute("length", new HassiumProperty(fileInfo.get_length));
            fileInfo.AddAttribute("name", new HassiumProperty(fileInfo.get_name));
            return fileInfo;
        }
        public HassiumDateTime get_creationTime(VirtualMachine vm, params HassiumObject[] args)
        {
            var ret = new HassiumDateTime();
            ret.DateTime = FileInfo.CreationTime;
            HassiumDateTime.AddAttributes(ret);
            return ret;
        }
        public HassiumNull set_creationTime(VirtualMachine vm, params HassiumObject[] args)
        {
            FileInfo.CreationTime = ((HassiumDateTime)args[0]).DateTime;
            return HassiumObject.Null;
        }
        public HassiumString get_directory(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(FileInfo.DirectoryName);
        }
        public HassiumString get_extension(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(FileInfo.Extension);
        }
        public HassiumInt get_length(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(FileInfo.Length);
        }
        public HassiumString get_name(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(FileInfo.Name);
        }
    }
}

