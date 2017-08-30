namespace Hassium.Runtime.Util
{
    public class HassiumUtilModule : InternalModule
    {
        public HassiumUtilModule() : base("Util")
        {
            AddAttribute("ColorNotFoundException", HassiumColorNotFoundException.TypeDefinition);
            AddAttribute("DateTime", HassiumDateTime.TypeDefinition);
            AddAttribute("OS", new HassiumOS());
            AddAttribute("Process", HassiumProcess.TypeDefinition);
            AddAttribute("StopWatch", HassiumStopWatch.TypeDefinition);
            AddAttribute("UI", new HassiumUI());
        }
    }
}
