using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hassium
{
    public class MathFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("pow", new InternalFunction(MathFunctions.Pow));
            result.Add("sqrt", new InternalFunction(MathFunctions.Sqrt));
            result.Add("hash", new InternalFunction(MathFunctions.Hash));

            return result;
        }
        public static object Pow(object[] args)
        {
            return (double)(Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1])));
        }

        public static object Sqrt(object[] args)
        {
            return (double)(Math.Sqrt(Convert.ToDouble(args[0])));
        }

        public static object Hash(object[] args)
        {
            byte[] encodedText = new UTF8Encoding().GetBytes(args[1].ToString());
            byte[] hash = ((HashAlgorithm) CryptoConfig.CreateFromName(args[0].ToString().ToUpper())).ComputeHash(encodedText);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }

        private static string arrayToString(object[] args, int startIndex = 0)
        {
            string result = "";

            for (int x = startIndex; x < args.Length; x++)
                result += args[x].ToString();

            return result;
        }
    }
}
