// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//  * Redistributions of source code must retain the above copyright notice, this list
//    of conditions and the following disclaimer.
// 
//  * Redistributions in binary form must reproduce the above copyright notice, this
//    list of conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
// Neither the name of the copyright holder nor the names of its contributors may be
// used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.

using System.Linq;
using System.Net;
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

        public HassiumObject getHostAddresses(HassiumObject[] args)
        {
            return new HassiumArray(Dns.GetHostAddresses(args[0].ToString()).Select(x => new HassiumString(x.ToString())));
        }

        public HassiumObject getHostEntry(HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostEntry(IPAddress.Parse(args[0].ToString())).HostName);
        }

        public HassiumObject getHostName(HassiumObject[] args)
        {
            return new HassiumString(Dns.GetHostName());
        }

        public HassiumObject resolve(HassiumObject[] args)
        {
            return Dns.GetHostEntry(args[0].ToString()).HostName;
        }
    }
}
