using System;
using System.Drawing;

using Hassium.Runtime.Objects.Types;

namespace Hassium.Runtime.Objects.Drawing
{
    public class HassiumColor: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Color");

        public Color Color { get; set; }

        public HassiumColor()
        {
            AddType(TypeDefinition);
            AddAttribute(HassiumObject.INVOKE, _new, 1, 3, 4);
        }

        public HassiumColor _new(VirtualMachine vm, params HassiumObject[] args)
        {
            HassiumColor color = new HassiumColor();
            switch (args.Length)
            {
                case 1:
                    if (args[0] is HassiumInt)
                        color.Color = Color.FromArgb((int)args[0].ToInt(vm).Int);
                    else
                        color.Color = Color.FromName(args[0].ToString(vm).String);
                    break;
                case 3:
                    color.Color = Color.FromArgb((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int, (int)args[2].ToInt(vm).Int);
                    break;
                case 4:
                    color.Color = Color.FromArgb((int)args[0].ToInt(vm).Int, (int)args[1].ToInt(vm).Int, (int)args[2].ToInt(vm).Int, (int)args[3].ToInt(vm).Int);
                    break;
            }
            color.AddAttribute("a",         new HassiumProperty(color.get_a));
            color.AddAttribute("b",         new HassiumProperty(color.get_b));
            color.AddAttribute("getArgb",   color.getArgb,                 0);
            color.AddAttribute("g",         new HassiumProperty(color.get_g));
            color.AddAttribute("r",         new HassiumProperty(color.get_r));
            return color;
        }

        public HassiumInt get_a(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Color.A);
        }
        public HassiumInt get_b(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Color.B);
        }
        public HassiumInt getArgb(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Color.ToArgb());
        }
        public HassiumInt get_g(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Color.G);
        }
        public HassiumInt get_r(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Color.R);
        }
    }
}