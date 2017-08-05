namespace Hassium.Runtime.IO
{
    public class HassiumIOModule : InternalModule
    {
        public HassiumIOModule() : base("IO")
        {
            AddAttribute("DirectoryNotFoundException", new HassiumDirectoryNotFoundException());
            AddAttribute("File", HassiumFile.TypeDefinition);
            AddAttribute("FileClosedException", new HassiumFileClosedException());
            AddAttribute("FileNotFoundException", new HassiumFileNotFoundException());
            AddAttribute("FS", new HassiumFS());
            AddAttribute("Path", new HassiumPath());
        }
    }
}
