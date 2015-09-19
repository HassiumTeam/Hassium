namespace Hassium.HassiumObjects.Types
{
    public class HassiumByte : HassiumObject
    {
        public byte Value { get; private set; }

        public HassiumByte(byte value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString("X2");
        }
    }
}