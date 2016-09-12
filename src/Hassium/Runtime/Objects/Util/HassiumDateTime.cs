using System;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Util
{
    public class HassiumDateTime: HassiumObject
    {
        public DateTime DateTime { get; set; }

        public HassiumDateTime()
        {
            AddAttribute("now",     new HassiumProperty(now));
            AddAttribute("parse",   parse,                 1);
            AddAttribute(HassiumObject.INVOKE, _new, 3, 6, 7);
        }

        public HassiumDateTime now(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumDateTime dateTime = new HassiumDateTime();
            dateTime.DateTime = DateTime.Now;
            AddAttributes(dateTime);
            return dateTime;
        }

        public HassiumDateTime parse(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumDateTime dateTime = new HassiumDateTime();
            dateTime.DateTime = DateTime.Parse(args[0].ToString(vm).String);
            AddAttributes(dateTime);
            return dateTime;
        }

        public HassiumDateTime _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumDateTime dateTime = new HassiumDateTime();
            switch (args.Length)
            {
                case 3:
                    dateTime.DateTime = new DateTime((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int, (int)args[2].ToInt(vm).Int);
                    break;
                case 6:
                    dateTime.DateTime = new DateTime((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int, (int)args[2].ToInt(vm).Int, (int)args[3].ToInt(vm).Int, (int)args[4].ToInt(vm).Int, (int)args[5].ToInt(vm).Int);
                    break;
                case 7:
                    dateTime.DateTime = new DateTime((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int, (int)args[2].ToInt(vm).Int, (int)args[3].ToInt(vm).Int, (int)args[4].ToInt(vm).Int, (int)args[5].ToInt(vm).Int, (int)args[6].ToInt(vm).Int);
                    break;
            }
            AddAttributes(dateTime);
            return dateTime;
        }

        public static void AddAttributes(HassiumDateTime dateTime)
        {
            dateTime.AddAttribute("day",            new HassiumProperty(dateTime.get_day));
            dateTime.AddAttribute("dayOfWeek",      new HassiumProperty(dateTime.get_dayOfWeek));
            dateTime.AddAttribute("dayOfYear",      new HassiumProperty(dateTime.get_dayOfYear));
            dateTime.AddAttribute("hour",           new HassiumProperty(dateTime.get_hour));
            dateTime.AddAttribute("millisecond",    new HassiumProperty(dateTime.get_millisecond));
            dateTime.AddAttribute("minute",         new HassiumProperty(dateTime.get_minute));
            dateTime.AddAttribute("month",          new HassiumProperty(dateTime.get_month));
            dateTime.AddAttribute("second",         new HassiumProperty(dateTime.get_second));
            dateTime.AddAttribute(HassiumObject.TOSTRING, dateTime.ToString, 0);
            dateTime.AddAttribute("year",           new HassiumProperty(dateTime.get_year));
        }

        public HassiumInt get_day(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Day);
        }
        public HassiumString get_dayOfWeek(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(DateTime.DayOfWeek.ToString());
        }
        public HassiumInt get_dayOfYear(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.DayOfYear);
        }
        public HassiumInt get_hour(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Hour);
        }
        public HassiumInt get_millisecond(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Millisecond);
        }
        public HassiumInt get_minute(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Minute);
        }
        public HassiumInt get_month(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Month);
        }
        public HassiumInt get_second(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Second);
        }
        public HassiumString toString(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumString(DateTime.ToString());
        }
        public HassiumInt get_year(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(DateTime.Year);
        }
    }
}

