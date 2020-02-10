namespace Avanade.Plugin.IdentityProvider.Ids4Adfs
{
    public class Ids4AdfsIdentityProvider : Sitecore.Plugin.IdentityProviders.IdentityProvider
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string MetadataAddress { get; set; }
        public string Authority { get; set; }

    }
}
