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
            this.Value = value;
            this.Attributes.Add("downloadString", new InternalFunction(downstr));
            this.Attributes.Add("downloadFile", new InternalFunction(downfile));
            this.Attributes.Add("uploadFile", new InternalFunction(upfile));
        }

        private HassiumObject downstr(HassiumObject[] args)
        {
            return new HassiumString(Value.DownloadString(((HassiumString)args[0]).Value));
        }

        private HassiumObject downfile(HassiumObject[] args)
        {
            Value.DownloadFile(((HassiumString)args[0]).Value, ((HassiumString)args[1]).Value);
            return null;
        }

        private HassiumObject upfile(HassiumObject[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Value.Headers.Add("Content-Type", "binary/octet-stream");
            return new HassiumString(Encoding.ASCII.GetString(Value.UploadFile(((HassiumString)args[0]).Value, "POST", ((HassiumString)args[1]).Value)));
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

