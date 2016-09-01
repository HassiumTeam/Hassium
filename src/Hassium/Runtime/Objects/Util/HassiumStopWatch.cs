using System;
using System.Diagnostics;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Util
{
    public class HassiumStopWatch: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("StopWatch");

        public Stopwatch StopWatch { get; private set; }
        public HassiumStopWatch()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, new HassiumFunction(_new, 0));
        }

        private HassiumStopWatch _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumStopWatch stopWatch = new HassiumStopWatch();

            stopWatch.StopWatch = new Stopwatch();
            stopWatch.AddAttribute("hours",        new HassiumProperty(stopWatch.get_Hours));
            stopWatch.AddAttribute("milliseconds", new HassiumProperty(stopWatch.get_Milliseconds));
            stopWatch.AddAttribute("minutes",      new HassiumProperty(stopWatch.get_Minutes));
            stopWatch.AddAttribute("reset",        stopWatch.reset,                       0);
            stopWatch.AddAttribute("restart",      stopWatch.restart,                     0);
            stopWatch.AddAttribute("running",      new HassiumProperty(stopWatch.get_Running));
            stopWatch.AddAttribute("seconds",      new HassiumProperty(stopWatch.get_Seconds));
            stopWatch.AddAttribute("start",        stopWatch.start,                       0);
            stopWatch.AddAttribute("stop",         stopWatch.stop,                        0);
            stopWatch.AddAttribute("ticks",        new HassiumProperty(stopWatch.get_Ticks));

            return stopWatch;
        }

        public HassiumInt get_Hours(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.Elapsed.Hours);
        }
        public HassiumInt get_Milliseconds(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.ElapsedMilliseconds);
        }
        public HassiumInt get_Minutes(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.Elapsed.Minutes);
        }
        public HassiumNull reset(VirtualMachine vm, HassiumObject[] args)
        {
            StopWatch.Reset();
            return HassiumObject.Null;
        }
        public HassiumNull restart(VirtualMachine mv, HassiumObject[] args)
        {
            StopWatch.Restart();
            return HassiumObject.Null;
        }
        public HassiumBool get_Running(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(StopWatch.IsRunning);
        }
        public HassiumInt get_Seconds(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.Elapsed.Seconds);
        }
        public HassiumNull start(VirtualMachine vm, HassiumObject[] args)
        {
            StopWatch.Start();
            return HassiumObject.Null;
        }
        public HassiumNull stop(VirtualMachine vm, HassiumObject[] args)
        {
            StopWatch.Stop();
            return HassiumObject.Null;
        }
        public HassiumInt get_Ticks(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.ElapsedTicks);
        }
    }
}