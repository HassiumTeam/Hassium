using System;

namespace Hassium.Runtime.Objects.IO
{
    public class HassiumIOModule: InternalModule
    {
        public HassiumIOModule() : base("IO")
        {
            AddAttribute("FileReader", new HassiumFileReader());
            AddAttribute("FileWriter", new HassiumFileWriter());
            AddAttribute("FS", new HassiumFS());
        }
    }
}

