using System;
using System.IO;
using System.Net;
using System.Text;

namespace Hassium.PackageManager
{
    public class HassiumPackageManager
    {
        private const string PACKAGE_URL = "https://raw.githubusercontent.com/HassiumTeam/Hassium/master/package/";
        private const string MANI_URL_FORMAT = PACKAGE_URL + "{0}/{0}.mani";
        private const string FILE_URL_FORMAT = PACKAGE_URL + "{0}/{1}";

        private string hassiumfolder;

        public HassiumPackageManager()
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                    Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            hassiumfolder = Path.Combine(homePath, ".Hassium/");
        }

        public bool CheckInstalled(string pkgname)
        {
            return Directory.Exists(Path.Combine(hassiumfolder, pkgname));
        }

        public bool InstallPackage(string pkgname)
        {
            if (CheckInstalled(pkgname))
                UninstallPackage(pkgname);

            string response;
            try
            {
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(string.Format(MANI_URL_FORMAT, pkgname));
                    Directory.CreateDirectory(Path.Combine(hassiumfolder, pkgname));
                    Directory.SetCurrentDirectory(Path.Combine(hassiumfolder, pkgname));

                    foreach (var line in response.Split('\n'))
                    {
                        var file = line.Trim();
                        if (file == string.Empty)
                            continue;
                        client.DownloadFile(string.Format(FILE_URL_FORMAT, pkgname, file), file);
                    }
                    return true;
                }
            }
            catch ( Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool UninstallPackage(string pkgname)
        {
            if (!CheckInstalled(pkgname))
                return false;

            Directory.Delete(Path.Combine(hassiumfolder, string.Format("{0}/", pkgname)), true);
            return true;
        }
    }
}
