namespace Avanade.Plugin.IdentityProvider.Ids4WsFederation.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Ids4WsFederation";
        public Ids4WsFederationIdentityProvider Ids4WsFederationIdentityProvider { get; set; } = new Ids4WsFederationIdentityProvider();
    }
}
