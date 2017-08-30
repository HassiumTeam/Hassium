using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Diagnostics;

namespace Hassium.Runtime.Util
{
    public class HassiumStopWatch : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new StopWatchTypeDef();

        public Stopwatch StopWatch { get; private set; }

        public HassiumStopWatch()
        {
            AddType(TypeDefinition);
        }

        public class StopWatchTypeDef : HassiumTypeDefinition
        {
            public StopWatchTypeDef() : base("StopWatch")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "hours", new HassiumProperty(get_hours)  },
                    { INVOKE, new HassiumFunction(_new, 0) },
                    { "isrunning", new HassiumProperty(get_isrunning)  },
                    { "milliseconds", new HassiumProperty(get_milliseconds)  },
                    { "minutes", new HassiumProperty(get_minutes)  },
                    { "restart", new HassiumFunction(restart, 0)  },
                    { "reset", new HassiumFunction(reset, 0)  },
                    { "seconds", new HassiumProperty(get_seconds)  },
                    { "start", new HassiumFunction(start, 0)  },
                    { "stop", new HassiumFunction(stop, 0)  },
                    { "ticks", new HassiumProperty(get_ticks)  },
                };
            }

            [DocStr(
                "@desc Constructs a new StopWatch object.",
                "@returns The new StopWatch object."
                )]
            [FunctionAttribute("func new () : StopWatch")]
            public static HassiumStopWatch _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumStopWatch watch = new HassiumStopWatch();

                watch.StopWatch = new Stopwatch();

                return watch;
            }

            [DocStr(
                "@desc Gets the readonly hours that have passed.",
                "@returns The elapsed hours as int."
                )]
            [FunctionAttribute("hours { get; }")]
            public static HassiumInt get_hours(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                return new HassiumInt(StopWatch.Elapsed.Hours);
            }

            [DocStr(
                "@desc Gets the readonly boolean indicating if the stopwatch is running.",
                "@returns true if this StopWatch is running, otherwise false."
                )]
            [FunctionAttribute("isrunning { get; }")]
            public static HassiumBool get_isrunning(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                return new HassiumBool(StopWatch.IsRunning);
            }

            [DocStr(
                "@desc Gets the readonly milliseconds that have passed.",
                "@returns The elapsed milliseconds as int."
                )]
            [FunctionAttribute("milliseconds { get; }")]
            public static HassiumInt get_milliseconds(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                return new HassiumInt(StopWatch.Elapsed.Milliseconds);
            }

            [DocStr(
                "@desc Gets the readonly minutes that have passed.",
                "@returns The elapsed minutes as int."
                )]
            [FunctionAttribute("minutes { get; }")]
            public static HassiumInt get_minutes(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                return new HassiumInt(StopWatch.Elapsed.Minutes);
            }

            [DocStr(
                "@desc Restarts this stopwatch.",
                "@returns null."
                )]
            [FunctionAttribute("func restart () : null")]
            public static HassiumNull restart(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                StopWatch.Restart();
                return Null;
            }

            [DocStr(
                "@desc Resets this stopwatch.",
                "@returns null."
                )]
            [FunctionAttribute("func reset () : null")]
            public static HassiumNull reset(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                StopWatch.Reset();
                return Null;
            }

            [DocStr(
                "@desc Gets the readonly seconds that have passed.",
                "@returns The elapsed seconds as int."
                )]
            [FunctionAttribute("seconds { get; }")]
            public static HassiumInt get_seconds(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                return new HassiumInt(StopWatch.Elapsed.Seconds);
            }

            [DocStr(
                "@desc Starts this stopwatch.",
                "@returns null."
                )]
            [FunctionAttribute("func start () : null")]
            public static HassiumNull start(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                StopWatch.Start();
                return Null;
            }

            [DocStr(
                "@desc Stops this stopwatch.",
                "@returns null."
                )]
            [FunctionAttribute("func stop () : null")]
            public static HassiumNull stop(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                StopWatch.Stop();
                return Null;
            }

            [DocStr(
                "@desc Gets the readonly ticks that have passed.",
                "@returns The elapsed ticks as int."
                )]
            [FunctionAttribute("ticks { get; }")]
            public static HassiumInt get_ticks(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var StopWatch = (self as HassiumStopWatch).StopWatch;
                return new HassiumInt(StopWatch.ElapsedTicks);
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
