using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Collections.Generic;
using System.Drawing;

namespace Hassium.Runtime.Drawing
{
    public class HassiumColor : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Color");

        public static Dictionary<string, HassiumObject> Attribs = new Dictionary<string, HassiumObject>()
        {

        };

        public Color Color { get; private set; }

        public HassiumColor()
        {
            AddType(TypeDefinition);
        }

        [FunctionAttribute("func new (colIntOrStr : object) : Color", "func new (r : int, g : int, b : int) : Color", "func new (a : int, r : int, g : int, b : int) : Color")]
        public HassiumColor _new(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
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
        public HassiumInt get_a(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.A);
        }

        public HassiumInt get_argb(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.ToArgb());
        }

        [FunctionAttribute("b { get; }")]
        public HassiumInt get_b(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.B);
        }

        [FunctionAttribute("g { get; }")]
        public HassiumInt get_g(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.G);
        }

        [FunctionAttribute("r { get; }")]
        public HassiumInt get_r(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.R);
        }
    }
}
