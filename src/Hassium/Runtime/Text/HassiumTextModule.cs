namespace Hassium.Runtime.Text
{
    public class HassiumTextModule : InternalModule
    {
        public HassiumTextModule() : base("Text")
        {
            AddAttribute("Encoding", HassiumEncoding.TypeDefinition);
            AddAttribute("RegEx", HassiumRegEx.TypeDefinition);
            AddAttribute("StringBuilder", HassiumStringBuilder.TypeDefinition);
        }
    }
}
