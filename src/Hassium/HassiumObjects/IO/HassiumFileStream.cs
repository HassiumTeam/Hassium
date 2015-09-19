using System.IO;
using Hassium.Functions;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumFileStream : HassiumStream
    {
        public new FileStream Value
        {
            get { return (FileStream) base.Value; }
            set { base.Value = value; }
        }

        public HassiumFileStream(Stream value) : base(value)
        {
            Attributes.Add("readChar", new InternalFunction(ReadChar, 0));
        }

        public HassiumObject ReadChar(HassiumObject[] args)
        {
            return ((char) Value.ReadByte()).ToString();
        }
    }
}