using System;
using System.Security.Cryptography;
using System.Text;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Math
{
    public class HassiumMath: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Math");

        public HassiumMath()
        {
            AddType(TypeDefinition);
            AddAttribute("abs",     abs,    1);
            AddAttribute("acos",    acos,   1);
            AddAttribute("asin",    asin,   1);
            AddAttribute("atan",    atan,   1);
            AddAttribute("atan2",   atan2,  2);
            AddAttribute("ceil",    ceil,   1);
            AddAttribute("cos",     cos,    1);
            AddAttribute("e", new HassiumProperty(get_e));
            AddAttribute("floor",   floor,  1);
            AddAttribute("hash",    hash,   2);
            AddAttribute("log",     log,    2);
            AddAttribute("log10",   log10,  1);
            AddAttribute("max",     max,    2);
            AddAttribute("min",     min,    2);
            AddAttribute("pi", new HassiumProperty(get_pi));
            AddAttribute("pow",     pow,    2);
            AddAttribute("round",   round,  1);
            AddAttribute("sin",     sin,    1);
            AddAttribute("sqrt",    sqrt,   1);
            AddAttribute("tan",     tan,    1);
        }

        private HassiumObject abs(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Abs(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Abs(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject acos(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Acos(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Acos(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject asin(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Asin(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Asin(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }

        private HassiumObject atan(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Atan(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Atan(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumFloat atan2(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Atan2(args[0].ToFloat(vm).Float,args[1].ToFloat(vm).Float));
        }
        private HassiumObject ceil(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Ceiling(args[0].ToFloat(vm).Float));
        }
        private HassiumObject cos(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Cos(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Cos(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumFloat get_e(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.E);
        }
        private HassiumFloat floor(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Floor(args[0].ToFloat(vm).Float));
        }
        private HassiumString hash(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = args[1].ToList(vm);
            byte[] bytes = new byte[list.List.Count];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)list.List[i].ToChar(vm).Char;

            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName(args[0].ToString(vm).String.ToUpper())).ComputeHash(bytes);
            return new HassiumString(BitConverter.ToString(hash).Replace("-", string.Empty).ToLower());
        }
        private HassiumObject log(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Log(args[0].ToInt(vm).Int, args[1].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Log(args[0].ToFloat(vm).Float, args[1].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject log10(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Log10(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Log10(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject max(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Max(args[0].ToInt(vm).Int, args[1].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Max(args[0].ToFloat(vm).Float, args[1].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject min(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Min(args[0].ToInt(vm).Int, args[1].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Min(args[0].ToFloat(vm).Float, args[1].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumFloat get_pi(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.PI);
        }
        private HassiumObject pow(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Pow(args[0].ToInt(vm).Int, args[1].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Pow(args[0].ToFloat(vm).Float, args[1].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject round(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumFloat(System.Math.Round(args[0].ToFloat(vm).Float));
        }
        private HassiumObject sin(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Sin(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Sin(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject sqrt(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Sqrt(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Sqrt(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
        private HassiumObject tan(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumFloat(System.Math.Tan(args[0].ToInt(vm).Int));
            else if (args[0] is HassiumFloat)
                return new HassiumFloat(System.Math.Tan(args[0].ToFloat(vm).Float));
            return HassiumObject.Null;
        }
    }
}