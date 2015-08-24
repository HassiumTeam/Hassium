using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hassium
{
    public class NetworkingFunctions : ILibrary
    {
        private static WebClient client = new WebClient();

        [IntFunc("dowstr")]
        public static object DowStr(object[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            return client.DownloadString(args[0].ToString());
        }

        [IntFunc("dowfile")]
        public static object DowFile(object[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            client.DownloadFile(args[0].ToString(), args[1].ToString());
            return null;
        }

        [IntFunc("upfile")]
        public static object UpFile(object[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            client.Headers.Add("Content-Type", "binary/octet-stream");
            return Encoding.ASCII.GetString(client.UploadFile(args[0].ToString(), "POST", args[1].ToString()));
        }
    }
}
