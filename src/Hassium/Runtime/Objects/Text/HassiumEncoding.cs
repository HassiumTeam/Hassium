using System;
using System.Text;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Text
{
    public class HassiumEncoding: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Encoding");

        public Encoding Encoding { get; set; }

        public HassiumEncoding()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 1);
        }

        public HassiumEncoding _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumEncoding encoding = new HassiumEncoding();
            switch (args[0].ToString(vm).String.ToUpper())
            {
                case "UTF8":
                    encoding.Encoding = Encoding.UTF8;
                    break;
                case "UTF7":
                    encoding.Encoding = Encoding.UTF7;
                    break;
                case "UTF32":
                    encoding.Encoding = Encoding.UTF32;
                    break;
                case "UNICODE":
                    encoding.Encoding = Encoding.Unicode;
                    break;
                default:
                    encoding.Encoding = Encoding.ASCII;
                    break;
            }
            encoding.AddAttribute("bodyName",         new HassiumProperty(encoding.get_bodyName));
            encoding.AddAttribute("encodingName",     new HassiumProperty(encoding.get_encodingName));
            encoding.AddAttribute("getBytes",         encoding.getBytes, 1);
            encoding.AddAttribute("getString",        getString, 1);
            encoding.AddAttribute("headerName",       new HassiumProperty(encoding.get_headerName));

            return encoding;
        }

        public HassiumString get_bodyName(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Encoding.BodyName);
        }
        public HassiumString get_encodingName(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Encoding.EncodingName);
        }
        public HassiumList getBytes(VirtualMachine vm, params HassiumObject[] args)
        {
            byte[] bytes = Encoding.GetBytes(args[0].ToString(vm).String);
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (byte b in bytes)
                list.add(vm, new HassiumChar((char)b));

            return list;
        }
        public HassiumString getString(VirtualMachine vm, params HassiumObject[] args)
        {
            var list = args[0].ToList(vm).List;
            byte[] bytes = new byte[list.Count];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)list[i].ToChar(vm).Char;
            return new HassiumString(Encoding.GetString(bytes));
        }
        public HassiumString get_headerName(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Encoding.HeaderName);
        }
    }
}

