using Hassium.Compiler;
using Hassium.Runtime.Types;

using System.Drawing;

namespace Hassium.Runtime.Drawing
{
    public class HassiumColor : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Color");

        public Color Color { get; private set; }

        public HassiumColor()
        {
            AddType(TypeDefinition);
            AddAttribute(INVOKE, _new, 1, 3, 4);
            ImportAttribs(this);
        }

        public static void ImportAttribs(HassiumColor color)
        {
            color.AddAttribute("a", new HassiumProperty(color.get_a));
            color.AddAttribute("argb", new HassiumProperty(color.get_argb));
            color.AddAttribute("b", new HassiumProperty(color.get_b));
            color.AddAttribute("g", new HassiumProperty(color.get_g));
            color.AddAttribute("r", new HassiumProperty(color.get_r));
        }

        [FunctionAttribute("func new (colIntOrStr : object) : Color", "func new (r : int, g : int, b : int) : Color", "func new (a : int, r : int, g : int, b : int) : Color")]
        public HassiumColor _new(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            HassiumColor color = new HassiumColor();

            switch (args.Length)
            {
                case 1:
                    if (args[0] is HassiumInt)
                        color.Color = Color.FromArgb((int)args[0].ToInt(vm, location).Int);
                    else
                        color.Color = Color.FromName(args[0].ToString(vm, location).String);
                    break;
                case 3:
                    color.Color = Color.FromArgb((int)args[0].ToInt(vm, location).Int, (int)args[1].ToInt(vm, location).Int, (int)args[2].ToInt(vm, location).Int);
                    break;
                case 4:
                    color.Color = Color.FromArgb((int)args[0].ToInt(vm, location).Int, (int)args[1].ToInt(vm, location).Int, (int)args[2].ToInt(vm, location).Int, (int)args[3].ToInt(vm, location).Int);
                    break;
            }
            ImportAttribs(color);

            return color;
        }

        [FunctionAttribute("a { get; }")]
        public HassiumInt get_a(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.A);
        }

        public HassiumInt get_argb(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.ToArgb());
        }

        [FunctionAttribute("b { get; }")]
        public HassiumInt get_b(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.B);
        }

        [FunctionAttribute("g { get; }")]
        public HassiumInt get_g(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.G);
        }

        [FunctionAttribute("r { get; }")]
        public HassiumInt get_r(VirtualMachine vm, SourceLocation location, params HassiumObject[] args)
        {
            return new HassiumInt(Color.R);
        }
    }
}
