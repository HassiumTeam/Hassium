using System.Net;
using System.Text;

namespace Hassium.Functions
{
    public class NetworkingFunctions : ILibrary
    {
        [IntFunc("newclient")]
        public static HassiumObject NewClient(HassiumArray args)
        {
            return new HassiumClient(new WebClient());
        }
    }
}
