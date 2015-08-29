using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hassium.Functions
{
	public class MiscFunctions : ILibrary
	{
		[IntFunc("free")]
		public static object Free(object[] args)
		{
			Interpreter.Globals.Remove(args[0].ToString());
			return null;
		}

		

		[IntFunc("type")]
		public static object Type(object[] args)
		{
			return args[0].GetType().ToString().Substring(args[0].GetType().ToString().LastIndexOf(".") + 1);
		}

		[IntFunc("throw")]
		public static object Throw(object[] args)
		{
			throw new Exception(String.Join("", args));
		}

		[IntFunc("runtimecall")]
		public static object RuntimeCall(object[] args)
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
				return t.GetField(membername).GetValue(null);
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
				return result;
			}
			return null;
		}
	}
}
