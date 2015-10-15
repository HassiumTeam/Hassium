using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Drawing
{
    public class HassiumGradient : HassiumObject
    {
        public HassiumColor Lower { get; set; }

        public HassiumColor Upper { get; set; }

        public List<HassiumColor> Content { get; private set; } 

        public string GradientType { get; set; }

        public HassiumGradient(IList<HassiumObject> args) : this()
        {
            if(args.Count == 2)
            {
                var type = args[0].ToString();
                var size = args[1].HInt().Value;

                GradientType = type;

                if(type == "hsl")
                {
                    for(int i = 0; i < size; i++)
                    {
                        double hue = i * (360.0 / size);
                        Content.Add(new HassiumColor("hsl", hue, 0.5, 0.5));
                    }
                }
                else if (type == "hsv")
                {
                    for (int i = 0; i < size; i++)
                    {
                        double hue = i * (360.0 / size);
                        Content.Add(new HassiumColor("hsv", hue, 0.5, 0.5));
                    }
                }
            }
            else if(args.Count == 3)
            {
                Lower = (HassiumColor) args[1];
                Upper = (HassiumColor) args[2];

                Content = createGradient(args[0].HInt(), Lower.Value, Upper.Value).Select(x => new HassiumColor(x)).ToList();
            }
            else if(args.Count > 2)
            {
                GradientType = "multiple";

                Content = createGradient(args[0].HInt(), args.Skip(1).Select(x => ((HassiumColor) x).Value).ToArray()).Select(x => new HassiumColor(x)).ToList();
            }
        }

        private List<Color> createGradient(int size, params Color[] colors)
        {
            List<Color> palette = new List<Color>(size);

            int colorSpan = colors.Length - 1;

            if (colors.Length > 0)
            {
                int lastPadding = size % colorSpan;

                int stepSize = size / colorSpan;

                for (int index = 0; index < colorSpan; index++)
                {
                    palette.AddRange
                    (
                        createGradient
                        (
                            index == colorSpan - 1 ?
                                stepSize + lastPadding : stepSize,
                            colors[index],
                            colors[index + 1]
                        )
                    );
                }
            }

            return palette;
        } 

        public HassiumGradient()
        {
            Lower = new HassiumColor(Color.White);
            Upper = new HassiumColor(Color.White);
            Content = new List<HassiumColor>();
            GradientType = "basic";

            Attributes.Add("lower", new HassiumProperty("lower", x => Lower, null, true));
            Attributes.Add("upper", new HassiumProperty("upper", x => Upper, null, true));
            Attributes.Add("colors", new HassiumProperty("colors", x => new HassiumArray(Content), null, true));
        }
    }
}
