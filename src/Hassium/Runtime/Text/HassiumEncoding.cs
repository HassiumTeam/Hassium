using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Text;

namespace Hassium.Runtime.Text
{
    public class HassiumEncoding : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Encoding");

        public Encoding Encoding { get; private set; }

        public HassiumEncoding()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 1);
        }

        [FunctionAttribute("func new (scheme : string) : Encoding")]
        public static HassiumEncoding _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumEncoding encoding = new HassiumEncoding();

            switch (args[0].ToString(vm, location).String)
            {
                case "UNICODE":
                    encoding.Encoding = Encoding.Unicode;
                    break;
                case "UTF7":
                    encoding.Encoding = Encoding.UTF7;
                    break;
                case "UTF8":
                    encoding.Encoding = Encoding.UTF8;
                    break;
                case "UTF32":
                    encoding.Encoding = Encoding.UTF32;
                    break;
                default:
                    encoding.Encoding = Encoding.ASCII;
                    break;
            }
            encoding.AddAttribute("bodyname", new HassiumProperty(encoding.get_bodyname));
            encoding.AddAttribute("encodingname", new HassiumProperty(encoding.get_encodingname));
            encoding.AddAttribute("getbytes", encoding.getbytes, 1);
            encoding.AddAttribute("getstring", encoding.getstring, 1);
            encoding.AddAttribute("headername", new HassiumProperty(encoding.get_headername));

            return encoding;
        }

        [FunctionAttribute("bodyname { get; }")]
        public HassiumString get_bodyname(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Encoding.BodyName);
        }

        [FunctionAttribute("encodingname { get; }")]
        public HassiumString get_encodingname(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Encoding.EncodingName);
        }

        [FunctionAttribute("func getbytes (str : string) : list")]
        public HassiumList getbytes(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            byte[] bytes = Encoding.GetBytes(args[0].ToString(vm, location).String);

            return new HassiumByteArray(bytes, new HassiumObject[0]);
        }

        [FunctionAttribute("func getstring (bytes : list) : string")]
        public HassiumString getstring(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            var list = args[0].ToList(vm, location).Values;
            byte[] bytes = new byte[list.Count];

            for (int i = 0; i < list.Count; i++)
                bytes[i] = (byte)list[i].ToChar(vm, location).Char;

            return new HassiumString(Encoding.GetString(bytes));
        }

        [FunctionAttribute("headername { get; }")]
        public HassiumString get_headername(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(Encoding.HeaderName);
        }
    }
}
