using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Hassium.Runtime.Net
{
    public class HassiumWeb : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new WebTypeDef();

        public WebClient WebClient { get; set; }

        public HassiumWeb()
        {
            AddType(TypeDefinition);
        }

        public class WebTypeDef : HassiumTypeDefinition
        {
            public WebTypeDef() : base("Web")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "downloaddata", new HassiumFunction(downloaddata, 1)  },
                    { "downloadfile", new HassiumFunction(downloadfile, 2)  },
                    { "downloadstr", new HassiumFunction(downloadstr, 1)  },
                    { "htmldecode", new HassiumFunction(htmldecode, 1)  },
                    { "htmlencode", new HassiumFunction(htmlencode, 1)  },
                    { "uploaddata", new HassiumFunction(uploaddata, 2)  },
                    { "uploadfile", new HassiumFunction(uploadfile, 2)  },
                    { "uploadstring", new HassiumFunction(uploadstr, 2)  },
                    { "urldecode", new HassiumFunction(urldecode, 1)  },
                    { "urlencode", new HassiumFunction(urlencode, 1)  }
                };
            }

            [FunctionAttribute("func new () : Web")]
            public static HassiumWeb _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumWeb web = new HassiumWeb();

                web.WebClient = new WebClient();

                return web;
            }

            [FunctionAttribute("func downloaddata (url : string) : list")]
            public HassiumList downloaddata(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                byte[] data = WebClient.DownloadData(args[0].ToString(vm, args[0], location).String);

                return new HassiumByteArray(data, new HassiumObject[0]);
            }

            [FunctionAttribute("func downloadfile (url : string, path : string) : null")]
            public HassiumNull downloadfile(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                WebClient.DownloadFile(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

                return Null;
            }

            [FunctionAttribute("func downloadstr (url : string) : string")]
            public HassiumString downloadstr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                return new HassiumString(WebClient.DownloadString(args[0].ToString(vm, args[0], location).String));
            }

            [FunctionAttribute("func htmldecode (str : string) : string")]
            public HassiumString htmldecode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(HttpUtility.HtmlDecode(args[0].ToString(vm, args[0], location).String));
            }

            [FunctionAttribute("func htmlencode (str : string) : string")]
            public HassiumString htmlencode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(HttpUtility.HtmlEncode(args[0].ToString(vm, args[0], location).String));
            }

            [FunctionAttribute("func uploaddata (url : string, data : list) : list")]
            public HassiumList uploaddata(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                var list = args[0].ToList(vm, args[0], location).Values;
                byte[] data;
                if (args[0] is HassiumByteArray)
                    data = (args[0] as HassiumByteArray).Values.ToArray();
                else
                {
                    data = new byte[list.Count];
                    for (int i = 0; i < data.Length; i++)
                        data[i] = (byte)list[i].ToChar(vm, list[i], location).Char;
                }

                var response = WebClient.UploadData(args[0].ToString(vm, args[0], location).String, data);

                return new HassiumByteArray(response, new HassiumObject[0]);
            }

            [FunctionAttribute("func uploadfile (url : string, path : string) : list")]
            public HassiumList uploadfile(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                var response = WebClient.UploadFile(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

                return new HassiumByteArray(response, new HassiumObject[0]);
            }

            [FunctionAttribute("func uploadstr (url : string, str : string) : string")]
            public HassiumString uploadstr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                var response = WebClient.UploadString(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

                return new HassiumString(response);
            }

            [FunctionAttribute("func urldecode (url : string) : string")]
            public HassiumString urldecode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(HttpUtility.UrlDecode(args[0].ToString(vm, args[0], location).String));
            }

            [FunctionAttribute("func urlencode (url : string) : string")]
            public HassiumString urlencode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(HttpUtility.UrlEncode(args[0].ToString(vm, args[0], location).String));
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(string attrib)
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
