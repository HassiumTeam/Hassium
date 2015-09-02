using System;
using System.IO;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium
{
    public class HassiumIOStream: HassiumObject
    {
        public Stream Value { get; private set; }

        public HassiumIOStream(Stream value)
        {
            this.Value = value;
        }
    }
}

