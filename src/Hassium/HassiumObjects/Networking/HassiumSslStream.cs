
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Types;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Networking
{
    public class HassiumSslStream: HassiumObject
    {
        public SslStream Value { get; private set; }

        public HassiumSslStream(HassiumStream stream)
        {
            Value = new SslStream(stream.Value);
            addAttributes();
        }

        public HassiumSslStream(HassiumStream stream, HassiumBool leaveInnerStreamOpen)
        {
            Value = new SslStream(stream.Value, leaveInnerStreamOpen.Value);
            addAttributes();
        }

        public HassiumSslStream(HassiumStream stream, HassiumBool leaveInnerStreamOpen, HassiumObject genericRemoteCertificateValidationCallback)
        {
            if (genericRemoteCertificateValidationCallback is HassiumBool)
            {
                if (((HassiumBool)genericRemoteCertificateValidationCallback).Value)
                    Value = new SslStream(stream.Value, leaveInnerStreamOpen.Value, new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true));
            }
            else
            {
                Value = new SslStream(stream.Value, leaveInnerStreamOpen, null);
            }
            addAttributes();
        }

        private void addAttributes()
        {
            Attributes.Add("authenticateAsClient", new InternalFunction(authenticateAsClient, 1));
            Attributes.Add("close", new InternalFunction(close, 0));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("flush", new InternalFunction(flush, 0));
            Attributes.Add("readByte", new InternalFunction(readByte, 0));
            Attributes.Add("writeByte", new InternalFunction(writeByte, 0));
        }

        private HassiumObject authenticateAsClient(HassiumObject[] args)
        {
            Value.AuthenticateAsClient(args[0].ToString());
            return null;
        }

        private HassiumObject close(HassiumObject[] args)
        {
            Value.Close();
            return null;
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();
            return null;
        }

        private HassiumObject flush(HassiumObject[] args)
        {
            Value.Flush();
            return null;
        }

        private HassiumObject readByte(HassiumObject[] args)
        {
            return new HassiumInt(Value.ReadByte());
        }

        private HassiumObject writeByte(HassiumObject[] args)
        {
            Value.WriteByte(((HassiumByte)args[0]).Value);
            return null;
        }
    }
}
