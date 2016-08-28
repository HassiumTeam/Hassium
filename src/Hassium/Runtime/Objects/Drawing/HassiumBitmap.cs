using System;
using System.Drawing;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Drawing
{
    public class HassiumBitmap: HassiumObject
	{
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Bitmap");

        public Bitmap Bitmap { get; set; }

		public HassiumBitmap()
		{
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 1, 2);
		}

        public HassiumBitmap _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumBitmap bitmap = new HassiumBitmap();
            switch (args.Length)
            {
                case 1:
                    bitmap.Bitmap = new Bitmap(args[0].ToString(vm).String);
                    break;
                case 2:
                    bitmap.Bitmap = new Bitmap((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int);
                    break;
            }
            bitmap.AddAttribute("getPixel",             bitmap.getPixel,        2);
            bitmap.AddAttribute("height",               new HassiumProperty(bitmap.get_height));
            bitmap.AddAttribute("horizontalResolution", new HassiumProperty(bitmap.get_horizontalResolution));
            bitmap.AddAttribute("makeTransparent",      bitmap.makeTransparent, 1);
            bitmap.AddAttribute("save",                 bitmap.save,            1);
            bitmap.AddAttribute("setPixel",             bitmap.setPixel,        3);
            bitmap.AddAttribute("setResolution",        bitmap.setResolution,   2);
            bitmap.AddAttribute("verticalResolution",   new HassiumProperty(bitmap.get_verticalResolution));
            bitmap.AddAttribute("width",                new HassiumProperty(bitmap.get_width));
            return bitmap;
        }

        public HassiumColor getPixel(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumColor()._new(vm, new HassiumInt(Bitmap.GetPixel((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int).ToArgb()));
        }
        public HassiumInt get_height(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Bitmap.Height);
        }
        public HassiumFloat get_horizontalResolution(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(Bitmap.HorizontalResolution);
        }
        public HassiumNull makeTransparent(VirtualMachine vm, params HassiumObject[] args)
        {
            Bitmap.MakeTransparent(((HassiumColor)args[0]).Color);
            return HassiumObject.Null;
        }
        public HassiumNull save(VirtualMachine vm, params HassiumObject[] args)
        {
            Bitmap.Save(args[0].ToString(vm).String);
            return HassiumObject.Null;
        }
        public HassiumNull setPixel(VirtualMachine vm, params HassiumObject[] args)
        {
            Bitmap.SetPixel((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int, ((HassiumColor)args[2]).Color);
            return HassiumObject.Null;
        }
        public HassiumNull setResolution(VirtualMachine vm, params HassiumObject[] args)
        {
            Bitmap.SetResolution((float)args[0].ToFloat(vm).Float, (float)args[1].ToFloat(vm).Float);
            return HassiumObject.Null;
        }
        public HassiumFloat get_verticalResolution(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumFloat(Bitmap.VerticalResolution);
        }
        public HassiumInt get_width(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Bitmap.Width);
        }
	}
}