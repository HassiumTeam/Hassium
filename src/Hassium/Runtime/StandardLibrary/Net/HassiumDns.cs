using System;
using System.Net;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Net
{
    public class HassiumDns: HassiumObject
    {
        public HassiumDns()
        {
            Attributes.Add("resolveAddress",    new HassiumFunction(resolveAddress, 1));
            Attributes.Add("resolveAddresses",  new HassiumFunction(resolveAddresses, 1));
            Attributes.Add("resolveHost",       new HassiumFunction(resolveHost, 1));
            Attributes.Add("resolveHosts",      new HassiumFunction(resolveHosts, 1));
            AddType("Dns");
        }

        private HassiumString resolveAddress(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostEntry(HassiumString.Create(args[0]).Value).AddressList[0].ToString());
        }
        private HassiumList resolveAddresses(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            IPAddress[] addresses = Dns.GetHostEntry(HassiumString.Create(args[0]).Value).AddressList;
            foreach (IPAddress address in addresses)
                list.Add(vm, new HassiumString(address.ToString()));

            return list;
        }
        private HassiumString resolveHost(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostAddresses(HassiumString.Create(args[0]).Value)[0].ToString());
        }
        private HassiumList resolveHosts(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumList list = new HassiumList(new HassiumObject[0]);
            IPAddress[] addresses = Dns.GetHostAddresses(HassiumString.Create(args[0]).Value);
            foreach (IPAddress address in addresses)
                list.Add(vm, new HassiumString(address.ToString()));

            return list;
        }
    }
}
