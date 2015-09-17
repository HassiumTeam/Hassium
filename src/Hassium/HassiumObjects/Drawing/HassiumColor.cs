using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;

namespace Hassium.HassiumObjects.Drawing
{
    public class HassiumColor: HassiumObject
    {
        public Color Value { get; private set; }

        public HassiumColor(HassiumObject[] args)
        {
            switch (args.Length)
            {
                case 0:
                    throw new ParseException("Not enough arguments for HassiumColor constructor", -1);
                case 1:
                    if(args[0] is HassiumString)
                    {
                        string col = args[0].ToString();
                        Value = col.StartsWith("#") ? ColorTranslator.FromHtml(col) : Color.FromName(col.ToLower());
                    }
                    break;
                case 3:
                    Value = Color.FromArgb(args[0].HInt().Value, args[1].HInt().Value, args[2].HInt().Value);
                    break;
                default:
                    if (args[0] is HassiumString)
                    {
                        string fmt = args[0].ToString().ToLower();
                        switch (fmt)
                        {
                            case "hsl":
                            {
                                double hue = args[1].HDouble().Value / 360;
                                double saturation = args[2].HDouble().Value / 100;
                                double luminosity = args[3].HDouble().Value / 100;

                                var red = luminosity;
                                var green = luminosity;
                                var blue = luminosity;
                                var v = (luminosity <= 0.5) ? (luminosity * (1.0 + saturation)) : (luminosity + saturation - luminosity * saturation);
                                if (v > 0)
                                {
                                    var m = luminosity + luminosity - v;
                                    var sv = (v - m) / v;
                                    hue *= 6.0;
                                    var sextant = (int)hue;
                                    var fract = hue - sextant;
                                    var vsf = v * sv * fract;
                                    var mid1 = m + vsf;
                                    var mid2 = v - vsf;
                                    switch (sextant)
                                    {
                                        case 0:
                                            red = v;
                                            green = mid1;
                                            blue = m;
                                            break;
                                        case 1:
                                            red = mid2;
                                            green = v;
                                            blue = m;
                                            break;
                                        case 2:
                                            red = m;
                                            green = v;
                                            blue = mid1;
                                            break;
                                        case 3:
                                            red = m;
                                            green = mid2;
                                            blue = v;
                                            break;
                                        case 4:
                                            red = mid1;
                                            green = m;
                                            blue = v;
                                            break;
                                        case 5:
                                            red = v;
                                            green = m;
                                            blue = mid2;
                                            break;
                                    }
                                }
                                Value = Color.FromArgb(Convert.ToByte(red * 255.0f), Convert.ToByte(green * 255.0f),
                                    Convert.ToByte(blue * 255.0f));
                            }
                                break;
                            case "hsv":
                            {
                                double hue = args[1].HDouble().Value;
                                double saturation = args[2].HDouble().Value / 100;
                                double value = args[3].HDouble().Value / 100;

                                int hi = Convert.ToInt32(System.Math.Floor(hue / 60)) % 6;
                                double f = hue / 60 - System.Math.Floor(hue / 60);

                                value = value * 255;
                                int v = Convert.ToInt32(value);
                                int p = Convert.ToInt32(value * (1 - saturation));
                                int q = Convert.ToInt32(value * (1 - f * saturation));
                                int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

                                switch (hi)
                                {
                                    case 0:
                                        Value = Color.FromArgb(255, v, t, p);
                                        break;
                                    case 1:
                                        Value = Color.FromArgb(255, q, v, p);
                                        break;
                                    case 2:
                                        Value = Color.FromArgb(255, p, v, t);
                                        break;
                                    case 3:
                                        Value = Color.FromArgb(255, p, q, v);
                                        break;
                                    case 4:
                                        Value = Color.FromArgb(255, t, p, v);
                                        break;
                                    default:
                                        Value = Color.FromArgb(255, v, p, q);
                                        break;
                                }
                            }
                                break;
                            case "rgbp":
                                Value = args.Length == 5
                                    ? Color.FromArgb(Convert.ToInt32(args[1].HDouble().Value * 255),
                                        Convert.ToInt32(args[2].HDouble().Value * 255),
                                        Convert.ToInt32(args[3].HDouble().Value * 255),
                                        Convert.ToInt32(args[4].HDouble().Value * 255))
                                    : Color.FromArgb(Convert.ToInt32(args[1].HDouble().Value * 255),
                                        Convert.ToInt32(args[2].HDouble().Value * 255),
                                        Convert.ToInt32(args[3].HDouble().Value * 255));
                                break;
                            case "cmyk":
                                double cyan = args[1].HDouble().Value / 100;
                                double magenta = args[2].HDouble().Value / 100;
                                double yellow = args[3].HDouble().Value / 100;
                                double black = args[3].HDouble().Value / 100;

                                int _red = Convert.ToInt32((1 - cyan) * (1 - black) * 255.0);
                                int _green = Convert.ToInt32((1 - magenta) * (1 - black) * 255.0);
                                int _blue = Convert.ToInt32((1 - yellow) * (1 - black) * 255.0);

                                Value = Color.FromArgb(_red, _green, _blue);
                                break;
                        }
                    }
                    else
                    {
                        Value = Color.FromArgb(args[0].HInt().Value, args[1].HInt().Value, args[2].HInt().Value, args[3].HInt().Value);
                    }
                    break;
            }

            Attributes.Add("alpha", new HassiumProperty("alpha", x => Value.A, x =>
            {
                Value = Color.FromArgb(x[0].HInt().Value, Value.R, Value.G, Value.B);
                return null;
            }));
            Attributes.Add("red", new HassiumProperty("red", x => Value.R, x =>
            {
                Value = Color.FromArgb(Value.A, x[0].HInt().Value, Value.G, Value.B);
                return null;
            }));
            Attributes.Add("green", new HassiumProperty("green", x => Value.G, x =>
            {
                Value = Color.FromArgb(Value.A, Value.R, x[0].HInt().Value, Value.B);
                return null;
            }));
            Attributes.Add("blue", new HassiumProperty("blue", x => Value.B, x =>
            {
                Value = Color.FromArgb(Value.A, Value.R, Value.G, x[0].HInt().Value);
                return null;
            }));
            Attributes.Add("argb", new HassiumProperty("argb", x => Value.ToArgb(), x =>
            {
                Value = Color.FromArgb(x[0].HInt().Value);
                return null;
            }));

            Attributes.Add("toHsl", new InternalFunction(ToHSL, 0));
            Attributes.Add("toHsv", new InternalFunction(ToHSV, 0));
            Attributes.Add("toCmyk", new InternalFunction(ToCMYK, 0));
        }

        public HassiumObject ToHSL(HassiumObject[] args)
        {
            var red = Value.R / 255.0;
            var green = Value.G / 255.0;
            var blue = Value.B / 255.0;   
            var v = System.Math.Max(System.Math.Max(red, green), blue);
            var m = System.Math.Min(System.Math.Min(red, green), blue);
            var hue = 0.0;
            var saturation = 0.0;
            var luminosity = (m + v) / 2.0;
            if (luminosity > 0.0)
            {
                var vm = v - m;
                saturation = vm;
                if (saturation > 0.0)
                {
                    saturation /= (luminosity <= 0.5) ? (v + m) : (2.0 - v - m);
                    var r2 = (v - red) / vm;
                    var g2 = (v - green) / vm;
                    var b2 = (v - blue) / vm;
                    if (red == v)
                    {
                        hue = (green == m ? 5.0 + b2 : 1.0 - g2);
                    }
                    else if (green == v)
                    {
                        hue = (blue == m ? 1.0 + r2 : 3.0 - b2);
                    }
                    else
                    {
                        hue = (red == m ? 3.0 + g2 : 5.0 - r2);
                    }
                    hue /= 6.0;
                }
            }

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"h", hue * 360},
                    {"s", saturation * 100},
                    {"l", luminosity * 100}
                });
        }

        public HassiumObject ToHSV(HassiumObject[] args)
        {
            int max = System.Math.Max(Value.R, System.Math.Max(Value.G, Value.B));
            int min = System.Math.Min(Value.R, System.Math.Min(Value.G, Value.B));

            double hue = Value.GetHue();
            double saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            double value = max / 255d;

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"h", hue},
                    {"s", saturation * 100},
                    {"v", value * 100}
                });
        }

        public HassiumObject ToCMYK(HassiumObject[] args)
        {
            double red = Value.R / 255d;
            double green = Value.G / 255d;
            double blue = Value.B / 255d;

            double black = new[] {1 - red, 1 - green, 1 - blue}.Min();
            double cyan = (1 - red - black) / (1 - black);
            double magenta = (1 - green - black) / (1 - black);
            double yellow = (1 - blue - black) / (1 - black);

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"c", cyan},
                    {"m", magenta},
                    {"y", yellow},
                    {"k", black}
                });
        }
    }
}
