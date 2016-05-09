using System;

using Hassium.Runtime.StandardLibrary.Types;

namespace Hassium.Runtime.StandardLibrary.Util
{
    public class HassiumUtilModule : InternalModule
    {
        public HassiumUtilModule() : base ("Util")
        {
            Attributes.Add("eval", new HassiumFunction(eval, 1));
            Attributes.Add("StopWatch", new HassiumStopWatch());
        }

        public HassiumNull eval(VirtualMachine vm, HassiumObject[] args)
        {
            HassiumExecuter.FromString(HassiumString.Create(args[0]).Value);
            return HassiumObject.Null;
        }
    }
}

