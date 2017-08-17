using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;

namespace Hassium.Runtime.Util
{
    public class HassiumDateTime : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("DateTime");

        public DateTime DateTime { get; set; }

        public HassiumDateTime()
        {
            AddType(TypeDefinition);

            AddAttribute("now", new HassiumProperty(get_now));
            AddAttribute(INVOKE, _new, 3, 6, 7);
            ImportAttribs(this);
        }

        [FunctionAttribute("func new (year : int, month : int, day : int) : DateTime", "func new (year : int, month : int, day : int, hour : int, min : int, sec : int) : DateTime", "func new (year : int, month : int, day : int, hour : int, min : int, sec : int, millisecond : int) : DateTime")]
        public static HassiumDateTime _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumDateTime time = new HassiumDateTime();

            switch (args.Length)
            {
                case 3:
                    time.DateTime = new DateTime((int)args[0].ToInt(vm, location).Int, (int)args[1].ToInt(vm, location).Int, (int)args[2].ToInt(vm, location).Int);
                    break;
                case 6:
                    time.DateTime = new DateTime((int)args[0].ToInt(vm, location).Int, (int)args[1].ToInt(vm, location).Int, (int)args[2].ToInt(vm, location).Int, (int)args[3].ToInt(vm, location).Int, (int)args[4].ToInt(vm, location).Int, (int)args[5].ToInt(vm, location).Int);
                    break;
                case 7:
                    time.DateTime = new DateTime((int)args[0].ToInt(vm, location).Int, (int)args[1].ToInt(vm, location).Int, (int)args[2].ToInt(vm, location).Int, (int)args[3].ToInt(vm, location).Int, (int)args[4].ToInt(vm, location).Int, (int)args[5].ToInt(vm, location).Int, (int)args[6].ToInt(vm, location).Int);
                    break;
            }
            ImportAttribs(time);

            return time;
        }

        public static void ImportAttribs(HassiumDateTime time)
        {
            time.AddAttribute("day", new HassiumProperty(time.get_day));
            time.AddAttribute("dayofweek", new HassiumProperty(time.get_dayofweek));
            time.AddAttribute("dayofyear", new HassiumProperty(time.get_dayofyear));
            time.AddAttribute("hour", new HassiumProperty(time.get_hour));
            time.AddAttribute("millisecond", new HassiumProperty(time.get_millisecond));
            time.AddAttribute("minute", new HassiumProperty(time.get_minute));
            time.AddAttribute("month", new HassiumProperty(time.get_month));
            time.AddAttribute("second", new HassiumProperty(time.get_second));
            time.AddAttribute(TOSTRING, time.ToString, 0);
            time.AddAttribute("year", new HassiumProperty(time.get_year));
        }

        [FunctionAttribute("day { get; }")]
        public HassiumInt get_day(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Day);
        }

        [FunctionAttribute("dayofweek { get; }")]
        public HassiumString get_dayofweek(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(DateTime.DayOfWeek.ToString());
        }

        [FunctionAttribute("dayofyear { get; }")]
        public HassiumInt get_dayofyear(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.DayOfYear);
        }

        [FunctionAttribute("hour { get; }")]
        public HassiumInt get_hour(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Hour);
        }

        [FunctionAttribute("millisecond { get; }")]
        public HassiumInt get_millisecond(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Millisecond);
        }

        [FunctionAttribute("minute { get; }")]
        public HassiumInt get_minute(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Minute);
        }

        [FunctionAttribute("month { get; }")]
        public HassiumInt get_month(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Month);
        }

        [FunctionAttribute("now { get; }")]
        public HassiumDateTime get_now(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumDateTime time = new HassiumDateTime();

            time.DateTime = DateTime.Now;

            return time;
        }

        [FunctionAttribute("second { get; }")]
        public HassiumInt get_second(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Second);
        }

        [FunctionAttribute("func tostring () : string")]
        public override HassiumString ToString(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumString(DateTime.ToString());
        }

        [FunctionAttribute("year { get; }")]
        public HassiumInt get_year(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Year);
        }
    }
}
