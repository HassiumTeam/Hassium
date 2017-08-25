using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Drawing;

namespace Hassium.Runtime.Drawing
{
    public class HassiumColor : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new ColorTypeDef();

        public Color Color { get; private set; }

        public HassiumColor()
        {
            AddType(TypeDefinition);
        }

        public class ColorTypeDef : HassiumTypeDefinition
        {
            public ColorTypeDef() : base("Color")
            {
                BoundAttributes = new Dictionary<string, HassiumObject>()
                {
                    { "a", new HassiumProperty(get_a) },
                    { "argb", new HassiumProperty(get_argb) },
                    { "b", new HassiumProperty(get_b) },
                    { "g", new HassiumProperty(get_g) },
                    { INVOKE, new HassiumFunction(_new, 1, 3, 4) },
                    { "r", new HassiumProperty(get_r) }
                };
            }

            [FunctionAttribute("func new (colIntOrStr : object) : Color", "func new (r : int, g : int, b : int) : Color", "func new (a : int, r : int, g : int, b : int) : Color")]
            public static HassiumColor _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                HassiumColor color = new HassiumColor();

                switch (args.Length)
                {
                    case 1:
                        if (args[0] is HassiumInt)
                            color.Color = Color.FromArgb((int)args[0].ToInt(vm, args[0], location).Int);
                        else
                            color.Color = Color.FromName(args[0].ToString(vm, args[0], location).String);
                        break;
                    case 3:
                        color.Color = Color.FromArgb((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (int)args[2].ToInt(vm, args[2], location).Int);
                        break;
                    case 4:
                        color.Color = Color.FromArgb((int)args[0].ToInt(vm, args[0], location).Int, (int)args[1].ToInt(vm, args[1], location).Int, (int)args[2].ToInt(vm, args[2], location).Int, (int)args[3].ToInt(vm, args[3], location).Int);
                        break;
                }

                return color;
            }

            [FunctionAttribute("a { get; }")]
            public static HassiumInt get_a(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Color = (self as HassiumColor).Color;
                return new HassiumInt(Color.A);
            }

            public static HassiumInt get_argb(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Color = (self as HassiumColor).Color;
                return new HassiumInt(Color.ToArgb());
            }

            [FunctionAttribute("b { get; }")]
            public static HassiumInt get_b(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Color = (self as HassiumColor).Color;
                return new HassiumInt(Color.B);
            }

            [FunctionAttribute("g { get; }")]
            public static HassiumInt get_g(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Color = (self as HassiumColor).Color;
                return new HassiumInt(Color.G);
            }

            [FunctionAttribute("r { get; }")]
            public static HassiumInt get_r(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
            {
                var Color = (self as HassiumColor).Color;
                return new HassiumInt(Color.R);
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
