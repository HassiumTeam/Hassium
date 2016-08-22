using System;
using System.Net;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Net
{
    public class HassiumDns: HassiumObject
    {
        public HassiumDns()
        {
            AddAttribute("resolveAddress",    resolveAddress,   1);
            AddAttribute("resolveAddresses",  resolveAddresses, 1);
            AddAttribute("resolveHost",       resolveHost,      1);
            AddAttribute("resolveHosts",      resolveHosts,     1);
        }

        public HassiumString resolveAddress(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostEntry(args[0].ToString(vm).String).AddressList[0].ToString());
        }
        public HassiumList resolveAddresses(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            IPAddress[] addresses = Dns.GetHostEntry(args[0].ToString(vm).String).AddressList;
            foreach (IPAddress address in addresses)
                list.Add(vm, new HassiumString(address.ToString()));

            return list;
        }
        public HassiumString resolveHost(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostAddresses(args[0].ToString(vm).String)[0].ToString());
        }
        public HassiumList resolveHosts(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (IPAddress address in Dns.GetHostAddresses(args[0].ToString(vm).String))
                list.Add(vm, new HassiumString(address.ToString()));
            return list;
        }
    }
}