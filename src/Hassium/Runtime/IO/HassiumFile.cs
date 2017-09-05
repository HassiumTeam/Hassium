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

        [DocStr(
            "@desc A class representing a File object.",
            "@returns File."
            )]
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

            [DocStr(
                "@desc Gets the readonly absolute path string.",
                "@returns The absolute path string."
                )]
            [FunctionAttribute("abspath { get; }")]
            public static HassiumString get_abspath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumFile).AbsolutePath;
            }

            [DocStr(
                "@desc Gets the mutable bool indicating if the file stream will autoflush.",
                "@returns True if the stream will automatically flush, otherwise false."
                )]
            [FunctionAttribute("autoflush { get; }")]
            public static HassiumBool get_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool((self as HassiumFile).autoFlush);
            }

            [DocStr(
                "@desc Sets the mutable bool determining if the file stream will autoflush.",
                "@returns null."
                )]
            [FunctionAttribute("autoflush { set; }")]
            public static HassiumNull set_autoflush(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumFile).autoFlush = args[0].ToBool(vm, args[0], location).Bool;

                return Null;
            }

            [DocStr(
                "@desc Closes the file stream.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Copies this file to the specified file path.",
                "@param path The string path to be copied to.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Deletes this file from the disc.",
                "@returns null."
                )]
            [FunctionAttribute("func delete () : null")]
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

            [DocStr(
                "@desc Gets the readonly bool indicating if the file exists on disc.",
                "@returns True if the file exists, otherwise false."
                )]
            [FunctionAttribute("exists { get; }")]
            public static HassiumBool get_exists(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool(File.Exists((self as HassiumFile).AbsolutePath.String));
            }

            [DocStr(
                "@desc Gets the mutable string of this file's extension.",
                "@returns This file's extension as string."
                )]
            [FunctionAttribute("extension { get; }")]
            public static HassiumString get_extension(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString((self as HassiumFile).FileInfo.Extension);
            }
            [DocStr(
                "@desc Sets the mutable string extension for this file.",
                "@returns null."
                )]
            [FunctionAttribute("extension { set; }")]
            public static HassiumNull set_extension(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                moveto(vm, self, location, new HassiumString(Path.ChangeExtension((self as HassiumFile).AbsolutePath.String, args[0].ToString(vm, args[0], location).String)));

                return Null;
            }

            [DocStr(
                "@desc Flushes this file stream.",
                "@returns null."
                )]
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
                file.StreamWriter.Flush();
                return Null;
            }

            [DocStr(
                "@desc Gets the readonly bool indicating if this file has been closed.",
                "@returns True if the file has been closed, otherwise false."
                )]
            [FunctionAttribute("isclosed { get; }")]
            public static HassiumBool get_isclosed(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumBool((self as HassiumFile).closed);
            }

            [DocStr(
                "@desc Gets the readonly int that represents the size of the file in bytes.",
                "@returns The size of the file in bytes as an int."
                )]
            [FunctionAttribute("length { get; }")]
            public static HassiumInt get_length(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumFile).Reader.BaseStream.Length);
            }

            [DocStr(
                "@desc Moves this file to the specified file path.",
                "@param path The string path to be moved to.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Gets the mutable string containing the name of the file.",
                "@returns The name of this file."
                )]
            [FunctionAttribute("name { get; }")]
            public static HassiumString get_name(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(Path.GetFileName((self as HassiumFile).AbsolutePath.String));
            }
            [DocStr(
                "@desc Sets the mutable string that sets the name of the file.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Gets the mutable int that represents the current position in the file stream.",
                "@returns The current position as int."
                )]
            [FunctionAttribute("position { get; }")]
            public static HassiumInt get_position(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumFile).Reader.BaseStream.Position);
            }
            [DocStr(
                "@desc Sets the mutable int that changes the position in the file stream.",
                "@returns null."
                )]
            [FunctionAttribute("position { set; }")]
            public static HassiumNull set_position(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                (self as HassiumFile).Reader.BaseStream.Position = args[0].ToInt(vm, args[0], location).Int;
                return Null;
            }

            [DocStr(
                "@desc Reads all of the bytes from this file and returns them in a list of chars.",
                "@returns A list of chars representing each line of the file."
                )]
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

                file.Reader.BaseStream.Position = 0;
                while (file.Reader.BaseStream.Position < file.Reader.BaseStream.Length)
                    list.Values.Add(file.Reader.ReadBytes(1)[0]);

                return list;
            }

            [DocStr(
                "@desc Reads all of the lines from this file and returns them in a list of strings.",
                "@returns A list of strings representing each line of the file."
                )]
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

                file.Reader.BaseStream.Position = 0;
                while (file.Reader.BaseStream.Position < file.Reader.BaseStream.Length)
                    HassiumList.add(vm, list, location, readline(vm, self, location));

                return list;
            }
            
            [DocStr(
                "@desc Reads all of the characters from this file and returns them as a single string.",
                "@returns The file as a string."
                )]
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

                file.Reader.BaseStream.Position = 0;
                while (file.Reader.BaseStream.Position < file.Reader.BaseStream.Length)
                {
                    var line = readline(vm, self, location);
                    sb.AppendLine(line.ToString(vm, line, location).String);
                }

                return new HassiumString(sb.ToString());
            }

            [DocStr(
                "@desc Reads a single byte from the stream and returns it as a char.",
                "@returns The byte as char."
                )]
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

            [DocStr(
                "@desc Reads the specified count of bytes from the stream and returns them in a list.",
                "@param count The amount of bytes to read.",
                "@returns A list containing the specified amount of bytes."
                )]
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

            [DocStr(
                "@desc Reads a single float from the stream and returns it.",
                "@returns The read float."
                )]
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

            [DocStr(
                "@desc Reads a single 32-bit integer from the stream and returns it.",
                "@returns The read 32-bit int."
                )]
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

            [DocStr(
                "@desc Reads a line from the stream and returns it as a string.",
                "@returns The read line string."
                )]
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

            [DocStr(
                "@desc Reads a single 64-bit integer from the stream and returns it.",
                "@returns The read 64-bit int."
                )]
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

            [DocStr(
                "@desc Reads a single 16-bit integer from the stream and returns it.",
                "@returns The read 16-bit int."
                )]
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

            [DocStr(
                "@desc Reads a single string from the stream and returns it.",
                "@returns The read string."
                )]
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

            [DocStr(
                "@desc Gets the readonly relative path of this file as a string.",
                "@returns The relative path of the file as string."
                )]
            [FunctionAttribute("relpath{ get; }")]
            public static HassiumString get_relpath(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return (self as HassiumFile).RelativePath;
            }

            [DocStr(
                "@desc Gets the readonly int that represents the size of the file in bytes.",
                "@returns The size of the file in bytes as an int."
                )]
            [FunctionAttribute("size { get; }")]
            public static HassiumInt get_size(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumInt((self as HassiumFile).FileInfo.Length);
            }

            [DocStr(
                "@desc Writes all of the bytes in the given list to the file stream.",
                "@param bytes The list of bytes to write.",
                "@returns null."
                )]
            [FunctionAttribute("func writeallbytes (bytes : list) : null")]
            public static HassiumNull writeallbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.BaseStream.Position = 0;
                for (int i = 0; i < args.Length; i++)
                    writeHassiumObject(file.Writer, args[i], vm, location);

                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [DocStr(
                "@desc Writes all of the string lines to the file stream.",
                "@param lines The list of lines to write.",
                "@returns null."
                )]
            [FunctionAttribute("func writealllines (lines : list) : null")]
            public static HassiumNull writealllines(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.BaseStream.Position = 0;
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

            [DocStr(
                "@desc Writes the specified string as the file contents.",
                "@param str The string that will become the file contents.",
                "@returns null."
                )]
            [FunctionAttribute("func writealltext (str : string) : null")]
            public static HassiumNull writealltext(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var file = (self as HassiumFile);
                if (file.closed)
                {
                    vm.RaiseException(HassiumFileClosedException.FileClosedExceptionTypeDef._new(vm, null, location, self, get_abspath(vm, self, location)));
                    return Null;
                }

                file.Writer.BaseStream.Position = 0;
                foreach (var c in args[0].ToString(vm, args[0], location).String)
                    file.Writer.Write(c);
                if (file.autoFlush)
                    file.Writer.Flush();
                return Null;
            }

            [DocStr(
                "@desc Writes the given single byte to the file stream.",
                "@param b The char to write.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Writes the given single float to the file stream.",
                "@param f The float to write.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Writes the given single 32-bit integer to the file stream.",
                "@param i The 32-bit int to write.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Writes the given string line to the file stream, followed by a newline.",
                "@param str The string to write.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Writes the byte value of each element in the given list to the file stream.",
                "@param l The list to write.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Writes the given 64-bit integer to the file stream.",
                "@param l The 64-bit int to write.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Writes the given 16-bit integer to the file stream.",
                "@param s The 16-bit int to write.",
                "@returns null."
                )]
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

            [DocStr(
                "@desc Writes the given string to the file stream.",
                "@param str The string to write.",
                "@returns null."
                )]
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
