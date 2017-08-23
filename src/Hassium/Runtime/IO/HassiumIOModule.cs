namespace Hassium.Runtime.IO
{
    public class HassiumIOModule : InternalModule
    {
        public HassiumIOModule() : base("IO")
        {
            AddAttribute("DirectoryNotFoundException", new HassiumDirectoryNotFoundException());
            AddAttribute("File", HassiumFile.TypeDefinition);
            AddAttribute("FileClosedException", HassiumFileClosedException.TypeDefinition);
            AddAttribute("FileNotFoundException", HassiumFileNotFoundException.TypeDefinition);
            AddAttribute("FS", new HassiumFS());
            AddAttribute("Path", new HassiumPath());
        }
    }
}
