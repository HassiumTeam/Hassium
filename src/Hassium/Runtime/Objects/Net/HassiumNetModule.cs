using System;

namespace Hassium.Runtime.Objects.Net
{
    public class HassiumNetModule: InternalModule
    {
        public HassiumNetModule() : base("Net")
        {
            AddAttribute("CGI",                 new HassiumCGI());
            AddAttribute("ConnectionListener",  new HassiumConnectionListener());
            AddAttribute("Dns",                 new HassiumDns());
            AddAttribute("HttpUtility",         new HassiumHttpUtility());
            AddAttribute("NetConnection",       new HassiumNetConnection());
            AddAttribute("Socket",              new HassiumSocket());
            AddAttribute("WebClient",           new HassiumWebClient());
        }
    }
}

