﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumArray : HassiumObject, IEnumerable
    {
        private List<HassiumObject> _value;

        public HassiumObject[] Value
        {
            get { return _value.ToArray(); }
        }

        public HassiumArray(IEnumerable<object> value)
        {
            Attributes.Add("length", new InternalFunction(x => Value.Length, true));
            Attributes.Add("toString", new InternalFunction(toString));

            Attributes.Add("add", new InternalFunction(Add));
            Attributes.Add("remove", new InternalFunction(Remove));

            Attributes.Add("resize", new InternalFunction(ResizeArr));
            Attributes.Add("join", new InternalFunction(ArrayJoin));
            Attributes.Add("reverse", new InternalFunction(ArrayReverse));
            Attributes.Add("contains", new InternalFunction(ArrayContains));

            Attributes.Add("op", new InternalFunction(ArrayOp));
            Attributes.Add("select", new InternalFunction(ArraySelect));
            Attributes.Add("where", new InternalFunction(ArrayWhere));
            Attributes.Add("any", new InternalFunction(ArrayAny));
            Attributes.Add("first", new InternalFunction(ArrayFirst));
            Attributes.Add("last", new InternalFunction(ArrayLast));
            Attributes.Add("zip", new InternalFunction(ArrayZip));

            _value = value.Select(ToHassiumObject).ToList();
        }

        public HassiumObject this[int index]
        {
            get { return Value[index]; }
            set { Value[index] = value; }
        }

        public override string ToString()
        {
            return "Array { " + string.Join(", ", Value.Select(x => x.ToString())) + " }";
        }

        public HassiumObject Add(HassiumObject[] args)
        {
            _value.Add(args[0]);
            return null;
        }

        public HassiumObject Remove(HassiumObject[] args)
        {
            return _value.Remove(args[0]);
        }

        public HassiumArray() : this(new List<HassiumObject>())
        {
        }

        public HassiumArray(HassiumObject[] value) : this(value.ToList())
        {
        }

        public HassiumArray(IEnumerable value) : this(value.Cast<HassiumObject>().ToList())
        {
        }

        public IEnumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public static implicit operator HassiumArray(object[] arr)
        {
            return new HassiumArray(arr);
        }



        private HassiumObject toString(HassiumObject[] args)
        {
            return ToString();
        }

        public HassiumObject ResizeArr(HassiumObject[] args)
        {
            HassiumObject[] objarr = Value;

            HassiumObject[] newobj = new HassiumObject[objarr.Length + args[0].HNum().ValueInt - 1];

            for (int x = 0; x < objarr.Length; x++)
                newobj[x] = objarr[x];

            return newobj;
        }

        public HassiumObject ArrayJoin(HassiumObject[] args)
        {
            HassiumObject[] objarr = Value;
            string separator = "";
            if (args.Length > 1) separator = args[0].ToString();

            return string.Join(separator, objarr.Select(x => x.ToString()));
        }

        public static HassiumObject ArrayFill(HassiumObject[] args)
        {
            int num = args[0].HNum().ValueInt;
            HassiumObject thing = args[1];
            return Enumerable.Repeat(thing, num).ToArray();
        }

        public HassiumObject ArrayReverse(HassiumObject[] args)
        {
            return Value.ToArray().Reverse().ToArray();
        }

        public HassiumObject ArrayOp(HassiumObject[] args)
        {
            return Value.Aggregate((a, b) => args[0].Invoke(a, b));
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

        public static HassiumObject Range(HassiumObject[] args)
        {
            var from = args[0].HNum().Value;
            var to = args[1].HNum().Value;
            if (args.Length > 2)
            {
                var step = args[2].HNum().Value;
                var list = new List<double>();
                if (step == 0) throw new Exception("The step for range() can't be zero");
                if (to < from && step > 0) step = -step;
                if (to > from && step < 0) step = -step;
                for (var i = from; step < 0 ? i > to : i < to; i += step)
                {
                    list.Add(i);
                }
                return list.ToArray().Select(x => new HassiumNumber(x)).ToArray();
            }
            return from == to
                ? new[] {from}.Select(x => new HassiumNumber(x)).ToArray()
                : (to < from
                    ? Enumerable.Range((int) to, (int) from).Reverse().ToArray()
                    : Enumerable.Range((int) from, (int) to)).Select(x => new HassiumNumber(x)).ToArray();
        }
    }
}

