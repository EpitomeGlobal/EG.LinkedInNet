using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace EG.LinkedInNet.Test;

public class Tests
{
    private IServiceCollection services;

    [SetUp]
    public void Setup()
    {
        services = new ServiceCollection()
            .AddLinkedInClient(a =>
            {

            });
    }

    [Test]
    public async Task Test1()
    {
        var sp = this.services.BuildServiceProvider(true);
        var client = sp.GetService<LinkedInClient>();
        client.ReadResponseAsString = true;
        var result = await client.GetClassifications("keyword", "business", "US", "en");
        Assert.Pass();
    }
}
