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

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using Hassium.Interpreter;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Networking.Mail
{
    public class HassiumSmtpClient: HassiumObject
    {
        public SmtpClient Value { get; private set; }

        public HassiumSmtpClient(HassiumString server, HassiumInt port)
        {
            Value = new SmtpClient(server.ToString(), port.Value);
            Attributes.Add("send", new InternalFunction(send, new int[] {1, 4}));

            Attributes.Add("credentials", new HassiumProperty("credentials",
                x =>
                {
                    var cred = new HassiumObject();
                    cred.Attributes.Add("username",
                        new HassiumProperty("username", y => (Value.Credentials as NetworkCredential).UserName,
                            (self, y) =>
                            {
                                Value.Credentials =
                                    new NetworkCredential(y[0].ToString(),
                                        (Value.Credentials as NetworkCredential).Password);
                                return null;
                            }));
                    cred.Attributes.Add("password",
                        new HassiumProperty("password", y => (Value.Credentials as NetworkCredential).Password,
                            (self, y) =>
                            {
                                Value.Credentials =
                                    new NetworkCredential((Value.Credentials as NetworkCredential).UserName,
                                        y[0].ToString());
                                return null;
                            }));

                    return cred;
                },
                (self, y) =>
                {
                    if(y[0] is HassiumTuple)
                    {
                        var tuple = (HassiumTuple) y[0];
                        Value.Credentials = new NetworkCredential(tuple.Items[0].ToString(), tuple.Items[1].ToString());
                    }
                         
                    return null;
                }));

            Attributes.Add("enableSsl",
                new HassiumProperty("enableSsl", x => Value.EnableSsl, (self, x) => Value.EnableSsl = x[0].HBool()));
        }

        private HassiumObject send(HassiumObject[] args)
        {
            if (args.Length == 1)
                Value.Send(((HassiumMailMessage)args[0]).Value);
            else if (args.Length >= 4)
                Value.Send(args[0].ToString(), args[1].ToString(), args[2].ToString(), args[3].ToString());
            else
                throw new ParseException("Incorrect arguments for send", Program.CurrentInterpreter.NodePos.Peek());

            return null;
        }
    }
}
