using System;
using System.Collections.Generic;
using Hassium;

namespace ConversionFunctions
{
	public class Program : ILibrary
	{
		Dictionary<string, InternalFunction> ILibrary.GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();

			result.Add("tonum", new InternalFunction(Functions.ToNum));
			result.Add("tostr", new InternalFunction(Functions.ToStr));
			result.Add("tobyte", new InternalFunction(Functions.ToByte));

			return result;
		}
	}
}
