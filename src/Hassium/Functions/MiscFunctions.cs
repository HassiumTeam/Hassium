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
using System.Reflection;
using System.Threading;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;

namespace Hassium.Functions
{
    public class MiscFunctions : ILibrary
    {
        [IntFunc("type", 1)]
        public static HassiumObject Type(HassiumObject[] args)
        {
            return
                args[0].GetType()
                    .ToString()
                    .Substring(args[0].GetType().ToString().LastIndexOf(".", StringComparison.Ordinal) + 1);
        }

        [IntFunc("throw", -1)]
        public static HassiumObject Throw(HassiumObject[] args)
        {
            throw new Exception(string.Join("", args.Cast<object>()));
        }

        [IntFunc("fill", 2)]
        public static HassiumObject Fill(HassiumObject[] args)
        {
            HassiumObject[] array = new HassiumObject[args[1].HInt().Value];

            var fillvalue = new[] {args[0]};
            Array.Copy(fillvalue, array, 1);
            int arraylgt = array.Length / 2;
            for (int i = 1; i < array.Length; i *= 2)
            {
                int cplength = i;
                if (i > arraylgt)
                {
                    cplength = array.Length - i;
                }
                Array.Copy(array, 0, array, i, cplength);
            }
            return array;
        }

        [IntFunc("fillzero", 1)]
        public static HassiumObject FillZero(HassiumObject[] args)
        {
            var zero = new HassiumInt(0);

            return Enumerable.Repeat(zero, args[0].HInt().Value).ToArray();
        }

        [IntFunc("range", new[] {2, 3})]
        public static HassiumObject Range(HassiumObject[] args)
        {
            var from = args[0].HDouble().Value;
            var to = args[1].HDouble().Value;
            if (args.Length > 2)
            {
                var step = args[2].HDouble().Value;
                var list = new List<double>();
                if (step == 0) throw new Exception("The step for range() can't be zero");
                if (to < from && step > 0) step = -step;
                if (to > from && step < 0) step = -step;
                for (var i = from; step < 0 ? i > to : i < to; i += step)
                {
                    list.Add(i);
                }
                return list.ToArray().Select(x => new HassiumDouble(x)).ToArray();
            }
            return from == to
                ? new[] {from}.Select(x => new HassiumDouble(x)).ToArray()
                : (to < from
                    ? Enumerable.Range((int) to, (int) from).Reverse().Select(x => new HassiumDouble(x)).ToArray()
                    : Enumerable.Range((int) from, (int) to).Select(x => new HassiumDouble(x)).ToArray());
        }

        [IntFunc("map", 2)]
        public static HassiumObject Map(HassiumObject[] args)
        {
            HassiumArray array = ((HassiumArray) args[0]);
            HassiumArray ret = new HassiumArray(array._value);
            for (int x = 0; x < array._value.Count; x++)
                ret[x] = ((HassiumMethod) args[1]).Invoke(array._value[x]);

            return ret;
        }

        [IntFunc("threadRun", 1)]
        public static HassiumObject threadRun(HassiumObject[] args)
        {
            HassiumMethod method = ((HassiumMethod)args[0]);
            new Thread(() => method.Invoke()).Start();

            return null;
        }

        [IntFunc("runtimecall", -1)]
        public static HassiumObject RuntimeCall(HassiumObject[] args)
        {
            string fullpath = args[0].ToString();
            string typename = fullpath.Substring(0, fullpath.LastIndexOf('.'));
            string membername = fullpath.Split('.').Last();
            object[] margs = args.Skip(1).ToArray();
            Type t = System.Type.GetType(typename);
            if (t == null) throw new ArgumentException("The type '" + typename + "' doesn't exist.");
            object instance = null;
            try
            {
                instance = Activator.CreateInstance(t);
            }
            catch (Exception)
            {
            }
            var test = t.GetMember(membername).First();
            object result = null;
            switch (test.MemberType)
            {
                case MemberTypes.Field:
                    result = t.GetField(membername).GetValue(null);
                    break;
                case MemberTypes.Method:
                case MemberTypes.Constructor:
                    result = t.InvokeMember(
                        membername,
                        BindingFlags.InvokeMethod,
                        null,
                        instance,
                        margs
                        );
                    break;
            }
            if (result is double) return new HassiumDouble((double)result);
            if (result is int) return new HassiumInt((int)result);
            if (result is string) return new HassiumString((string)result);
            if (result is Array) return new HassiumArray((Array)result);
            if (result is IDictionary) return new HassiumDictionary((IDictionary)result);
            if (result is bool) return new HassiumBool((bool)result);
            else return (HassiumObject)(object)result;
        }
    }
}