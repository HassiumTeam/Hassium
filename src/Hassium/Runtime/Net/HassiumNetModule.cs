namespace Hassium.Runtime.Net
{
    public class HassiumNetModule : InternalModule
    {
        public HassiumNetModule() : base("Net")
        {
            //AddAttribute("CGI", new HassiumCGI());
            AddAttribute("DNS", HassiumDNS.TypeDefinition);
            AddAttribute("IPAddr", HassiumIPAddr.TypeDefinition);
            AddAttribute("Socket", HassiumSocket.TypeDefinition);
            AddAttribute("SocketClosedException", HassiumSocketClosedException.TypeDefinition);
            AddAttribute("SocketListener", HassiumSocketListener.TypeDefinition);
            AddAttribute("Web", HassiumWeb.TypeDefinition);
        }
    }
}
