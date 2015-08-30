using System;

namespace Hassium
{
    public class HassiumString: HassiumObject
    {
        public string Value { get; private set; }

        public HassiumString(string value)
        {
            this.Value = value;
            this.Attributes.Add("tolower", new InternalFunction(tolower));
            this.Attributes.Add("toupper", new InternalFunction(toupper));
        }

        private HassiumObject tolower(HassiumObject[] args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.ToLower());
        }

        private HassiumObject toupper(HassiumObject[] args)
        {
            return new HassiumString(((HassiumString)args[0]).Value.ToUpper());
        }
    }
}

