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

using System.Net.Sockets;
using System.Text;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumSocket : HassiumObject
    {
        public Socket Value { get; set; }

        public HassiumSocket(Socket value)
        {
            Value = value;

            Attributes.Add("connect", new InternalFunction(connect, 2));
            Attributes.Add("accept", new InternalFunction(accept, 0));
            Attributes.Add("available", new InternalFunction(x => value.Available, 0, true));
            Attributes.Add("connected", new InternalFunction(x => value.Connected, 0, true));
            Attributes.Add("protocolType", new InternalFunction(x => value.ProtocolType.ToString(), 0, true));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("disconnect", new InternalFunction(disconnect, 1));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("listen", new InternalFunction(listen, 1));
            Attributes.Add("sendFile", new InternalFunction(sendFile, 1));
            Attributes.Add("send", new InternalFunction(send, 1));
            Attributes.Add("stream", new HassiumProperty("stream", x => new HassiumNetworkStream(new NetworkStream(Value)), null, true));
        }

        private HassiumObject connect(HassiumObject[] args)
        {
            Value.Connect(args[0].ToString(), args[1].HInt().Value);
            return null;
        }

        private HassiumObject accept(HassiumObject[] args)
        {
            return new HassiumSocket(Value.Accept());
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Shutdown(SocketShutdown.Both);
            Value.Close();
            return null;
        }

        private HassiumObject disconnect(HassiumObject[] args)
        {
            Value.Disconnect(args[0].HBool().Value);
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        private HassiumObject listen(HassiumObject[] args)
        {
            Value.Listen(args[0].HInt().Value);
            return null;
        }

        private HassiumObject sendFile(HassiumObject[] args)
        {
            Value.SendFile(args[0].ToString());
            return null;
        }

        private HassiumObject send(HassiumObject[] args)
        {
            return Value.Send(Encoding.ASCII.GetBytes(args[0].ToString()));
        }
    }
}