using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Numerics;

namespace Hassium.Runtime.Crypto
{
    public class HassiumRSA : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new RSATypeDef();

        public HassiumRSA()
        {
            AddType(TypeDefinition);

            AddAttribute("decrypt", decrypt, 3);
            AddAttribute("encrypt", encrypt, 3);
        }

        [DocStr(
            "@desc A class containing methods for encrypting and decrypting messages using RSA.",
            "@returns RSA."
            )]
        public class RSATypeDef : HassiumTypeDefinition
        {
            public RSATypeDef() : base("RSA")
            {
            }
        }

        [DocStr(
            "@desc Decrypts the given msg using the given public modulus BigInt and private key BigInt, returning the resulting bytes.",
            "@param pubmod The public key modulus BigInt.",
            "@param privkey The private key BigInt.",
            "@param msg The msg to decrypt.",
            "@returns The decrypted msg bytes."
            )]
        [FunctionAttribute("func decrypt (pubmod : BigInt, privkey : BigInt, msg : object)")]
        public HassiumList decrypt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumByteArray(BigInteger.ModPow(new BigInteger(ListToByteArr(vm, location, args[2].ToList(vm, args[2], location))), new BigInteger(ListToByteArr(vm, location, args[1].ToList(vm, args[1], location))), new BigInteger(ListToByteArr(vm, location, args[0].ToList(vm, args[0], location)))).ToByteArray(), new HassiumObject[0]);
        }

        [DocStr(
            "@desc Encrypts the given msg using the given public modulus BigInt and public e BigInt, return the resulting bytes.",
            "@param pubmod The public key modulus BigInt.",
            "@param pube The public e BigInt.",
            "@param msg The msg to encrypt.",
            "@returns the encrytped msg bytes."
            )]
        [FunctionAttribute("func encrypt (pubmod : object, pube : object, msg : object)")]
        public HassiumList encrypt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumByteArray(BigInteger.ModPow(new BigInteger(ListToByteArr(vm, location, args[2].ToList(vm, args[2], location))), new BigInteger(ListToByteArr(vm, location, args[1].ToList(vm, args[1], location))), new BigInteger(ListToByteArr(vm, location, args[0].ToList(vm, args[0], location)))).ToByteArray(), new HassiumObject[0]);
        }
    }
}
