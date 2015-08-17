using System;
using System.Collections.Generic;
using Hassium;

namespace MiscFunctions
{
	public class Program : ILibrary
	{
		public Dictionary<string, InternalFunction> GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();

			result.Add("free", new InternalFunction(Functions.Free));
			result.Add("exit", new InternalFunction(Functions.Exit));
			result.Add("type", new InternalFunction(Functions.Type));
			result.Add("throw", new InternalFunction(Functions.Throw));

			return result;
		}
	}
}
