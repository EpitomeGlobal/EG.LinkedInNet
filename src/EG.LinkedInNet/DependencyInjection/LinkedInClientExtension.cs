namespace Microsoft.Extensions.DependencyInjection;

using EG.LinkedInNet;
using Extensions;

public static class LinkedInClientExtension
{
    public static IServiceCollection AddLinkedInClient(this IServiceCollection services,
        Action<LinkedInConfiguration> configureOptions)
    {
        services.Configure(configureOptions);
        services.TryAddSingleton<IClientTokenManagementService, ClientTokenManagementService>();
        services.TryAddScoped<ClientAccessTokenHandler>();
        services.AddHttpClient<LinkedInClient>()
            .AddHttpMessageHandler<ClientAccessTokenHandler>();
        return services;
    }
}
