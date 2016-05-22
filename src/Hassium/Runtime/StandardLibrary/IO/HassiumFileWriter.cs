using System;
using System.IO;
using System.Text;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.IO
{
    public class HassiumFileWriter: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("FileWriter");

        public BinaryWriter BinaryWriter { get; set; }
        public HassiumFileWriter()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 1));
            AddType(HassiumFileWriter.TypeDefinition);
        }

        private HassiumFileWriter _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumFileWriter hassiumFileWriter = new HassiumFileWriter();

            if (args[0] is HassiumString)
                hassiumFileWriter.BinaryWriter = new BinaryWriter(new StreamWriter(HassiumString.Create(args[0]).Value).BaseStream);
            else if (args[0] is HassiumStream)
                hassiumFileWriter.BinaryWriter = new BinaryWriter(((HassiumStream)args[0]).Stream);
            hassiumFileWriter.Attributes.Add("flush",       new HassiumFunction(hassiumFileWriter.flush, 0));
            hassiumFileWriter.Attributes.Add("position",    new HassiumProperty(hassiumFileWriter.get_Position));
            hassiumFileWriter.Attributes.Add("write",       new HassiumFunction(hassiumFileWriter.write, 1));
            hassiumFileWriter.Attributes.Add("writeBool",   new HassiumFunction(hassiumFileWriter.writeBool, 1));
            hassiumFileWriter.Attributes.Add("writeChar",   new HassiumFunction(hassiumFileWriter.writeChar, 1));
            hassiumFileWriter.Attributes.Add("writeDouble", new HassiumFunction(hassiumFileWriter.writeDouble, 1));
            hassiumFileWriter.Attributes.Add("writeInt16",  new HassiumFunction(hassiumFileWriter.writeInt16, 1));
            hassiumFileWriter.Attributes.Add("writeInt32",  new HassiumFunction(hassiumFileWriter.writeInt32, 1));
            hassiumFileWriter.Attributes.Add("writeInt64",  new HassiumFunction(hassiumFileWriter.writeInt64, 1));
            hassiumFileWriter.Attributes.Add("writeLine",   new HassiumFunction(hassiumFileWriter.writeLine, 1));
            hassiumFileWriter.Attributes.Add("writeList",   new HassiumFunction(hassiumFileWriter.writeList, 1));
            hassiumFileWriter.Attributes.Add("writeString", new HassiumFunction(hassiumFileWriter.writeString, 1));

            return hassiumFileWriter;
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
            else if (args[0] is HassiumDouble)
                writeDouble(vm, args);
            else if (args[0] is HassiumInt)
                writeInt64(vm, args);
            else if (args[0] is HassiumList)
                writeList(vm, args);
            else if (args[0] is HassiumString)
                writeString(vm, args);
            else
                throw new InternalException("Cannot write type " + args[0].GetType().Name);
            return HassiumObject.Null;
        }
        public HassiumNull writeBool(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(HassiumBool.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull writeChar(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(HassiumChar.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull writeDouble(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(HassiumDouble.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull writeInt16(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write((Int16)HassiumInt.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull writeInt32(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write((Int32)HassiumInt.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull writeInt64(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write((Int64)HassiumInt.Create(args[0]).Value);
            return HassiumObject.Null;
        }
        public HassiumNull writeLine(VirtualMachine vm, HassiumObject[] args)
        {
            string str = HassiumString.Create(args[0]).Value;
            foreach (char c in str)
                BinaryWriter.Write(c);
            BinaryWriter.Write('\r');
            BinaryWriter.Write('\n');
            return HassiumObject.Null;
        }
        public HassiumNull writeList(VirtualMachine vm, HassiumObject[] args)
        {
            foreach (HassiumObject obj in HassiumList.Create(args[0]).Value)
                write(vm, new HassiumObject[] { obj });
            return HassiumObject.Null;
        }
        public HassiumNull writeString(VirtualMachine vm, HassiumObject[] args)
        {
            BinaryWriter.Write(HassiumString.Create(args[0]).Value);
            return HassiumObject.Null;
        }
    }
}