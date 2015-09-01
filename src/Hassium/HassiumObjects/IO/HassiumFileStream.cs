using System.IO;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumFileStream: HassiumStream
    {
        public new FileStream Value
        {
            get { return (FileStream)base.Value; }
            set { base.Value = value; }
        }

        public HassiumFileStream(FileStream value) : base(value)
        {
            Attributes.Add("readChar", new InternalFunction(ReadChar));
        }

        public HassiumObject ReadChar(HassiumObject[] args)
        {
            return ((char) Value.ReadByte()).ToString();
        }
    }
}

