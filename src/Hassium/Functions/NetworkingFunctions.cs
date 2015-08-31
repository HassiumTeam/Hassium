using System.Net;
using System.Text;

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
