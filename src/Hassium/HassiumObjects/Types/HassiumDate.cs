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
            Attributes.Add("year", new InternalFunction(x => Value.Year, 0, true));
            Attributes.Add("month", new InternalFunction(x => Value.Month, 0, true));
            Attributes.Add("day", new InternalFunction(x => Value.Day, 0, true));
            Attributes.Add("hour", new InternalFunction(x => Value.Hour, 0, true));
            Attributes.Add("minute", new InternalFunction(x => Value.Minute, 0, true));
            Attributes.Add("second", new InternalFunction(x => Value.Second, 0, true));
            Attributes.Add("isLeapYear", new InternalFunction(x => DateTime.IsLeapYear(Value.Year), 0, true));
            Attributes.Add("timeStamp", new InternalFunction(x => GetTimestamp(new HassiumObject[]{}), 0, true));
        }

        public HassiumObject GetTimestamp(HassiumObject[] args)
        {
            return new HassiumInt((int)(Value - new DateTime(1970, 1, 1)).TotalSeconds);
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
