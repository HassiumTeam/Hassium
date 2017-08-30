using Hassium.Compiler;
using Hassium.Runtime.IO;
using Hassium.Runtime.Types;

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hassium.Runtime.Crypto
{
    public class HassiumAES : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("AES");

        public HassiumAES()
        {
            AddType(TypeDefinition);
            AddAttribute("decryptbytes", decryptbytes, 3);
            AddAttribute("decryptfilebytes", decryptfilebytes, 3);
            AddAttribute("encryptbytes", encryptbytes, 3);
            AddAttribute("encryptfilebytes", encryptfilebytes, 3);
        }

        [DocStr(
            "@desc Decrypts the given byte string or list using the specified 16 byte key and iv, returning the result.",
            "@param key The 16 byte long AES key.",
            "@param iv The 16 byte long AES iv.",
            "@param dataStrOrList The data string or list to decrypt.",
            "@returns A new list of decrypted bytes."
            )]
        [FunctionAttribute("func decryptbytes (key : list, iv : list, dataStrOrList : object) : list")]
        public HassiumList decryptbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            byte[] key = ListToByteArr(vm, location, args[0].ToList(vm, args[0], location));
            byte[] iv = ListToByteArr(vm, location, args[1].ToList(vm, args[1], location));

            byte[] data;
            if (args[2] is HassiumString)
                data = ASCIIEncoding.ASCII.GetBytes(args[2].ToString(vm, args[2], location).String);
            else
                data = ListToByteArr(vm, location, args[2].ToList(vm, args[2], location));

            return new HassiumByteArray(decrypt(key, iv, data), new HassiumObject[0]);

        }

        [DocStr(
            "@desc Decrypts the given File object using the specified 16 byte key and iv, returning the result.",
            "@param key The 16 byte long AES key.",
            "@param iv The 16 byte long AES iv.",
            "@param file The IO.File object.",
            "@returns A new list of decrypted bytes."
            )]
        [FunctionAttribute("func decryptfilebytes (key : list, iv : list, file : File) : list")]
        public HassiumList decryptfilebytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            byte[] key = ListToByteArr(vm, location, args[0].ToList(vm, args[0], location));
            byte[] iv = ListToByteArr(vm, location, args[1].ToList(vm, args[1], location));
            HassiumFile file = args[2] is HassiumFile ? (args[2] as HassiumFile) : new HassiumFile(args[2].ToString(vm, args[2], location).String);
            byte[] data = (HassiumFile.FileTypeDef.readallbytes(vm, file, location) as HassiumByteArray).Values.ToArray();

            return new HassiumByteArray(decrypt(key, iv, data), new HassiumObject[0]);
        }

        [DocStr(
            "@desc Encrypts the given byte string or list using the specified 16 byte key and iv, returning the result.",
            "@param key The 16 byte long AES key.",
            "@param iv The 16 byte long AES iv.",
            "@param dataStrOrList The data string or list to decrypt.",
            "@returns A new list of encrypted bytes."
            )]
        [FunctionAttribute("func encryptbytes (key : list, iv : list, dataStrOrList : object) : list")]
        public HassiumList encryptbytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            byte[] key = ListToByteArr(vm, location, args[0].ToList(vm, args[0], location));
            byte[] iv = ListToByteArr(vm, location, args[1].ToList(vm, args[1], location));

            byte[] data;
            if (args[2] is HassiumString)
                data = ASCIIEncoding.ASCII.GetBytes(args[2].ToString(vm, args[2], location).String);
            else
                data = ListToByteArr(vm, location, args[2].ToList(vm, args[2], location));

            using (var memStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memStream, new RijndaelManaged().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }
                return new HassiumByteArray(memStream.ToArray(), new HassiumObject[0]);
            }

        }

        [DocStr(
            "@desc Encrypts the given File object using the specified 16 byte key and iv, returning the result.",
            "@param key The 16 byte long AES key.",
            "@param iv The 16 byte long AES iv.",
            "@param file The IO.File object.",
            "@returns A new list of encrypted bytes."
            )]
        [FunctionAttribute("func encryptfilebytes (key : list, iv : list, file : File) : list")]
        public HassiumList encryptfilebytes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            byte[] key = ListToByteArr(vm, location, args[0].ToList(vm, args[0], location));
            byte[] iv = ListToByteArr(vm, location, args[1].ToList(vm, args[1], location));
            HassiumFile file = args[2] is HassiumFile ? (args[2] as HassiumFile) : new HassiumFile(args[2].ToString(vm, args[2], location).String);

            using (var memStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memStream, new RijndaelManaged().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    while (file.Reader.BaseStream.Position < file.Reader.BaseStream.Length)
                    {
                        byte[] buff = new byte[(byte)(HassiumFile.FileTypeDef.readbyte(vm, file, location) as HassiumChar).Char];
                        cryptoStream.Write(buff, 0, 1);
                    }
                }
                return new HassiumByteArray(memStream.ToArray(), new HassiumObject[0]);
            }
        }

        private byte[] decrypt(byte[] key, byte[] iv, byte[] data)
        {
            using (var memStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memStream, new RijndaelManaged().CreateDecryptor(key, iv), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }
                return memStream.ToArray();
            }
        }
    }
}
