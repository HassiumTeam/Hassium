﻿using Hassium.Compiler;
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

        [DocStr(
            "@desc A class representing a specific instance of time and date.",
            "@returns DateTime."
            )]
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
                    { "now", new HassiumProperty(get_now) },
                    { "second", new HassiumProperty(get_second)  },
                    { TOSTRING, new HassiumFunction(tostring, 0)  },
                    { "year", new HassiumProperty(get_year)  },
                };
            }

            [DocStr(
                "@desc Constructs a new DateTime object using the specified year, month, day, and optional hour, min, second, and millisecond integers.",
                "@param year The int year.",
                "@param month The int month (1-12).",
                "@param day The int day.",
                "@optional hour The int hour.",
                "@optional minute The int minute.",
                "@optional second The int second.",
                "@optional millisecond The int millisecond.",
                "@returns The new DateTime object."
                )]
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

            [DocStr(
                "@desc Gets the readonly int day.",
                "@returns The day as int."
                )]
            [FunctionAttribute("day { get; }")]
            public static HassiumInt get_day(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Day);
            }

            [DocStr(
                "@desc Gets the readonly int dayofweek (1-7).",
                "@returns The day of week as int."
                )]
            [FunctionAttribute("dayofweek { get; }")]
            public static HassiumString get_dayofweek(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumString(DateTime.DayOfWeek.ToString());
            }

            [DocStr(
                "@desc Gets the readonly int dayofyear.",
                "@returns The day of year as int."
                )]
            [FunctionAttribute("dayofyear { get; }")]
            public static HassiumInt get_dayofyear(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.DayOfYear);
            }

            [DocStr(
                "@desc Gets the readonly hour.",
                "@returns The hour as int."
                )]
            [FunctionAttribute("hour { get; }")]
            public static HassiumInt get_hour(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Hour);
            }

            [DocStr(
                "@desc Gets the readonly millisecond.",
                "@returns The millisecond as int."
                )]
            [FunctionAttribute("millisecond { get; }")]
            public static HassiumInt get_millisecond(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Millisecond);
            }

            [DocStr(
                "@desc Gets the readonly minute.",
                "@returns The minute as int."
                )]
            [FunctionAttribute("minute { get; }")]
            public static HassiumInt get_minute(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Minute);
            }

            [DocStr(
                "@desc Gets the readonly month.",
                "@returns The month as int."
                )]
            [FunctionAttribute("month { get; }")]
            public static HassiumInt get_month(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Month);
            }

            [DocStr(
                "@desc Returns a new DateTime object with the values for date and time based off of the system clock.",
                "@returns The new DateTime object with the current date and time."
                )]
            [FunctionAttribute("now { get; }")]
            public static HassiumDateTime get_now(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumDateTime time = new HassiumDateTime();

                time.DateTime = DateTime.Now;

                return time;
            }

            [DocStr(
                "@desc Gets the readonly second.",
                "@returns The second as int."
                )]
            [FunctionAttribute("second { get; }")]
            public static HassiumInt get_second(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumInt(DateTime.Second);
            }

            [DocStr(
                "@desc Gets the string value of this date and time.",
                "@returns The string value of the DateTime."
                )]
            [FunctionAttribute("func tostring () : string")]
            public static HassiumString tostring(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var DateTime = (self as HassiumDateTime).DateTime;
                return new HassiumString(DateTime.ToString());
            }

            [DocStr(
                "@desc Gets the readonly year.",
                "@returns The year as int."
                )]
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
