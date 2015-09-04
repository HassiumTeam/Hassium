using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Math
{
    public class HassiumMath: HassiumObject
    {
        public HassiumMath()
        {
            Attributes.Add("hash", new InternalFunction(Hash));
            Attributes.Add("pow", new InternalFunction(Pow));
            Attributes.Add("sqrt", new InternalFunction(Sqrt));
            Attributes.Add("abs", new InternalFunction(Abs));
            Attributes.Add("acos", new InternalFunction(Acos));
            Attributes.Add("asin", new InternalFunction(Asin));
            Attributes.Add("atan", new InternalFunction(Atan));
            Attributes.Add("atan2", new InternalFunction(Atan2));
            Attributes.Add("ceil", new InternalFunction(Ceil));
            Attributes.Add("cos", new InternalFunction(Cos));
            Attributes.Add("cosh", new InternalFunction(Cosh));
            Attributes.Add("exp", new InternalFunction(Exp));
            Attributes.Add("floor", new InternalFunction(Floor));
            Attributes.Add("ln", new InternalFunction(Ln));
            Attributes.Add("log", new InternalFunction(Log));
            Attributes.Add("log10", new InternalFunction(Log10));
            Attributes.Add("max", new InternalFunction(Max));
            Attributes.Add("min", new InternalFunction(Min));
            Attributes.Add("round", new InternalFunction(Round));
            Attributes.Add("sin", new InternalFunction(Sin));
            Attributes.Add("sinh", new InternalFunction(Sinh));
            Attributes.Add("tan", new InternalFunction(Tan));
            Attributes.Add("tanh", new InternalFunction(Tanh));
        }

        public HassiumObject Hash(HassiumObject[] args)
        {
            byte[] encodedText = new UTF8Encoding().GetBytes(args[1].ToString());
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName(args[0].ToString().ToUpper())).ComputeHash(encodedText);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }

        public HassiumObject Pow(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Pow(((HassiumNumber)args[0]).Value, ((HassiumNumber)args[1]).Value));
        }

        public HassiumObject Sqrt(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Sqrt(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Abs(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Abs(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Acos(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Acos(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Asin(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Asin(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Atan(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Acos(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Atan2(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Atan2(((HassiumNumber)args[0]).Value, ((HassiumNumber)args[1]).Value));
        }

        public HassiumObject Ceil(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Ceiling(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Cos(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Cos(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Cosh(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Cosh(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Exp(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Exp(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Floor(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Floor(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Ln(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Log(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Log(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Log(((HassiumNumber)args[0]).Value, ((HassiumNumber)args[1]).Value));
        }

        public HassiumObject Log10(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Log10(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Max(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Max(((HassiumNumber)args[0]).Value, ((HassiumNumber)args[1]).Value));
        }

        public HassiumObject Min(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Min(((HassiumNumber)args[0]).Value, ((HassiumNumber)args[1]).Value));
        }

        public HassiumObject Round(HassiumObject[] args)
        {
            return args.Count() > 1 ? new HassiumNumber(System.Math.Round(((HassiumNumber)args[0]).Value, ((HassiumNumber)args[1]).ValueInt)) : new HassiumNumber(System.Math.Round(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Sin(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Sin(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Sinh(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Sinh(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Tan(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Tan(((HassiumNumber)args[0]).Value));
        }

        public HassiumObject Tanh(HassiumObject[] args)
        {
            return new HassiumNumber(System.Math.Tanh(((HassiumNumber)args[0]).Value));
        }
    }
}
