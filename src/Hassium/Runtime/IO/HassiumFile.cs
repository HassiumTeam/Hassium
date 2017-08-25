using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hassium.Runtime.IO
{
    public class HassiumFile : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new FileTypeDef();

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
        }

        public class FileTypeDef : HassiumTypeDefinition
        {
            public FileTypeDef() : base("File")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "abspath", new HassiumProperty(get_abspath)  },
                    { "autoflush", new HassiumProperty(get_autoflush, set_autoflush)  },
                    { "close", new HassiumFunction(close, 0)  },
                    { "copyto", new HassiumFunction(copyto, 1)  },
                    { "exists", new HassiumProperty(get_exists)  },
                    { "extension", new HassiumProperty(get_extension, set_extension)  },
                    { "flush", new HassiumFunction(flush, 0)  },
                    { "isclosed", new HassiumProperty(get_isclosed)  },
                    { "length", new HassiumProperty(get_length)  },
                    { "moveTo", new HassiumFunction(moveto, 1)  },
                    { "name", new HassiumProperty(get_name, set_name)  },
                    { "position", new HassiumProperty(get_position, set_position)  },
                    { "readallbytes", new HassiumFunction(readallbytes, 0)  },
                    { "readalllines", new HassiumFunction(readalllines, 0)  },
                    { "readalltext", new HassiumFunction(readalltext, 0)  },
                    { "readbyte", new HassiumFunction(readbyte, 0)  },
                    { "readlist", new HassiumFunction(readbytes, 1)  },
                    { "readint", new HassiumFunction(readint, 0)  },
                    { "readline", new HassiumFunction(readline, 0)  },
                    { "readlong", new HassiumFunction(readlong, 0)  },
                    { "readshort", new HassiumFunction(readshort, 0)  },
                    { "readstring", new HassiumFunction(readstring, 0)  },
                    { "relpath", new HassiumProperty(get_relpath)  },
                    { "size", new HassiumProperty(get_size)  },
                    { "writeallbytes", new HassiumFunction(writeallbytes, 1)  },
                    { "writealllines", new HassiumFunction(writealllines, -1)  },
                    { "writealltext", new HassiumFunction(writealltext, 1)  },
                    { "writebyte", new HassiumFunction(writebyte, 1)  },
                    { "writefloat", new HassiumFunction(writefloat, 1)  },
                    { "writeint", new HassiumFunction(writeint, 1)  },
                    { "writeline", new HassiumFunction(writeline, 1)  },
                    { "writelist", new HassiumFunction(writelist, 1)  },
                    { "writelong", new HassiumFunction(writelong, 1)  },
                    { "writeshort", new HassiumFunction(writeshort, 1)  },
                    { "writestring", new HassiumFunction(writestring, 1)  }
                };
            }

            [FunctionAttribute("abspath { get; }")]
            public static HassiumString get_abspath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumFile).AbsolutePath;
            }

            [FunctionAttribute("autoflush { get; }")]
            public static HassiumBool get_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool((self as HassiumFile).autoFlush);
            }
            [FunctionAttribute("autoflush { set; }")]
            public static HassiumNull set_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumFile).autoFlush = args[0].ToBool(vm, args[0], location).Bool;

                return Null;
            }

            [FunctionAttribute("func close () : null")]
            public static HassiumNull close(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                try
                {
                    file.Reader.Close();
                    file.Writer.Close();
                    return Null;
                }
                finally
                {
                    file.closed = true;
                }
            }

            [FunctionAttribute("func copyto (path : string) : null")]
            public static HassiumNull copyto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var AbsolutePath = (self as HassiumFile).AbsolutePath;
                if (!File.Exists(AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, AbsolutePath));
                    return Null;
                }

                File.Copy(AbsolutePath.String, args[0].ToString(vm, args[0], location).String);

                return Null;
            }

            [FunctionAttribute("func delete (path : string) : null")]
            public static HassiumNull delete(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var AbsolutePath = (self as HassiumFile).AbsolutePath;
                if (!File.Exists(AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, AbsolutePath));
                    return Null;
                }

                File.Delete(AbsolutePath.String);

                return Null;
            }

            [FunctionAttribute("exists { get; }")]
            public static HassiumBool get_exists(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool(File.Exists((self as HassiumFile).AbsolutePath.String));
            }

            [FunctionAttribute("extension { get; }")]
            public static HassiumString get_extension(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString((self as HassiumFile).FileInfo.Extension);
            }
            [FunctionAttribute("extension { set; }")]
            public static HassiumNull set_extension(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                moveto(vm, self, location, new HassiumString(Path.ChangeExtension((self as HassiumFile).AbsolutePath.String, args[0].ToString(vm, args[0], location).String)));

                return Null;
            }

            [FunctionAttribute("func flush () : null")]
            public static HassiumNull flush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("isclosed { get; }")]
            public static HassiumBool get_isclosed(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool((self as HassiumFile).closed);
            }

            [FunctionAttribute("length { get; }")]
            public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumFile).Reader.BaseStream.Length);
            }

            [FunctionAttribute("func moveto (path : string) : null")]
            public static HassiumNull moveto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var AbsolutePath = (self as HassiumFile).AbsolutePath;
                if (!File.Exists(AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, AbsolutePath));
                    return Null;
                }

                File.Move(AbsolutePath.String, args[0].ToString(vm, args[0], location).String);

                return Null;
            }

            [FunctionAttribute("name { get; }")]
            public static HassiumString get_name(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Path.GetFileName((self as HassiumFile).AbsolutePath.String));
            }
            [FunctionAttribute("name { get; }")]
            public static HassiumNull set_name(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                if (!File.Exists((self as HassiumFile).AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, (self as HassiumFile).AbsolutePath));
                    return Null;
                }

                File.Move((self as HassiumFile).AbsolutePath.String, args[0].ToString(vm, args[0], location).String);

                return Null;
            }

            [FunctionAttribute("position { get; }")]
            public static HassiumInt get_position(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumFile).Reader.BaseStream.Position);
            }
            [FunctionAttribute("position { set; }")]
            public static HassiumNull set_position(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumFile).Reader.BaseStream.Position = args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [FunctionAttribute("func readallbytes () : list")]
            public static HassiumObject readallbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                HassiumByteArray list = new HassiumByteArray(new byte[0], new HassiumObject[0]);

                while (file.Reader.BaseStream.Position < file.Reader.BaseStream.Length)
                    list.Values.Add(file.Reader.ReadBytes(1)[0]);

                return list;
            }

            [FunctionAttribute("func readalllines () : list")]
            public static HassiumObject readalllines(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                HassiumList list = new HassiumList(new HassiumObject[0]);

                while (file.Reader.BaseStream.Position < file.Reader.BaseStream.Length)
                    HassiumList.add(vm, list, location, readline(vm, self, location));

                return list;
            }

            [FunctionAttribute("func readalltext () : string")]
            public static HassiumObject readalltext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                StringBuilder sb = new StringBuilder();

                while (file.Reader.BaseStream.Position < file.Reader.BaseStream.Length)
                {
                    var line = readline(vm, self, location);
                    sb.AppendLine(line.ToString(vm, line, location).String);
                }

                return new HassiumString(sb.ToString());
            }

            [FunctionAttribute("func readbyte () : char")]
            public static HassiumObject readbyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                return new HassiumChar((char)file.Reader.ReadBytes(1)[0]);
            }

            [FunctionAttribute("func readbytes (count : int) : list")]
            public static HassiumObject readbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                HassiumList list = new HassiumList(new HassiumObject[0]);
                int count = (int)args[0].ToInt(vm, args[0], location).Int;
                for (int i = 0; i < count; i++)
                    HassiumList.add(vm, list, location, new HassiumChar((char)file.Reader.ReadBytes(1)[0]));

                return list;
            }

            [FunctionAttribute("func readfloat () : float")]
            public static HassiumObject readfloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                return new HassiumFloat(file.Reader.ReadDouble());
            }

            [FunctionAttribute("func readint () : int")]
            public static HassiumObject readint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                return new HassiumInt(file.Reader.ReadInt32());
            }

            [FunctionAttribute("func readline () : string")]
            public static HassiumObject readline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                return new HassiumString(file.StreamReader.ReadLine());
            }

            [FunctionAttribute("func readlong () : int")]
            public static HassiumObject readlong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                return new HassiumInt(file.Reader.ReadInt64());
            }

            [FunctionAttribute("func readshort () : int")]
            public static HassiumObject readshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                return new HassiumInt(file.Reader.ReadInt16());
            }

            [FunctionAttribute("func readstring () : string")]
            public static HassiumObject readstring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (!File.Exists(file.AbsolutePath.String))
                {
                    vm.RaiseException(HassiumFileNotFoundException.FileNotFoundExceptionTypeDef._new(vm, null, location, file.AbsolutePath));
                    return Null;
                }

                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                return new HassiumString(file.Reader.ReadString());
            }

            [FunctionAttribute("relpath{ get; }")]
            public static HassiumString get_relpath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumFile).RelativePath;
            }

            [FunctionAttribute("size { get; }")]
            public static HassiumInt get_size(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumFile).FileInfo.Length);
            }

            [FunctionAttribute("func writeallbytes (bytes : list) : null")]
            public static HassiumNull writeallbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                for (int i = 0; i < args.Length; i++)
                    writeHassiumObject(file.Writer, args[i], vm, location);

                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writealllines (lines : list) : null")]
            public static HassiumNull writealllines(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
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

                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writealltext (str : string) : null")]
            public static HassiumNull writealltext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                foreach (var c in args[0].ToString(vm, args[0], location).String)
                    file.Writer.Write(c);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writebyte (b : char) : null")]
            public static HassiumNull writebyte(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.Write((byte)args[0].ToChar(vm, args[0], location).Char);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writefloat (f : float) : null")]
            public static HassiumNull writefloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.Write(args[0].ToFloat(vm, args[0], location).Float);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writeint (i : int) : null")]
            public static HassiumNull writeint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.Write((int)args[0].ToInt(vm, args[0], location).Int);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writeline (str : string) : null")]
            public static HassiumNull writeline(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                string str = args[0].ToString(vm, args[0], location).String;

                file.StreamWriter.WriteLine(str);

                if (file.autoFlush)
                    file.StreamWriter.Flush();
                return Null;
            }

            [FunctionAttribute("func writelist (l : list) : null")]
            public static HassiumNull writelist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                foreach (var i in args[0].ToList(vm, args[0], location).Values)
                    writeHassiumObject(file.Writer, i, vm, location);

                return Null;
            }

            [FunctionAttribute("func writelong (l : int) : null")]
            public static HassiumNull writelong(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.Write(args[0].ToInt(vm, args[0], location).Int);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writeshort (s : int) : null")]
            public static HassiumNull writeshort(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.Write((short)args[0].ToInt(vm, args[0], location).Int);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [FunctionAttribute("func writestring (str : string) : null")]
            public static HassiumNull writestring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.Write(args[0].ToString(vm, args[0], location).String);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            private static void writeHassiumObject(BinaryWriter writer, HassiumObject obj, VirtualMachine vm, SourceLocation location)
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

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
