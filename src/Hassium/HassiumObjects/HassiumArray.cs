using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hassium.HassiumObjects
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
            this.Attributes.Add("length", new InternalFunction(x => Value.Length, true));
            this.Attributes.Add("tostring", new InternalFunction(tostring));

            this.Attributes.Add("resize", new InternalFunction(ResizeArr));
            this.Attributes.Add("join", new InternalFunction(ArrayJoin));
            this.Attributes.Add("reverse", new InternalFunction(ArrayReverse));
            this.Attributes.Add("op", new InternalFunction(ArrayOp));

            this.Attributes.Add("select", new InternalFunction(ArraySelect));
            this.Attributes.Add("where", new InternalFunction(ArrayWhere));
            this.Attributes.Add("any", new InternalFunction(ArrayAny));
            this.Attributes.Add("first", new InternalFunction(ArrayFirst));
            this.Attributes.Add("last", new InternalFunction(ArrayLast));
            this.Attributes.Add("contains", new InternalFunction(ArrayContains));
            this.Attributes.Add("zip", new InternalFunction(ArrayZip));

            _value = value.Select(ToHassiumObject).ToList();
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

        public HassiumObject Add(HassiumObject[] args)
        {
            _value.Add(args[0]);
            return null;
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



        private HassiumObject tostring(HassiumObject[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (HassiumObject obj in this.Value)
            {
                sb.Append(obj.ToString());
            }

            return sb.ToString();
        }

        public HassiumObject ResizeArr(HassiumObject[] args)
        {
            HassiumObject[] objarr = this.Value;

            HassiumObject[] newobj = new HassiumObject[objarr.Length + args[0].HNum().ValueInt - 1];

            for (int x = 0; x < objarr.Length; x++)
                newobj[x] = objarr[x];

            return newobj;
        }

        public HassiumObject ArrayJoin(HassiumObject[] args)
        {
            HassiumObject[] objarr = this.Value;
            string separator = "";
            if (args.Length > 1) separator = args[0].ToString();

            return objarr.Aggregate((a, b) => a + separator + b);
        }

        public static HassiumObject ArrayFill(HassiumObject[] args)
        {
            int num = args[0].HNum().ValueInt;
            HassiumObject thing = args[1];
            return Enumerable.Repeat(thing, num).ToArray();
        }

        public HassiumObject ArrayReverse(HassiumObject[] args)
        {
            return this.Value.ToArray().Reverse().ToArray();
        }

        public HassiumObject ArrayOp(HassiumObject[] args)
        {
            return this.Value.Aggregate((a, b) => args[0].Invoke(a, b));
        }

        #region LINQ-like functions

        public HassiumObject ArraySelect(HassiumObject[] args)
        {
            return this.Value.Select(x => args[0].Invoke(x)).ToArray();
        }

        public HassiumObject ArrayWhere(HassiumObject[] args)
        {
            return this.Value.Where(x => args[0].Invoke(x)).ToArray();
        }

        public HassiumObject ArrayAny(HassiumObject[] args)
        {
            return this.Value.Any(x => args[0].Invoke(x));
        }

        public HassiumObject ArrayFirst(HassiumObject[] args)
        {
            if (args.Length == 1)
                return this.Value.First(x => args[0].Invoke(x));
            else
                return this.Value.First();
        }

        public HassiumObject ArrayLast(HassiumObject[] args)
        {
            if (args.Length == 1)
                return this.Value.Last(x => args[0].Invoke(x));
            else
                return this.Value.Last();
        }

        public HassiumObject ArrayContains(HassiumObject[] args)
        {
            return this.Value.Contains(args[0]);
        }

        public HassiumObject ArrayZip(HassiumObject[] args)
        {
            return this.Value.Zip(args[0].HArray().Value, (x, y) => args[1].Invoke(x, y)).ToArray();
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
                return list.ToArray().Cast<HassiumNumber>().ToArray();
            }
            return from == to
                ? new[] {from}.Cast<HassiumNumber>().ToArray()
                : (to < from
                    ? Enumerable.Range((int) to, (int) from).Reverse().ToArray()
                    : Enumerable.Range((int) from, (int) to)).Cast<HassiumNumber>().ToArray();
        }
    }
}

