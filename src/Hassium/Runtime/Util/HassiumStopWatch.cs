using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Diagnostics;

namespace Hassium.Runtime.Util
{
    public class HassiumStopWatch : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("StopWatch");

        public Stopwatch StopWatch { get; private set; }

        public HassiumStopWatch()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 0);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumStopWatch watch)
        {
            watch.AddAttribute("hours", new HassiumProperty(watch.get_hours));
            watch.AddAttribute("isrunning", new HassiumProperty(watch.get_isrunning));
            watch.AddAttribute("milliseconds", new HassiumProperty(watch.get_milliseconds));
            watch.AddAttribute("minutes", new HassiumProperty(watch.get_minutes));
            watch.AddAttribute("restart", watch.restart, 0);
            watch.AddAttribute("reset", watch.reset, 0);
            watch.AddAttribute("seconds", new HassiumProperty(watch.get_seconds));
            watch.AddAttribute("start", watch.start, 0);
            watch.AddAttribute("stop", watch.stop, 0);
            watch.AddAttribute("ticks", new HassiumProperty(watch.get_ticks));
        }

        [FunctionAttribute("func new () : StopWatch")]
        public static HassiumStopWatch _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumStopWatch watch = new HassiumStopWatch();

            watch.StopWatch = new Stopwatch();
            ImportAttribs(watch);

            return watch;
        }

        [FunctionAttribute("hours { get; }")]
        public HassiumInt get_hours(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.Elapsed.Hours);
        }

        [FunctionAttribute("isrunning { get; }")]
        public HassiumBool get_isrunning(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumBool(StopWatch.IsRunning);
        }

        [FunctionAttribute("milliseconds { get; }")]
        public HassiumInt get_milliseconds(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.Elapsed.Milliseconds);
        }

        [FunctionAttribute("minutes { get; }")]
        public HassiumInt get_minutes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.Elapsed.Minutes);
        }

        [FunctionAttribute("func restart () : null")]
        public HassiumNull restart(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StopWatch.Restart();
            return Null;
        }

        [FunctionAttribute("func reset () : null")]
        public HassiumNull reset(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StopWatch.Reset();
            return Null;
        }

        [FunctionAttribute("seconds { get; }")]
        public HassiumInt get_seconds(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.Elapsed.Seconds);
        }

        [FunctionAttribute("func start () : null")]
        public HassiumNull start(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StopWatch.Start();
            return Null;
        }

        [FunctionAttribute("func stop () : null")]
        public HassiumNull stop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            StopWatch.Stop();
            return Null;
        }

        [FunctionAttribute("ticks { get; }")]
        public HassiumInt get_ticks(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(StopWatch.ElapsedTicks);
        }
    }
}
