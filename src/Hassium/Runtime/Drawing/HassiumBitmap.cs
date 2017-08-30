using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Drawing;

namespace Hassium.Runtime.Drawing
{
    public class HassiumBitmap : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new BitmapTypeDef();

        public Bitmap Bitmap { get; private set; }

        public HassiumBitmap()
        {
            AddType(TypeDefinition);
        }

        public class BitmapTypeDef : HassiumTypeDefinition
        {
            public BitmapTypeDef() : base("Bitmap")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "getpixel", new HassiumFunction(getpixel, 2)  },
                    { "height", new HassiumProperty(get_height)  },
                    { "hres", new HassiumProperty(get_hres)  },
                    { INVOKE, new HassiumFunction(_new, 1, 2) },
                    { "save", new HassiumFunction(save, 1)  },
                    { "setpixel", new HassiumFunction(setpixel, 3)  },
                    { "setres", new HassiumFunction(setres, 2)  },
                    { "vres", new HassiumProperty(get_vres)  },
                    { "width", new HassiumProperty(get_width)  }
                };
            }

            [DocStr(
                "@desc Constructs a new Bitmap with either the specified string name or the specified height and width.",
                "@optional path The file path on disc to the bitmap.",
                "@optional height The height in pixels for the new bitmap.",
                "@optional width The width in pixels for the new bitmap.",
                "@returns The new Bitmap object."
                )]
            [FunctionAttribute("func new (path : string) : Bitmap", "func new (height : int, width : int) : Bitmap")]
            public static HassiumBitmap _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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

                return bitmap;
            }

            [DocStr(
                "@desc Returns a new Drawing.Color object with the value of the pixel at the specified x and y coordinates.",
                "@param x The x coordinate.",
                "@param y The y coordinate.",
                "@returns The color at (x, y)."
                )]
            [FunctionAttribute("func getpixel (x : int, y : int) : Color")]
            public static HassiumObject getpixel(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return HassiumColor.ColorTypeDef._new(vm, null, location, new HassiumInt(Bitmap.GetPixel((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int).ToArgb()));
            }

            [DocStr(
                "@desc Gets the readonly height of the Bitmap in pixels.",
                "@returns Height as int."
                )]
            [FunctionAttribute("height { get; }")]
            public static HassiumInt get_height(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return new HassiumInt(Bitmap.Height);
            }

            [DocStr(
                "@desc Gets the readonly horizontal resolution of the Bitmap.",
                "@returns Horizontal resolution as int."
                )]
            [FunctionAttribute("hres { get; }")]
            public static HassiumFloat get_hres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return new HassiumFloat(Bitmap.HorizontalResolution);
            }

            [DocStr(
                "@desc Saves this Bitmap to the specified path on disc.",
                "@param path The path to save the bitmap to.",
                "@returns null."
                )]
            [FunctionAttribute("func save (path : string) : null")]
            public static HassiumNull save(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                Bitmap.Save(args[0].ToString(vm, args[0], location).String);

                return Null;
            }

            [DocStr(
                "@desc Sets the value of the pixel at the specified x and y coorinates to the given Drawing.Color object.",
                "@param x The x coordinate.",
                "@param y The y coordinate.",
                "@param col The Drawing.Color",
                "@returns null."
                )]
            [FunctionAttribute("func setpixel (x : int, y : int, col : Color) : null")]
            public static HassiumNull setpixel(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                Bitmap.SetPixel((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (args[2] as HassiumColor).Color);

                return Null;
            }

            [DocStr(
                "@desc Sets the resolution to the given horitontal and vertical resolutions.",
                "@param x The horizontal resolution.",
                "@param y The vertical resolution.",
                "@returns null."
                )]
            [FunctionAttribute("func setres (x : float, y : float) : null")]
            public static HassiumNull setres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                Bitmap.SetResolution((float)args[0].ToFloat(vm, args[0], location).Float, (float)args[1].ToFloat(vm, args[1], location).Float);

                return Null;
            }

            [DocStr(
                "@desc Gets the readonly vertical resolution of the Bitmap.",
                "@returns Vertical resolution as int."
                )]
            [FunctionAttribute("vres { get; }")]
            public static HassiumFloat get_vres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return new HassiumFloat(Bitmap.VerticalResolution);
            }

            [DocStr(
                "@desc Gets the readonly width of the Bitmap in pixels.",
                "@returns Width as int."
                )]
            [FunctionAttribute("width { get; }")]
            public static HassiumInt get_width(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return new HassiumInt(Bitmap.Width);
            }
        }

        public override bool ContainsAttribute(string attrib)
        {
            return BoundAttributes.ContainsKey(attrib) || TypeDefinition.BoundAttributes.ContainsKey(attrib);
        }

        public override HassiumObject GetAttribute(VirtualMachine vm, string attrib)
        {
            if (BoundAttributes.ContainsKey(attrib))
                return BoundAttributes[attrib];
            else
                return (TypeDefinition.BoundAttributes[attrib].Clone() as HassiumObject).SetSelfReference(this);
        }

        public override Dictionary<string, HassiumObject> GetAttributes()
        {
            foreach (var pair in TypeDefinition.BoundAttributes)
                if (!BoundAttributes.ContainsKey(pair.Key))
                    BoundAttributes.Add(pair.Key, (pair.Value.Clone() as HassiumObject).SetSelfReference(this));
            return BoundAttributes;
        }
    }
}
