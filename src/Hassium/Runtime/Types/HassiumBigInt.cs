using Hassium.Compiler;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Hassium.Runtime.Types
{
    public class HassiumBigInt : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("BigInt");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {
            { "abs", new HassiumFunction(abs, 0)  },
            { ADD, new HassiumFunction(add, 1)  },
            { DIVIDE, new HassiumFunction(divide, 1)  },
            { EQUALTO, new HassiumFunction(equalto, 1)  },
            { GREATERTHAN, new HassiumFunction(greaterthan, 1)  },
            { GREATERTHANOREQUAL, new HassiumFunction(greaterthanorequal, 1)  },
            { INVOKE, new HassiumFunction(_new, 1) },
            { LESSERTHAN, new HassiumFunction(lesserthan, 1)  },
            { LESSERTHANOREQUAL, new HassiumFunction(lesserthanorequal, 1)  },
            { "log", new HassiumFunction(log, -1)  },
            { "modpow", new HassiumFunction(modpow, 3) },
            { MULTIPLY, new HassiumFunction(multiply, 1)  },
            { NOTEQUALTO, new HassiumFunction(notequalto, 1)  },
            { SUBTRACT, new HassiumFunction(subtract, 1)  },
            { TOFLOAT, new HassiumFunction(tofloat, 0)  },
            { TOINT, new HassiumFunction(toint, 0)  },
            { TOLIST, new HassiumFunction(tolist, 0)  },
            { TOSTRING, new HassiumFunction(tostring, 0)  },

        };

        public BigInteger BigInt { get; set; }

        public HassiumBigInt()
        {
            AddType(Number);
            AddType(TypeDefinition);
        }

        [FunctionAttribute("func new (obj : object) : BigInt")]
        public static HassiumBigInt _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumBigInt bigint = new HassiumBigInt();
            
            var type = args[0].Type();
            if (type == HassiumFloat.TypeDefinition)
                bigint.BigInt = new BigInteger(args[0].ToFloat(vm, args[0], location).Float);
            else if (type == HassiumInt.TypeDefinition)
                bigint.BigInt = new BigInteger(args[0].ToInt(vm, args[0], location).Int);
            else if (type == HassiumList.TypeDefinition)
                bigint.BigInt = new BigInteger(ListToByteArr(vm, location, args[0].ToList(vm, args[0], location, args[0]) as HassiumList));
            else
                bigint.BigInt = BigInteger.Parse(args[0].ToString(vm, args[0], location).String);

            return bigint;
        }
        

        [FunctionAttribute("func abs () : BigInt")]
        public static HassiumBigInt abs(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            return new HassiumBigInt() { BigInt = BigInteger.Abs(BigInt) };
        }

        [FunctionAttribute("func __add__ (num : number) : number")]
        public static HassiumObject add(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Add(BigInt, (bigintarg.BigInt)) };
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Add(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __divide__ (num : number) : number")]
        public static HassiumObject divide(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Divide(BigInt, (bigintarg.BigInt)) };
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Divide(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __equals__ (bigint : BigInt) : bool")]
        public static HassiumBool equalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var list = tolist(vm, self, location);
            return list.EqualTo(vm, list, location, (args[0] as HassiumBigInt).ToList(vm, args[0], location));
        }

        [FunctionAttribute("func __greater__ (num : number) : bool")]
        public static HassiumObject greaterthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) == 1);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) == 1);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __greaterorequal__ (num : number) : bool")]
        public static HassiumObject greaterthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) >= 0);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) >= 0);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __lesser__ (num : number) : bool")]
        public static HassiumObject lesserthan(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) == -1);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) == -1);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __lesserorequal__ (num : number) : bool")]
        public static HassiumObject lesserthanorequal(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (bigintarg as HassiumBigInt).BigInt) <= 0);
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBool(BigInteger.Compare(BigInt, (intarg as HassiumInt).Int) <= 0);
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func log (base : float) : BigInt")]
        public static HassiumBigInt log(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            return _new(vm, self, location, new HassiumFloat(BigInteger.Log(BigInt, args[0].ToFloat(vm, args[0], location).Float)));
        }

        [FunctionAttribute("func modpow (val : BigInt, exp : BigInt, mod : BigInt) : BigInt")]
        public static HassiumBigInt modpow(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBigInt() { BigInt = BigInteger.ModPow(args[0].ToBigInt(vm, args[0], location).BigInt, args[1].ToBigInt(vm, args[1], location).BigInt, args[2].ToBigInt(vm, args[2], location).BigInt) };
        }

        [FunctionAttribute("func __multiply__ (num : number) : number")]
        public static HassiumObject multiply(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Multiply(BigInt, (bigintarg.BigInt)) };
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Multiply(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func __notequal__ (bigint : BigInt) : bool")]
        public static HassiumBool notequalto(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var list = tolist(vm, self, location);
            return list.NotEqualTo(vm, list, location, (args[0] as HassiumBigInt).ToList(vm, args[0], location));
        }

        [FunctionAttribute("func __subtract__ (num : number) : number")]
        public static HassiumObject subtract(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            var bigintarg = args[0] as HassiumBigInt;
            if (bigintarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Subtract(BigInt, (bigintarg.BigInt)) };
            var intarg = args[0] as HassiumInt;
            if (intarg != null)
                return new HassiumBigInt() { BigInt = BigInteger.Subtract(BigInt, new BigInteger(args[0].ToInt(vm, args[0], location).Int)) };
            vm.RaiseException(HassiumConversionFailedException.Attribs[INVOKE].Invoke(vm, location, args[0], Number));
            return Null;
        }

        [FunctionAttribute("func tofloat () : float")]
        public static HassiumFloat tofloat(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            return new HassiumFloat(BitConverter.ToDouble(BigInt.ToByteArray(), 0));
        }

        [FunctionAttribute("func toint () : int")]
        public static HassiumInt toint(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            return new HassiumInt(BitConverter.ToInt64(BigInt.ToByteArray(), 0));
        }

        [FunctionAttribute("func tolist () : list")]
        public static HassiumList tolist(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            return new HassiumByteArray(BigInt.ToByteArray(), new HassiumObject[0]);
        }

        [FunctionAttribute("func tostring () : string")]
        public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            var BigInt = (self as HassiumBigInt).BigInt;
            return new HassiumString(BigInt.ToString());
        }
        
        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || Attribs.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (Attribs[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in Attribs)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
