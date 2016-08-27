using System;

namespace Hassium
{
    public class HassiumArgumentParser
    {
        public static void DisplayHelp()
        {
            Console.WriteLine("Usage: Hassium.exe ([FLAGS]) [PATH] [ARGS]");
            Console.WriteLine("[PATH]: The Hassium source file to execute.");
            Console.WriteLine("[ARGS]: The arguments to pass to the Hassium VM.");
            Console.WriteLine("-h --help                    Displays this help and exits.");
            Console.WriteLine("-s --show-tokens [PATH]      Scans and outputs the tokens in the source file.");
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
            while (position < args.Length)
            {
                switch (args[position++].ToLower())
                {
                    case "-h":
                    case "--help":
                        DisplayHelp();
                        break;
                    case "-s":
                    case "--show-tokens":
                        config.ShowTokens = true;
                        break;
                    default:
                        position--;
                        config.FilePath = expectData("[PATH]");
                        for (int i = 1; i < args.Length; i++)
                            config.Arguments.Add(args[i]);
                        return config;
                }
            }
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

