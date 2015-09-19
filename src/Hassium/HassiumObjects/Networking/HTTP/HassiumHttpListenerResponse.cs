using System.Net;
using Hassium.Functions;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Text;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListenerResponse : HassiumObject
    {
        public HttpListenerResponse Value { get; private set; }

        public HassiumHttpListenerResponse(HttpListenerResponse value)
        {
            Value = value;
            Attributes.Add("contentEncoding",
                new HassiumProperty("contentEncoding", x => new HassiumEncoding(Value.ContentEncoding),
                    x =>
                    {
                        value.ContentEncoding = ((HassiumEncoding) x[0]).Value;
                        return null;
                    }));
            Attributes.Add("contentLength", new HassiumProperty("contentLength", x => value.ContentLength64,
                x =>
                {
                    value.ContentLength64 = x[0].HInt().Value;
                    return null;
                }));
            Attributes.Add("outputStream", new InternalFunction(outputStream, 0, true));
            Attributes.Add("abort", new InternalFunction(abort, 0));
            Attributes.Add("appendHeader", new InternalFunction(appendHeader, 2));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("redirect", new InternalFunction(redirect, 1));
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