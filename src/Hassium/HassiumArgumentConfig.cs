using System;
using System.IO;
using System.IO.Compression;

namespace Hassium
{
    public class HassiumArgumentConfig
    {
        public bool CreatePackage { get; set; }
        public string PackageFile { get; set; }
        public string SourceFile { get; set; }

        public static void ExecuteConfig(HassiumArgumentConfig config)
        {
            if (config.CreatePackage)
            {
                File.WriteAllText("manifest.conf", config.SourceFile);
                ZipFile.CreateFromDirectory(Directory.GetCurrentDirectory(), config.PackageFile);
            }
            else
            {
                if (config.SourceFile.EndsWith(".pkg"))
                {
                    string path = Directory.GetCurrentDirectory() + "/.pkg";
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                    Directory.CreateDirectory(path);
                    ZipFile.ExtractToDirectory(config.SourceFile, path);
                    Directory.SetCurrentDirectory(path);
                    HassiumExecuter.FromFilePath(File.ReadAllText("manifest.conf"));
                }
                else
                    HassiumExecuter.FromFilePath(config.SourceFile);
            }
        }
    }
}