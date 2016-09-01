using System;
using System.IO;
using System.Text;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.IO
{
    public class HassiumFileWriter: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("FileWriter");

        public BinaryWriter BinaryWriter { get; set; }
        public HassiumStream BaseStream { get; set; }

        public HassiumFileWriter()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 1);
        }

        private HassiumFileWriter _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumFileWriter fileWriter = new HassiumFileWriter();

            if (args[0] is HassiumString)
                fileWriter.BinaryWriter = new BinaryWriter(new StreamWriter(args[0].ToString(vm).String).BaseStream);
            else if (args[0] is HassiumStream)
                fileWriter.BinaryWriter = new BinaryWriter(((HassiumStream)args[0]).Stream);
            fileWriter.BaseStream = new HassiumStream(fileWriter.BinaryWriter.BaseStream);
            fileWriter.AddAttribute(HassiumObject.DISPOSE, fileWriter.Dispose, 0);
            fileWriter.AddAttribute("baseStream",   new HassiumProperty(get_baseStream));
            fileWriter.AddAttribute("endOfFile",    new HassiumProperty(get_endOfFile));
            fileWriter.AddAttribute("flush",        fileWriter.flush, 0);
            fileWriter.AddAttribute("length",       new HassiumProperty(get_length));
            fileWriter.AddAttribute("position",     new HassiumProperty(get_position, set_position));
            fileWriter.AddAttribute("write",        fileWriter.write, 1);
            fileWriter.AddAttribute("writeBool",    fileWriter.writeBool, 1);
            fileWriter.AddAttribute("writeChar",    fileWriter.writeChar, 1);
            fileWriter.AddAttribute("writeDouble",  fileWriter.writeDouble, 1);
            fileWriter.AddAttribute("writeInt16",   fileWriter.writeInt16, 1);
            fileWriter.AddAttribute("writeInt32",   fileWriter.writeInt32, 1);
            fileWriter.AddAttribute("writeInt64",   fileWriter.writeInt64, 1);
            fileWriter.AddAttribute("writeLine",    fileWriter.writeLine, 1);
            fileWriter.AddAttribute("writeList",    fileWriter.writeList, 1);
            fileWriter.AddAttribute("writeString",  fileWriter.writeString, 1);

            return fileWriter;
        }

        public HassiumStream get_baseStream(VirtualMachine vm, params HassiumObject[] args)
        {
            return BaseStream;
        }
        public HassiumBool get_endOfFile(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(BinaryWriter.BaseStream.Position < BinaryWriter.BaseStream.Length);
        }
        public HassiumInt get_length(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(BinaryWriter.BaseStream.Length);
        }
        public HassiumInt get_position(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(BinaryWriter.BaseStream.Position);
        }
        public HassiumNull set_position(VirtualMachine vm, params HassiumObject[] args)
        {
            BinaryWriter.BaseStream.Position = args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }
        public HassiumNull flush(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Flush();
            return HassiumObject.Null;
        }
        public HassiumInt get_Position(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(BinaryWriter.BaseStream.Position);
        }
        public HassiumNull write(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumBool)
                writeBool(vm, args);
            else if (args[0] is HassiumChar)
                writeChar(vm, args);
            else if (args[0] is HassiumFloat)
                writeDouble(vm, args);
            else if (args[0] is HassiumInt)
                writeInt64(vm, args);
            else if (args[0] is HassiumList)
                writeList(vm, args);
            else if (args[0] is HassiumString)
                writeString(vm, args);
            else
                throw new InternalException(vm, "Cannot write type {0}!", args[0].Type());
            return HassiumObject.Null;
        }
        public HassiumNull writeBool(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(args[0].ToBool(vm).Bool);
            return HassiumObject.Null;
        }
        public HassiumNull writeChar(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(args[0].ToChar(vm).Char);
            return HassiumObject.Null;
        }
        public HassiumNull writeDouble(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(args[0].ToFloat(vm).Float);
            return HassiumObject.Null;
        }
        public HassiumNull writeInt16(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write((Int16)args[0].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        public HassiumNull writeInt32(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write((Int32)args[0].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        public HassiumNull writeInt64(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write((Int64)args[0].ToInt(vm).Int);
            return HassiumObject.Null;
        }
        public HassiumNull writeLine(VirtualMachine vm, HassiumObject[] args)
        {
            string str = args[0].ToString(vm).String;
            foreach (char c in str)
                BinaryWriter.Write(c);
            BinaryWriter.Write('\r');
            BinaryWriter.Write('\n');
            return HassiumObject.Null;
        }
        public HassiumNull writeList(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in args[0].ToList(vm).List)
                write(vm, new HassiumObject[] { obj });
            return HassiumObject.Null;
        }
        public HassiumNull writeString(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }

        public override HassiumObject Dispose(VirtualMachine vm, params HassiumObject[] args)
        {
            BinaryWriter.Dispose();
            return HassiumObject.Null;
        }
    }
}