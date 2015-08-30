using System;
using System.Collections;
using System.Linq;

namespace Hassium
{
    public class HassiumArray: HassiumObject, IEnumerable
    {
        public HassiumObject[] Value { get; private set; }

        public HassiumArray(object[] value)
        {
            this.Value = value.Cast<HassiumObject>().ToArray();
        }

        public HassiumObject this[int index]
        {
            get { return Value[index]; }
            set { Value[index] = value; }
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }


        public HassiumArray()
        {
            this.Value = new HassiumObject[] {};
        }

        public HassiumArray(HassiumObject[] value)
        {
            this.Value = value;
        }

        public HassiumArray(IEnumerable value)
        {
            this.Value = value.Cast<HassiumObject>().ToArray();
        }
        public IEnumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public static implicit operator HassiumArray(object[] arr)
        {
            return new HassiumArray(arr);
        }
    }
}

