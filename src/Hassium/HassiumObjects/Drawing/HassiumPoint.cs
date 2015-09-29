using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Hassium.Functions;

namespace Hassium.HassiumObjects.Drawing
{
    public class HassiumPoint : HassiumObject
    {
        public Point Value { get; set; }

        public HassiumPoint(int x, int y)
        {
            Value = new Point(x, y);

            Attributes.Add("x", new HassiumProperty("x", a => Value.X, a =>
            {
                Value = new Point(a[0].HInt().Value, Value.Y);
                return true;
            }));

            Attributes.Add("y", new HassiumProperty("y", a => Value.Y, a =>
            {
                Value = new Point(Value.X, a[0].HInt().Value);
                return true;
            }));
            Attributes.Add("isEmpty", new HassiumProperty("isEmpty", a => Value.IsEmpty, null, true));
            Attributes.Add("offset", new InternalFunction(Offset, new []{1,2}));
        }

        public HassiumObject Offset(HassiumObject[] args)
        {
            if(args.Length == 2)
            {
                Value.Offset(args[0].HInt().Value, args[1].HInt().Value);
            }
            else
            {
                Value.Offset(((HassiumPoint)args[0]).Value);
            }
            return this;
        }
    }
}
