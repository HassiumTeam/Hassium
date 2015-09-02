using System;
using System.Net;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListenerContext: HassiumObject
    {
        public HttpListenerContext Value { get; private set; }

        public HassiumHttpListenerContext(HttpListenerContext value)
        {
            this.Value = value;
            this.Attributes.Add("request", new InternalFunction(request));
            this.Attributes.Add("response", new InternalFunction(response));
        }

        private HassiumObject request(HassiumObject[] args)
        {
            return new HassiumHttpListenerRequest(this.Value.Request);
        }

        private HassiumObject response(HassiumObject[] args)
        {
            return new HassiumHttpListenerResponse(this.Value.Response);
        }
    }
}

