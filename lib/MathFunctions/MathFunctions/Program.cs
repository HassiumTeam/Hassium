using System;
using System.Collections.Generic;
using Hassium;

namespace MathFunctions
{
	public class Program : ILibrary
	{
		Dictionary<string, InternalFunction> ILibrary.GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();

			result.Add("pow", new InternalFunction(Functions.Pow));
			result.Add("sqrt", new InternalFunction(Functions.Sqrt));

			return result;
		}
	}
}
