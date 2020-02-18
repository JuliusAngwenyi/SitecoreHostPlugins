using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;
using Avanade.Plugin.IdentityProvider.Ids4WsFederation.Configuration;
using System;


namespace Avanade.Plugin.IdentityProvider.Ids4WsFederation
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration scConfig, ILogger<ConfigureSitecore> logger)
        {
            _logger      = logger;
            _appSettings = new AppSettings();
            scConfig.GetSection(AppSettings.SectionName).Bind(_appSettings.Ids4WsFederationIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var identityProvider = _appSettings.Ids4WsFederationIdentityProvider;
            if (!identityProvider.Enabled)
                return;
            _logger.LogDebug("Configure '" + identityProvider.DisplayName + "'. AuthenticationScheme = " + identityProvider.AuthenticationScheme + ", MetadataAddress = " + identityProvider.MetadataAddress + ", Wtrealm = " + identityProvider.Wtrealm, Array.Empty<object>());
            new AuthenticationBuilder(services).AddWsFederation(identityProvider.AuthenticationScheme, identityProvider.DisplayName, (Action<WsFederationOptions>)(options =>
            {
                options.SignInScheme    = "idsrv.external";
                options.MetadataAddress = identityProvider.MetadataAddress;
                options.Wtrealm         = identityProvider.Wtrealm;
            }));
        }
    }
}
