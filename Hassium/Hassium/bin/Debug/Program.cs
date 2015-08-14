using System;
using System.IO;
using Hassium;

namespace HassiumTest
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			new HassiumInterpreter().Execute(File.ReadAllText("code.txt"));
		}
	}
}
