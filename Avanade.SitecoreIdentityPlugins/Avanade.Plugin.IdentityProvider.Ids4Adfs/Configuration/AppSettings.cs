namespace Avanade.Plugin.IdentityProvider.Ids4Adfs.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Ids4Adfs";

        public Ids4AdfsIdentityProvider Ids4AdfsIdentityProvider { get; set; } = new Ids4AdfsIdentityProvider();
    }
}
