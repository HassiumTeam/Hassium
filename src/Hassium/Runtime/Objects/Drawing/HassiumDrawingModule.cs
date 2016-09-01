using System;

namespace Hassium.Runtime.Objects.Drawing
{
    public class HassiumDrawingModule: InternalModule
    {
        public HassiumDrawingModule() : base("Drawing")
        {
            AddAttribute("Bitmap",  new HassiumBitmap());
            AddAttribute("Color",   new HassiumColor());
        }
    }
}

