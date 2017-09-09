using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Net;

namespace Hassium.Runtime.Net
{
    public class HassiumDNS : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new DNSTypeDef();

        public HassiumDNS()
        {
            AddType(TypeDefinition);
        }

        [DocStr(
            "@desc A class containing methods for resolving IP addresses and hostnames through the DNS protocol.",
            "@returns DNS."
            )]
        public class DNSTypeDef : HassiumTypeDefinition
        {
            public DNSTypeDef() : base("DNS")
            {
                AddAttribute("gethost", gethost, 1);
                AddAttribute("gethosts", gethosts, 1);
                AddAttribute("getip", getip, 1);
                AddAttribute("getips", getips, 1);
            }

            [DocStr(
                "@desc Gets the first hostname of the specified Net.IPAddr object or string ip.",
                "@param IPAddrOrStr The Net.IPAddr object or string ip address.",
                "@returns The first hostname as a string."
            )]
            [FunctionAttribute("func gethost (IPAddrOrStr : object) : string")]
            public HassiumString gethost(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return gethosts(vm, self, location, args[0]).Values[0] as HassiumString;
            }

            [DocStr(
                "@desc Gets a list of hostnames for the specified Net.IPAddr or string ip.",
                "@param IPAddrOrStr The Net.IPAddr object or string ip address.",
                "@returns The list of hostnames."
            )]
            [FunctionAttribute("func gethosts (IPAddrOrStr : object) : list")]
            public HassiumList gethosts(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumList list = new HassiumList(new HassiumObject[0]);

                var hosts = Dns.GetHostAddresses(args[0].ToString(vm, args[0], location).String);

                foreach (var host in hosts)
                    HassiumList.add(vm, list, location, new HassiumString(host.ToString()));

                return list;
            }

            [DocStr(
                "@desc Gets the first ip address of the specified hostname as a Net.IPAddr object.",
                "@param host The hostname as a string.",
                "@returns A new Net.IPAddr object."
            )]
            [FunctionAttribute("func getip (host : string) : IPAddr")]
            public HassiumIPAddr getip(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                return getips(vm, self, location, args[0]).Values[0] as HassiumIPAddr;
            }

            [DocStr(
                "@desc Gets a list of Net.IPAddr ip addresses for the specified hostname.",
                "@paran host The hostname as a string.",
                "@returns A new list of Net.IPAddr objects."
            )]
            [FunctionAttribute("func getips (host : string) : list")]
            public HassiumList getips(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumList list = new HassiumList(new HassiumObject[0]);

                var ips = Dns.GetHostAddresses(args[0].ToString(vm, args[0], location).String);

                foreach (var ip in ips)
                    HassiumList.add(vm, list, location, HassiumIPAddr.IPAddrTypeDef._new(vm, null, location, new HassiumString(ip.ToString())));

                return list;
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
