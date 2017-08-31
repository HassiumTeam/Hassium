using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Hassium.Runtime.Net
{
    public class HassiumCGI : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("CGI");

        public HassiumDictionary Get { get; private set; }
        public HassiumDictionary Post { get; private set; }

        public HassiumCGI()
        {
            AddType(TypeDefinition);

            Get = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
            string query_string;
            if (Environment.GetEnvironmentVariables().Contains("QUERY_STRING"))
            {
                query_string = Environment.GetEnvironmentVariable("QUERY_STRING");

                foreach (var arg in query_string.Split('&'))
                {
                    if (!arg.Contains("=")) Get.Dictionary.Add(new HassiumString(arg), new HassiumString(string.Empty));
                    else
                    {
                        var key = arg.Split('=')[0];
                        var value = HttpUtility.UrlDecode(arg.Split('=')[1]);
                        Get.Dictionary.Add(new HassiumString(key), new HassiumString(value));
                    }
                }
            }
            Post = new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>());
            if (Environment.GetEnvironmentVariable("CONTENT_LENGTH") != null)
            {
                query_string = string.Empty;
                int PostedDataLength = Convert.ToInt32(Environment.GetEnvironmentVariable("CONTENT_LENGTH"));
                for (int i = 0; i < PostedDataLength; i++)
                    query_string += Convert.ToChar(Console.Read()).ToString();
            }
            else
            {
                //var stdin = new StreamReader(Console.OpenStandardInput());
                query_string = string.Empty;//stdin.ReadToEnd();
            }
            if (!string.IsNullOrWhiteSpace(query_string))
            {
                foreach (var currentArg in query_string.Split('&'))
                {
                    if (!currentArg.Contains("=")) Post.Dictionary.Add(new HassiumString(currentArg), new HassiumString(""));
                    else
                    {
                        var key = currentArg.Split('=')[0];
                        var value = HttpUtility.UrlDecode(currentArg.Split('=')[1]);
                        Post.Dictionary.Add(new HassiumString(key), new HassiumString(value));
                    }
                }
            }

        }

        public HassiumDictionary get_get(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return Get;
        }
    }
}
 