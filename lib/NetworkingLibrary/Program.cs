using System;
using System.Collections.Generic;
using Hassium;

namespace NetworkingFunctions
{
	public class Program : ILibrary
	{
		public Dictionary<string, InternalFunction> GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();

			result.Add("dowstr", new InternalFunction(Functions.DowStr));
			result.Add("dowfile", new InternalFunction(Functions.DowFile));
			result.Add("upfile", new InternalFunction(Functions.UpFile));

			return result;
		}
	}
}
