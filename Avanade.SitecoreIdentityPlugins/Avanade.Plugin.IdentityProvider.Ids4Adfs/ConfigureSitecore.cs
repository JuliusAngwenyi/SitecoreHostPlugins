using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Sitecore.Framework.Runtime.Configuration;
using Avanade.Plugin.IdentityProvider.Ids4Adfs.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Avanade.Plugin.IdentityProvider.Ids4Adfs
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
            scConfig.GetSection(AppSettings.SectionName).Bind((object)this._appSettings.Ids4AdfsIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Ids4AdfsIdentityProvider identityProvider = this._appSettings.Ids4AdfsIdentityProvider;
            if (!identityProvider.Enabled)
                return;
            this._logger.LogDebug("Configure '" + identityProvider.DisplayName + "'. AuthenticationScheme = " + identityProvider.AuthenticationScheme + ", ClientId = " + identityProvider.ClientId, Array.Empty<object>());
            new AuthenticationBuilder(services).AddOpenIdConnect(identityProvider.AuthenticationScheme, identityProvider.DisplayName, (Action<OpenIdConnectOptions>)(options =>
            {
                options.ClientId = identityProvider.ClientId;
                options.ClientSecret = identityProvider.ClientSecret;
                options.Authority = identityProvider.Authority;
                options.ResponseType = "id_token";
                options.CallbackPath = "/signin-adfs";
                options.SignedOutCallbackPath = "/signout-callback-adfs";
                options.RemoteSignOutPath = "/signout-adfs";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
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
