using System.Net;
using Hassium.Functions;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Text;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListenerResponse: HassiumObject
    {
        public HttpListenerResponse Value { get; private set; }

        public HassiumHttpListenerResponse(HttpListenerResponse value)
        {
            Value = value;
            Attributes.Add("contentEncoding", new InternalFunction(contentEncoding));
            Attributes.Add("contentLength", new InternalFunction(contentLength));
            Attributes.Add("outputStream", new InternalFunction(outputStream));
            Attributes.Add("abort", new InternalFunction(abort));
            Attributes.Add("appendHeader", new InternalFunction(appendHeader));
            Attributes.Add("close", new InternalFunction(close));
            Attributes.Add("redirect", new InternalFunction(redirect));
        }

        private HassiumObject contentEncoding(HassiumObject[] args)
        {
            Value.ContentEncoding = ((HassiumEncoding)args[0]).Value;
            return null;
        }

        private HassiumObject contentLength(HassiumObject[] args)
        {
            Value.ContentLength64 = ((HassiumNumber)args[0]).ValueInt;
            return null;
        }

        private HassiumObject outputStream(HassiumObject[] args)
        {
            return new HassiumStream(Value.OutputStream);
        }

        private HassiumObject abort(HassiumObject[] args)
        {
            Value.Abort();
            return null;
        }

        private HassiumObject appendHeader(HassiumObject[] args)
        {
            Value.AppendHeader(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        private HassiumObject redirect(HassiumObject[] args)
        {
            Value.Redirect(args[0].ToString());
            return null;
        }
    }
}

