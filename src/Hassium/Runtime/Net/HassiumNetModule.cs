namespace Hassium.Runtime.Net
{
    public class HassiumNetModule : InternalModule
    {
        public HassiumNetModule() : base("Net")
        {
            AddAttribute("DNS", new HassiumDNS());
            AddAttribute("IPAddr", new HassiumIPAddr());
            AddAttribute("Socket", new HassiumSocket());
            AddAttribute("SocketListener", new HassiumSocketListener());
            AddAttribute("SocketClosedException", new HassiumSocketClosedException());
            AddAttribute("Web", new HassiumWeb());
        }
    }
}
