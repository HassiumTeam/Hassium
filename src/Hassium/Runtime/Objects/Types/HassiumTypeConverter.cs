using System;
using System.Text;

namespace Hassium.Runtime.Objects.Types
{
	public class HassiumTypeConverter: HassiumObject
	{
		public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("TypeConverter");

		public HassiumTypeConverter()
		{
			AddType(TypeDefinition);
            AddAttribute("getBytes",    getBytes,      1);
            AddAttribute("toBool",      toBool,     1, 2);
            AddAttribute("toChar",      toChar,     1, 2);
            AddAttribute("toFloat",     toFloat,    1, 2);
            AddAttribute("toInt16",     toInt16,    1, 2);
            AddAttribute("toInt32",     toInt32,    1, 2);
            AddAttribute("toInt64",     toInt64,    1, 2);
            AddAttribute("toString",    toString,   1, 2);
		}

		public HassiumList getBytes(VirtualMachine vm, params HassiumObject[] args)
		{
			HassiumList result = new HassiumList(new HassiumObject[0]);
			byte[] bytes;
            switch (args[0].Type().TypeName)
            {
                case "bool":
                    bytes = BitConverter.GetBytes(args[0].ToBool(vm).Bool);
                    break;
                case "char":
                    bytes = BitConverter.GetBytes(args[0].ToChar(vm).Char);
                    break;
                case "float":
                    bytes = BitConverter.GetBytes(args[0].ToFloat(vm).Float);
                    break;
                case "int":
                    bytes = BitConverter.GetBytes(args[0].ToInt(vm).Int);
                    break;
                case "string":
                    bytes = ASCIIEncoding.ASCII.GetBytes(args[0].ToString(vm).String);
                    break;
                default:
                    throw new InternalException(vm, "Non-convertable type: {0}!", args[0].Type().TypeName);
            }
            foreach (byte b in bytes)
                result.add(vm, new HassiumChar((char)b));
			return result;
		}

        public HassiumBool toBool(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumBool(BitConverter.ToBoolean(listToByteArray(vm, args[0].ToList(vm)), args.Length == 2 ? (int)args[1].ToInt(vm).Int : 0));
        }
        public HassiumChar toChar(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumChar(BitConverter.ToChar(listToByteArray(vm, args[0].ToList(vm)), args.Length == 2 ? (int)args[1].ToInt(vm).Int : 0));
        }
        public HassiumFloat toFloat(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(BitConverter.ToDouble(listToByteArray(vm, args[0].ToList(vm)), args.Length == 2 ? (int)args[1].ToInt(vm).Int : 0));
        }
        public HassiumInt toInt16(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(BitConverter.ToInt16(listToByteArray(vm, args[0].ToList(vm)), args.Length == 2 ? (int)args[1].ToInt(vm).Int : 0));
        }
        public HassiumInt toInt32(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(BitConverter.ToInt32(listToByteArray(vm, args[0].ToList(vm)), args.Length == 2 ? (int)args[1].ToInt(vm).Int : 0));
        }
        public HassiumInt toInt64(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(BitConverter.ToInt64(listToByteArray(vm, args[0].ToList(vm)), args.Length == 2 ? (int)args[1].ToInt(vm).Int : 0));
        }
        public HassiumString toString(VirtualMachine vm, params HassiumObject[] args)
        {
            byte[] bytes = listToByteArray(vm, args[0].ToList(vm));
            return new HassiumString(ASCIIEncoding.ASCII.GetString(bytes, args.Length == 2 ? (int)args[1].ToInt(vm).Int : 0, bytes.Length));
        }

        private byte[] listToByteArray(VirtualMachine vm, HassiumList list)
        {
            byte[] result = new byte[list.List.Count];
            for (int i = 0; i < result.Length; i++)
                result[i] = (byte)list.List[i].ToChar(vm).Char;
            return result;
        }
	}
}