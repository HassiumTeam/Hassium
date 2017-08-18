using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Net;
using System.Web;

namespace Hassium.Runtime.Net
{
    public class HassiumWeb : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Web");

        public WebClient WebClient { get; set; }

        public HassiumWeb()
        {
            AddType(TypeDefinition);

            AddAttribute(INVOKE, _new, 0);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumWeb web)
        {
            web.AddAttribute("downloaddata", web.downloaddata, 1);
            web.AddAttribute("downloadfile", web.downloadfile, 2);
            web.AddAttribute("downloadstr", web.downloadstr, 1);
            web.AddAttribute("htmldecode", web.htmldecode, 1);
            web.AddAttribute("htmlencode", web.htmlencode, 1);
            web.AddAttribute("uploaddata", web.uploaddata, 2);
            web.AddAttribute("uploadfile", web.uploadfile, 2);
            web.AddAttribute("uploadstring", web.uploadstr, 2);
            web.AddAttribute("urldecode", web.urldecode, 1);
            web.AddAttribute("urlencode", web.urlencode, 1);
        }

        [FunctionAttribute("func new () : Web")]
        public static HassiumWeb _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumWeb web = new HassiumWeb();

            web.WebClient = new WebClient();
            ImportAttribs(web);

            return web;
        }

        [FunctionAttribute("func downloaddata (url : string) : list")]
        public HassiumList downloaddata(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            byte[] data = WebClient.DownloadData(args[0].ToString(vm, args[0], location).String);
            
            return new HassiumByteArray(data, new HassiumObject[0]);
        }

        [FunctionAttribute("func downloadfile (url : string, path : string) : null")]
        public HassiumNull downloadfile(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            WebClient.DownloadFile(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

            return Null;
        }

        [FunctionAttribute("func downloadstr (url : string) : string")]
        public HassiumString downloadstr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
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
            var response = WebClient.UploadFile(args[0].ToString(vm, args[0], location).String, args[1].ToString(vm, args[1], location).String);

            return new HassiumByteArray(response, new HassiumObject[0]);
        }
   
        [FunctionAttribute("func uploadstr (url : string, str : string) : string")]
        public HassiumString uploadstr(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
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
}
