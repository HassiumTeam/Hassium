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
            Attributes.Add("start", new InternalFunction(start, 0));
            Attributes.Add("prefixAdd", new InternalFunction(prefixAdd, 1));
            Attributes.Add("stop", new InternalFunction(stop, 0));
            Attributes.Add("abort", new InternalFunction(abort, 0));
            Attributes.Add("getContext", new InternalFunction(getContext, 0));
            Attributes.Add("close", new InternalFunction(close, 0));
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

