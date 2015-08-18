using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hassium
{
    public class NetworkingFunctions : ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();
            result.Add("dowstr", new InternalFunction(NetworkingFunctions.DowStr));
            result.Add("dowfile", new InternalFunction(NetworkingFunctions.DowFile));
            result.Add("upfile", new InternalFunction(NetworkingFunctions.UpFile));

            return result;
        }
        private static WebClient client = new WebClient();

        public static object DowStr(object[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            return client.DownloadString(args[0].ToString());
        }

        public static object DowFile(object[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            client.DownloadFile(args[0].ToString(), args[1].ToString());
            return null;
        }

        public static object UpFile(object[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            client.Headers.Add("Content-Type", "binary/octet-stream");
            return Encoding.ASCII.GetString(client.UploadFile(args[0].ToString(), "POST", args[1].ToString()));
        }
    }
}
