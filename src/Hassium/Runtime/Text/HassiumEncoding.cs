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

        [DocStr(
            "@desc A class representing a specific string encoding scheme.",
            "@returns Encoding."
            )]
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

            [DocStr(
                "@desc Constructs a new Encoding object using the specified encoding scheme.",
                "@param scheme The string name of the scheme to use. UNICODE, UTF7, UTF8, UTF32 or ASCII.",
                "@returns The new Encoding object."
                )]
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

            [DocStr(
                "@desc Gets the readonly string body name of this encoding.",
                "@returns The body name as string."
                )]
            [FunctionAttribute("bodyname { get; }")]
            public static HassiumString get_bodyname(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                return new HassiumString(Encoding.BodyName);
            }
            
            [DocStr(
                "@desc Gets the readonly string encoding name of this encoding.",
                "@returns The encoding name as string."
                )]
            [FunctionAttribute("encodingname { get; }")]
            public static HassiumString get_encodingname(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                return new HassiumString(Encoding.EncodingName);
            }

            [DocStr(
                "@desc Converts the specified string into a list of bytes using this encoding.",
                "@param str The string to convert.",
                "@returns The bytes of str as a list."
                )]
            [FunctionAttribute("func getbytes (str : string) : list")]
            public static HassiumList getbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                byte[] bytes = Encoding.GetBytes(args[0].ToString(vm, args[0], location).String);

                return new HassiumByteArray(bytes, new HassiumObject[0]);
            }

            [DocStr(
                "@desc Converts the given list of bytes into a string using this encoding.",
                "@param bytes The list of bytes to convert.",
                "@returns The string value of the bytes."
                )]
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

            [DocStr(
                "@desc Gets the readonly string header name of this encoding.",
                "@returns The header name as string."
                )]
            [FunctionAttribute("headername { get; }")]
            public static HassiumString get_headername(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Encoding = (self as HassiumEncoding).Encoding;
                return new HassiumString(Encoding.HeaderName);
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
