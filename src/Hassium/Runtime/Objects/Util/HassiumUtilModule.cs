using System;

namespace Hassium.Runtime.Objects.Util
{
    public class HassiumUtilModule: InternalModule
    {
        public HassiumUtilModule() : base("Util")
        {
            AddAttribute("OS",              new HassiumOS());
            AddAttribute("Process",         new HassiumProcess());
            AddAttribute("ProcessContext",  new HassiumProcessContext());
            AddAttribute("StopWatch",       new HassiumStopWatch());
            AddAttribute("UI",              new HassiumUI());
        }
    }
}

