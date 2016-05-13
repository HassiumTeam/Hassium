using System;

using Hassium.Runtime.StandardLibrary;

namespace Hassium.Runtime.StandardLibrary.IO
{
    public class HassiumIOModule: InternalModule
    {
        public HassiumIOModule() : base("IO")
        {
            Attributes.Add("File",          new HassiumFile());
            Attributes.Add("FileReader",    new HassiumFileReader());
            Attributes.Add("FileWriter",    new HassiumFileWriter());
            Attributes.Add("Path",          new HassiumPath());
            Attributes.Add("UI",            new HassiumUI());
        }
    }
}