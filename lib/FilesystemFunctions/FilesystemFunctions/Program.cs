using System;
using System.Collections.Generic;
using Hassium;

namespace FilesystemFunctions
{
	public class Program : ILibrary
	{
		Dictionary<string, InternalFunction> ILibrary.GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();

			result.Add("puts", new InternalFunction(Functions.Puts));
			result.Add("mdir", new InternalFunction(Functions.Mdir));
			result.Add("ddir", new InternalFunction(Functions.Ddir));
			result.Add("dfile", new InternalFunction(Functions.Dfile));
			result.Add("getdir", new InternalFunction(Functions.Getdir));
			result.Add("setdir", new InternalFunction(Functions.Setdir));
			result.Add("fexists", new InternalFunction(Functions.Fexists));
			result.Add("dexists", new InternalFunction(Functions.Dexists));
			result.Add("system", new InternalFunction(Functions.System));

			return result;
		}
	}
}
