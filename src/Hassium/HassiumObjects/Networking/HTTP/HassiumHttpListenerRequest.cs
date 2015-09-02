using System;
using System.Net;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListenerRequest: HassiumObject
    {
        public HttpListenerRequest Value { get; private set; }

        public HassiumHttpListenerRequest(HttpListenerRequest value)
        {
            this.Value = value;
            this.Attributes.Add("contentLength", new InternalFunction(contentLength));
            this.Attributes.Add("httpMethod", new InternalFunction(httpMethod));
            this.Attributes.Add("inputStream", new InternalFunction(inputStream));
            this.Attributes.Add("localEndPoint", new InternalFunction(localEndPoint));
            this.Attributes.Add("queryString", new InternalFunction(queryString));
            this.Attributes.Add("rawUrl", new InternalFunction(rawUrl));
            this.Attributes.Add("remoteEndPoint", new InternalFunction(remoteEndPoint));
            this.Attributes.Add("url", new InternalFunction(url));
            this.Attributes.Add("userAgent", new InternalFunction(userAgent));
        }

        private HassiumObject contentLength(HassiumObject[] args)
        {
            return new HassiumNumber(this.Value.ContentLength64);
        }

        private HassiumObject httpMethod(HassiumObject[] args)
        {
            return new HassiumString(this.Value.HttpMethod);
        }

        private HassiumObject inputStream(HassiumObject[] args)
        {
            return new HassiumStream(this.Value.InputStream);
        }

        private HassiumObject localEndPoint(HassiumObject[] args)
        {
            return new HassiumString(this.Value.LocalEndPoint.ToString());
        }

        private HassiumObject queryString(HassiumObject[] args)
        {
            return new HassiumString(this.Value.QueryString.ToString());
        }

        private HassiumObject rawUrl(HassiumObject[] args)
        {
            return new HassiumString(this.Value.RawUrl);
        }

        private HassiumObject remoteEndPoint(HassiumObject[] args)
        {
            return new HassiumString(this.Value.RemoteEndPoint.ToString());
        }

        private HassiumObject url(HassiumObject[] args)
        {
            return new HassiumString(this.Value.Url.ToString());
        }

        private HassiumObject userAgent(HassiumObject[] args)
        {
            return new HassiumString(this.Value.UserAgent);
        }
    }
}

