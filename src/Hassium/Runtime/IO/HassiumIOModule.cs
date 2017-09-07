namespace Hassium.Runtime.IO
{
    public class HassiumIOModule : InternalModule
    {
        public HassiumIOModule() : base("IO")
        {
            AddAttribute("DirectoryNotFoundException", HassiumDirectoryNotFoundException.TypeDefinition);
            AddAttribute("File", HassiumFile.TypeDefinition);
            AddAttribute("FileClosedException", HassiumFileClosedException.TypeDefinition);
            AddAttribute("FileNotFoundException", HassiumFileNotFoundException.TypeDefinition);
            AddAttribute("FS", HassiumFS.TypeDefinition);
            AddAttribute("Path", HassiumPath.TypeDefinition);
        }
    }
}
