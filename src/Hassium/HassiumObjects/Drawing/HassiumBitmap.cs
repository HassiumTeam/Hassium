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
    public class HassiumBitmap: HassiumObject
    {
        public Bitmap Value { get; private set; }

        public HassiumBitmap(HassiumObject[] cctr)
        {
            if (cctr[0] is HassiumString)
                Value = new Bitmap(((HassiumString)cctr).Value);
            else if (cctr[0] is HassiumImage)
                Value = new Bitmap(((HassiumImage)cctr).Value);
            else if (cctr[0] is HassiumInt)
                Value = new Bitmap(((HassiumInt)cctr[0]).Value, ((HassiumInt)cctr[1]).Value);
            else if (cctr[0] is HassiumDouble)
                Value = new Bitmap(((HassiumDouble)cctr[0]).ValueInt, ((HassiumDouble)cctr[1]).ValueInt);
            else
                throw new Exception("Unknown type " + cctr[0].GetType().ToString() + " in HassiumBitmap constructor");

            Attributes.Add("height", new HassiumProperty("height", x => Value.Height, x => null, true));
            Attributes.Add("width", new HassiumProperty("width", x => Value.Width, x => null, true));
            Attributes.Add("dispose", new InternalFunction(dispose, 0));
            Attributes.Add("makeTransparent", new InternalFunction(makeTransparent, 0));
            Attributes.Add("save", new InternalFunction(save, 1));
            Attributes.Add("setPixel", new InternalFunction(setPixel, 3));
            Attributes.Add("setResolution", new InternalFunction(setResolution, 2));
            Attributes.Add("toString", new InternalFunction(toString, 0));
        }

        private HassiumObject dispose(HassiumObject[] args)
        {
            Value.Dispose();

            return null;
        }

        private HassiumObject makeTransparent(HassiumObject[] args)
        {
            if (args.Length <= 0)
                Value.MakeTransparent();
            else
                Value.MakeTransparent(((HassiumColor)args[0]).Value);

            return null;
        }

        private HassiumObject save(HassiumObject[] args)
        {
            Value.Save(((HassiumString)args[0]).Value);

            return null;
        }

        private HassiumObject setPixel(HassiumObject[] args)
        {
            Value.SetPixel(((HassiumDouble)args[0]).ValueInt, ((HassiumDouble)args[1]).ValueInt, ((HassiumColor)args[2]).Value);

            return null;
        }

        private HassiumObject setResolution(HassiumObject[] args)
        {
            Value.SetResolution(((float)((HassiumDouble)args[0]).Value), ((float)((HassiumDouble)args[1]).Value));

            return null;
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return new HassiumString(Value.ToString());
        }
    }
}
