using System;
using System.Security.Cryptography;
using System.Text;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Math
{
    public class HassiumMath: HassiumObject
    {
        public HassiumMath()
        {
            Attributes.Add("abs",   new HassiumFunction(abs, 1));
            Attributes.Add("acos",  new HassiumFunction(acos, 1));
            Attributes.Add("asin",  new HassiumFunction(asin, 1));
            Attributes.Add("atan",  new HassiumFunction(atan, 1));
            Attributes.Add("ceil",  new HassiumFunction(ceil, 1));
            Attributes.Add("cos",   new HassiumFunction(cos, 1));
            Attributes.Add("e",     new HassiumProperty(get_e));
            Attributes.Add("floor", new HassiumFunction(floor, 1));
            Attributes.Add("hash",  new HassiumFunction(hash, 2));
            Attributes.Add("log",   new HassiumFunction(log, 2));
            Attributes.Add("log10", new HassiumFunction(log10, 2));
            Attributes.Add("max",   new HassiumFunction(max, 2));
            Attributes.Add("min",   new HassiumFunction(min, 2));
            Attributes.Add("Pi",    new HassiumProperty(get_Pi));
            Attributes.Add("pow",   new HassiumFunction(pow, 2));
            Attributes.Add("round", new HassiumFunction(round, 1));
            Attributes.Add("sin",   new HassiumFunction(sin, 1));
            Attributes.Add("sqrt",  new HassiumFunction(sqrt, 1));
            Attributes.Add("tan",   new HassiumFunction(tan, 1));
            AddType("Math");
        }

        private HassiumObject abs(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Abs(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Abs(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject acos(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Acos(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Acos(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject asin(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Asin(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Asin(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
   
        private HassiumObject atan(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Atan(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Atan(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject ceil(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Ceiling(HassiumDouble.Create(args[0]).Value));
        }
        private HassiumObject cos(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Cos(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Cos(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
        private HassiumDouble get_e(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.E);
        }
        private HassiumDouble floor(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Floor(HassiumDouble.Create(args[0]).Value));
        }
        private HassiumString hash(VirtualMachine vm, HassiumObject[] args)
        {
            byte[] bytes = null;
            if (args[1] is HassiumString)
            {
                bytes = new UTF8Encoding().GetBytes(args[1].ToString());
            }
            else if (args[1] is HassiumList)
            {
                HassiumList list = args[1] as HassiumList;
                bytes = new byte[list.Value.Count];
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (list.Value[i] is HassiumChar)
                        bytes[i] = (byte)HassiumChar.Create(list.Value[i]).Value;
                    else if (list.Value[i] is HassiumInt)
                        bytes[i] = (byte)HassiumInt.Create(list.Value[i]).Value;
                }
            }
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName(HassiumString.Create(args[0]).Value.ToUpper())).ComputeHash(bytes);
            return new HassiumString(BitConverter.ToString(hash).Replace("-", string.Empty).ToLower());
        }
        private HassiumObject log(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Log(HassiumInt.Create(args[0]).Value, HassiumInt.Create(args[1]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Log(HassiumDouble.Create(args[0]).Value, HassiumDouble.Create(args[1]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject log10(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Log10(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Log10(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject max(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Max(HassiumInt.Create(args[0]).Value, HassiumInt.Create(args[1]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Max(HassiumDouble.Create(args[0]).Value, HassiumDouble.Create(args[1]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject min(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Min(HassiumInt.Create(args[0]).Value, HassiumInt.Create(args[1]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Min(HassiumDouble.Create(args[0]).Value, HassiumDouble.Create(args[1]).Value));
            return HassiumObject.Null;
        }
        private HassiumDouble get_Pi(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.PI);
        }
        private HassiumObject pow(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Pow(HassiumInt.Create(args[0]).Value, HassiumInt.Create(args[1]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Pow(HassiumDouble.Create(args[0]).Value, HassiumDouble.Create(args[1]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject round(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Round(HassiumDouble.Create(args[0]).Value));
        }
        private HassiumObject sin(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Sin(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Sin(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject sqrt(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Sqrt(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Sqrt(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
        private HassiumObject tan(VirtualMachine vm, HassiumObject[] args)
        {
            if (args[0] is HassiumInt)
                return new HassiumDouble(System.Math.Tan(HassiumInt.Create(args[0]).Value));
            else if (args[0] is HassiumDouble)
                return new HassiumDouble(System.Math.Tan(HassiumDouble.Create(args[0]).Value));
            return HassiumObject.Null;
        }
    }
}

