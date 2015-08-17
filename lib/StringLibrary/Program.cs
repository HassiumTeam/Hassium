using System;
using System.Collections.Generic;
using Hassium;

namespace StringFunctions
{
	public class Program : ILibrary
	{
		public Dictionary<string, InternalFunction> GetFunctions()
		{
			Dictionary<string, InternalFunction> result = new Dictionary<string, InternalFunction>();

            		result.Add("strcat", new InternalFunction(Functions.Strcat));
                	result.Add("strlen", new InternalFunction(Functions.Strlen));
                	result.Add("getch", new InternalFunction(Functions.Getch));
	                result.Add("sstr", new InternalFunction(Functions.Sstr));
        	        result.Add("begins", new InternalFunction(Functions.Begins));
                	result.Add("toupper", new InternalFunction(Functions.ToUpper));
	                result.Add("tolower", new InternalFunction(Functions.ToLower));

			return result;
		}
	}
}
