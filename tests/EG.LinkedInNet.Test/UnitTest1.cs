namespace EG.LinkedInNet.Test;

using Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
    public void DeserializeClassification()
    {
        var json = File.ReadAllText("classification.json");
        var test = JsonConvert.DeserializeObject<LinkedInResponse<Classification>>(json);
        Assert.IsNotNull(test);
        Assert.True(test.Elements.Count == 38);

    }

    [Test]
    public void DeserializeAsset()
    {
        var json = File.ReadAllText("asset.json");
        var test = JsonConvert.DeserializeObject<LinkedInResponse<LearningAsset>>(json);
        Assert.IsNotNull(test);
        Assert.True(test.Elements.Count == 20);
    }

    [Test]
    public async Task TestClassification()
    {
        var sp = this.services.BuildServiceProvider(true);
        var client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        var result = await client.GetClassifications("keyword", "business", "US", "en");

        Assert.True(result.Elements.Any());
    }

    [Test]
    public async Task TestAsset()
    {
        var sp = this.services.BuildServiceProvider(true);
        var client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        var request = new LearningAssetRequest()
        {
            Query = "criteria",
            AssetType = new[] { AssetType.COURSE },
            ExpandDepth = 3,
            IncludeRetired = true,
            Start = 0,
            Count = 20,
        };
        var result = await client.GetLearningAssets(request);

        Assert.True(result.Elements.Any());
    }
}
