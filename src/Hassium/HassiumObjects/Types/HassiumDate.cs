using System;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Types
{
    public class HassiumDate : HassiumObject
    {
        public DateTime Value { get; private set; }

        public HassiumDate(DateTime value)
        {
            Value = value;
            Attributes.Add("year", new InternalFunction(x => Value.Year, true));
            Attributes.Add("month", new InternalFunction(x => Value.Month, true));
            Attributes.Add("day", new InternalFunction(x => Value.Day, true));
            Attributes.Add("hour", new InternalFunction(x => Value.Hour, true));
            Attributes.Add("minute", new InternalFunction(x => Value.Minute, true));
            Attributes.Add("second", new InternalFunction(x => Value.Second, true));
            Attributes.Add("isLeapYear", new InternalFunction(x => DateTime.IsLeapYear(Value.Year), true));
        }

        public HassiumObject GetTimestamp(HassiumObject[] args)
        {
            return new HassiumNumber((Value - new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static HassiumDate operator +(HassiumDate d1, HassiumDate d2)
        {
            var a = d1.Value;
            var b = d2.Value;
            var c = new HassiumDate(a + new TimeSpan(b.Year * 365 + b.DayOfYear, b.Hour, b.Minute, b.Second, b.Millisecond));
            return c;
        }

        public static HassiumDate operator -(HassiumDate d1, HassiumDate d2)
        {
            return new HassiumDate(new DateTime().Add(d1.Value - d2.Value));
        }
    }
}
