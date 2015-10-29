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
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Networking;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.HassiumObjects
{

    public class HassiumObject : IFunction
    {
        public bool IsInstance { get; set; }

        public event AttributeChangedHandler AttributeChanged = (a, v) => { };

        public delegate void AttributeChangedHandler(string attrname, HassiumObject value);

        public Dictionary<string, HassiumObject> Attributes { get; protected set; }

        public HassiumObject()
        {
            Attributes = new Dictionary<string, HassiumObject>();
            IsInstance = true;
        }

        public void SetAttribute(string name, HassiumObject value)
        {
            if (value is HassiumMethod)
            {
                ((HassiumMethod) value).SelfReference = this;
            }
            if (Attributes.ContainsKey(name) && Attributes[name] is HassiumProperty)
            {
                var prop = ((HassiumProperty) Attributes[name]);
                if (prop.ReadOnly) throw new ParseException("The property " + prop.Name + " is read-only", -1);
                if (IsInstance)
                    prop.SetValue(this, value);
                else
                    prop.SetValue(value);
            }
            else Attributes[name] = value;
            AttributeChanged(name, value);
        }

        public HassiumObject GetAttribute(string name, int pos)
        {
            if ((name == "toString" || name == "toString`0") & !Attributes.ContainsKey(name)) return new InternalFunction(x => ToString(), 0);
            if (!Attributes.ContainsKey(name))
                throw new ParseException("The attribute '" + name + "' doesn't exist for the specified object.", pos);
            if (Attributes.ContainsKey(name) && Attributes[name] is HassiumProperty)
                return ((HassiumProperty) Attributes[name]).GetValue(this);
            if (Attributes.ContainsKey(name) && Attributes[name] is HassiumMethod)
            {
                HassiumMethod ret = (HassiumMethod)Attributes[name];
                ret.SelfReference = this;
                return ret;
            }
            else return Attributes[name];
        }

        public override string ToString()
        {
            if (
                Attributes.Any(
                    x =>
                        x.Key == "toString" &&
                        (x.Value is InternalFunction && ((InternalFunction) x.Value).Arguments.Length == 0)))
                return GetAttribute("toString", -1).Invoke();
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

        public static implicit operator double(HassiumObject obj)
        {
            return ((HassiumDouble) obj).Value;
        }

        public static implicit operator HassiumObject(double d)
        {
            return new HassiumDouble(d);
        }

        public static implicit operator string(HassiumObject obj)
        {
            return obj.ToString();
        }

        public static implicit operator HassiumObject(string s)
        {
            return new HassiumString(s);
        }

        public static implicit operator char(HassiumObject obj)
        {
            return ((HassiumChar) obj).Value;
        }

        public static implicit operator HassiumObject(char s)
        {
            return new HassiumChar(s);
        }

        public static implicit operator object[](HassiumObject obj)
        {
            return ((HassiumArray) obj).Value.Cast<object>().ToArray();
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
            return ((HassiumDictionary) obj).Value.ToDictionary(x => (object) x.Key, x => (object) x.Value);
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
            if (fv is double) return new HassiumDouble((double) fv);
            if (fv is int) return new HassiumDouble((int) fv);
            if (fv is string) return new HassiumString((string) fv);
            if (fv is char) return new HassiumChar((char) fv);
            if (fv is Array) return new HassiumArray((Array) fv);
            if (fv is IDictionary) return new HassiumDictionary((IDictionary) fv);
            if (fv is bool) return new HassiumBool((bool) fv);
            if (fv is KeyValuePair<HassiumObject, HassiumObject>)
                return HKvp((KeyValuePair<HassiumObject, HassiumObject>) fv);
            else return (HassiumObject) (object) fv;
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
            return (HassiumArray) this;
        }

        public HassiumBool HBool()
        {
            return (HassiumBool) this;
        }

        public HassiumWebClient HClient()
        {
            return (HassiumWebClient) this;
        }

        public HassiumChar HChar()
        {
            if (this is HassiumString) return new HassiumChar(((HassiumString) this).Value[0]);
            return (HassiumChar) this;
        }

        public HassiumDictionary HDict()
        {
            return (HassiumDictionary) this;
        }

        public HassiumFile HFile()
        {
            return (HassiumFile) this;
        }

        public HassiumDouble HDouble()
        {
            if (this is HassiumInt) return new HassiumDouble(HInt().Value);
            return (HassiumDouble) this;
        }

        public HassiumInt HInt()
        {
            return (HassiumInt) this;
        }

        public HassiumString HString()
        {
            if (this is HassiumChar) return new HassiumString(((HassiumChar) this).Value.ToString());
            return (HassiumString) this;
        }

        #endregion
    }
}