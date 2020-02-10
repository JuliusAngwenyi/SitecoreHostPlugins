using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;
using Avanade.Plugin.IdentityProvider.Ids4Demo.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Avanade.Plugin.IdentityProvider.Ids4Demo
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration scConfig, ILogger<ConfigureSitecore> logger)
        {
            this._logger = logger;
            this._appSettings = new AppSettings();
            scConfig.GetSection(AppSettings.SectionName);
            scConfig.GetSection(AppSettings.SectionName).Bind((object)this._appSettings.Ids4DemoIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Ids4DemoIdentityProvider identityProvider = this._appSettings.Ids4DemoIdentityProvider;
            if (!identityProvider.Enabled)
                return;
            this._logger.LogDebug("Configure '" + identityProvider.DisplayName + "'. AuthenticationScheme = " + identityProvider.AuthenticationScheme + ", ClientId = " + identityProvider.ClientId, Array.Empty<object>());
            new AuthenticationBuilder(services).AddOpenIdConnect(identityProvider.AuthenticationScheme, identityProvider.DisplayName, (Action<OpenIdConnectOptions>)(options =>
            {
                options.SignInScheme = "idsrv.external";
                options.ClientId = identityProvider.ClientId;
                options.Authority = identityProvider.Authority;
                options.MetadataAddress = identityProvider.MetadataAddress;
                options.CallbackPath = "/signin-idsrv";
                options.Events.OnRedirectToIdentityProvider += (Func<RedirectContext, Task>)(context =>
                {
                    Claim first = context.HttpContext.User.FindFirst("idp");
                    if (string.Equals(first != null ? first.Value : (string)null, identityProvider.AuthenticationScheme, StringComparison.Ordinal))
                        context.ProtocolMessage.Prompt = "select_account";
                    return Task.CompletedTask;
                });
            }));
        }
    }
}
