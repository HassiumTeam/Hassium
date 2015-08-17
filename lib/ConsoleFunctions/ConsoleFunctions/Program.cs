using System;
using System.Collections.Generic;
using Hassium;

namespace ConsoleFunctions
{
	public class Program : ILibrary
	{
		public Dictionary<string, InternalFunction> GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();

			result.Add("print", new InternalFunction(Functions.Print));
			result.Add("input", new InternalFunction(Functions.Input));
			result.Add("pause", new InternalFunction(Functions.Pause));
			result.Add("cls", new InternalFunction(Functions.Cls));
			result.Add("setfcol", new InternalFunction(Functions.Setfcol));
			result.Add("setbcol", new InternalFunction(Functions.Setbcol));
			result.Add("getfcol", new InternalFunction(Functions.Getfcol));
			result.Add("getbcol", new InternalFunction(Functions.Getbcol));

			return result;
		}
	}
}
