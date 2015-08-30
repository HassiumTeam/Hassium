using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hassium
{
    public class HassiumDictionary: HassiumObject, IEnumerable
    {
        public Dictionary<HassiumObject, HassiumObject> Value { get; private set; }

        public HassiumDictionary(Dictionary<HassiumObject, HassiumObject> value)
        {
            this.Value = value;
        }
        public HassiumObject this[HassiumObject key]
        {
            get { return Value[key]; }
            set { Value[key] = value; }
        }
        public override string ToString()
        {
            return Convert.ToString(Value);
        }
        public HassiumDictionary(Dictionary<object, object> value)
        {
            this.Value = value.ToDictionary(k => (HassiumObject) (k.Key), k => (HassiumObject) (k.Value));
        }
        public HassiumDictionary(IDictionary value)
        {
            this.Value =
                value.Keys.Cast<object>()
                    .Zip(value.Values.Cast<object>(), (a, b) => new KeyValuePair<object, object>(a, b))
                    .ToDictionary(x => (HassiumObject)x.Key, x => (HassiumObject)x.Value);
        }
        public IEnumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }
    }
}

