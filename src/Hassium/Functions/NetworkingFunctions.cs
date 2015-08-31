using System.Net;
using System.Text;
using Hassium.HassiumObjects;

namespace Hassium.Functions
{
    public class NetworkingFunctions : ILibrary
    {
        [IntFunc("newclient")]
        public static HassiumObject NewClient(HassiumObject[] args)
        {
            return new HassiumClient(new WebClient());
        }
    }
}
