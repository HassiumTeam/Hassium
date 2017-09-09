namespace Hassium.Runtime.Crypto
{
    public class HassiumCryptoModule : InternalModule
    {
        public HassiumCryptoModule() : base("Crypto")
        {
            AddAttribute("AES", HassiumAES.TypeDefinition);
            AddAttribute("RSA", HassiumRSA.TypeDefinition);
        }
    }
}
