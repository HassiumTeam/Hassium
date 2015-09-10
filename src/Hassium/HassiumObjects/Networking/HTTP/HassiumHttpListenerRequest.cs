using System.Net;
using Hassium.Functions;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListenerRequest: HassiumObject
    {
        public HttpListenerRequest Value { get; private set; }

        public HassiumHttpListenerRequest(HttpListenerRequest value)
        {
            Value = value;
            Attributes.Add("contentLength", new InternalFunction(contentLength, 0, true));
            Attributes.Add("httpMethod", new InternalFunction(httpMethod, 0, true));
            Attributes.Add("inputStream", new InternalFunction(inputStream, 0, true));
            Attributes.Add("localEndPoint", new InternalFunction(localEndPoint, 0, true));
            Attributes.Add("queryString", new InternalFunction(queryString, 0, true));
            Attributes.Add("rawUrl", new InternalFunction(rawUrl, 0, true));
            Attributes.Add("remoteEndPoint", new InternalFunction(remoteEndPoint, 0, true));
            Attributes.Add("url", new InternalFunction(url, 0, true));
            Attributes.Add("userAgent", new InternalFunction(userAgent, 0, true));
        }

        private HassiumObject contentLength(HassiumObject[] args)
        {
            return Value.ContentLength64;
        }

        private HassiumObject httpMethod(HassiumObject[] args)
        {
            return Value.HttpMethod;
        }

        private HassiumObject inputStream(HassiumObject[] args)
        {
            return new HassiumStream(Value.InputStream);
        }

        private HassiumObject localEndPoint(HassiumObject[] args)
        {
            return Value.LocalEndPoint.ToString();
        }

        private HassiumObject queryString(HassiumObject[] args)
        {
            return Value.QueryString.ToString();
        }

        private HassiumObject rawUrl(HassiumObject[] args)
        {
            return Value.RawUrl;
        }

        private HassiumObject remoteEndPoint(HassiumObject[] args)
        {
            return Value.RemoteEndPoint.ToString();
        }

        private HassiumObject url(HassiumObject[] args)
        {
            return Value.Url.ToString();
        }

        private HassiumObject userAgent(HassiumObject[] args)
        {
            return Value.UserAgent;
        }
    }
}

