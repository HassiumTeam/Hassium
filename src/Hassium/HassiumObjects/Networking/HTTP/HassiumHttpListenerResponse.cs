using System;
using System.Net;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;
using Hassium.HassiumObjects.Networking;

namespace Hassium
{
    public class HassiumHttpListenerResponse: HassiumObject
    {
        public HttpListenerResponse Value { get; private set; }

        public HassiumHttpListenerResponse(HttpListenerResponse value)
        {
            this.Value = value;
            this.Attributes.Add("contentEncoding", new InternalFunction(contentEncoding));
            this.Attributes.Add("contentLength", new InternalFunction(contentLength));
            this.Attributes.Add("outputStream", new InternalFunction(outputStream));
            this.Attributes.Add("abort", new InternalFunction(abort));
            this.Attributes.Add("appendHeader", new InternalFunction(appendHeader));
            this.Attributes.Add("close", new InternalFunction(close));
            this.Attributes.Add("redirect", new InternalFunction(redirect));
        }

        private HassiumObject contentEncoding(HassiumObject[] args)
        {
            this.Value.ContentEncoding = ((HassiumEncoding)args[0]).Value;
            return null;
        }

        private HassiumObject contentLength(HassiumObject[] args)
        {
            this.Value.ContentLength64 = ((HassiumNumber)args[0]).ValueInt;
            return null;
        }

        private HassiumObject outputStream(HassiumObject[] args)
        {
            return new HassiumStream(this.Value.OutputStream);
        }

        private HassiumObject abort(HassiumObject[] args)
        {
            this.Value.Abort();
            return null;
        }

        private HassiumObject appendHeader(HassiumObject[] args)
        {
            this.Value.AppendHeader(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject close(HassiumObject[] args)
        {
            this.Value.Close();
            return null;
        }

        private HassiumObject redirect(HassiumObject[] args)
        {
            this.Value.Redirect(args[0].ToString());
            return null;
        }
    }
}

