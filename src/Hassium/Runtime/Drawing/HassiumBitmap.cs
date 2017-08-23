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

            [FunctionAttribute("func getpixel (x : int, y : int) : Color")]
            public static HassiumObject getpixel(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return _new(vm, null, location, new HassiumInt(Bitmap.GetPixel((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int).ToArgb()));
            }

            [FunctionAttribute("height { get; }")]
            public static HassiumInt get_height(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return new HassiumInt(Bitmap.Height);
            }

            [FunctionAttribute("hres { get; }")]
            public static HassiumFloat get_hres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return new HassiumFloat(Bitmap.HorizontalResolution);
            }

            [FunctionAttribute("func save (path : string) : null")]
            public static HassiumNull save(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                Bitmap.Save(args[0].ToString(vm, args[0], location).String);

                return Null;
            }

            [FunctionAttribute("func setpixel (x : int, y : int, col : Color) : null")]
            public static HassiumNull setpixel(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                Bitmap.SetPixel((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (args[2] as HassiumColor).Color);

                return Null;
            }

            [FunctionAttribute("func setres (x : float, y : float) : null")]
            public static HassiumNull setres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                Bitmap.SetResolution((float)args[0].ToFloat(vm, args[0], location).Float, (float)args[1].ToFloat(vm, args[1], location).Float);

                return Null;
            }

            [FunctionAttribute("vres { get; }")]
            public static HassiumFloat get_vres(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Bitmap = (self as HassiumBitmap).Bitmap;
                return new HassiumFloat(Bitmap.VerticalResolution);
            }

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

        public override HassiumObject GetAttribute(string attrib)
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
