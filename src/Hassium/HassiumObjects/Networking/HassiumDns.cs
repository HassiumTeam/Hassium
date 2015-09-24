using System.Linq;
using System.Net;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumDns : HassiumObject
    {
        public HassiumDns()
        {
            Attributes.Add("getHostAddresses", new InternalFunction(getHostAddresses, 1));
            Attributes.Add("getHostEntry", new InternalFunction(getHostEntry, 1));
            Attributes.Add("getHostName", new InternalFunction(getHostName, 0));
            Attributes.Add("resolve", new InternalFunction(resolve, 1));
        }

        private HassiumObject getHostAddresses(HassiumObject[] args)
        {
            IPAddress[] array = Dns.GetHostAddresses(args[0].ToString());
            HassiumString[] addresses = new HassiumString[array.Length];
            for (int x = 0; x < array.Length; x++)
                addresses[x] = new HassiumString(array[x].ToString());

            return new HassiumArray(addresses);
        }

        private HassiumObject getHostEntry(HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostEntry(IPAddress.Parse(args[0].ToString())).ToString());
        }

        private HassiumObject getHostName(HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostName());
        }

        private HassiumObject resolve(HassiumObject[] args)
        {
            return new HassiumString(Dns.Resolve(args[0].ToString()).ToString());
        }
    }
}
