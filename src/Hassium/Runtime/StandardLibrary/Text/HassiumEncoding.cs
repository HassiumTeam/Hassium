using System;
using System.Text;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Text
{
    public class HassiumEncoding: HassiumObject
    {
        public new Encoding Value { get; set; }
        public HassiumEncoding()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 1));
        }

        private HassiumEncoding _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumEncoding hassiumEncoding = new HassiumEncoding();
            switch (HassiumString.Create(args[0]).Value.ToUpper())
            {
                case "UTF8":
                    hassiumEncoding.Value = Encoding.UTF8;
                    break;
                case "UTF7":
                    hassiumEncoding.Value = Encoding.UTF7;
                    break;
                case "UTF32":
                    hassiumEncoding.Value = Encoding.UTF32;
                    break;
                case "UNICODE":
                    hassiumEncoding.Value = Encoding.Unicode;
                    break;
                default:
                    hassiumEncoding.Value = Encoding.ASCII;
                    break;
            }
            hassiumEncoding.Attributes.Add("bodyName",      new HassiumProperty(hassiumEncoding.get_BodyName));
            hassiumEncoding.Attributes.Add("encodingName",  new HassiumProperty(hassiumEncoding.get_EncodingName));
            hassiumEncoding.Attributes.Add("getBytes",      new HassiumFunction(hassiumEncoding.getBytes, 1));
            hassiumEncoding.Attributes.Add("headerName",    new HassiumProperty(hassiumEncoding.get_HeaderName));
            hassiumEncoding.AddType("Encoding");

            return hassiumEncoding;
        }

        public HassiumString get_BodyName(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.BodyName);
        }
        public HassiumString get_EncodingName(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.EncodingName);
        }
        public HassiumList getBytes(VirtualMachine vm, HassiumObject[] args)
        {
            byte[] bytes = Value.GetBytes(HassiumString.Create(args[0]).Value);
            HassiumList list = new HassiumList(new HassiumObject[0]);
            foreach (byte b in bytes)
                list.Value.Add(new HassiumChar((char)b));

            return list;
        }
        public HassiumString get_HeaderName(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumString(Value.HeaderName);
        }
    }
}