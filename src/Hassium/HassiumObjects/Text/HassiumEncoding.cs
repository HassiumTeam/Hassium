using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumEncoding : HassiumObject
    {
        public Encoding Value { get; private set; }

        public HassiumEncoding(HassiumString type)
        {
            switch (type.Value.ToUpper())
            {
                case "UTF8":
                    Value = Encoding.UTF8;
                    break;
                case "UTF7":
                    Value = Encoding.UTF7;
                    break;
                case "UTF32":
                    Value = Encoding.UTF32;
                    break;
                case "UNICODE":
                    Value = Encoding.Unicode;
                    break;
                default:
                    Value = Encoding.ASCII;
                    break;
            }
            Attributes.Add("bodyName", new InternalFunction(bodyName, 0, true));
            Attributes.Add("headerName", new InternalFunction(headerName, 0, true));
            Attributes.Add("getChar", new InternalFunction(getChar, 1));
            Attributes.Add("getByte", new InternalFunction(getByte, 1));
            Attributes.Add("getBytes", new InternalFunction(getBytes, 1));
        }

        public HassiumEncoding(Encoding type)
        {
            Value = type;
            Attributes.Add("bodyName", new InternalFunction(bodyName, 0, true));
            Attributes.Add("headerName", new InternalFunction(headerName, 0, true));
        }

        private HassiumObject bodyName(HassiumObject[] args)
        {
            return new HassiumString(Value.BodyName);
        }

        private HassiumObject headerName(HassiumObject[] args)
        {
            return new HassiumString(Value.HeaderName);
        }

        private HassiumObject getChar(HassiumObject[] args)
        {
            return Value.GetChars(new[] {(byte) args[0].HInt().Value})[0].ToString();
        }

        private HassiumObject getBytes(HassiumObject[] args)
        {
            return Value.GetBytes(args[0].HString().Value).Select(x => new HassiumInt(x)).ToArray();
        }

        private HassiumObject getByte(HassiumObject[] args)
        {
            return (int) (Value.GetBytes(args[0].HString().Value)[0]);
        }
    }
}