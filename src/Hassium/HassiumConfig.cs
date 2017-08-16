using System;

using Hassium.Compiler.Emit;
using Hassium.Compiler.Exceptions;
using Hassium.PackageManager;
using Hassium.Runtime;
using Hassium.Runtime.Types;

namespace Hassium
{
    public class HassiumConfig
    {
        public static void ExecuteConfig(HassiumConfig config)
        {
            if (config.HassiumRunType == HassiumRunType.Code)
            {
                try
                {
                    VirtualMachine vm = new VirtualMachine();
                    var module = HassiumCompiler.CompileModuleFromFilePath(config.FilePath, config.SuppressWarnings);
                    HassiumList hargs = new HassiumList(new HassiumObject[0]);

                    foreach (var arg in config.Args)
                        hargs.Values.Add(new HassiumString(arg));

                    vm.Execute(module, hargs);
                }
                catch (CompilerException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (config.Dev)
                        Console.WriteLine(ex);
                }
                catch (ParserException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (config.Dev)
                        Console.WriteLine(ex);
                }
                catch (ScannerException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (config.Dev)
                        Console.WriteLine(ex);
                }
            }
            else if (config.HassiumRunType == HassiumRunType.PackageManager)
            {
                var mgr = new HassiumPackageManager();

                bool status;
                switch (config.Action.ToLower())
                {
                    case "check":
                        status = mgr.CheckInstalled(config.Package);
                        if (status)
                            Console.WriteLine("Package '{0}' is installed! Remove it using Hassium.exe --pkg uninstall [PKGNAME]", config.Package);
                        else
                            Console.WriteLine("Package '{0}' is not installed! Install it using Hassium.exe --pkg install [PKGNAME]", config.Package);
                        break;
                    case "install":
                        status = mgr.InstallPackage(config.Package);
                        if (status)
                            Console.WriteLine("Successfully installed '{0}'. Include ```use * from {0}/<mod>``` to use.", config.Package);
                        else
                            Console.WriteLine("Installation failed! Is the package name correct?");
                        break;
                    case "uninstall":
                        status = mgr.UninstallPackage(config.Package);
                        if (status)
                            Console.WriteLine("Successfully uninstalled '{0}'.", config.Package);
                        else
                            Console.WriteLine("Uninstall failed! Is the package already uninstalled?");
                        break;
                }
            }
        }


        public HassiumRunType HassiumRunType { get; set; }

        // Code
        public string[] Args { get; set; }
        public bool Dev { get; set; }
        public string FilePath { get; set; }
        public bool SuppressWarnings { get; set; }

        // PackageManager
        public string Action { get; set; }
        public string Package { get; set; }

        public HassiumConfig()
        {
            HassiumRunType = HassiumRunType.Code;

            Args = new string[0];
            Dev = false;
            FilePath = string.Empty;
            SuppressWarnings = false;

            Action = string.Empty;
            Package = string.Empty;
        }
    }

    public enum HassiumRunType
    {
        Code,
        PackageManager
    }
}
