namespace Hassium.Runtime.Net
{
    public class HassiumNetModule : InternalModule
    {
        public HassiumNetModule() : base("Net")
        {
            AddAttribute("CGI", new HassiumCGI());
            AddAttribute("DNS", new HassiumDNS());
            AddAttribute("IPAddr", new HassiumIPAddr());
            AddAttribute("Socket", new HassiumSocket());
            AddAttribute("SocketClosedException", new HassiumSocketClosedException());
            AddAttribute("SocketListener", new HassiumSocketListener());
            AddAttribute("Web", new HassiumWeb());
        }
    }
}
