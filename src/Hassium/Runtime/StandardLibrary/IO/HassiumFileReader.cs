using System;
using System.IO;
using System.Text;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.IO
{
    public class HassiumFileReader: HassiumObject
    {
        public static HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("FileReader");

        public BinaryReader BinaryReader { get; set; }
        public HassiumFileReader()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 1));
            AddType(HassiumFileReader.TypeDefinition);
        }

        private HassiumFileReader _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumFileReader hassiumFileReader = new HassiumFileReader();

            if (args[0] is HassiumString)
                hassiumFileReader.BinaryReader = new BinaryReader(new StreamReader(HassiumString.Create(args[0]).Value).BaseStream);
            else if (args[0] is HassiumStream)
                hassiumFileReader.BinaryReader = new BinaryReader(((HassiumStream)args[0]).Stream);
            hassiumFileReader.Attributes.Add("endOfFile",   new HassiumProperty(hassiumFileReader.get_EndOfFile));
            hassiumFileReader.Attributes.Add("length",      new HassiumProperty(hassiumFileReader.get_Length));
            hassiumFileReader.Attributes.Add("position",    new HassiumProperty(hassiumFileReader.get_Position));
            hassiumFileReader.Attributes.Add("readBool",    new HassiumFunction(hassiumFileReader.readBool, 0));
            hassiumFileReader.Attributes.Add("readByte",    new HassiumFunction(hassiumFileReader.readByte, 0));
            hassiumFileReader.Attributes.Add("readChar",    new HassiumFunction(hassiumFileReader.readChar, 0));
            hassiumFileReader.Attributes.Add("readDouble",  new HassiumFunction(hassiumFileReader.readDouble, 0));
            hassiumFileReader.Attributes.Add("readInt16",   new HassiumFunction(hassiumFileReader.readInt16, 0));
            hassiumFileReader.Attributes.Add("readInt32",   new HassiumFunction(hassiumFileReader.readInt32, 0));
            hassiumFileReader.Attributes.Add("readInt64",   new HassiumFunction(hassiumFileReader.readInt64, 0));
            hassiumFileReader.Attributes.Add("readLine",    new HassiumFunction(hassiumFileReader.readLine, 0));
            hassiumFileReader.Attributes.Add("readString",  new HassiumFunction(hassiumFileReader.readString, 0));

            return hassiumFileReader;
        }
        public HassiumBool get_EndOfFile(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(BinaryReader.BaseStream.Position >= BinaryReader.BaseStream.Length);
        }
        public HassiumInt get_Length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(BinaryReader.BaseStream.Length);
        }
        public HassiumInt get_Position(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(BinaryReader.BaseStream.Position);
        }
        public HassiumBool readBool(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(BinaryReader.ReadBoolean());
        }
        public HassiumChar readByte(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumChar(Convert.ToChar(BinaryReader.ReadByte()));
        }
        public HassiumChar readChar(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumChar(BinaryReader.ReadChar());
        }
        public HassiumDouble readDouble(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(BinaryReader.ReadDouble());
        }
        public HassiumInt readInt16(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(BinaryReader.ReadInt16());
        }
        public HassiumInt readInt32(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(BinaryReader.ReadInt32());
        }
        public HassiumInt readInt64(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(BinaryReader.ReadInt64());
        }
        public HassiumString readLine(VirtualMachine vm, HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                char ch = readChar(vm, args).Value;
                if (ch != '\n')
                    sb.Append(ch);
                else
                    return new HassiumString(sb.ToString());
            }
        }
        public HassiumString readString(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(BinaryReader.ReadString());
        }
    }
}