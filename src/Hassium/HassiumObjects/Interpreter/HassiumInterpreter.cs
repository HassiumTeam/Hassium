using System.IO;
using System.Reflection;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Interpreter
{
    public class HassiumInterpreter : HassiumObject
    {
        public HassiumInterpreter()
        {
            Attributes.Add("version", new HassiumProperty("version", x => Program.GetVersion(), null, true));
            Attributes.Add("buildDate", new HassiumProperty("buildDate", x => new HassiumDate(new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime), null, true));
        }
    }
}
