using System;
using System.Diagnostics;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Util
{
    public class HassiumStopWatch: HassiumObject
    {
        public new Stopwatch Value { get; private set; }
        public HassiumStopWatch()
        {
            Attributes.Add(HassiumObject.INVOKE_FUNCTION, new HassiumFunction(_new, 0));
        }

        private HassiumStopWatch _new(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumStopWatch hassiumStopWatch = new HassiumStopWatch();

            hassiumStopWatch.Value = new Stopwatch();
            hassiumStopWatch.Attributes.Add("hours",        new HassiumProperty(hassiumStopWatch.get_Hours));
            hassiumStopWatch.Attributes.Add("milliseconds", new HassiumProperty(hassiumStopWatch.get_Milliseconds));
            hassiumStopWatch.Attributes.Add("minutes",      new HassiumProperty(hassiumStopWatch.get_Minutes));
            hassiumStopWatch.Attributes.Add("reset",        new HassiumFunction(hassiumStopWatch.reset, 0));
            hassiumStopWatch.Attributes.Add("restart",      new HassiumFunction(hassiumStopWatch.restart, 0));
            hassiumStopWatch.Attributes.Add("running",      new HassiumProperty(hassiumStopWatch.get_Running));
            hassiumStopWatch.Attributes.Add("seconds",      new HassiumProperty(hassiumStopWatch.get_Seconds));
            hassiumStopWatch.Attributes.Add("start",        new HassiumFunction(hassiumStopWatch.start, 0));
            hassiumStopWatch.Attributes.Add("stop",         new HassiumFunction(hassiumStopWatch.stop, 0));
            hassiumStopWatch.Attributes.Add("ticks",        new HassiumProperty(hassiumStopWatch.get_Ticks));

            return hassiumStopWatch;
        }

        public HassiumInt get_Hours(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.Elapsed.Hours);
        }
        public HassiumInt get_Milliseconds(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.ElapsedMilliseconds);
        }
        public HassiumInt get_Minutes(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.Elapsed.Minutes);
        }
        public HassiumNull reset(VirtualMachine vm, HassiumObject[] args)
        {
            Value.Reset();
            return HassiumObject.Null;
        }
        public HassiumNull restart(VirtualMachine mv, HassiumObject[] args)
        {
            Value.Restart();
            return HassiumObject.Null;
        }
        public HassiumBool get_Running(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumBool(Value.IsRunning);
        }
        public HassiumInt get_Seconds(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.Elapsed.Seconds);
        }
        public HassiumNull start(VirtualMachine vm, HassiumObject[] args)
        {
            Value.Start();
            return HassiumObject.Null;
        }
        public HassiumNull stop(VirtualMachine vm, HassiumObject[] args)
        {
            Value.Stop();
            return HassiumObject.Null;
        }
        public HassiumInt get_Ticks(VirtualMachine vm, HassiumObject[] args)
        {
            return new HassiumInt(Value.ElapsedTicks);
        }
    }
}