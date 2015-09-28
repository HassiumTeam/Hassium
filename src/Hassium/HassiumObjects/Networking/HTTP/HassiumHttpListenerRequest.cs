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

using System.Net;
using Hassium.Functions;
using Hassium.HassiumObjects.IO;

namespace Hassium.HassiumObjects.Networking.HTTP
{
    public class HassiumHttpListenerRequest : HassiumObject
    {
        public HttpListenerRequest Value { get; set; }

        public HassiumHttpListenerRequest(HttpListenerRequest value)
        {
            Value = value;
            Attributes.Add("contentLength", new InternalFunction(contentLength, 0, true));
            Attributes.Add("httpMethod", new InternalFunction(httpMethod, 0, true));
            Attributes.Add("inputStream", new InternalFunction(inputStream, 0, true));
            Attributes.Add("localEndPoint", new InternalFunction(localEndPoint, 0, true));
            Attributes.Add("queryString", new InternalFunction(queryString, 0, true));
            Attributes.Add("rawUrl", new InternalFunction(rawUrl, 0, true));
            Attributes.Add("remoteEndPoint", new InternalFunction(remoteEndPoint, 0, true));
            Attributes.Add("url", new InternalFunction(url, 0, true));
            Attributes.Add("userAgent", new InternalFunction(userAgent, 0, true));
        }

        private HassiumObject contentLength(HassiumObject[] args)
        {
            return Value.ContentLength64;
        }

        private HassiumObject httpMethod(HassiumObject[] args)
        {
            return Value.HttpMethod;
        }

        private HassiumObject inputStream(HassiumObject[] args)
        {
            return new HassiumStream(Value.InputStream);
        }

        private HassiumObject localEndPoint(HassiumObject[] args)
        {
            return Value.LocalEndPoint.ToString();
        }

        private HassiumObject queryString(HassiumObject[] args)
        {
            return Value.QueryString.ToString();
        }

        private HassiumObject rawUrl(HassiumObject[] args)
        {
            return Value.RawUrl;
        }

        private HassiumObject remoteEndPoint(HassiumObject[] args)
        {
            return Value.RemoteEndPoint.ToString();
        }

        private HassiumObject url(HassiumObject[] args)
        {
            return Value.Url.ToString();
        }

        private HassiumObject userAgent(HassiumObject[] args)
        {
            return Value.UserAgent;
        }
    }
}