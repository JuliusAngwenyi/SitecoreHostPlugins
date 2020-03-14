using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;
using Avanade.Plugin.IdentityProvider.Ids4Facebook.Configuration;
using System;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace Avanade.Plugin.IdentityProvider.Ids4Facebook
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration scConfig, ILogger<ConfigureSitecore> logger)
        {
            _logger      = logger;
            _appSettings = new AppSettings();
            scConfig.GetSection(AppSettings.SectionName).Bind(_appSettings.Ids4FacebookIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var identityProvider = _appSettings.Ids4FacebookIdentityProvider;
            if (!identityProvider.Enabled)
                return;
            _logger.LogDebug("Configure '" + identityProvider.DisplayName + "'. AuthenticationScheme = " + identityProvider.AuthenticationScheme + ", MetadataAddress = " + identityProvider.AppId + ", Wtrealm = " + identityProvider.AppSecret, Array.Empty<object>());
            new AuthenticationBuilder(services).AddFacebook(identityProvider.AuthenticationScheme, identityProvider.DisplayName, (Action<FacebookOptions>)(options =>
            {
                options.SignInScheme = "idsrv.external";
                options.AppId = identityProvider.AppId;
                options.AppSecret = identityProvider.AppSecret;
            }));
        }
    }
}
