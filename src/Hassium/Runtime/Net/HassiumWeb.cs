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

            [DocStr(
                "@desc Constructs a new Web object.",
                "@returns The new Web object."
                )]
            [FunctionAttribute("func new () : Web")]
            public static HassiumWeb _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumWeb web = new HassiumWeb();

                web.WebClient = new WebClient();

                return web;
            }

            [DocStr(
                "@desc Downloads bytes from the specified url and returns them in a list.",
                "@param url The string url to download from.",
                "@returns A new list of downloaded bytes."
                )]
            [FunctionAttribute("func downloaddata (url : string) : list")]
            public HassiumList downloaddata(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                byte[] data = WebClient.DownloadData(args[0].ToString(vm, args[0], location).String);

                return new HassiumByteArray(data, new HassiumObject[0]);
            }

            [DocStr(
                "@desc Downloads a file from the specified url and saves it to the specified path.",
                "@param url The string url to download from.",
                "@param path The string path to save the file to.",
                "@returns null."
                )]
            [FunctionAttribute("func downloadfile (url : string, path : string) : null")]
            public HassiumNull downloadfile(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                WebClient.DownloadFile(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

                return Null;
            }

            [DocStr(
                "@desc Downloads the specified url as a string and returns it.",
                "@param url The url to download from.",
                "@returns The downloaded string."
                )]
            [FunctionAttribute("func downloadstr (url : string) : string")]
            public HassiumString downloadstr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                return new HassiumString(WebClient.DownloadString(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Decodes the specified html encoded string and returns it.",
                "@param str The html encoded string.",
                "@returns The decoded string."
                )]
            [FunctionAttribute("func htmldecode (str : string) : string")]
            public HassiumString htmldecode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(HttpUtility.HtmlDecode(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Encoded the specified string with html encoding and returns it.",
                "@param str The string to be html encoded.",
                "@returns The encoded string."
                )]
            [FunctionAttribute("func htmlencode (str : string) : string")]
            public HassiumString htmlencode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(HttpUtility.HtmlEncode(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Uploads the given byte list to the specified url string and returns the response.",
                "@param url The url to upload to.",
                "@param data The list of bytes to upload.",
                "@returns The response from the server as a list of bytes."
                )]
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

            [DocStr(
                "@desc Uploads the given file path to the specified url string and returns the response.",
                "@param url The url to upload to.",
                "@param path The file path to upload.",
                "@returns The response from the server as a list of bytes."
                )]
            [FunctionAttribute("func uploadfile (url : string, path : string) : list")]
            public HassiumList uploadfile(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                var response = WebClient.UploadFile(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

                return new HassiumByteArray(response, new HassiumObject[0]);
            }

            [DocStr(
                "@desc Uploads the given string to the specified url string and returns the response.",
                "@param url The url to upload to.",
                "@param str The string to upload.",
                "@returns The response from the server as a list of bytes."
                )]
            [FunctionAttribute("func uploadstr (url : string, str : string) : string")]
            public HassiumString uploadstr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var WebClient = (self as HassiumWeb).WebClient;
                var response = WebClient.UploadString(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

                return new HassiumString(response);
            }

            [DocStr(
                "@desc Decodes the specified url encoded string and returns it.",
                "@param str The url encoded string.",
                "@returns The decoded string."
                )]
            [FunctionAttribute("func urldecode (url : string) : string")]
            public HassiumString urldecode(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return new HassiumString(HttpUtility.UrlDecode(args[0].ToString(vm, args[0], location).String));
            }

            [DocStr(
                "@desc Encoded the specified string with url encoding and returns it.",
                "@param str The string to be url encoded.",
                "@returns The encoded string."
                )]
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
