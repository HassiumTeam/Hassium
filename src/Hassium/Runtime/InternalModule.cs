using System.Collections.Generic;

using Hassium.Runtime.Drawing;
using Hassium.Runtime.IO;
using Hassium.Runtime.Math;
using Hassium.Runtime.Net;
using Hassium.Runtime.Text;
using Hassium.Runtime.Types;
using Hassium.Runtime.Util;

namespace Hassium.Runtime
{
    public class InternalModule : HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("Module");

        public string Name { get; private set; }

        public InternalModule(string name)
        {
            AddType(TypeDefinition);
            Name = name;
        }

        public static Dictionary<string, InternalModule> InternalModules = new Dictionary<string, InternalModule>()
        {
            { "Drawing",    new HassiumDrawingModule() },
            { "IO",         new HassiumIOModule() },
            { "Math",       new HassiumMathModule() },
            { "Net",        new HassiumNetModule() },
            { "Text",       new HassiumTextModule() },
            { "Types",      new HassiumTypesModule() },
            { "Util",       new HassiumUtilModule() }
        };
    }
}
