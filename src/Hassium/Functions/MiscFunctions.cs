using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.Functions
{
	public class MiscFunctions : ILibrary
	{
		[IntFunc("type", 1)]
		public static HassiumObject Type(HassiumObject[] args)
		{
			return args[0].GetType().ToString().Substring(args[0].GetType().ToString().LastIndexOf(".", StringComparison.Ordinal) + 1);
		}

		[IntFunc("throw", -1)]
		public static HassiumObject Throw(HassiumObject[] args)
		{
			throw new Exception(String.Join("", args.Cast<object>()));
		}

        [IntFunc("range", new []{2,3})]
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
                ? new[] { from }.Select(x => new HassiumDouble(x)).ToArray()
                : (to < from
                    ? Enumerable.Range((int)to, (int)from).Reverse().ToArray()
                    : Enumerable.Range((int)from, (int)to)).Select(x => new HassiumDouble(x)).ToArray();
        }

        [IntFunc("runtimecall", -1)]
		public static HassiumObject RuntimeCall(HassiumObject[] args)
		{
			string fullpath = args[0].ToString();
			string typename = fullpath.Substring(0, fullpath.LastIndexOf('.'));
			string membername = fullpath.Split('.').Last();
			object[] margs = args.Skip(1).ToArray();
			Type t = System.Type.GetType(typename);
			if(t == null) throw new ArgumentException("The type '" + typename + "' doesn't exist.");
			object instance = null;
			try
			{
				instance = Activator.CreateInstance(t);
			}
			catch (Exception)
			{
			}
			var test = t.GetMember(membername).First();
			switch (test.MemberType)
			{
				case MemberTypes.Field:
					var fv = t.GetField(membername).GetValue(null);
					if(fv is double) return new HassiumDouble((double)fv);
					if(fv is int) return new HassiumInt((int)fv);
					if(fv is string) return new HassiumString((string)fv);
					if(fv is Array) return new HassiumArray((Array)fv);
					if(fv is IDictionary) return new HassiumDictionary((IDictionary)fv);
					if (fv is bool) return new HassiumBool((bool) fv);
					else return (HassiumObject)(object) fv;
				case MemberTypes.Method:
				case MemberTypes.Constructor:
					var result = t.InvokeMember(
						membername,
						BindingFlags.InvokeMethod,
						null,
						instance,
						margs
						);
					return (HassiumObject)result;
			}
			return null;
		}
	}
}
