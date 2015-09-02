using System;
using System.Net;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListener: HassiumObject
    {
        public HttpListener Value { get; private set; }

        public HassiumHttpListener(HttpListener value)
        {
            this.Value = value;
            this.Attributes.Add("start", new InternalFunction(start));
            this.Attributes.Add("prefixAdd", new InternalFunction(prefixAdd));
            this.Attributes.Add("stop", new InternalFunction(stop));
            this.Attributes.Add("abort", new InternalFunction(abort));
            this.Attributes.Add("getContext", new InternalFunction(getContext));
        }

        private HassiumObject start(HassiumObject[] args)
        {
            this.Value.Start();
            return null;
        }

        private HassiumObject prefixAdd(HassiumObject[] args)
        {
            this.Value.Prefixes.Add(args[0].ToString());
            return null;
        }

        private HassiumObject stop(HassiumObject[] args)
        {
            this.Value.Stop();
            return null;
        }

        private HassiumObject abort(HassiumObject[] args)
        {
            this.Value.Abort();
            return null;
        }

        private HassiumObject close(HassiumObject[] args)
        {
            this.Value.Abort();
            return null;
        }

        private HassiumObject getContext(HassiumObject[] args)
        {
            return new HassiumHttpListenerContext(this.Value.GetContext());
        }
    }
}

