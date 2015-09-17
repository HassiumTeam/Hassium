using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Text;
using Hassium;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Drawing
{
    public class HassiumColor: HassiumObject
    {
        public Color Value { get; private set; }

        public HassiumColor(HassiumString col)
        {
            Value = Color.FromName(col.Value.ToLower());
        }
    }
}
