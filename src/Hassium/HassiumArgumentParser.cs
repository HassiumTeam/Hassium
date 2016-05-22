using System;
using System.Collections.Generic;
using System.IO;

namespace Hassium
{
    public class HassiumArgumentParser
    {
        private string[] args;
        private int position;
        private List<string> hassiumArgs = new List<string>();

        public HassiumArgumentParser(string[] args)
        {
            this.args = args;
        }

        public HassiumArgumentConfig Parse()
        {
            var result = new HassiumArgumentConfig()
            {
                CreatePackage = false
            };

            for (position = 0; position < args.Length; position++)
            {
                if (File.Exists(args[position]))
                {
                    if (result.SourceFile == null)
                        result.SourceFile = args[position];
                    else
                        hassiumArgs.Add(args[position]);
                }
                else
                    switch (args[position])
                    {
                        case "-p":
                        case "--package":
                            result.CreatePackage = true;
                            result.PackageFile = expectData("package file");
                            break;
                        default:
                            hassiumArgs.Add(args[position]);
                            break;
                    }
            }

            result.HassiumArgs = hassiumArgs;

            return result;
        }

        private string expectData(string type)
        {
            if (args[++position].StartsWith("-"))
            {
                Console.WriteLine("Expected {0} instead of flag {1}!", type, args[position]);
                Environment.Exit(0);
            }
            return args[position];
        }
    }
}