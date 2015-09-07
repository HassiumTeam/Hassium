using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hassium.Functions;

namespace Hassium.HassiumObjects.IO
{
    public class HassiumIO : HassiumObject
    {
        public HassiumIO()
        {
            Attributes.Add("stdin", new InternalFunction(x => new HassiumStream(Console.OpenStandardInput()), true));
            Attributes.Add("stdout", new InternalFunction(x => new HassiumStream(Console.OpenStandardOutput()), true));
            Attributes.Add("stderr", new InternalFunction(x => new HassiumStream(Console.OpenStandardError()), true));
        }
    }
}
