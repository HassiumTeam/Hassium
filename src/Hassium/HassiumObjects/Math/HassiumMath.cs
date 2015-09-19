using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Math
{
    public class HassiumMath : HassiumObject
    {
        public HassiumMath()
        {
            Attributes.Add("pi", new InternalFunction(x => System.Math.PI, 0, true));
            Attributes.Add("e", new InternalFunction(x => System.Math.E, 0, true));
            Attributes.Add("hash", new InternalFunction(Hash, 2));
            Attributes.Add("pow", new InternalFunction(Pow, 2));
            Attributes.Add("sqrt", new InternalFunction(Sqrt, 1));
            Attributes.Add("abs", new InternalFunction(Abs, 1));
            Attributes.Add("acos", new InternalFunction(Acos, 1));
            Attributes.Add("asin", new InternalFunction(Asin, 1));
            Attributes.Add("atan", new InternalFunction(Atan, 1));
            Attributes.Add("atan2", new InternalFunction(Atan2, 2));
            Attributes.Add("ceil", new InternalFunction(Ceil, 1));
            Attributes.Add("cos", new InternalFunction(Cos, 1));
            Attributes.Add("cosh", new InternalFunction(Cosh, 1));
            Attributes.Add("exp", new InternalFunction(Exp, 1));
            Attributes.Add("floor", new InternalFunction(Floor, 1));
            Attributes.Add("ln", new InternalFunction(Ln, 1));
            Attributes.Add("log", new InternalFunction(Log, 2));
            Attributes.Add("log10", new InternalFunction(Log10, 1));
            Attributes.Add("max", new InternalFunction(Max, 2));
            Attributes.Add("min", new InternalFunction(Min, 2));
            Attributes.Add("round", new InternalFunction(Round, new[] {1, 2}));
            Attributes.Add("sin", new InternalFunction(Sin, 1));
            Attributes.Add("sinh", new InternalFunction(Sinh, 1));
            Attributes.Add("tan", new InternalFunction(Tan, 1));
            Attributes.Add("tanh", new InternalFunction(Tanh, 1));
        }

        public HassiumObject Hash(HassiumObject[] args)
        {
            byte[] encodedText = new UTF8Encoding().GetBytes(args[1].ToString());
            byte[] hash =
                ((HashAlgorithm) CryptoConfig.CreateFromName(args[0].ToString().ToUpper())).ComputeHash(encodedText);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }

        public HassiumObject Pow(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Pow(args[0].HDouble().Value, args[1].HDouble().Value));
        }

        public HassiumObject Sqrt(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Sqrt(args[0].HDouble().Value));
        }

        public HassiumObject Abs(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Abs(args[0].HDouble().Value));
        }

        public HassiumObject Acos(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Acos(args[0].HDouble().Value));
        }

        public HassiumObject Asin(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Asin(args[0].HDouble().Value));
        }

        public HassiumObject Atan(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Acos(args[0].HDouble().Value));
        }

        public HassiumObject Atan2(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Atan2(args[0].HDouble().Value, args[1].HDouble().Value));
        }

        public HassiumObject Ceil(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Ceiling(args[0].HDouble().Value));
        }

        public HassiumObject Cos(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Cos(args[0].HDouble().Value));
        }

        public HassiumObject Cosh(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Cosh(args[0].HDouble().Value));
        }

        public HassiumObject Exp(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Exp(args[0].HDouble().Value));
        }

        public HassiumObject Floor(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Floor(args[0].HDouble().Value));
        }

        public HassiumObject Ln(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Log(args[0].HDouble().Value));
        }

        public HassiumObject Log(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Log(args[0].HDouble().Value, args[1].HDouble().Value));
        }

        public HassiumObject Log10(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Log10(args[0].HDouble().Value));
        }

        public HassiumObject Max(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Max(args[0].HDouble().Value, args[1].HDouble().Value));
        }

        public HassiumObject Min(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Min(args[0].HDouble().Value, ((HassiumDouble) args[1].HDouble().Value)));
        }

        public HassiumObject Round(HassiumObject[] args)
        {
            return args.Count() > 1
                ? new HassiumDouble(System.Math.Round(args[0].HDouble().Value, args[1].HInt().Value))
                : new HassiumDouble(System.Math.Round(args[0].HDouble().Value));
        }

        public HassiumObject Sin(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Sin(args[0].HDouble().Value));
        }

        public HassiumObject Sinh(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Sinh(args[0].HDouble().Value));
        }

        public HassiumObject Tan(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Tan(args[0].HDouble().Value));
        }

        public HassiumObject Tanh(HassiumObject[] args)
        {
            return new HassiumDouble(System.Math.Tanh(args[0].HDouble().Value));
        }
    }
}