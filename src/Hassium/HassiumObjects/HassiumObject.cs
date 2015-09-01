using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects
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
            if(!_attributes.ContainsKey(name)) throw new ArgumentException("The attribute '" + name + "' doesn't exist for the specified object.");
            return _attributes[name];
        }

        public override string ToString()
        {
            return "";
        }

        public virtual HassiumObject Invoke(params HassiumObject[] args)
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
            return obj.ToString();
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
            return ((HassiumDictionary) obj).ToDictionary();
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

        public static HassiumObject ToHassiumObject(object fv)
        {
            if (fv is double) return new HassiumNumber((double)fv);
            if (fv is int) return new HassiumNumber((int)fv);
            if (fv is string) return new HassiumString((string)fv);
            if (fv is Array) return new HassiumArray((Array)fv);
            if (fv is IDictionary) return new HassiumDictionary((IDictionary)fv);
            if (fv is bool) return new HassiumBool((bool)fv);
            if (fv is KeyValuePair<HassiumObject, HassiumObject>) return HKvp((KeyValuePair<HassiumObject, HassiumObject>)fv);
            else return (HassiumObject)(object)fv;
        }

        public KeyValuePair<HassiumObject, HassiumObject> HKvp(HassiumKeyValuePair kvp)
        {
            return new KeyValuePair<HassiumObject, HassiumObject>(kvp.Key, kvp.Value);
        }

        public static HassiumKeyValuePair HKvp(KeyValuePair<HassiumObject, HassiumObject> kvp)
        {
            return new HassiumKeyValuePair(kvp.Key, kvp.Value);
        }

        public HassiumArray HArray()
        {
            return (HassiumArray)this;
        }

        public HassiumBool HBool()
        {
            return (HassiumBool)this;
        }

        public HassiumWebClient HClient()
        {
            return (HassiumWebClient)this;
        }

        public HassiumDictionary HDict()
        {
            return (HassiumDictionary)this;
        }

        public HassiumFile HFile()
        {
            return (HassiumFile)this;
        }

        public HassiumNumber HNum()
        {
            return (HassiumNumber)this;
        }

        public HassiumString HString()
        {
            return (HassiumString) this;
        }
    }
}