using Hassium.Compiler;
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
            AddAttribute("encryptbytes", encryptbytes, 3);
        }

        [FunctionAttribute("func decryptbytes (key : list, iv : list, dataStrOrList : object) : list")]
        public HassiumList decryptbytes(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            byte[] key = listToByteArr(vm, location, args[0].ToList(vm, location));
            byte[] iv = listToByteArr(vm, location, args[1].ToList(vm, location));

            byte[] data;
            if (args[2] is HassiumString)
                data = ASCIIEncoding.ASCII.GetBytes(args[2].ToString(vm, location).String);
            else
                data = listToByteArr(vm, location, args[2].ToList(vm, location));
                
            using (var memStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memStream, new RijndaelManaged().CreateDecryptor(key, iv), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }

                return new HassiumByteArray(memStream.ToArray(), new HassiumObject[0]);
            }

        }

        [FunctionAttribute("func encryptbytes (key : list, iv : list, dataStrOrList : object) : list")]
        public HassiumList encryptbytes(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            byte[] key = listToByteArr(vm, location, args[0].ToList(vm, location));
            byte[] iv = listToByteArr(vm, location, args[1].ToList(vm, location));

            byte[] data;
            if (args[2] is HassiumString)
                data = ASCIIEncoding.ASCII.GetBytes(args[2].ToString(vm, location).String);
            else
                data = listToByteArr(vm, location, args[2].ToList(vm, location));

            using (var memStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memStream, new RijndaelManaged().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                }
                return new HassiumByteArray(memStream.ToArray(), new HassiumObject[0]);
            }

        }

        private byte[] listToByteArr(VirtualMachine vm, SourceLocation location, HassiumList list)
        {
            if (list is HassiumByteArray)
                return (list as HassiumByteArray).Values.ToArray();
            byte[] bytes = new byte[list.Values.Count];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)list.Values[i].ToChar(vm, location).Char;

            return bytes;
        }
    }
}
