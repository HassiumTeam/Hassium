namespace Hassium.Runtime.Text
{
    public class HassiumTextModule : InternalModule
    {
        public HassiumTextModule() : base("Text")
        {
            AddAttribute("Encoding", new HassiumEncoding());
            AddAttribute("StringBuilder", new HassiumStringBuilder());
        }
    }
}
