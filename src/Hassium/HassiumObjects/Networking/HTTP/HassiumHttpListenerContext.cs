using System.Net;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListenerContext: HassiumObject
    {
        public HttpListenerContext Value { get; private set; }

        public HassiumHttpListenerContext(HttpListenerContext value)
        {
            Value = value;
            Attributes.Add("request", new InternalFunction(request, 0, true));
            Attributes.Add("response", new InternalFunction(response, 0, true));
        }

        private HassiumObject request(HassiumObject[] args)
        {
            return new HassiumHttpListenerRequest(Value.Request);
        }

        private HassiumObject response(HassiumObject[] args)
        {
            return new HassiumHttpListenerResponse(Value.Response);
        }
    }
}

