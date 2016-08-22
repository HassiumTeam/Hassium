using System;

namespace Hassium
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            HassiumArgumentConfig.ExecuteConfig(new HassiumArgumentParser().Parse(args));
        }
    }
}