namespace Hassium
{
    public class Program
    {
        public static string MasterPath = string.Empty;

        static void Main(string[] args)
        {
            MasterPath = System.IO.Directory.GetCurrentDirectory();

            var config = new HassiumArgumentParser().Parse(args);
            HassiumConfig.ExecuteConfig(config);
        }
    }
}
