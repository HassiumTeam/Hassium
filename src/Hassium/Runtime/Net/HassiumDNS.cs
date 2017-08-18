using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Net;

namespace Hassium.Runtime.Net
{
    public class HassiumDNS : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("DNS");

        public HassiumDNS()
        {
            AddType(TypeDefinition);

            AddAttribute("gethost", gethost, 1);
            AddAttribute("gethosts", gethosts, 1);
            AddAttribute("getip", getip, 1);
            AddAttribute("getips", getips, 1);
        }

        [FunctionAttribute("func gethost (IPAddrOrStr : object) : string")]
        public HassiumString gethost(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return gethosts(vm, self, location, args[0]).Values[0] as HassiumString;
        }

        [FunctionAttribute("func gethosts (IPAddrOrStr : object) : list")]
        public HassiumList gethosts(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            var hosts = Dns.GetHostAddresses(args[0].ToString(vm, args[0], location).String);

            foreach (var host in hosts)
                HassiumList.add(vm, list, location, new HassiumString(host.ToString()));

            return list;
        }

        [FunctionAttribute("func getip (host : string) : IPAddr")]
        public HassiumIPAddr getip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return getips(vm, self, location, args[0]).Values[0] as HassiumIPAddr;
        }

        [FunctionAttribute("func getips (host : string) : list")]
        public HassiumList getips(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);

            var ips = Dns.GetHostAddresses(args[0].ToString(vm, args[0], location).String);

            foreach (var ip in ips)
                HassiumList.add(vm, list, location, HassiumIPAddr.Attribs[INVOKE].Invoke(vm, location, new HassiumString(ip.ToString())));

            return list;
        }
    }
}
