using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Net
{
    public class HassiumNetModule: InternalModule
    {
        public HassiumNetModule() : base("Net")
        {
            Attributes.Add("ConnectionListener",    new HassiumConnectionListener());
            Attributes.Add("Dns",                   new HassiumDns());
            Attributes.Add("HttpUtility",           new HassiumHttpUtility());
            Attributes.Add("NetConnection",         new HassiumNetConnection());
            Attributes.Add("Socket",                new HassiumSocket());
            Attributes.Add("WebClient",             new HassiumWebClient());
            Attributes.Add("CGI", new HassiumCGI());
        }
    }
}

