using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium
{
    public class HassiumObject : object, IFunction
    {
        private readonly Dictionary<string, HassiumObject> _attributes;

        public Dictionary<string, HassiumObject> Attributes
        {
            get { return _attributes; }
        }

        public HassiumObject()
        {
            _attributes = new Dictionary<string, HassiumObject>();
        }

        public void SetAttribute(string name, HassiumObject value)
        {
            if (value is HassiumFunction)
            {
                value = new HassiumMethod((HassiumFunction)value, this);
            }
            _attributes[name] = value;
        }

        public HassiumObject GetAttribute(string name)
        {
            return _attributes[name];
        }

        public override string ToString()
        {
            return "";
        }

        public virtual HassiumObject Invoke(HassiumObject[] args)
        {
            return null;
        }

        public static implicit operator int(HassiumObject obj)
        {
            return (int)((HassiumNumber) obj).Value;
        }

        public static implicit operator HassiumObject(int i)
        {
            return new HassiumNumber(i);
        }

        public static implicit operator double (HassiumObject obj)
        {
            return ((HassiumNumber)obj).Value;
        }

        public static implicit operator HassiumObject(double d)
        {
            return new HassiumNumber(d);
        }

        public static implicit operator string (HassiumObject obj)
        {
            return ((HassiumString)obj).Value;
        }

        public static implicit operator HassiumObject(string s)
        {
            return new HassiumString(s);
        }

        public static implicit operator object[] (HassiumObject obj)
        {
            return ((HassiumArray)obj).Value.Cast<object>().ToArray();
        }

        public static implicit operator Dictionary<HassiumObject, HassiumObject>(HassiumObject obj)
        {
            return ((HassiumDictionary) obj).Value;
        }

        public static implicit operator HassiumObject(Dictionary<HassiumObject, HassiumObject> dict)
        {
            return new HassiumDictionary(dict);
        }

        public static implicit operator Dictionary<object, object>(HassiumObject obj)
        {
            return ((HassiumDictionary)obj).Value.ToDictionary(x => (object)x.Key, x => (object)x.Value);
        }

        public static implicit operator HassiumObject(Dictionary<object, object> dict)
        {
            return new HassiumDictionary(dict);
        }

        public static implicit operator bool(HassiumObject obj)
        {
            return ((HassiumBool) obj).Value;
        }

        public static implicit operator HassiumObject(bool bo)
        {
            return new HassiumBool(bo);
        }

        public static implicit operator HassiumObject(Array arr)
        {
            return new HassiumArray(arr);
        }

        public static implicit operator HassiumObject(object[] arr)
        {
            return new HassiumArray(arr);
        }
    }
}