using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Drawing;

namespace Hassium.Runtime.Drawing
{
    public class HassiumBitmap : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Bitmap");

        public Bitmap Bitmap { get; private set; }

        public HassiumBitmap()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 1, 2);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumBitmap bitmap)
        {
            bitmap.AddAttribute("getpixel", bitmap.getpixel, 2);
            bitmap.AddAttribute("height", new HassiumProperty(bitmap.get_height));
            bitmap.AddAttribute("hres", new HassiumProperty(bitmap.get_hres));
            bitmap.AddAttribute("save", bitmap.save, 1);
            bitmap.AddAttribute("setpixel", bitmap.setpixel, 3);
            bitmap.AddAttribute("setres", bitmap.setres, 2);
            bitmap.AddAttribute("vres", new HassiumProperty(bitmap.get_vres));
            bitmap.AddAttribute("width", new HassiumProperty(bitmap.get_width));
        }

        [FunctionAttribute("func new (path : string) : Bitmap", "func new (height : int, width : int) : Bitmap")]
        public HassiumBitmap _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            HassiumBitmap bitmap = new HassiumBitmap();

            switch (args.Length)
            {
                case 1:
                    bitmap.Bitmap = Bitmap.FromFile(args[0].ToString(vm, args[0], location).String) as Bitmap;
                    break;
                case 2:
                    bitmap.Bitmap = new Bitmap((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int);
                    break;
            }
            ImportAttribs(bitmap);

            return bitmap;
        }
        
        [FunctionAttribute("func getpixel (x : int, y : int) : Color")]
        public HassiumObject getpixel(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return HassiumColor.Attribs[INVOKE].Invoke(vm, location, new HassiumInt(Bitmap.GetPixel((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int).ToArgb()));
        }

        [FunctionAttribute("height { get; }")]
        public HassiumInt get_height(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Bitmap.Height);
        }

        [FunctionAttribute("hres { get; }")]
        public HassiumFloat get_hres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Bitmap.HorizontalResolution);
        }

        [FunctionAttribute("func save (path : string) : null")]
        public HassiumNull save(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Bitmap.Save(args[0].ToString(vm, args[0], location).String);

            return Null;
        }

        [FunctionAttribute("func setpixel (x : int, y : int, col : Color) : null")]
        public HassiumNull setpixel(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Bitmap.SetPixel((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (args[2] as HassiumColor).Color);

            return Null;
        }

        [FunctionAttribute("func setres (x : float, y : float) : null")]
        public HassiumNull setres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Bitmap.SetResolution((float)args[0].ToFloat(vm, args[0], location).Float, (float)args[1].ToFloat(vm, args[1], location).Float);

            return Null;
        }

        [FunctionAttribute("vres { get; }")]
        public HassiumFloat get_vres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumFloat(Bitmap.VerticalResolution);
        }

        [FunctionAttribute("width { get; }")]
        public HassiumInt get_width(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Bitmap.Width);
        }
    }
}
