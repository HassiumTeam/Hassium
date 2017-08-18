using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Numerics;

namespace Hassium.Runtime.Crypto
{
    public class HassiumRSA : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("RSA");

        public HassiumRSA()
        {
            AddType(TypeDefinition);

            AddAttribute("decrypt", decrypt, 3);
            AddAttribute("encrypt", encrypt, 3);
        }

        [FunctionAttribute("func decrypt (pubmod : object, privkey : object, msg : object")]
        public HassiumList decrypt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumByteArray(BigInteger.ModPow(new BigInteger(ListToByteArr(vm, location, args[2].ToList(vm, args[2], location))), new BigInteger(ListToByteArr(vm, location, args[1].ToList(vm, args[1], location))), new BigInteger(ListToByteArr(vm, location, args[0].ToList(vm, args[0], location)))).ToByteArray(), new HassiumObject[0]);
        }

        [FunctionAttribute("func encrypt (pubmod : object, pube : object, msg : object")]
        public HassiumList encrypt(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumByteArray(BigInteger.ModPow(new BigInteger(ListToByteArr(vm, location, args[2].ToList(vm, args[2], location))), new BigInteger(ListToByteArr(vm, location, args[1].ToList(vm, args[1], location))), new BigInteger(ListToByteArr(vm, location, args[0].ToList(vm, args[0], location)))).ToByteArray(), new HassiumObject[0]);
        }
    }
}
