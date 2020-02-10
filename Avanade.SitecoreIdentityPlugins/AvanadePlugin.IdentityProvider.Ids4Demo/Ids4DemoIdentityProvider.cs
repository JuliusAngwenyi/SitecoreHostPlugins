namespace Avanade.Plugin.IdentityProvider.Ids4Demo
{
    public class Ids4DemoIdentityProvider : Sitecore.Plugin.IdentityProviders.IdentityProvider
    {
        public string ClientId { get; set; }
        public string MetadataAddress { get; set; }
        public string Authority { get; set; }

    }
}
