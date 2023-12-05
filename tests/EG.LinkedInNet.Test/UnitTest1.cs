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
        Assert.That(test, Is.Not.Null);
        Assert.That(test.Elements.Count == 38, Is.True);
    }

    [Test]
    public void DeserializeReport()
    {
        string json = File.ReadAllText("report.json");
        LinkedInResponse<LearningReport>? test = JsonConvert.DeserializeObject<LinkedInResponse<LearningReport>>(json);
        Assert.That(test, Is.Not.Null);
        Assert.That(test.Elements.Count == 1, Is.True);
    }


    [Test]
    public void DeserializeAsset()
    {
        string json = File.ReadAllText("asset.json");
        LinkedInResponse<LearningAsset>? test = JsonConvert.DeserializeObject<LinkedInResponse<LearningAsset>>(json);
        Assert.That(test, Is.Not.Null);
        Assert.That(test.Elements.Count == 20, Is.True);
    }

    [Test]
    public async Task TestClassification()
    {
        ServiceProvider sp = this.services.BuildServiceProvider(true);
        LinkedInClient? client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        LinkedInResponse<Classification> result = await client.GetClassifications("keyword", "business", "US", "en");

        Assert.That(result.Elements.Any(), Is.True);
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
        Assert.That(result.Elements.Any(), Is.True);
    }

    [Test]
    public async Task TestReport()
    {
        ServiceProvider sp = this.services.BuildServiceProvider(true);
        LinkedInClient? client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        var request = new LearningReportRequest()
        {
              OffsetUnit = TimeOffset.DAY,
              Duration = 1,
              Start = 0,
              Count = 1,
              ContentSource = ContentSource.LINKEDIN_LEARNING,
              StartedAt = DateTime.Parse("2 Nov 2022"),
              Primary = AggregationCriteria.INDIVIDUAL,
              Secondary = AggregationCriteria.CONTENT,
              AssetType = AssetType.COURSE
        };

        LinkedInResponse<LearningReport> result = await client.GetLearningActivityReports(request);

        Assert.That(result.Elements.Any(), Is.True);
    }
}
