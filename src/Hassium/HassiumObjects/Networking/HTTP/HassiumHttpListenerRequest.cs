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
            Attributes.Add("contentLength", new InternalFunction(contentLength));
            Attributes.Add("httpMethod", new InternalFunction(httpMethod));
            Attributes.Add("inputStream", new InternalFunction(inputStream));
            Attributes.Add("localEndPoint", new InternalFunction(localEndPoint));
            Attributes.Add("queryString", new InternalFunction(queryString));
            Attributes.Add("rawUrl", new InternalFunction(rawUrl));
            Attributes.Add("remoteEndPoint", new InternalFunction(remoteEndPoint));
            Attributes.Add("url", new InternalFunction(url));
            Attributes.Add("userAgent", new InternalFunction(userAgent));
        }

        private HassiumObject contentLength(HassiumObject[] args)
        {
            return new HassiumNumber(Value.ContentLength64);
        }

        private HassiumObject httpMethod(HassiumObject[] args)
        {
            return new HassiumString(Value.HttpMethod);
        }

        private HassiumObject inputStream(HassiumObject[] args)
        {
            return new HassiumStream(Value.InputStream);
        }

        private HassiumObject localEndPoint(HassiumObject[] args)
        {
            return new HassiumString(Value.LocalEndPoint.ToString());
        }

        private HassiumObject queryString(HassiumObject[] args)
        {
            return new HassiumString(Value.QueryString.ToString());
        }

        private HassiumObject rawUrl(HassiumObject[] args)
        {
            return new HassiumString(Value.RawUrl);
        }

        private HassiumObject remoteEndPoint(HassiumObject[] args)
        {
            return new HassiumString(Value.RemoteEndPoint.ToString());
        }

        private HassiumObject url(HassiumObject[] args)
        {
            return new HassiumString(Value.Url.ToString());
        }

        private HassiumObject userAgent(HassiumObject[] args)
        {
            return new HassiumString(Value.UserAgent);
        }
    }
}

