namespace EG.LinkedInNet.Test;

using Microsoft.Extensions.DependencyInjection;
using Models;
using Newtonsoft.Json;

public class Tests
{
    private IServiceCollection services;

    [SetUp]
    public void Setup()
    {
        this.services = new ServiceCollection()
            .AddLinkedInClient(a =>
            {
            });
    }

    [Test]
    public void DeserializeClassification()
    {
        string json = File.ReadAllText("classification.json");
        LinkedInResponse<Classification>? test = JsonConvert.DeserializeObject<LinkedInResponse<Classification>>(json);
        Assert.IsNotNull(test);
        Assert.True(test.Elements.Count == 38);
    }

    [Test]
    public void DeserializeAsset()
    {
        string json = File.ReadAllText("asset.json");
        LinkedInResponse<LearningAsset>? test = JsonConvert.DeserializeObject<LinkedInResponse<LearningAsset>>(json);
        Assert.IsNotNull(test);
        Assert.True(test.Elements.Count == 20);
    }

    [Test]
    public async Task TestClassification()
    {
        ServiceProvider sp = this.services.BuildServiceProvider(true);
        LinkedInClient? client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        LinkedInResponse<Classification> result = await client.GetClassifications("keyword", "business", "US", "en");

        Assert.True(result.Elements.Any());
    }

    [Test]
    public async Task TestAsset()
    {
        ServiceProvider sp = this.services.BuildServiceProvider(true);
        LinkedInClient? client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        var request = new LearningAssetRequest
        {
            AssetType = new[] { AssetType.COURSE },
            IncludeRetired = false,
            Start = 0,
            Count = 20
        };
        LinkedInResponse<LearningAsset> result = await client.GetLearningAssets(request);
        Assert.True(result.Elements.Any());
    }

    [Test]
    public async Task TestReport()
    {
        ServiceProvider sp = this.services.BuildServiceProvider(true);
        LinkedInClient? client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        var request = new LearningReportRequest()
        {
            //AssetType = AssetType.COURSE,
            Primary = AggregationCriteria.ACCOUNT,
            StartedAt = DateTime.UtcNow.Millisecond,
            LanguageType = Language.ES,
            Duration = 7,
            OffsetUnit = TimeOffset.DAY,
        };
        LinkedInResponse<LearningReport> result = await client.GetLearningActivityReports(request);

        Assert.True(result.Elements.Any());
    }
}
