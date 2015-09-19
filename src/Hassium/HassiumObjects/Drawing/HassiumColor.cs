using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Hassium.Functions;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;

namespace Hassium.HassiumObjects.Drawing
{
    public class HassiumColor : HassiumObject
    {
        public Color Value { get; private set; }

        public HassiumColor(IList<HassiumObject> args)
        {
            switch (args.Count)
            {
                case 0:
                    throw new ParseException("Not enough arguments for HassiumColor constructor", -1);
                case 1:
                    if (args[0] is HassiumString)
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
                                var v = (luminosity <= 0.5)
                                    ? (luminosity * (1.0 + saturation))
                                    : (luminosity + saturation - luminosity * saturation);
                                if (v > 0)
                                {
                                    var m = luminosity + luminosity - v;
                                    var sv = (v - m) / v;
                                    hue *= 6.0;
                                    var sextant = (int) hue;
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
                            case "hsb":
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
                            case "rgbpercent":
                                Value = args.Count == 5
                                    ? Color.FromArgb(Convert.ToInt32(args[1].HDouble().Value * 255),
                                        Convert.ToInt32(args[2].HDouble().Value * 255),
                                        Convert.ToInt32(args[3].HDouble().Value * 255),
                                        Convert.ToInt32(args[4].HDouble().Value * 255))
                                    : Color.FromArgb(Convert.ToInt32(args[1].HDouble().Value * 255),
                                        Convert.ToInt32(args[2].HDouble().Value * 255),
                                        Convert.ToInt32(args[3].HDouble().Value * 255));
                                break;
                            case "cmyk":
                            {
                                double cyan = args[1].HDouble().Value / 100;
                                double magenta = args[2].HDouble().Value / 100;
                                double yellow = args[3].HDouble().Value / 100;
                                double black = args[4].HDouble().Value / 100;

                                Value =
                                    new HassiumColor(new HassiumObject[]
                                    {
                                        "cmy",
                                        (cyan * (1 - black) + black) * 100,
                                        (magenta * (1 - black) + black) * 100,
                                        (yellow * (1 - black) + black) * 100
                                    }).Value;
                                break;
                            }
                            case "cmy":
                            {
                                double cyan = args[1].HDouble().Value / 100;
                                double magenta = args[2].HDouble().Value / 100;
                                double yellow = args[3].HDouble().Value / 100;

                                Value = Color.FromArgb(Convert.ToInt32((1 - cyan) * 255.0),
                                    Convert.ToInt32((1 - magenta) * 255.0), Convert.ToInt32((1 - yellow) * 255.0));
                                break;
                            }
                            case "xyz":
                            {
                                double x = args[1].HDouble().Value / 100;
                                double y = args[2].HDouble().Value / 100;
                                double z = args[3].HDouble().Value / 100;

                                var r = x * 3.2406 + y * -1.5372 + z * -0.4986;
                                var g = x * -0.9689 + y * 1.8758 + z * 0.0415;
                                var b = x * 0.0557 + y * -0.2040 + z * 1.0570;

                                r = r > 0.0031308 ? 1.055 * System.Math.Pow(r, 1 / 2.4) - 0.055 : 12.92 * r;
                                g = g > 0.0031308 ? 1.055 * System.Math.Pow(g, 1 / 2.4) - 0.055 : 12.92 * g;
                                b = b > 0.0031308 ? 1.055 * System.Math.Pow(b, 1 / 2.4) - 0.055 : 12.92 * b;

                                Value = Color.FromArgb(Convert.ToInt32(r * 255), Convert.ToInt32(g * 255),
                                    Convert.ToInt32(b * 255));
                                break;
                            }
                            case "lab":
                            case "cie-lab":
                            {
                                var _l = args[1].HDouble().Value;
                                var _a = args[2].HDouble().Value;
                                var _b = args[3].HDouble().Value;

                                double _y = (_l + 16.0) / 116.0;
                                double _x = _a / 500.0 + _y;
                                double _z = _y - _b / 200.0;

                                double _x3 = _x * _x * _x;
                                double _z3 = _z * _z * _z;


                                Value =
                                    new HassiumColor(new HassiumObject[]
                                    {
                                        "xyz",
                                        95.047 * (_x3 > 0.00885645168 ? _x3 : (_x - 16.0 / 116.0) / 7.787),
                                        100.000 *
                                        (_l > (903.296296 * 0.00885645168)
                                            ? System.Math.Pow(((_l + 16.0) / 116.0), 3)
                                            : _l / 903.296296),
                                        108.883 * (_z3 > 0.00885645168 ? _z3 : (_z - 16.0 / 116.0) / 7.787)
                                    }).Value;
                                break;
                            }
                            case "lch":
                            case "cie-lch":
                            {
                                var _l = args[1].HDouble().Value;
                                var _c = args[2].HDouble().Value;
                                var _h = args[3].HDouble().Value;
                                var hrad = _h * System.Math.PI / 180.0;

                                Value = new HassiumColor(new HassiumObject[]
                                {
                                    "lab",
                                    Convert.ToInt32(_l),
                                    Convert.ToInt32(System.Math.Cos(hrad) * _c),
                                    Convert.ToInt32(System.Math.Sin(hrad) * _c)
                                }).Value;
                                break;
                            }
                            case "luv":
                            case "cie-luv":
                            {
                                var _l = args[1].HDouble().Value;
                                var _u = args[2].HDouble().Value;
                                var _v = args[3].HDouble().Value;

                                var uPrime = (4.0 * 95.047) / 1921.696;
                                var vPrime = (9.0 * 100.000) / 1921.696;
                                var a = (1.0 / 3.0) * ((52.0 * _l) / (_u + 13 * _l * uPrime) - 1.0);
                                var imteL_16_116 = (_l + 16.0) / 116.0;
                                var y = _l > 903.296296 * 0.00885645168
                                    ? imteL_16_116 * imteL_16_116 * imteL_16_116
                                    : _l / 903.296296;
                                var b = -5.0 * y;
                                var d = y * ((39.0 * _l) / (_v + 13.0 * _l * vPrime) - 5.0);
                                var x = (d - b) / (a - (-1.0 / 3.0));
                                var z = x * a + b;

                                Value =
                                    new HassiumColor(new HassiumObject[]
                                    {
                                        "xyz",
                                        100 * x,
                                        100 * y,
                                        100 * z
                                    }).Value;
                                break;
                            }
                            case "hunterlab":
                            case "hunter-lab":
                            case "hlab":
                            case "htlab":
                            {
                                var _l = args[1].HDouble().Value;
                                var _a = args[2].HDouble().Value;
                                var _b = args[3].HDouble().Value;

                                var x = (_a / 17.5) * (_l / 10.0);
                                var itemL_10 = _l / 10.0;
                                var y = itemL_10 * itemL_10;
                                var z = _b / 7.0 * _l / 10.0;

                                Value =
                                    new HassiumColor(new HassiumObject[]
                                    {
                                        "xyz",
                                        (x + y) / 1.02,
                                        y,
                                        -(z - y) / 0.847
                                    }).Value;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Value = Color.FromArgb(args[0].HInt().Value, args[1].HInt().Value, args[2].HInt().Value,
                            args[3].HInt().Value);
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
            Attributes.Add("toCmy", new InternalFunction(ToCMY, 0));
            Attributes.Add("toXyz", new InternalFunction(ToXYZ, 0));
            Attributes.Add("toLab", new InternalFunction(ToLAB, 0));
            Attributes.Add("toLch", new InternalFunction(ToLCH, 0));
            Attributes.Add("toLuv", new InternalFunction(ToLUV, 0));
            Attributes.Add("toHunterLab", new InternalFunction(ToHunterLab, 0));
            //Attributes.Add("toYuv", new InternalFunction(ToYUV, 0));
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
                    {"c", 100 * cyan},
                    {"m", 100 * magenta},
                    {"y", 100 * yellow},
                    {"k", 100 * black}
                });
        }

        public HassiumObject ToCMY(HassiumObject[] args)
        {
            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"c", (1 - Value.R / 255.0) * 100.0},
                    {"m", (1 - Value.G / 255.0) * 100.0},
                    {"y", (1 - Value.B / 255.0) * 100.0}
                });
        }

        public HassiumObject ToXYZ(HassiumObject[] args)
        {
            double red = Value.R / 255d;
            double green = Value.G / 255d;
            double blue = Value.B / 255d;

            double r = (red > 0.04045) ? System.Math.Pow((red + 0.055) / 1.055, 2.4) : (red / 12.92);
            double g = (green > 0.04045) ? System.Math.Pow((green + 0.055) / 1.055, 2.4) : (green / 12.92);
            double b = (blue > 0.04045) ? System.Math.Pow((blue + 0.055) / 1.055, 2.4) : (blue / 12.92);

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"x", (r * 0.4124 + g * 0.3576 + b * 0.1805) * 100.0},
                    {"y", (r * 0.2126 + g * 0.7152 + b * 0.0722) * 100.0},
                    {"z", (r * 0.0193 + g * 0.1192 + b * 0.9505) * 100.0}
                });
        }

        public HassiumObject ToLAB(HassiumObject[] args)
        {
            double[] xyz = ToXYZ(args).HDict().Value.Select(k => k.Value.HDouble().Value).ToArray();
            Func<double, double> pivot =
                n => n > 0.00885645168 ? System.Math.Pow(n, 1.0 / 3.0) : (903.296296 * n + 16) / 116;
            double x = pivot(xyz[0] / 95.047);
            double y = pivot(xyz[1] / 100.000);
            double z = pivot(xyz[2] / 108.883);

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"l", System.Math.Max(0, 116 * y - 16)},
                    {"a", 500 * (x - y)},
                    {"b", 200 * (y - z)}
                });
        }

        public HassiumObject ToLCH(HassiumObject[] args)
        {
            double[] lab = ToLAB(args).HDict().Value.Select(k => k.Value.HDouble().Value).ToArray();
            double h = System.Math.Atan2(lab[2], lab[1]);

            if (h > 0)
            {
                h = (h / System.Math.PI) * 180.0;
            }
            else
            {
                h = 360 - (System.Math.Abs(h) / System.Math.PI) * 180.0;
            }

            if (h < 0)
            {
                h += 360.0;
            }
            else if (h >= 360)
            {
                h -= 360.0;
            }

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"l", lab[0]},
                    {"c", System.Math.Sqrt(lab[1] * lab[1] + lab[2] * lab[2])},
                    {"h", h}
                });
        }

        public HassiumObject ToLUV(HassiumObject[] args)
        {
            double[] xyz = ToXYZ(args).HDict().Value.Select(k => k.Value.HDouble().Value).ToArray();

            var y = xyz[1] / 100.000;
            var L = y > 0.00885645168 ? 116.0 * System.Math.Pow(y, 1.0 / 3.0) - 16.0 : 903.296296 * y;
            var target = xyz[0] + 15.0 * xyz[1] + 3.0 * xyz[2];
            var reference = 1921.696;

            var xtarget = target == 0 ? 0 : ((4.0 * xyz[0] / target) - (4.0 * 95.047 / reference));
            var ytarget = target == 0 ? 0 : ((9.0 * xyz[1] / target) - (9.0 * 100.000 / reference));

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"l", L},
                    {"u", 13.0 * L * xtarget},
                    {"v", 13.0 * L * ytarget}
                });
        }

        public HassiumObject ToHunterLab(HassiumObject[] args)
        {
            double[] xyz = ToXYZ(args).HDict().Value.Select(k => k.Value.HDouble().Value).ToArray();


            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"l", 10.0 * System.Math.Sqrt(xyz[1])},
                    {"a", xyz[1] != 0 ? 17.5 * (((1.02 * xyz[0]) - xyz[1]) / System.Math.Sqrt(xyz[1])) : 0},
                    {"b", xyz[1] != 0 ? 7.0 * ((xyz[1] - (0.847 * xyz[2])) / System.Math.Sqrt(xyz[1])) : 0}
                });
        }

        /*public HassiumObject ToYUV(HassiumObject[] args)
        {
            var R = Value.R / 255.0;
            var G = Value.G / 255.0;
            var B = Value.B / 255.0;

            var Y = 0.299 * R + 0.587 * G + 0.114 * B;
            var U = -0.1471376975169300226 * R - 0.2888623024830699774 * G + 0.436 * B;
            var V = 0.615 * R - 0.5149857346647646220 * G - 0.1000142653352353780 * B;

            return
                new HassiumDictionary(new Dictionary<HassiumObject, HassiumObject>
                {
                    {"y", Y * 255},
                    {"u", U * 255},
                    {"v", V * 255}
                });
        }*/
    }
}