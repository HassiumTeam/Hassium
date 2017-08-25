using Hassium.Compiler;
using Hassium.Runtime.Types;

using System;
using System.Collections.Generic;

namespace Hassium.Runtime.Util
{
    public class HassiumDateTime : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new DateTimeTypeDef();

        public DateTime DateTime { get; set; }

        public HassiumDateTime()
        {
            AddType(TypeDefinition);
        }

        public class DateTimeTypeDef : HassiumTypeDefinition
        {
            public DateTimeTypeDef() : base("DateTime")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "day", new HassiumProperty(get_day)  },
                    { "dayofweek", new HassiumProperty(get_dayofweek)  },
                    { "dayofyear", new HassiumProperty(get_dayofyear)  },
                    { "hour", new HassiumProperty(get_hour)  },
                    { INVOKE, new HassiumFunction(_new, 3, 6, 7) },
                    { "millisecond", new HassiumProperty(get_millisecond)  },
                    { "minute", new HassiumProperty(get_minute)  },
                    { "month", new HassiumProperty(get_month)  },
                    { "second", new HassiumProperty(get_second)  },
                    { TOSTRING, new HassiumFunction(tostring, 0)  },
                    { "year", new HassiumProperty(get_year)  },
                };
            }

            [FunctionAttribute("func new (year : int, month : int, day : int) : DateTime", "func new (year : int, month : int, day : int, hour : int, min : int, sec : int) : DateTime", "func new (year : int, month : int, day : int, hour : int, min : int, sec : int, millisecond : int) : DateTime")]
            public static HassiumDateTime _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumDateTime time = new HassiumDateTime();

                switch (args.Length)
                {
                    case 3:
                        time.DateTime = new DateTime((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (int)args[2].ToInt(vm, args[2], location).Int);
                        break;
                    case 6:
                        time.DateTime = new DateTime((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (int)args[2].ToInt(vm, args[2], location).Int, (int)args[3].ToInt(vm, args[3], location).Int, (int)args[4].ToInt(vm, args[4], location).Int, (int)args[5].ToInt(vm, args[5], location).Int);
                        break;
                    case 7:
                        time.DateTime = new DateTime((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (int)args[2].ToInt(vm, args[2], location).Int, (int)args[3].ToInt(vm, args[3], location).Int, (int)args[4].ToInt(vm, args[4], location).Int, (int)args[5].ToInt(vm, args[5], location).Int, (int)args[6].ToInt(vm, args[6], location).Int);
                        break;
                }

                return time;
            }

            [FunctionAttribute("day { get; }")]
            public static HassiumInt get_day(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Day);
            }

            [FunctionAttribute("dayofweek { get; }")]
            public static HassiumString get_dayofweek(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumString(DateTime.DayOfWeek.ToString());
            }

            [FunctionAttribute("dayofyear { get; }")]
            public static HassiumInt get_dayofyear(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.DayOfYear);
            }

            [FunctionAttribute("hour { get; }")]
            public static HassiumInt get_hour(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Hour);
            }

            [FunctionAttribute("millisecond { get; }")]
            public static HassiumInt get_millisecond(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Millisecond);
            }

            [FunctionAttribute("minute { get; }")]
            public static HassiumInt get_minute(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Minute);
            }

            [FunctionAttribute("month { get; }")]
            public static HassiumInt get_month(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Month);
            }

            [FunctionAttribute("now { get; }")]
            public static HassiumDateTime get_now(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumDateTime time = new HassiumDateTime();

                time.DateTime = DateTime.Now;

                return time;
            }

            [FunctionAttribute("second { get; }")]
            public static HassiumInt get_second(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Second);
            }

            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumString(DateTime.ToString());
            }

            [FunctionAttribute("year { get; }")]
            public static HassiumInt get_year(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Year);
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
