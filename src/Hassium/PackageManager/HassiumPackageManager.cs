using System;
using System.IO;
using System.Net;

namespace Hassium.PackageManager
{
    public class HassiumPackageManager
    {
        private string hassiumfolder;

        public HassiumPackageManager()
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                    Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            hassiumfolder = Path.Combine(homePath, "/.Hassium/");
        }

        public void InstallPackage(string pkgname)
        {
            
        }

        public bool PackageInstalled(string pkgname)
        {
            return Directory.Exists(Path.Combine(hassiumfolder, pkgname));
        }
    }
}
