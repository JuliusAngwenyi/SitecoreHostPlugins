namespace Avanade.Plugin.IdentityProvider.Ids4Facebook.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Ids4Facebook";

        public Ids4FacebookIdentityProvider Ids4FacebookIdentityProvider { get; set; } = new Ids4FacebookIdentityProvider();
    }
}
