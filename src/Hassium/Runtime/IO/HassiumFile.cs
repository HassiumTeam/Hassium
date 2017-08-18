using System.IO;
using System.Text;

using Hassium.Compiler;
using Hassium.Runtime.Types;

namespace Hassium.Runtime.IO
{
    public class HassiumFile : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("File");

        public HassiumString AbsolutePath { get; private set; }
        public HassiumString RelativePath { get; private set; }

        public FileInfo FileInfo { get; private set; }

        public BinaryReader Reader { get; set; }
        public BinaryWriter Writer { get; set; }

        public StreamReader StreamReader { get; set; }
        public StreamWriter StreamWriter { get; set; }

        private bool closed = false;
        private bool autoFlush = true;

        public HassiumFile(string path)
        {
            AddType(TypeDefinition);

            AbsolutePath = new HassiumString(Path.GetFullPath(path));
            RelativePath = new HassiumString(path);

            FileInfo = new FileInfo(path);
            var fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Reader = new BinaryReader(fs);
            Writer = new BinaryWriter(fs);

            StreamReader = new StreamReader(fs);
            StreamWriter = new StreamWriter(fs);

            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumFile file)
        {
            file.AddAttribute("abspath", new HassiumProperty(file.get_abspath));
            file.AddAttribute("autoflush", new HassiumProperty(file.get_autoflush, file.set_autoflush));
            file.AddAttribute("close", file.close, 0);
            file.AddAttribute("copyto", file.copyto, 1);
            file.AddAttribute("exists", new HassiumProperty(file.get_exists));
            file.AddAttribute("extension", new HassiumProperty(file.get_extension, file.set_extension));
            file.AddAttribute("flush", file.flush, 0);
            file.AddAttribute("isclosed", new HassiumProperty(file.get_isclosed));
            file.AddAttribute("length", new HassiumProperty(file.get_length));
            file.AddAttribute("moveTo", file.moveto, 1);
            file.AddAttribute("name", new HassiumProperty(file.get_name, file.set_name));
            file.AddAttribute("position", new HassiumProperty(file.get_position, file.set_position));
            file.AddAttribute("readallbytes", file.readallbytes, 0);
            file.AddAttribute("readalllines", file.readalllines, 0);
            file.AddAttribute("readalltext", file.readalltext, 0);
            file.AddAttribute("readbyte", file.readbyte, 0);
            file.AddAttribute("readlist", file.readbytes, 1);
            file.AddAttribute("readint", file.readint, 0);
            file.AddAttribute("readline", file.readline, 0);
            file.AddAttribute("readlong", file.readlong, 0);
            file.AddAttribute("readshort", file.readshort, 0);
            file.AddAttribute("readstring", file.readstring, 0);
            file.AddAttribute("relpath", new HassiumProperty(file.get_relpath));
            file.AddAttribute("size", new HassiumProperty(file.get_size));
            file.AddAttribute("writeallbytes", file.writeallbytes, 1);
            file.AddAttribute("writealllines", file.writealllines, -1);
            file.AddAttribute("writealltext", file.writealltext, 1);
            file.AddAttribute("writebyte", file.writebyte, 1);
            file.AddAttribute("writefloat", file.writefloat, 1);
            file.AddAttribute("writeint", file.writeint, 1);
            file.AddAttribute("writeline", file.writeline, 1);
            file.AddAttribute("writelist", file.writelist, 1);
            file.AddAttribute("writelong", file.writelong, 1);
            file.AddAttribute("writeshort", file.writeshort, 1);
            file.AddAttribute("writestring", file.writestring, 1);
        }

        [FunctionAttribute("abspath { get; }")]
        public HassiumString get_abspath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return AbsolutePath;
        }

        [FunctionAttribute("autoflush { get; }")]
        public HassiumBool get_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(autoFlush);
        }
        [FunctionAttribute("autoflush { set; }")]
         public HassiumNull set_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            autoFlush = args[0].ToBool(vm, args[0], location).Bool;

            return Null;
        }

        [FunctionAttribute("func close () : null")]
        public HassiumNull close(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            try
            {
                Reader.Close();
                Writer.Close();
                return Null;
            }
            finally
            {
                closed = true;
            }
        }

        [FunctionAttribute("func copyto (path : string) : null")]
        public HassiumNull copyto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            File.Copy(AbsolutePath.String, args[0].ToString(vm, args[0], location).String);

            return Null;
        }

        [FunctionAttribute("func delete (path : string) : null")]
        public HassiumNull delete(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            File.Delete(AbsolutePath.String);

            return Null;
        }

        [FunctionAttribute("exists { get; }")]
        public HassiumBool get_exists(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(File.Exists(AbsolutePath.String));
        }

        [FunctionAttribute("extension { get; }")]
        public HassiumString get_extension(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(FileInfo.Extension);
        }
        [FunctionAttribute("extension { set; }")]
        public HassiumNull set_extension(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            moveto(vm, self, location, new HassiumString(Path.ChangeExtension(AbsolutePath.String, args[0].ToString(vm, args[0], location).String)));

            return Null;
        }

        [FunctionAttribute("func flush () : null")]
        public HassiumNull flush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            Writer.Flush();
            return Null;
        }

        [FunctionAttribute("isclosed { get; }")]
        public HassiumBool get_isclosed(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(closed);
        }

        [FunctionAttribute("length { get; }")]
        public HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Reader.BaseStream.Length);
        }

        [FunctionAttribute("func moveto (path : string) : null")]
        public HassiumNull moveto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            File.Move(AbsolutePath.String, args[0].ToString(vm, args[0], location).String);

            return Null;
        }

        [FunctionAttribute("name { get; }")]
        public HassiumString get_name(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Path.GetFileName(AbsolutePath.String));
        }
        [FunctionAttribute("name { get; }")]
        public HassiumNull set_name(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            File.Move(AbsolutePath.String, args[0].ToString(vm, args[0], location).String);

            return Null;
        }

        [FunctionAttribute("position { get; }")]
        public HassiumInt get_position(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Reader.BaseStream.Position);
        }
        [FunctionAttribute("position { set; }")]
        public HassiumNull set_position(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Reader.BaseStream.Position = args[0].ToInt(vm, args[0], location).Int;
            return Null;
        }

        [FunctionAttribute("func readallbytes () : list")]
        public HassiumObject readallbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            HassiumByteArray list = new HassiumByteArray(new byte[0], new HassiumObject[0]);

            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
                list.Values.Add(Reader.ReadBytes(1)[0]);

            return list;
        }

        [FunctionAttribute("func readalllines () : list")]
        public HassiumObject readalllines(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            HassiumList list = new HassiumList(new HassiumObject[0]);

            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
                HassiumList.add(vm, list, location, readline(vm, self, location));

            return list;
        }

        [FunctionAttribute("func readalltext () : string")]
        public HassiumObject readalltext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            StringBuilder sb = new StringBuilder();

            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                var line = readline(vm, self, location);
                sb.AppendLine(line.ToString(vm, line, location).String);
            }

            return new HassiumString(sb.ToString());
        }

        [FunctionAttribute("func readbyte () : char")]
        public HassiumObject readbyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            return new HassiumChar((char)Reader.ReadBytes(1)[0]);
        }

        [FunctionAttribute("func readbytes (count : int) : list")]
        public HassiumObject readbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            HassiumList list = new HassiumList(new HassiumObject[0]);
            int count = (int)args[0].ToInt(vm, args[0], location).Int;
            for (int i = 0; i < count; i++)
                HassiumList.add(vm, list, location, new HassiumChar((char)Reader.ReadBytes(1)[0]));

            return list;
        }

        [FunctionAttribute("func readfloat () : float")]
        public HassiumObject readfloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            return new HassiumFloat(Reader.ReadDouble());
        }

        [FunctionAttribute("func readint () : int")]
        public HassiumObject readint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            return new HassiumInt(Reader.ReadInt32());
        }

        [FunctionAttribute("func readline () : string")]
        public HassiumObject readline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            return new HassiumString(StreamReader.ReadLine());
        }

        [FunctionAttribute("func readlong () : int")]
        public HassiumObject readlong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            return new HassiumInt(Reader.ReadInt64());
        }

        [FunctionAttribute("func readshort () : int")]
        public HassiumObject readshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            return new HassiumInt(Reader.ReadInt16());
        }

        [FunctionAttribute("func readstring () : string")]
        public HassiumObject readstring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (!File.Exists(AbsolutePath.String))
            {
                vm.RaiseException(HassiumFileNotFoundException.Attribs[INVOKE].Invoke(vm, location, AbsolutePath));
                return Null;
            }

            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            return new HassiumString(Reader.ReadString());
        }

        [FunctionAttribute("relpath{ get; }")]
        public HassiumString get_relpath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return RelativePath;
        }

        [FunctionAttribute("size { get; }")]
        public HassiumInt get_size(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(FileInfo.Length);
        }
        
        [FunctionAttribute("func writeallbytes (bytes : list) : null")]
        public HassiumNull writeallbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            for (int i = 0; i < args.Length; i++)
                writeHassiumObject(Writer, args[i], vm, location);

            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writealllines (lines : list) : null")]
        public HassiumNull writealllines(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            for (int i = 0; i < args.Length; i++)
            {
                var type = args[i].Type();

                if (type == HassiumList.TypeDefinition)
                    foreach (var item in args[i].ToList(vm, args[i], location).Values)
                        writeline(vm, self, location, item.ToString(vm, item, location));
                else if (type == HassiumString.TypeDefinition)
                    writeline(vm, self, location, args[i].ToString(vm, args[i], location));
            }

            if (autoFlush)
                Writer.Flush();
            return Null;
        }
        
        [FunctionAttribute("func writealltext (str : string) : null")]
        public HassiumNull writealltext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            foreach (var c in args[0].ToString(vm, args[0], location).String)
                Writer.Write(c);
            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writebyte (b : char) : null")]
        public HassiumNull writebyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            Writer.Write((byte)args[0].ToChar(vm, args[0], location).Char);
            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writefloat (f : float) : null")]
        public HassiumNull writefloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            Writer.Write(args[0].ToFloat(vm, args[0], location).Float);
            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writeint (i : int) : null")]
        public HassiumNull writeint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            Writer.Write((int)args[0].ToInt(vm, args[0], location).Int);
            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writeline (str : string) : null")]
        public HassiumNull writeline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            string str = args[0].ToString(vm, args[0], location).String;

            StreamWriter.WriteLine(str);

            if (autoFlush)
                StreamWriter.Flush();
            return Null;
        }

        [FunctionAttribute("func writelist (l : list) : null")]
        public HassiumNull writelist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            foreach (var i in args[0].ToList(vm, args[0], location).Values)
                writeHassiumObject(Writer, i, vm, location);

            return Null;
        }

        [FunctionAttribute("func writelong (l : int) : null")]
        public HassiumNull writelong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            Writer.Write(args[0].ToInt(vm, args[0], location).Int);
            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writeshort (s : int) : null")]
        public HassiumNull writeshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            Writer.Write((short)args[0].ToInt(vm, args[0], location).Int);
            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        [FunctionAttribute("func writestring (str : string) : null")]
        public HassiumNull writestring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            if (closed)
            {
                vm.RaiseException(HassiumFileClosedException.Attribs[INVOKE].Invoke(vm, location, this, get_abspath(vm, self, location)));
                return Null;
            }

            Writer.Write(args[0].ToString(vm, args[0], location).String);
            if (autoFlush)
                Writer.Flush();
            return Null;
        }

        private void writeHassiumObject(BinaryWriter writer, HassiumObject obj, VirtualMachine vm, SourceLocation location)
        {
            var type = obj.Type();

            if (type == HassiumBool.TypeDefinition)
                writer.Write(obj.ToBool(vm, obj, location).Bool);
            else if (type == HassiumChar.TypeDefinition)
                writer.Write((byte)obj.ToChar(vm, obj, location).Char);
            else if (type == HassiumFloat.TypeDefinition)
                writer.Write(obj.ToFloat(vm, obj, location).Float);
            else if (type == HassiumInt.TypeDefinition)
                writer.Write(obj.ToInt(vm, obj, location).Int);
            else if (type == HassiumList.TypeDefinition)
                foreach (var item in obj.ToList(vm, obj, location).Values)
                    writeHassiumObject(writer, item, vm, location);
            else if (type == HassiumString.TypeDefinition)
                writer.Write(obj.ToString(vm, obj, location).String);
            else if (type == HassiumTuple.TypeDefinition)
                foreach (var item in obj.ToTuple(vm, obj, location).Values)
                    writeHassiumObject(writer, item, vm, location);
        }
    }
}
