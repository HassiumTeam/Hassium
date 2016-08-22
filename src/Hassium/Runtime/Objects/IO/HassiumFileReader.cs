using System;
using System.IO;
using System.Text;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.IO
{
    public class HassiumFileReader: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("FileReader");

        public BinaryReader BinaryReader { get; set; }
        public HassiumStream BaseStream { get; set; }
        public HassiumFileReader()
        {
            AddType(TypeDefinition);
            Attributes.Add(HassiumObject.INVOKE, new HassiumFunction(_new, 1));
        }

        private HassiumFileReader _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumFileReader fileReader = new HassiumFileReader();

            if (args[0] is HassiumString)
                fileReader.BinaryReader = new BinaryReader(new StreamReader(args[0].ToString(vm).String).BaseStream);
            else if (args[0] is HassiumStream)
                fileReader.BinaryReader = new BinaryReader(((HassiumStream)args[0]).Stream);
            fileReader.BaseStream = new HassiumStream(fileReader.BinaryReader.BaseStream);
            fileReader.AddAttribute("baseStream",   new HassiumProperty(get_baseStream, set_baseStream));
            fileReader.AddAttribute("endOfFile",    new HassiumProperty(fileReader.get_endOfFile));
            fileReader.AddAttribute("length",       new HassiumProperty(fileReader.get_length));
            fileReader.AddAttribute("position",     new HassiumProperty(fileReader.get_position));
            fileReader.AddAttribute("readBool",     fileReader.readBool,    0);
            fileReader.AddAttribute("readByte",     fileReader.readByte,    0);
            fileReader.AddAttribute("readChar",     fileReader.readChar,    0);
            fileReader.AddAttribute("readFloat",    fileReader.readFloat,   0);
            fileReader.AddAttribute("readInt16",    fileReader.readInt16,   0);
            fileReader.AddAttribute("readInt32",    fileReader.readInt32,   0);
            fileReader.AddAttribute("readInt64",    fileReader.readInt64,   0);
            fileReader.AddAttribute("readLine",     fileReader.readLine,    0);
            fileReader.AddAttribute("readString",   fileReader.readString,  0);

            return fileReader;
        }
        public HassiumStream get_baseStream(VirtualMachine vm, HassiumObject[] args)
        {
            return BaseStream;
        }
        public HassiumNull set_baseStream(VirtualMachine vm, HassiumObject[] args)
        {
            BaseStream = args[0] as HassiumStream;
            return HassiumObject.Null;
        }
        public HassiumBool get_endOfFile(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(BinaryReader.BaseStream.Position >= BinaryReader.BaseStream.Length);
        }
        public HassiumInt get_length(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(BinaryReader.BaseStream.Length);
        }
        public HassiumInt get_position(VirtualMachine vm, HassiumObject[] args)
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
        public HassiumFloat readFloat(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumFloat(BinaryReader.ReadDouble());
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
                char ch = readChar(vm, args).Char;
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