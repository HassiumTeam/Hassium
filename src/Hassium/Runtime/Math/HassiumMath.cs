using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Security.Cryptography;

namespace Hassium.Runtime.Math
{
    public class HassiumMath : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Math");

        public HassiumMath()
        {
            AddType(TypeDefinition);

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

        [FunctionAttribute("func abs (num : number) : float")]
        public HassiumObject abs(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Abs(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Abs(args[0].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func acos (num : number) : float")]
        public HassiumObject acos(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Acos(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Acos(args[0].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func asin (num : number) : float")]
        public HassiumObject asin(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Asin(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Asin(args[0].ToFloat(vm, location).Float));
            return Null;
        }


        [FunctionAttribute("func atan (num : number) : float")]
        public HassiumObject atan(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Atan(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Atan(args[0].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func atan2 (y : float, x : float) : float")]
        public HassiumFloat atan2(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Atan2(args[0].ToFloat(vm, location).Float, args[1].ToFloat(vm, location).Float));
        }

        [FunctionAttribute("func ceil (num : number) : float")]
        public HassiumFloat ceil(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Ceiling(args[0].ToFloat(vm, location).Float));
        }

        [FunctionAttribute("func cos (num : number) : float")]
        public HassiumObject cos(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Cos(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Cos(args[0].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("e { get; }")]
        public HassiumFloat get_e(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.E);
        }

        [FunctionAttribute("func floor (num : number) : float")]
        public HassiumFloat floor(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Floor(args[0].ToFloat(vm, location).Float));
        }

        [FunctionAttribute("hash (algo : string, obj : object) : list")]
        public HassiumList hash(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            HassiumList list = args[1].ToList(vm, location);
            byte[] bytes = new byte[list.Values.Count];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)list.Values[i].ToChar(vm, location).Char;

            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName(args[0].ToString(vm, location).String.ToUpper())).ComputeHash(bytes);
            return new HassiumByteArray(hash, new HassiumObject[0]);
        }
        
        [FunctionAttribute("func log (num : number, base : number")]
        public HassiumObject log(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Log(args[0].ToInt(vm, location).Int, args[1].ToInt(vm, location).Int));
        }

        [FunctionAttribute("func log10 (num : number) : float")]
        public HassiumObject log10(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Log10(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Log10(args[0].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func max (num1 : number, num2 : number) : float")]
        public HassiumObject max(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Max(args[0].ToInt(vm, location).Int, args[1].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Max(args[0].ToFloat(vm, location).Float, args[1].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func min (num1 : number, num2 : number) : float")]
        public HassiumObject min(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Min(args[0].ToInt(vm, location).Int, args[1].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Min(args[0].ToFloat(vm, location).Float, args[1].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("pi { get; }")]
        public HassiumFloat get_pi(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.PI);
        }

        [FunctionAttribute("func pow (num : number, power : number) : float")]
        public HassiumObject pow(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Pow(args[0].ToInt(vm, location).Int, args[1].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Pow(args[0].ToFloat(vm, location).Float, args[1].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func round (f : float) : float")]
        public HassiumFloat round(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Round(args[0].ToFloat(vm, location).Float));
        }

        [FunctionAttribute("func sin (num : number) : float")]
        public HassiumObject sin(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Sin(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Sin(args[0].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func sqrt (num : number) : float")]
        public HassiumObject sqrt(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Sqrt(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Sqrt(args[0].ToFloat(vm, location).Float));
            return Null;
        }

        [FunctionAttribute("func tan (num : number) : float")]
        public HassiumObject tan(VirtualMachine vm, SourceLocation location, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Tan(args[0].ToInt(vm, location).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Tan(args[0].ToFloat(vm, location).Float));
            return Null;
        }
    }
}
