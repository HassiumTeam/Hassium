using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hassium.HassiumObjects
{
    public class HassiumDate : HassiumObject
    {
        public DateTime Value { get; private set; }

        public HassiumDate(DateTime value)
        {
            this.Value = value;
            this.Attributes.Add("year", new InternalFunction(x => Value.Year, true));
            this.Attributes.Add("month", new InternalFunction(x => Value.Month, true));
            this.Attributes.Add("day", new InternalFunction(x => Value.Day, true));
            this.Attributes.Add("hour", new InternalFunction(x => Value.Hour, true));
            this.Attributes.Add("minute", new InternalFunction(x => Value.Minute, true));
            this.Attributes.Add("second", new InternalFunction(x => Value.Second, true));
            this.Attributes.Add("isLeapYear", new InternalFunction(x => DateTime.IsLeapYear(Value.Year), true));
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
