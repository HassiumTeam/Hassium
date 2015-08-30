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
            this.Value = value.Select(fv => 
            {
                if (fv is double || fv is int) return new HassiumNumber((double)fv);
                if (fv is string) return new HassiumString((string)fv);
                if (fv is Array) return new HassiumArray((Array)fv);
                if (fv is IDictionary) return new HassiumDictionary((IDictionary)fv);
                if (fv is bool) return new HassiumBool((bool)fv);
                else return (HassiumObject)(object)fv;
            }).ToArray();
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

