using System;

namespace Hassium
{
    public class HassiumArgumentParser
    {
        public static void DisplayHelp()
        {
            Console.WriteLine("Usage: Hassium.exe [PATH] [ARGS]");
            Console.WriteLine("[PATH]: The Hassium source file to execute.");
            Console.WriteLine("[ARGS]: The arguments to pass to the Hassium VM.");
            Environment.Exit(0);
        }

        private string[] args;
        private int position;

        public HassiumArgumentConfig Parse(string[] args)
        {
            this.args = args;
            position = 0;
            HassiumArgumentConfig config = new HassiumArgumentConfig();

            if (args.Length == 0)
                DisplayHelp();
            if (args[0] == "-h" || args[0].ToLower() == "--help")
                DisplayHelp();
            config.FilePath = expectData("[PATH]");

            for (int i = 1; i < args.Length; i++)
                config.Arguments.Add(args[i]);
            
            return config;
        }

        private string expectData(string type)
        {
            if (!args[position].StartsWith("-"))
                return args[position++];
            Console.WriteLine("Expected data {0}, not flag {1}!", type, args[position]);
            Environment.Exit(0);
            return string.Empty;
        }
    }
}

