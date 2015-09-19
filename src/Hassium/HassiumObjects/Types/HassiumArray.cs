using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumArray : HassiumObject, IEnumerable
    {
        public List<HassiumObject> _value { get; set; }

        public HassiumObject[] Value
        {
            get { return _value.ToArray(); }
        }

        public HassiumArray(IEnumerable<object> value)
        {
            Attributes.Add("length", new InternalFunction(x => Value.Length, 0, true));
            Attributes.Add("toString", new InternalFunction(toString, 0));

            Attributes.Add("add", new InternalFunction(Add, 1));
            Attributes.Add("remove", new InternalFunction(Remove, 1));

            Attributes.Add("resize", new InternalFunction(ResizeArr, 1));
            Attributes.Add("join", new InternalFunction(ArrayJoin, new[] {0, 1}));
            Attributes.Add("reverse", new InternalFunction(ArrayReverse, 0));
            Attributes.Add("contains", new InternalFunction(ArrayContains, 1));

            Attributes.Add("op", new InternalFunction(ArrayOp, 1));
            Attributes.Add("select", new InternalFunction(ArraySelect, 1));
            Attributes.Add("where", new InternalFunction(ArrayWhere, 1));
            Attributes.Add("any", new InternalFunction(ArrayAny, 1));
            Attributes.Add("first", new InternalFunction(ArrayFirst, 1));
            Attributes.Add("last", new InternalFunction(ArrayLast, 1));
            Attributes.Add("zip", new InternalFunction(ArrayZip, 2));

            _value = value.Select(ToHassiumObject).ToList();
        }

        public HassiumObject this[int index]
        {
            get { return _value[index]; }
            set { _value[index] = value; }
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

            HassiumObject[] newobj = new HassiumObject[objarr.Length + args[0].HDouble().ValueInt - 1];

            for (int x = 0; x < objarr.Length; x++)
                newobj[x] = objarr[x];

            return newobj;
        }

        public HassiumObject ArrayJoin(HassiumObject[] args)
        {
            HassiumObject[] objarr = Value;
            string separator = "";
            if (args.Length > 0) separator = args[0].ToString();

            return string.Join(separator, objarr.Select(x => x.ToString()));
        }

        /* TODO: Implement static methods
        public static HassiumObject ArrayFill(HassiumObject[] args)
        {
            int num = args[0].HDouble().ValueInt;
            HassiumObject thing = args[1];
            return Enumerable.Repeat(thing, num).ToArray();
        }
        */

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
            return Value.Select(v => (args[0].Invoke(v))).ToArray();
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
}