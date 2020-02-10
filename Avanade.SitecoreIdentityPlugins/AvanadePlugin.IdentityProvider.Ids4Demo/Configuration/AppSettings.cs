namespace Avanade.Plugin.IdentityProvider.Ids4Demo.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Ids4Demo";

        public Ids4DemoIdentityProvider Ids4DemoIdentityProvider { get; set; } = new Ids4DemoIdentityProvider();
    }
}
