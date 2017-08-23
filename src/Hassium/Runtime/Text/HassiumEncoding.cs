using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Text;

namespace Hassium.Runtime.Text
{
    public class HassiumEncoding : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new EncodingTypeDef();

        public Encoding Encoding { get; private set; }

        public HassiumEncoding()
        {
            AddType(TypeDefinition);
        }

        public class EncodingTypeDef : HassiumTypeDefinition
        {
            public EncodingTypeDef() : base("Encoding")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "bodyname", new HassiumProperty(get_bodyname)  },
                    { "encodingname", new HassiumProperty(get_encodingname)  },
                    { "getbytes", new HassiumFunction(getbytes, 1)  },
                    { "getstring", new HassiumFunction(getstring, 1)  },
                    { "headername", new HassiumProperty(get_headername)  },
                    { INVOKE, new HassiumFunction(_new, 1) }
                };
            }
            [FunctionAttribute("func new (scheme : string) : Encoding")]
            public static HassiumEncoding _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumEncoding encoding = new HassiumEncoding();

                switch (args[0].ToString(vm, args[0], location).String)
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

                return encoding;
            }

            [FunctionAttribute("bodyname { get; }")]
            public static HassiumString get_bodyname(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                return new HassiumString(Encoding.BodyName);
            }

            [FunctionAttribute("encodingname { get; }")]
            public static HassiumString get_encodingname(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                return new HassiumString(Encoding.EncodingName);
            }

            [FunctionAttribute("func getbytes (str : string) : list")]
            public static HassiumList getbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                byte[] bytes = Encoding.GetBytes(args[0].ToString(vm, args[0], location).String);

                return new HassiumByteArray(bytes, new HassiumObject[0]);
            }

            [FunctionAttribute("func getstring (bytes : list) : string")]
            public static HassiumString getstring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                var list = args[0].ToList(vm, args[0], location).Values;
                byte[] bytes = new byte[list.Count];

                for (int i = 0; i < list.Count; i++)
                    bytes[i] = (byte)list[i].ToChar(vm, args[i], location).Char;

                return new HassiumString(Encoding.GetString(bytes));
            }

            [FunctionAttribute("headername { get; }")]
            public static HassiumString get_headername(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                return new HassiumString(Encoding.HeaderName);
            }
        }
    }
}
