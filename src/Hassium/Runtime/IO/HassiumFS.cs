using System.IO;
using System.Text;

using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime.IO
{
    public class HassiumFS : HassiumObject
    {
        public HassiumFS()
        {
            AddAttribute("close", close, 1);
            AddAttribute("copy", copy, 2);
            AddAttribute("createdir", createdir, 1);
            AddAttribute("createfile", createfile, 1);
            AddAttribute("cwd", new HassiumProperty(get_cwd, set_cwd));
            AddAttribute("delete", delete, 1);
            AddAttribute("deletedir", deletedir, 1);
            AddAttribute("deletefile", deletefile, 1);
            AddAttribute("gettempfile", gettempfile, 0);
            AddAttribute("gettemppath", gettemppath, 0);
            AddAttribute("listdirs", listdirs, 1);
            AddAttribute("listfiles", listfiles, 1);
            AddAttribute("move", move, 2);
            AddAttribute("open", open, 1);
            AddAttribute("readbytes", readbytes, 1);
            AddAttribute("readlines", readlines, 1);
            AddAttribute("readstring", readstring, 1);
            AddAttribute("writebytes", writebytes, -1);
            AddAttribute("writelines", writelines, -1);
            AddAttribute("writestring", writestring, -1);
        }

        [FunctionAttribute("func close () : null")]
        public HassiumNull close(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            (args[0] as HassiumFile).close(vm, location);
            return Null;
        }

        [FunctionAttribute("func copy (src : string, dest : string) : null")]
        public HassiumNull copy(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string source = args[0].ToString(vm, location).String;
            if (!File.Exists(source))
            {
                vm.RaiseException(HassiumFileNotFoundException._new(vm, location, args[0].ToString(vm, location)));
                return Null;
            }
            File.Copy(source, args[1].ToString(vm, location).String);

            return Null;
        }

        [FunctionAttribute("func createdir (path : string) : null")]
        public HassiumNull createdir(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Directory.CreateDirectory(args[0].ToString(vm, location).String);
            return Null;
        }

        [FunctionAttribute("func createfile (path : string) : null")]
        public HassiumNull createfile(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            File.Create(args[0].ToString(vm, location).String);
            return Null;
        }

        [FunctionAttribute("cwd { get; }")]
        public HassiumString get_cwd(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Directory.GetCurrentDirectory());
        }

        [FunctionAttribute("cwd { set; }")]
        public HassiumString set_cwd(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            Directory.SetCurrentDirectory(args[0].ToString(vm, location).String);
            return get_cwd(vm, location);
        }

        [FunctionAttribute("func delete (path : string) : null")]
        public HassiumNull delete(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm, location).String;

            if (File.Exists(path))
                File.Delete(path);
            else if (Directory.Exists(path))
                Directory.Delete(path);
            else
                vm.RaiseException(HassiumFileNotFoundException._new(vm, location, args[0].ToString(vm, location)));

            return Null;
        }

        [FunctionAttribute("func deletedir (dir : string) : null")]
        public HassiumNull deletedir(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string dir = args[0].ToString(vm, location).String;

            if (Directory.Exists(dir))
                Directory.Delete(dir);
            else
                vm.RaiseException(HassiumDirectoryNotFoundException._new(vm, location, args[0].ToString(vm, location)));
            return Null;
        }

        [FunctionAttribute("func deletefile (file : string) : null")]
        public HassiumNull deletefile(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm, location).String;

            if (File.Exists(path))
                File.Delete(path);
            else
                vm.RaiseException(HassiumFileNotFoundException._new(vm, location, args[0].ToString(vm, location)));
            return Null;
        }

        [FunctionAttribute("func gettempfile () : string")]
        public HassiumString gettempfile(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempFileName());
        }

        [FunctionAttribute("func gettemppath () : string")]
        public HassiumString gettemppath(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetTempPath());
        }

        [FunctionAttribute("func listdirs (path : string) : list")]
        public HassiumList listdirs(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (string dir in Directory.GetDirectories(args[0].ToString(vm, location).String))
                result.add(vm, location, new HassiumString(dir));
            return result;
        }

        [FunctionAttribute("func listfiles (path : string) : list")]
        public HassiumList listfiles(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList result = new HassiumList(new HassiumObject[0]);
            foreach (string dir in Directory.GetFiles(args[0].ToString(vm, location).String))
                result.add(vm, location, new HassiumString(dir));
            return result;
        }

        [FunctionAttribute("func move (src : string, dest : string) : null")]
        public HassiumNull move(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string source = args[0].ToString(vm, location).String;
            if (!File.Exists(source))
            {
                vm.RaiseException(HassiumFileNotFoundException._new(vm, location, args[0].ToString(vm, location)));
                return Null;
            }
            File.Move(source, args[1].ToString(vm, location).String);

            return Null;
        }

        [FunctionAttribute("func open (path : string) : File")]
        public HassiumFile open(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm, location).String;
            return new HassiumFile(path);
        }

        [FunctionAttribute("func readbytes (path : string) : list")]
        public HassiumList readbytes(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            var stream = new FileStream(args[0].ToString(vm, location).String, FileMode.Open, FileAccess.Read, FileShare.Read);
            var reader = new BinaryReader(stream);

            try
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                    list.add(vm, location, new HassiumChar((char)reader.ReadBytes(1)[0]));

                return list;
            }
            finally
            {
                reader.Close();
            }
        }

        [FunctionAttribute("func readlines (path : string) : list")]
        public HassiumList readlines(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            var stream = new FileStream(args[0].ToString(vm, location).String, FileMode.Open, FileAccess.Read, FileShare.Read);
            var reader = new StreamReader(stream);

            try
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                    list.add(vm, location, new HassiumString(reader.ReadLine()));

                return list;
            }
            finally
            {
                reader.Close();
            }
        }

        [FunctionAttribute("func readstring (path : string) : string")]
        public HassiumString readstring(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();

            var stream = new FileStream(args[0].ToString(vm, location).String, FileMode.Open, FileAccess.Read, FileShare.Read);
            var reader = new StreamReader(stream);

            try
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                    sb.AppendLine(reader.ReadLine());

                return new HassiumString(sb.ToString());
            }
            finally
            {
                reader.Close();
            }
        }

        [FunctionAttribute("func writebytes (path : string, bytes : list) : null")]
        public HassiumNull writebytes(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm, location).String;

            var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            var writer = new BinaryWriter(stream);

            try
            {
                for (int i = 1; i < args.Length; i++)
                    writeHassiumObject(writer, args[i], vm, location);

                return Null;
            }
            finally
            {
                writer.Close();
            }
        }

        [FunctionAttribute("func writelines (path : string, lines : list) : null")]
        public HassiumNull writelines(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm, location).String;

            var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            var writer = new StreamWriter(stream);

            try
            {
                for (int i = 1; i < args.Length; i++)
                {
                    var type = args[i].Type();

                    if (type == HassiumList.TypeDefinition)
                        foreach (var item in args[i].ToList(vm, location).Values)
                            writer.WriteLine(item.ToString(vm, location).String);
                    else if (type == HassiumString.TypeDefinition)
                        writer.WriteLine(args[i].ToString(vm, location).String);
                    writer.Flush();
                }

                return Null;
            }
            finally
            {
                writer.Close();
            }
        }

        [FunctionAttribute("func writestring (path : string, str : string) : null")]
        public HassiumNull writestring(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            string path = args[0].ToString(vm, location).String;

            File.WriteAllText(path, args[1].ToString(vm, location).String);

            return Null;
        }

        private void writeHassiumObject(BinaryWriter writer, HassiumObject obj, VirtualMachine vm, SourceLocation location)
        {
            var type = obj.Type();

            if (type == HassiumBool.TypeDefinition)
                writer.Write(obj.ToBool(vm, location).Bool);
            else if (type == HassiumChar.TypeDefinition)
                writer.Write((byte)obj.ToChar(vm, location).Char);
            else if (type == HassiumFloat.TypeDefinition)
                writer.Write(obj.ToFloat(vm, location).Float);
            else if (type == HassiumInt.TypeDefinition)
                writer.Write(obj.ToInt(vm, location).Int);
            else if (type == HassiumList.TypeDefinition)
                foreach (var item in obj.ToList(vm, location).Values)
                    writeHassiumObject(writer, item, vm, location);
            else if (type == HassiumString.TypeDefinition)
                writer.Write(obj.ToString(vm, location).String);
            else if (type == HassiumTuple.TypeDefinition)
                foreach (var item in obj.ToTuple(vm, location).Values)
                    writeHassiumObject(writer, item, vm, location);
            writer.Flush();
        }
    }
}
