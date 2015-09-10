using System.Net;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumWebClient: HassiumObject
    {
        public WebClient Value { get; private set; }

        public HassiumWebClient(WebClient value)
        {
            Value = value;
            Attributes.Add("downloadString", new InternalFunction(downstr, 1));
            Attributes.Add("downloadFile", new InternalFunction(downfile, 2));
            Attributes.Add("uploadFile", new InternalFunction(upfile, 2));
        }

        private HassiumObject downstr(HassiumObject[] args)
        {
            return new HassiumString(Value.DownloadString(args[0].ToString()));
        }

        private HassiumObject downfile(HassiumObject[] args)
        {
            Value.DownloadFile(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject upfile(HassiumObject[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Value.Headers.Add("Content-Type", "binary/octet-stream");
            return new HassiumString(Encoding.ASCII.GetString(Value.UploadFile(args[0].ToString(), "POST", args[1].ToString())));
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

