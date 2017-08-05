namespace Hassium.Runtime.Util
{
    public class HassiumUtilModule : InternalModule
    {
        public HassiumUtilModule() : base("Util")
        {
            AddAttribute("DateTime", new HassiumDateTime());
            AddAttribute("OS", new HassiumOS());
            AddAttribute("Process", new HassiumProcess());
            AddAttribute("StopWatch", new HassiumStopWatch());
            AddAttribute("UI", new HassiumUI());
        }
    }
}
