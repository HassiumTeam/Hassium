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
            switch (type.Value)
            {
                case "ASCII":
                    this.Value = Encoding.ASCII;
                    break;
                case "UTF8":
                    this.Value = Encoding.UTF8;
                    break;
                case "UTF7":
                    this.Value = Encoding.UTF7;
                    break;
                case "UTF32":
                    this.Value = Encoding.UTF32;
                    break;
                default:
                    this.Value = Encoding.ASCII;
                    break;
            }
            this.Attributes.Add("bodyName", new InternalFunction(bodyName));
            this.Attributes.Add("headerName", new InternalFunction(headerName));

        }

        private HassiumObject bodyName(HassiumObject[] args)
        {
            return new HassiumString(this.Value.BodyName);
        }

        private HassiumObject headerName(HassiumObject[] args)
        {
            return new HassiumString(this.Value.HeaderName);
        }
    }
}

