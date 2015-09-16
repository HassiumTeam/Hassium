using System;
using System.Collections.Generic;
using Hassium;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Debug
{
    public class HassiumDebug: HassiumObject
    {
        public HassiumDebug()
        {
        }

        private HassiumObject localVariables(HassiumObject[] args)
        {
            Dictionary<HassiumString, HassiumString> res = new Dictionary<HassiumString, HassiumString>();
            foreach (KeyValuePair<string, HassiumObject> entry in HassiumInterpreter.CurrentInterpreter.CallStack.Peek().Locals)
            {
                try
                {
                    res.Add(new HassiumString(entry.Key), new HassiumString(entry.Value.ToString()));
                }
                catch
                {
                    res.Add(new HassiumString(entry.Key), new HassiumString("Non-convertable format"));
                }
            }

            return new HassiumDictionary(res);
        }

        private HassiumObject globalVariables(HassiumObject[] args)
        {
            Dictionary<HassiumString, HassiumString> res = new Dictionary<HassiumString, HassiumString>();
            foreach (KeyValuePair<string, HassiumObject> entry in HassiumInterpreter.CurrentInterpreter.Globals)
            {
                try
                {
                    res.Add(new HassiumString(entry.Key), new HassiumString(entry.Value.ToString()));
                }
                catch
                {
                    res.Add(new HassiumString(entry.Key), new HassiumString("Non-convertable format"));
                }
            }

            return new HassiumDictionary(res);
        }
    }
}

