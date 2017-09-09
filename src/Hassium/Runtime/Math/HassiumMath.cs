using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Hassium.Runtime.Math
{
    public class HassiumMath : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new MathTypeDef();

        public HassiumMath()
        {
            AddType(TypeDefinition);

        }

        [DocStr(
            "@desc A class containing methods for advanced mathematical operations.",
            "@returns Math."
            )]
        public class MathTypeDef : HassiumTypeDefinition
        {
            public MathTypeDef() : base("Math")
            {
                AddAttribute("abs", abs, 1);
                AddAttribute("acos", acos, 1);
                AddAttribute("asin", asin, 1);
                AddAttribute("atan", atan, 1);
                AddAttribute("atan2", atan2, 2);
                AddAttribute("ceil", ceil, 1);
                AddAttribute("cos", cos, 1);
                AddAttribute("e", new HassiumProperty(get_e));
                AddAttribute("floor", floor, 1);
                AddAttribute("hash", hash, 2);
                AddAttribute("log", log, 2);
                AddAttribute("log10", log10, 1);
                AddAttribute("max", max, 2);
                AddAttribute("min", min, 2);
                AddAttribute("pi", new HassiumProperty(get_pi));
                AddAttribute("pow", pow, 2);
                AddAttribute("round", round, 1);
                AddAttribute("sin", sin, 1);
                AddAttribute("sqrt", sqrt, 1);
                AddAttribute("tan", tan, 1);
            }

            [DocStr(
                "@desc Returns the absolute value of the specified number.",
                "@param num The number.",
                "@returns The absolute value."
            )]
            [FunctionAttribute("func abs (num : number) : float")]
            public static HassiumObject abs(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Abs(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Abs(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the acosine value of the specified number.",
                "@param num The number.",
                "@returns The acosine value."
            )]
            [FunctionAttribute("func acos (num : number) : float")]
            public static HassiumObject acos(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Acos(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Acos(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the asine value of the specified number.",
                "@param num The number.",
                "@returns The asine value."
            )]
            [FunctionAttribute("func asin (num : number) : float")]
            public static HassiumObject asin(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Asin(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Asin(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the atangent value of the specified number.",
                "@param num The number.",
                "@returns The atangent value."
            )]
            [FunctionAttribute("func atan (num : number) : float")]
            public static HassiumObject atan(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Atan(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Atan(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the atangent2 value of the specified y and x values.",
                "@param y The y value.",
                "@param x The x value.",
                "@returns The atangent2 value."
            )]
            [FunctionAttribute("func atan2 (y : float, x : float) : float")]
            public static HassiumFloat atan2(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                return new HassiumFloat(System.Math.Atan2(args[0].ToFloat(vm, args[0], location).Float, args[1].ToFloat(vm, args[1], location).Float));
            }

            [DocStr(
                "@desc Returns the next number greater than or equal to the specified number.",
                "@param num The number.",
                "@returns The ceiling value."
            )]
            [FunctionAttribute("func ceil (num : number) : float")]
            public static HassiumFloat ceil(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                return new HassiumFloat(System.Math.Ceiling(args[0].ToFloat(vm, args[0], location).Float));
            }

            [DocStr(
                "@desc Returns the cosine value of the specified number.",
                "@param num The number.",
                "@returns The cosine value."
            )]
            [FunctionAttribute("func cos (num : number) : float")]
            public static HassiumObject cos(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Cos(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Cos(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Gets the readonly float value of e.",
                "@returns The constant value e."
            )]
            [FunctionAttribute("e { get; }")]
            public static HassiumFloat get_e(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                return new HassiumFloat(System.Math.E);
            }

            [DocStr(
                "@desc Returns the next number lesser than or equal to the specified number.",
                "@param num The number.",
                "@returns The floor value."
            )]
            [FunctionAttribute("func floor (num : number) : float")]
            public static HassiumFloat floor(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                return new HassiumFloat(System.Math.Floor(args[0].ToFloat(vm, args[0], location).Float));
            }

            [DocStr(
                "@desc Returns a list with the resulting bytes of a hash operating using the specified string algo and the either string or byte value data.",
                "@param algo The string hasing algorithm to use.",
                "@param strOrList The hash input data which is either a string or list of bytes.",
                "@returns A list of the hash bytes."
            )]
            [FunctionAttribute("hash (algo : string, strOrList : object) : list")]
            public static HassiumList hash(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                HassiumList list = args[1].ToList(vm, args[1], location);
                byte[] bytes = new byte[list.Values.Count];
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = (byte)list.Values[i].ToChar(vm, list.Values[i], location).Char;

                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName(args[0].ToString(vm, args[0], location).String.ToUpper())).ComputeHash(bytes);
                return new HassiumByteArray(hash, new HassiumObject[0]);
            }

            [DocStr(
                "@desc Calculates the logarithm of the specified number to the specified base.",
                "@param num The number.",
                "@param base The base.",
                "@returns The logarithm of num to base."
            )]
            [FunctionAttribute("func log (num : number, base : number")]
            public static HassiumObject log(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                return new HassiumFloat(System.Math.Log(args[0].ToInt(vm, args[0], location).Int, args[1].ToInt(vm, args[1], location).Int));
            }

            [DocStr(
                "@desc Calculates the logarithm of the specified number to the base of 10.",
                "@param num The number.",
                "@returns The logarithm of num to 10."
            )]
            [FunctionAttribute("func log10 (num : number) : float")]
            public static HassiumObject log10(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Log10(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Log10(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the larger of the two numbers given.",
                "@param num1 The first number to compare.",
                "@param num2 The second number to cmopare.",
                "@returns num1 if larger than num2, otherwise num2."
            )]
            [FunctionAttribute("func max (num1 : number, num2 : number) : float")]
            public static HassiumObject max(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Max(args[0].ToInt(vm, args[0], location).Int, args[1].ToInt(vm, args[1], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Max(args[0].ToFloat(vm, args[0], location).Float, args[1].ToFloat(vm, args[1], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the lesser of the two numbers given.",
                "@param num1 The first number to compare.",
                "@param num2 The second number to compare.",
                "@returns num1 if lesser than num2, otherwise num2."
            )]
            [FunctionAttribute("func min (num1 : number, num2 : number) : float")]
            public static HassiumObject min(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Min(args[0].ToInt(vm, args[0], location).Int, args[1].ToInt(vm, args[1], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Min(args[0].ToFloat(vm, args[0], location).Float, args[1].ToFloat(vm, args[1], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Gets the readonly value of pi.",
                "@returns The constant value pi."
            )]
            [FunctionAttribute("pi { get; }")]
            public static HassiumFloat get_pi(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                return new HassiumFloat(System.Math.PI);
            }

            [DocStr(
                "@desc Returns the specified number to the specified power.",
                "@param num The number.",
                "@param power The power.",
                "@returns num to the power."
            )]
            [FunctionAttribute("func pow (num : number, power : number) : float")]
            public static HassiumObject pow(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Pow(args[0].ToInt(vm, args[0], location).Int, args[1].ToInt(vm, args[1], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Pow(args[0].ToFloat(vm, args[0], location).Float, args[1].ToFloat(vm, args[1], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Rounds the specified float and returns it.",
                "@param f The number to round.",
                "@returns Rounded f."
            )]
            [FunctionAttribute("func round (f : float) : float")]
            public static HassiumFloat round(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                return new HassiumFloat(System.Math.Round(args[0].ToFloat(vm, args[0], location).Float));
            }

            [DocStr(
                "@desc Returns the sine value of the specified number.",
                "@param num The number.",
                "@returns The sine value."
            )]
            [FunctionAttribute("func sin (num : number) : float")]
            public static HassiumObject sin(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Sin(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Sin(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the square root value of the specified number.",
                "@param num The number.",
                "@returns The square root of num."
            )]
            [FunctionAttribute("func sqrt (num : number) : float")]
            public static HassiumObject sqrt(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Sqrt(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Sqrt(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
            }

            [DocStr(
                "@desc Returns the tangent value of num.",
                "@param num The number.",
                "@returns The tangent value."
            )]
            [FunctionAttribute("func tan (num : number) : float")]
            public static HassiumObject tan(VirtualMachine vm, HassiumObject self, SourceLocation location, HassiumObject[] args)
            {
                if (args[0] is HassiumInt)
                    return new HassiumFloat(System.Math.Tan(args[0].ToInt(vm, args[0], location).Int));
                else if (args[0] is HassiumFloat)
                    return new HassiumFloat(System.Math.Tan(args[0].ToFloat(vm, args[0], location).Float));
                return Null;
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
