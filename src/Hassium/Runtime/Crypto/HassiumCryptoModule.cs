namespace Hassium.Runtime.Crypto
{
    public class HassiumCryptoModule : InternalModule
    {
        public HassiumCryptoModule() : base("Crypto")
        {
            AddAttribute("AES", new HassiumAES());
            AddAttribute("RSA", new HassiumRSA());
        }
    }
}
