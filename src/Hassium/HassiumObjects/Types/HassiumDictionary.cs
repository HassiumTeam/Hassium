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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumDictionary : HassiumObject, IEnumerable
    {
        protected bool Equals(HassiumDictionary other)
        {
            return Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((HassiumDictionary) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public List<HassiumKeyValuePair> Value { get; private set; }

        public HassiumDictionary(Dictionary<HassiumObject, HassiumObject> value)
            : this(value.Select(x => (HassiumKeyValuePair) x).ToList())
        {
            Attributes.Add("length", new InternalFunction(x => Value.Count, 0, true));
            Attributes.Add("toString", new InternalFunction(tostring, 0));

            Attributes.Add("keys",
                new HassiumProperty("keys",
                    x => new HassiumArray(Value.Select(y => y.Key)), x => null, true));
            Attributes.Add("values",
                new HassiumProperty("values",
                    x => new HassiumArray(Value.Select(y => y.Value)), x => null, true));

            Attributes.Add("resize", new InternalFunction(ResizeArr, 1));
            Attributes.Add("reverse", new InternalFunction(ArrayReverse, 0));
            Attributes.Add("contains", new InternalFunction(ArrayContains, 1));
            Attributes.Add("containsKey", new InternalFunction(ContainsKey, 1));
            Attributes.Add("containsValue", new InternalFunction(ContainsValue, 1));

            Attributes.Add("op", new InternalFunction(ArrayOp, 1));
            Attributes.Add("select", new InternalFunction(ArraySelect, 1));
            Attributes.Add("where", new InternalFunction(ArrayWhere, 1));
            Attributes.Add("any", new InternalFunction(ArrayAny, 1));
            Attributes.Add("first", new InternalFunction(ArrayFirst, 1));
            Attributes.Add("last", new InternalFunction(ArrayLast, 1));
            Attributes.Add("zip", new InternalFunction(ArrayZip, 2));
        }

        public static bool operator ==(HassiumDictionary a, HassiumDictionary b)
        {
            return a.Value.SequenceEqual(b.Value);
        }

        public static bool operator !=(HassiumDictionary a, HassiumDictionary b)
        {
            return !(a == b);
        }

        public HassiumDictionary(List<HassiumKeyValuePair> ls)
        {
            Value = ls;
        }

        public HassiumObject ContainsKey(HassiumObject[] args)
        {
            return Value.Any(x => x.Key.ToString() == args[0].ToString());
        }

        public HassiumObject ContainsValue(HassiumObject[] args)
        {
            return Value.Any(x => x.Value.ToString() == args[0].ToString());
        }

        public HassiumObject this[HassiumObject key]
        {
            get { return Value.First(x => x.Key.ToString() == key.ToString()).Value; }
            set
            {
                if (Value.Any(x => x.Key.ToString() == key.ToString()))
                    Value =
                        Value.Select(x => x.Key.ToString() == key.ToString() ? new HassiumKeyValuePair(key, value) : x)
                            .ToList();
                else
                    Value.Add(new HassiumKeyValuePair(key, value));
            }
        }

        public HassiumObject ResizeArr(HassiumObject[] args)
        {
            HassiumObject[] objarr = Value.ToArray();

            HassiumObject[] newobj = new HassiumObject[objarr.Length + args[0].HDouble().ValueInt - 1];

            for (int x = 0; x < objarr.Length; x++)
                newobj[x] = objarr[x];

            return newobj;
        }

        public override string ToString()
        {
            return "Dictionary { " +
                   string.Join(", ", Value.Select(x => "[" + x.Key.ToString() + "] => " + (x.Value ?? "null"))) + " }";
        }

        public HassiumDictionary(Dictionary<object, object> value)
            : this(value.Select(x => new HassiumKeyValuePair((HassiumObject) x.Key, (HassiumObject) x.Value)).ToList())
        {
        }

        public HassiumDictionary(IDictionary value) : this(value.Keys.Cast<object>()
            .Zip(value.Values.Cast<object>(), (a, b) => new KeyValuePair<object, object>(a, b))
            .Select(x => new HassiumKeyValuePair((HassiumObject) x.Key, (HassiumObject) x.Value)).ToList())
        {
        }

        public IEnumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public Dictionary<HassiumObject, HassiumObject> ToDictionary()
        {
            return Value.ToDictionary(x => x.Key, x => x.Value);
        }

        private HassiumObject tostring(HassiumObject[] args)
        {
            return ToString();
        }

        public HassiumObject ArrayReverse(HassiumObject[] args)
        {
            Value.Reverse();
            return this;
        }

        public HassiumObject ArrayOp(HassiumObject[] args)
        {
            return Value.Aggregate((a, b) => (HassiumKeyValuePair) args[0].Invoke(a, b));
        }

        #region LINQ-like functions

        public HassiumObject ArraySelect(HassiumObject[] args)
        {
            return Value.Select(x => args[0].Invoke(x)).ToArray();
        }

        public HassiumObject ArrayWhere(HassiumObject[] args)
        {
            return Value.Where(x => args[0].Invoke(x)).ToArray();
        }

        public HassiumObject ArrayAny(HassiumObject[] args)
        {
            return Value.Any(x => args[0].Invoke(x));
        }

        public HassiumObject ArrayFirst(HassiumObject[] args)
        {
            return args.Length == 1 ? Value.First(x => args[0].Invoke(x)) : Value.First();
        }

        public HassiumObject ArrayLast(HassiumObject[] args)
        {
            return args.Length == 1 ? Value.Last(x => args[0].Invoke(x)) : Value.Last();
        }

        public HassiumObject ArrayContains(HassiumObject[] args)
        {
            return Value.Contains(args[0]);
        }

        public HassiumObject ArrayZip(HassiumObject[] args)
        {
            return Value.Zip(args[0].HArray().Value, (x, y) => args[1].Invoke(x, y)).ToArray();
        }

        #endregion
    }

    public class HassiumKeyValuePair : HassiumObject, IConvertible
    {
        public HassiumObject Key { get; set; }
        public HassiumObject Value { get; set; }

        public HassiumKeyValuePair(HassiumObject k, HassiumObject v)
        {
            Key = k;
            Value = v;

            Attributes.Add("key", new InternalFunction(x => Key, 0, true));
            Attributes.Add("value", new InternalFunction(x => Value, 0, true));
        }

        public static implicit operator KeyValuePair<HassiumObject, HassiumObject>(HassiumKeyValuePair kvp)
        {
            return new KeyValuePair<HassiumObject, HassiumObject>(kvp.Key, kvp.Value);
        }

        public static implicit operator HassiumKeyValuePair(KeyValuePair<HassiumObject, HassiumObject> kvp)
        {
            return new HassiumKeyValuePair(kvp.Key, kvp.Value);
        }

        public override string ToString()
        {
            return "[" + Key + " => " + Value + "]";
        }

        #region IConvertible stuff

        public TypeCode GetTypeCode()
        {
            return TypeCode.Single;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean((object) Value);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte((object) Value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar((object) Value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime((object) Value);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal((object) Value);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble((object) Value);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16((object) Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32((object) Value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64((object) Value);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte((object) Value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle((object) Value);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16((object) Value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32((object) Value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64((object) Value);
        }

        #endregion
    }
}