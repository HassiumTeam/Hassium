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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Hassium.Functions;
using Hassium.HassiumObjects.Networking.HTTP;
using Hassium.Interpreter;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumString : HassiumObject, IConvertible
    {
        protected bool Equals(HassiumString other)
        {
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(HassiumString a, HassiumString b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(HassiumString a, HassiumString b)
        {
            return a.Value != b.Value;
        }

        public string Value { get; set; }

        public HassiumString(string value)
        {
            Value = value;
            Attributes.Add("toArray", new InternalFunction(toArray, 0));
            Attributes.Add("toLower", new InternalFunction(tolower, 0));
            Attributes.Add("toUpper", new InternalFunction(toupper, 0));
            Attributes.Add("startsWith", new InternalFunction(begins, 1));
            Attributes.Add("endsWith", new InternalFunction(ends, 1));
            Attributes.Add("getAt", new InternalFunction(getat, 1));
            Attributes.Add("substring", new InternalFunction(substring, new[] {1, 2}));
            Attributes.Add("concat", new InternalFunction(concat, 1));
            Attributes.Add("contains", new InternalFunction(contains, 1));
            Attributes.Add("split", new InternalFunction(split, 1));
            Attributes.Add("replace", new InternalFunction(replace, 2));
            Attributes.Add("index", new InternalFunction(index, 1));
            Attributes.Add("isWhiteSpace", new InternalFunction(isWhiteSpace, 0));
            Attributes.Add("lastIndex", new InternalFunction(lastindex, 1));
            Attributes.Add("occurences", new InternalFunction(occurences, 1));
            Attributes.Add("padLeft", new InternalFunction(padleft, 1));
            Attributes.Add("padRight", new InternalFunction(padright, 1));
            Attributes.Add("trim", new InternalFunction(trim, 0));
            Attributes.Add("trimLeft", new InternalFunction(trimleft, 0));
            Attributes.Add("trimRight", new InternalFunction(trimright, 0));
            Attributes.Add("urlEncode", new InternalFunction(urlEncode, 0));
            Attributes.Add("urlDecode", new InternalFunction(urlDecode, 0));
            Attributes.Add("length", new InternalFunction(x => Value.Length, 0, true));
            Attributes.Add("toInt", new InternalFunction(toInt, 0));
            Attributes.Add("toDouble", new InternalFunction(toDouble, 0));
            Attributes.Add("toByte", new InternalFunction(toByte, 0));
            Attributes.Add("toBool", new InternalFunction(toBool, 0));
            Attributes.Add("addSlashes", new InternalFunction(addSlashes, 0));
            Attributes.Add("wordWrap", new InternalFunction(wordWrap, new []{1,2}));
        }

        public static implicit operator HassiumArray(HassiumString s)
        {
            return new HassiumArray(s.Value);
        }

        public static implicit operator HassiumString(HassiumChar c)
        {
            return new HassiumString(c.ToString());
        }

        public HassiumObject occurences(HassiumObject[] args)
        {
            return Regex.Matches(Value, args[0].ToString()).Count;
        }

        public HassiumObject wordWrap(HassiumObject[] args)
        {
            bool cut = false;
            int length = args[0].HInt();
            if(length < 1) throw new ParseException("The length of word-wrap must be greater than 0", -1);
            if (args.Length == 2) cut = args[1].HBool();

            var result = new List<string>();
            string currentLine = "";

            if (cut)
            {
                foreach (char c in Value)
                {
                    currentLine += c;
                    if (currentLine.Length == length)
                    {
                        result.Add(currentLine);
                        currentLine = "";
                    }
                }

            }
            else
            {
                foreach (string word in Value.Split(' '))
                {
                    if ((currentLine.Length > length) ||
                        ((currentLine.Length + word.Length) > length))
                    {
                        result.Add(currentLine);
                        currentLine = "";
                    }

                    if (currentLine.Length + word.Length > length && word.Length > length)
                    {
                        int k = 0;
                        var words =
                            word.ToLookup(c => (int)System.Math.Floor(k++ / (double)(length - 1))).Select(e => new string(e.ToArray()) + "-");
                        k = 0;
                        foreach(string cword in words)
                        {
                            result.Add(k == words.Count() - 1 ? cword.Substring(0, cword.Length - 1) : cword);
                            k++;
                        }
                        currentLine = "";
                    }
                    else currentLine += word + " ";

                    if (currentLine.EndsWith(" ")) currentLine = currentLine.Substring(0, currentLine.Length - 1);
                }

                if(currentLine.Length > 0)
                    result.Add(currentLine);
            }

            return string.Join("\n", result);
        }

        public HassiumObject addSlashes(HassiumObject[] args)
        {
            return Value.Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t")
                .Replace("'", "\\'")
                .Replace("\"", "\\\"");
        }

        private HassiumObject toArray(HassiumObject[] args)
        {
            return Value.ToCharArray().Select(x => new HassiumChar(x)).ToArray();
        }

        private HassiumObject toInt(HassiumObject[] args)
        {
            return new HassiumInt(Convert.ToInt32(Value));
        }

        private HassiumObject toDouble(HassiumObject[] args)
        {
            return new HassiumDouble(Convert.ToDouble(Value));
        }

        private HassiumObject toBool(HassiumObject[] args)
        {
            return new HassiumBool(Convert.ToBoolean(Value));
        }

        private HassiumObject toByte(HassiumObject[] args)
        {
            byte[] text = System.Text.Encoding.ASCII.GetBytes(Value);
            HassiumByte[] bytes = new HassiumByte[text.Length];
            for (int x = 0; x < text.Length; x++)
                bytes[x] = new HassiumByte(text[x]);

            return new HassiumArray(bytes);
        }

        private HassiumObject tolower(HassiumObject[] args)
        {
            return Value.ToLower();
        }

        private HassiumObject toupper(HassiumObject[] args)
        {
            return Value.ToUpper();
        }

        private HassiumObject begins(HassiumObject[] args)
        {
            return Value.StartsWith(args[0].ToString());
        }

        private HassiumObject ends(HassiumObject[] args)
        {
            return Value.EndsWith(args[0].ToString());
        }

        private HassiumObject getat(HassiumObject[] args)
        {
            return Value[args[0].HInt().Value].ToString();
        }

        private HassiumObject substring(HassiumObject[] args)
        {
            var lower = args[0].HInt().Value;
            if (lower < 0) lower = Value.Length + lower;
            var upper = args.Length == 2 ? args[1].HInt().Value : Value.Length - lower;
            if (upper < 0) upper = Value.Length + upper - lower;
            if(lower >= Value.Length || lower + upper >= Value.Length) throw new ParseException("Out of bounds", -1);
            return Value.Substring(lower, upper);
        }

        private HassiumObject concat(HassiumObject[] args)
        {
            return Value + args[0];
        }

        private HassiumObject contains(HassiumObject[] args)
        {
            return Value.Contains(args[0].ToString());
        }

        private HassiumObject split(HassiumObject[] args)
        {
            return new HassiumArray(Value.Split(args[0].HChar()));
        }

        private HassiumObject index(HassiumObject[] args)
        {
            return Value.IndexOf(args[0].ToString());
        }

        private HassiumObject lastindex(HassiumObject[] args)
        {
            return Value.LastIndexOf(args[0].ToString(), StringComparison.Ordinal);
        }

        private HassiumObject padleft(HassiumObject[] args)
        {
            return Value.PadLeft(args[0].HInt().Value);
        }

        private HassiumObject padright(HassiumObject[] args)
        {
            return Value.PadRight(args[0].HInt().Value);
        }

        private HassiumObject replace(HassiumObject[] args)
        {
            return Value.Replace(args[0].ToString(), args[1].ToString());
        }

        private HassiumObject trim(HassiumObject[] args)
        {
            return Value.Trim();
        }

        private HassiumObject trimleft(HassiumObject[] args)
        {
            return Value.TrimStart();
        }

        private HassiumObject trimright(HassiumObject[] args)
        {
            return Value.TrimEnd();
        }

        private HassiumObject isWhiteSpace(HassiumObject[] args)
        {
            return char.IsWhiteSpace(Convert.ToChar(Value));
        }

        private HassiumObject urlEncode(HassiumObject[] args)
        {
            return HttpUtility.UrlEncode(Value);
        }

        private HassiumObject urlDecode(HassiumObject[] args)
        {
            return HttpUtility.UrlDecode(Value);
        }

        public static implicit operator HassiumDouble(HassiumString str)
        {
            return new HassiumDouble(Convert.ToDouble(str.Value));
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.ToString() == Value;
        }

        #region IConvertible stuff

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return Value;
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        public override string ToString()
        {
            return Value;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }

        #endregion
    }
}