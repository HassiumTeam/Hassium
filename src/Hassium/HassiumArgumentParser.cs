using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium
{
    public class HassiumArgumentParser
    {
        private string[] args;
        private int position;

        public HassiumConfig Parse(string[] args)
        {
            this.args = args;
            var config = new HassiumConfig();

            if (args.Length <= 0)
                displayHelp();

            for (position = 0; position < args.Length; position++)
            {
                switch (args[position].ToLower())
                {
                    case "-d":
                    case "--dev":
                        config.Dev = true;
                        break;
                    case "-h":
                    case "--help":
                        displayHelp();
                        break;
                    case "-p":
                    case "--pkg":
                        config.HassiumRunType = HassiumRunType.PackageManager;
                        config.Action = expectData("[ACTION]");
                        config.Package = expectData("[PKGNAME]");
                        break;
                    case "-r":
                    case "--repl":
                        config.HassiumRunType = HassiumRunType.REPL;
                        break;
                    case "-s":
                    case "--suppress-warns":
                        config.SuppressWarnings = true;
                        break;
                    default:
                        config.FilePath = args[position++];
                        List<string> pargs = new List<string>();
                        for (; position < args.Length; position++)
                            pargs.Add(args[position]);
                        config.Args = pargs.ToArray();
                        break;
                }
            }

            return config;
        }

        private void displayHelp()
        {
            Console.WriteLine("USAGE: Hassium.exe   -[hargs]... [FILE].has [pargs]...");
            Console.WriteLine("USAGE: Hassium.exe   --pkg [ACTION] [PKGNAME]");
            Console.WriteLine("\n[hargs]");
            Console.WriteLine("-d --dev             Runs in developer mode (verbose errors).");
            Console.WriteLine("-r --repl            Executes an REPL shell.");
            Console.WriteLine("-s --suppress-warns  Suppresses compiler warnings.");
            Console.WriteLine("\n[ACTION]");
            Console.WriteLine("check                Checks if PKGNAME is installed.");
            Console.WriteLine("install              Installs PKGNAME to ~/.Hassium/.");
            Console.WriteLine("uninstall            Uninstalls PKGNAME.");
            die(string.Empty);
        }

        private string expectData(string type)
        {
            if (args[++position].StartsWith("-"))
                die(string.Format("Unexpected flag {0}, expected {1}!", args[position], type));
            return args[position];
        }

        private void die(string msg)
        {
            Console.WriteLine(msg);
            Environment.Exit(0);
        }
    }
}
