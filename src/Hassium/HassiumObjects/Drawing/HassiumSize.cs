using System.Drawing;

namespace Hassium.HassiumObjects.Drawing
{
    public class HassiumSize : HassiumObject
    {
        public Size Value { get; set; }

        public HassiumSize(int w, int h)
        {
            Value = new Size(w, h);

            Attributes.Add("width", new HassiumProperty("width", x => Value.Width, (self, x) =>
            {
                Value = new Size(x[0].HInt().Value, Value.Height);
                return true;
            }));

            Attributes.Add("height", new HassiumProperty("height", x => Value.Height, (self, x) =>
            {
                Value = new Size(Value.Width, x[0].HInt().Value);
                return true;
            }));
            
            Attributes.Add("isEmpty", new HassiumProperty("isEmpty", x => Value.IsEmpty, null, true));
        }
    }
}
