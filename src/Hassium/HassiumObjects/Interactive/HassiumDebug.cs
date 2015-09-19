using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            Attributes.Add("localVariables", new HassiumProperty("localVariables", x => localVariables(new HassiumObject[] {}), null, true));
            Attributes.Add("globalVariables", new HassiumProperty("globalVariables", x => globalVariables(new HassiumObject[] { }), null, true));
            Attributes.Add("fileName", new HassiumProperty("fileName", x => HassiumInterpreter.options.FilePath, null, true));
            Attributes.Add("secureMode", new HassiumProperty("secureMode", x => HassiumInterpreter.options.Secure, null, true));
            Attributes.Add("sourceCode", new HassiumProperty("sourceCode", x => HassiumInterpreter.options.Code, null, true));
        }

        private HassiumObject localVariables(HassiumObject[] args)
        {
            return new HassiumDictionary(HassiumInterpreter.CurrentInterpreter.CallStack.SelectMany(x => x.Locals).ToDictionary(x => new HassiumString(x.Key), x => x.Value));
        }

        private HassiumObject globalVariables(HassiumObject[] args)
        {
            var res = HassiumInterpreter.CurrentInterpreter.Globals;
            HassiumInterpreter.CurrentInterpreter.Constants.All(x =>
            {
                res.Add(x.Key, x.Value);
                return true;
            });
            return new HassiumDictionary(res.ToDictionary(x => new HassiumString(x.Key), x => x.Value));
        }
    }
}

