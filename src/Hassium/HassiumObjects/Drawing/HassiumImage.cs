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
    public class HassiumImage : HassiumObject
    {
        public Image Value { get; private set; }

        public HassiumImage(HassiumString path)
        {
            Value = Image.FromFile(path);
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("save", new InternalFunction(save, 1));
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();

            return null;
        }

        private HassiumObject save(HassiumObject[] args)
        {
            Value.Save(((HassiumString) args[0]).ToString());

            return null;
        }
    }
}