using System.Net;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListener: HassiumObject
    {
        public HttpListener Value { get; private set; }

        public HassiumHttpListener(HttpListener value)
        {
            Value = value;
            Attributes.Add("start", new InternalFunction(start));
            Attributes.Add("prefixAdd", new InternalFunction(prefixAdd));
            Attributes.Add("stop", new InternalFunction(stop));
            Attributes.Add("abort", new InternalFunction(abort));
            Attributes.Add("getContext", new InternalFunction(getContext));
            Attributes.Add("close", new InternalFunction(close));
        }

        private HassiumObject start(HassiumObject[] args)
        {
            Value.Start();
            return null;
        }

        private HassiumObject prefixAdd(HassiumObject[] args)
        {
            Value.Prefixes.Add(args[0].ToString());
            return null;
        }

        private HassiumObject stop(HassiumObject[] args)
        {
            Value.Stop();
            return null;
        }

        private HassiumObject abort(HassiumObject[] args)
        {
            Value.Abort();
            return null;
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Abort();
            return null;
        }

        private HassiumObject getContext(HassiumObject[] args)
        {
            return new HassiumHttpListenerContext(Value.GetContext());
        }
    }
}

