using System;
using System.IO;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.IO
{
    public class HassiumStream: HassiumObject
    {
        public Stream Stream { get; private set; }
        public HassiumStream(Stream stream)
        {
            Stream = stream;
        }
    }
}
