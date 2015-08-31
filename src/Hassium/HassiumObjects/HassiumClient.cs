using System.Net;
using System.Text;

namespace Hassium.HassiumObjects
{
    public class HassiumClient: HassiumObject
    {
        public WebClient Value { get; private set; }

        public HassiumClient(WebClient value)
        {
            this.Value = value;
            this.Attributes.Add("downstr", new InternalFunction(downstr));
            this.Attributes.Add("downfile", new InternalFunction(downfile));
            this.Attributes.Add("upfile", new InternalFunction(upfile));
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

