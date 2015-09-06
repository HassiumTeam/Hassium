using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.Functions
{
	public class MiscFunctions : ILibrary
	{
		[IntFunc("type")]
		public static HassiumObject Type(HassiumObject[] args)
		{
			return args[0].GetType().ToString().Substring(args[0].GetType().ToString().LastIndexOf(".", StringComparison.Ordinal) + 1);
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
