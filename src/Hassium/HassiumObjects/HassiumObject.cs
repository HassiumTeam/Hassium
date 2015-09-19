using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.HassiumObjects
{
    public class HassiumObject : object, IFunction
    {
        private readonly Dictionary<string, HassiumObject> _attributes;

        public bool IsInstance { get; set; }

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
            if (value is HassiumMethod)
            {
                ((HassiumMethod) value).SelfReference = this;
            }
            if (_attributes.ContainsKey(name) && _attributes[name] is HassiumProperty)
            {
                var prop = ((HassiumProperty)_attributes[name]);
                if(prop.ReadOnly) throw new ParseException("The property " + prop.Name + " is read-only", -1);
                prop.SetValue(this, value);
            }
            else _attributes[name] = value;
        }

        public HassiumObject GetAttribute(string name, int pos)
        {
            if(!_attributes.ContainsKey(name)) throw new ParseException("The attribute '" + name + "' doesn't exist for the specified object.", pos);
            if (_attributes.ContainsKey(name) && _attributes[name] is HassiumProperty) return ((HassiumProperty)_attributes[name]).GetValue(this);
            else return _attributes[name];
        }

        public override string ToString()
        {
            if (Attributes.Any(x => x.Key == "toString" && (x.Value is InternalFunction && ((InternalFunction)x.Value).Arguments.Length == 0))) return GetAttribute("toString", -1).Invoke();
            return "";
        }

        public virtual HassiumObject Invoke(params HassiumObject[] args)
        {
            return null;
        }

        #region Cast
        public static implicit operator int(HassiumObject obj)
        {
            return ((HassiumInt) obj).Value;
        }

        public static implicit operator HassiumObject(int i)
        {
            return new HassiumInt(i);
        }

        public static implicit operator double (HassiumObject obj)
        {
            return ((HassiumDouble)obj).Value;
        }

        public static implicit operator HassiumObject(double d)
        {
            return new HassiumDouble(d);
        }

        public static implicit operator string (HassiumObject obj)
        {
            return obj.ToString();
        }

        public static implicit operator HassiumObject(string s)
        {
            return new HassiumString(s);
        }

        public static implicit operator char (HassiumObject obj)
        {
            return ((HassiumChar) obj).Value;
        }

        public static implicit operator HassiumObject(char s)
        {
            return new HassiumChar(s);
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
            if (fv is double) return new HassiumDouble((double)fv);
            if (fv is int) return new HassiumDouble((int)fv);
            if (fv is string) return new HassiumString((string)fv);
            if(fv is char) return new HassiumChar((char)fv);
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

        public HassiumChar HChar()
        {
            if (this is HassiumString) return new HassiumChar(((HassiumString) this).Value[0]);
            return (HassiumChar) this;
        }

        public HassiumDictionary HDict()
        {
            return (HassiumDictionary)this;
        }

        public HassiumFile HFile()
        {
            return (HassiumFile)this;
        }

        public HassiumDouble HDouble()
        {
            if(this is HassiumInt) return new HassiumDouble(this.HInt().Value);
            return (HassiumDouble)this;
        }

        public HassiumInt HInt()
        {
            return (HassiumInt)this;
        }

        public HassiumString HString()
        {
            return (HassiumString) this;
        }
        #endregion
    }
}