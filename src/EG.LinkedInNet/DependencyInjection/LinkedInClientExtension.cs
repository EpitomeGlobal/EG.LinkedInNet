using System;
using EG.LinkedInNet;
using IdentityModel.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class LinkedInClientExtension
{
    public static IServiceCollection AddLinkedInClient(this IServiceCollection services, Action<LinkedInConfiguration> configureOptions)
    {
        services.AddClientAccessTokenManagement((sp,d) =>
        {
            services.Configure(configureOptions);
            var config = new LinkedInConfiguration();
            configureOptions.Invoke(config);
            d.Clients["LinkedInScheme"] = new ClientCredentialsTokenRequest()
            {
                ClientId = config.Client,
                ClientSecret = config.Secret,
                Address = config.TokenEndpoint,
            };
        });
        
        services.AddHttpClient<LinkedInClient>()
            .AddClientAccessTokenHandler("LinkedInScheme");
        return services;
    }
}