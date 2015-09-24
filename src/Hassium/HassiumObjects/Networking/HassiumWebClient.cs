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
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumWebClient : HassiumObject
    {
        public WebClient Value { get; private set; }

        public HassiumWebClient(WebClient value)
        {
            Value = value;
            Attributes.Add("downloadString", new InternalFunction(downstr, 1));
            Attributes.Add("downloadFile", new InternalFunction(downfile, 2));
            Attributes.Add("uploadFile", new InternalFunction(upfile, 2));
            Attributes.Add("downloadData", new InternalFunction(downloadData, 1));
        }

        private HassiumObject downstr(HassiumObject[] args)
        {
            return new HassiumString(Value.DownloadString(args[0].ToString()));
        }

        private HassiumObject downfile(HassiumObject[] args)
        {
            Value.DownloadFile(args[0].ToString(), args[1].ToString());
            return null;
        }

        private HassiumObject upfile(HassiumObject[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Value.Headers.Add("Content-Type", "binary/octet-stream");
            return
                new HassiumString(
                    Encoding.ASCII.GetString(Value.UploadFile(args[0].ToString(), "POST", args[1].ToString())));
        }

        private HassiumObject downloadData(HassiumObject[] args)
        {
            byte[] data = Value.DownloadData(args[0].ToString());
            HassiumByte[] result = new HassiumByte[data.Length]; 
            for (int x = 0; x < result.Length; x++)
                result[x] = new HassiumByte(data[x]);

            return new HassiumArray(result);       
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}