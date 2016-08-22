using System;
using System.IO;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.IO
{
    public class HassiumFS: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("FS");

        public HassiumFS()
        {
            AddType(TypeDefinition);
            AddAttribute("combinePath",                     combinePath,               -1);
            AddAttribute("createDirectory",                 createDirectory,            1);
            AddAttribute("createFile",                      createFile,                 1);
            AddAttribute("currentDirectory",                new HassiumProperty(get_currentDirectory, set_currentDirectory));
            AddAttribute("delete",                          delete,                     1);
            AddAttribute("deleteDirectory",                 deleteDirectory,            1);
            AddAttribute("deleteFile",                      deleteFile,                 1);
            AddAttribute("directoryExists",                 directoryExists,            1);
            AddAttribute("exists",                          exists,                     1);
            AddAttribute("fileExists",                      fileExists,                 1);
            AddAttribute("getDirectoryList",                getDirectoryList,           1);
            AddAttribute("getFileList",                     getFileList,                1);
            AddAttribute("getTempFile",                     getTempFile,                0);
            AddAttribute("getTempPath",                     getTempPath,                0);
            AddAttribute("readBytes",                       readBytes,                  1);
            AddAttribute("readLines",                       readLines,                  1);
            AddAttribute("readString",                      readString,                 1);
            AddAttribute("parseDirectoryName",              parseDirectoryName,         1);
            AddAttribute("parseExtension",                  parseExtension,             1);
            AddAttribute("parseFileName",                   parseFileName,              1);
            AddAttribute("parseFileNameWithoutExtension",   parseFileNameWithoutExtension, 1);
            AddAttribute("parseRoot",                       parseRoot,                  1);
            AddAttribute("writeBytes",                      writeBytes,                 2);
            AddAttribute("writeLines",                      writeLines,                 2);
            AddAttribute("writeString",                     writeString,                2);
        }

        public HassiumString combinePath(VirtualMachine vm, params HassiumObject[] args)
        {
            string[] paths = new string[args.Length];
            for (int i = 0; i < paths.Length; i++)
                paths[i] = args[i].ToString(vm).String;
            return new HassiumString(Path.Combine(paths));
        }
        public HassiumNull createDirectory(VirtualMachine vm, params HassiumObject[] args)
        {
            Directory.CreateDirectory(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }
        public HassiumNull createFile(VirtualMachine vm, params HassiumObject[] args)
        {
            File.Create(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }
        public HassiumString get_currentDirectory(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Directory.GetCurrentDirectory());
        }
        public HassiumString set_currentDirectory(VirtualMachine vm, params HassiumObject[] args)
        {
            Directory.SetCurrentDirectory(args[0].ToString(vm).String);
            return get_currentDirectory(vm);
        }
        public HassiumNull delete(VirtualMachine vm, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm).String;
            if (File.Exists(path))
                File.Delete(path);
            else if (Directory.Exists(path))
                Directory.Delete(path);
            return HassiumObject.Null;
        }
        public HassiumNull deleteDirectory(VirtualMachine vm, params HassiumObject[] args)
        {
            Directory.Delete(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }
        public HassiumNull deleteFile(VirtualMachine vm, params HassiumObject[] args)
        {
            File.Delete(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }
        public HassiumBool directoryExists(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(Directory.Exists(args[0].ToString(vm).String));
        }
        public HassiumBool exists(VirtualMachine vm, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm).String;
            if (File.Exists(path) || Directory.Exists(path))
                return new HassiumBool(true);
            return new HassiumBool(false);
        }
        public HassiumBool fileExists(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(File.Exists(args[0].ToString(vm).String));
        }
        public HassiumList getDirectoryList(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (string dir in Directory.GetDirectories(args[0].ToString(vm).String))
                result.add(vm, new HassiumString(dir));
            return result;
        }
        public HassiumList getFileList(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (string dir in Directory.GetFiles(args[0].ToString(vm).String))
                result.add(vm, new HassiumString(dir));
            return result;
        }
        public HassiumString getTempFile(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempFileName());
        }
        public HassiumString getTempPath(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempPath());
        }
        public HassiumString parseDirectoryName(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetDirectoryName(args[0].ToString(vm).String));
        }
        public HassiumString parseExtension(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetExtension(args[0].ToString(vm).String));
        }
        public HassiumString parseFileName(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileName(args[0].ToString(vm).String));
        }
        public HassiumString parseFileNameWithoutExtension(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileNameWithoutExtension(args[0].ToString(vm).String));
        }
        public HassiumString parseRoot(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetPathRoot(args[0].ToString(vm).String));
        }
        public HassiumList readBytes(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            BinaryReader reader = new BinaryReader(new StreamReader(args[0].ToString(vm).String).BaseStream);
            while (reader.BaseStream.Position < reader.BaseStream.Length)
                result.add(vm, new HassiumChar((char)reader.ReadBytes(1)[0]));
            reader.Close();
            return result;
        }
        public HassiumList readLines(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            StreamReader reader = new StreamReader(args[0].ToString(vm).String);
            while (reader.BaseStream.Position < reader.BaseStream.Length)
                result.add(vm, new HassiumString(reader.ReadLine()));
            reader.Close();
            return result;
        }
        public HassiumString readString(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(File.ReadAllText(args[0].ToString(vm).String));
        }
        public HassiumNull writeBytes(VirtualMachine vm, params HassiumObject[] args)
        {
            var list = args[1].ToList(vm).List;
            BinaryWriter writer = new BinaryWriter(new StreamWriter(args[0].ToString(vm).String).BaseStream);
            foreach (var obj in list)
                writer.Write((byte)obj.ToChar(vm).Char);
            writer.Close();
            return HassiumObject.Null;
        }
        public HassiumNull writeLines(VirtualMachine vm, params HassiumObject[] args)
        {
            var list = args[1].ToList(vm).List;
            StreamWriter writer = new StreamWriter(args[0].ToString(vm).String);
            foreach (var obj in list)
                writer.WriteLine(obj.ToString(vm).String);
            writer.Close();
            return HassiumObject.Null;
        }
        public HassiumNull writeString(VirtualMachine vm, params HassiumObject[] args)
        {
            File.WriteAllText(args[0].ToString(vm).String, args[1].ToString(vm).String);
            return HassiumObject.Null;
        }
    }
}

