using System;
using System.Net;
using System.Text;

namespace Hassium
{
    public partial class BuiltInFunctions
    {
        private static WebClient client = new WebClient();

        public static object DowStr(object[] args)
        {
            return client.DownloadString(args[0].ToString());
        }

        public static object DowFile(object[] args)
        {
            client.DownloadFile(args[0].ToString(), args[1].ToString());
            return null;
        }

        public static object UpFile(object[] args)
        {
            client.Headers.Add("Content-Type", "binary/octet-stream");
            return Encoding.ASCII.GetString(client.UploadFile(args[0].ToString(), "POST", args[1].ToString()));
        }
    }
}

