namespace Hassium.Runtime.Util
{
    public class HassiumUtilModule : InternalModule
    {
        public HassiumUtilModule() : base("Util")
        {
            AddAttribute("ColorNotFoundException", HassiumColorNotFoundException.TypeDefinition);
            AddAttribute("DateTime", HassiumDateTime.TypeDefinition);
            AddAttribute("OS", HassiumOS.TypeDefinition);
            AddAttribute("Process", HassiumProcess.TypeDefinition);
            AddAttribute("StopWatch", HassiumStopWatch.TypeDefinition);
            AddAttribute("UI", HassiumUI.TypeDefinition);
        }
    }
}
