using System.Text;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Text
{
    public class HassiumEncoding: HassiumObject
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
            Attributes.Add("bodyName", new InternalFunction(bodyName));
            Attributes.Add("headerName", new InternalFunction(headerName));

        }

        private HassiumObject bodyName(HassiumObject[] args)
        {
            return new HassiumString(Value.BodyName);
        }

        private HassiumObject headerName(HassiumObject[] args)
        {
            return new HassiumString(Value.HeaderName);
        }
    }
}

