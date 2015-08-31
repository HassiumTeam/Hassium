using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hassium.Functions
{
	public class MiscFunctions : ILibrary
	{
		[IntFunc("free")]
		public static HassiumObject Free(HassiumObject[] args)
		{
			HassiumInterpreter.CurrentInterpreter.FreeVariable(args[0].ToString());
			return null;
		}

		

		[IntFunc("type")]
		public static HassiumObject Type(HassiumObject[] args)
		{
			return args[0].GetType().ToString().Substring(args[0].GetType().ToString().LastIndexOf(".") + 1);
		}

		[IntFunc("throw")]
		public static HassiumObject Throw(HassiumObject[] args)
		{
			throw new Exception(String.Join("", args.Cast<object>()));
		}

		[IntFunc("runtimecall")]
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
			if(test.MemberType == MemberTypes.Field)
			{
				var fv = t.GetField(membername).GetValue(null);
				if(fv is double || fv is int) return new HassiumNumber((double)fv);
				if(fv is string) return new HassiumString((string)fv);
				if(fv is Array) return new HassiumArray((Array)fv);
				if(fv is IDictionary) return new HassiumDictionary((IDictionary)fv);
			    if (fv is bool) return new HassiumBool((bool) fv);
			    else return (HassiumObject)(object) fv;
			}
			else if (test.MemberType == MemberTypes.Method || test.MemberType == MemberTypes.Constructor)
			{
				object result = t.InvokeMember(
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
