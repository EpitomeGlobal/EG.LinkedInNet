namespace EG.LinkedInNet.Test;

using System.Text.Json;
using Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
    protected record WrappedResponse<T>
    {
        public T elements { get; init; }

    }

    [Test]
    public void Deserialize()
    {
        string jsond = "{\"paging\":{\"start\":0,\"count\":100,\"links\":[],\"total\":38},\"elements\":[{\"type\":\"LIBRARY\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business\"},\"urn\":\"urn:li:lyndaCategory:412016\"},{\"type\":\"SUBJECT\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Analysis and Strategy\"},\"urn\":\"urn:li:lyndaCategory:592008\"},{\"type\":\"TOPIC\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Analysis\"},\"urn\":\"urn:li:lyndaCategory:7231\"},{\"type\":\"TOPIC\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Strategy\"},\"urn\":\"urn:li:lyndaCategory:7422\"},{\"type\":\"TOPIC\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Intelligence\"},\"urn\":\"urn:li:lyndaCategory:7232\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Development\"},\"urn\":\"urn:li:skill:39\"},{\"type\":\"TOPIC\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Analytics\"},\"urn\":\"urn:li:lyndaCategory:575033\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business English\"},\"urn\":\"urn:li:skill:4758\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Communications\"},\"urn\":\"urn:li:skill:6454\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Management\"},\"urn\":\"urn:li:skill:2597\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Planning\"},\"urn\":\"urn:li:skill:770\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Writing\"},\"urn\":\"urn:li:skill:43713\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Process Improvement\"},\"urn\":\"urn:li:skill:1833\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Administration\"},\"urn\":\"urn:li:skill:50111\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business-to-Business (B2B)\"},\"urn\":\"urn:li:skill:853\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Acumen\"},\"urn\":\"urn:li:skill:2805\"},{\"type\":\"TOPIC\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Small Business Marketing\"},\"urn\":\"urn:li:lyndaCategory:7406\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"SAP Business One\"},\"urn\":\"urn:li:skill:8515\"},{\"type\":\"TOPIC\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Small Business Management\"},\"urn\":\"urn:li:lyndaCategory:7405\"},{\"type\":\"TOPIC\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Music Business\"},\"urn\":\"urn:li:lyndaCategory:7347\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Small Business\"},\"urn\":\"urn:li:skill:246\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Process Management\"},\"urn\":\"urn:li:skill:18204\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"International Business\"},\"urn\":\"urn:li:skill:2695\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Economics\"},\"urn\":\"urn:li:skill:11241\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Valuation\"},\"urn\":\"urn:li:skill:2551\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Transformation\"},\"urn\":\"urn:li:skill:1937\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Coaching\"},\"urn\":\"urn:li:skill:43718\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Model Canvas\"},\"urn\":\"urn:li:skill:55465\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Networking\"},\"urn\":\"urn:li:skill:3050\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Ethics\"},\"urn\":\"urn:li:skill:4970\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Statistics\"},\"urn\":\"urn:li:skill:13067\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Case Development\"},\"urn\":\"urn:li:skill:50394\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Relationship Management\"},\"urn\":\"urn:li:skill:1314\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Process Automation\"},\"urn\":\"urn:li:skill:18238\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Growth Strategies\"},\"urn\":\"urn:li:skill:27225\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Mathematics\"},\"urn\":\"urn:li:skill:27832\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Ideas\"},\"urn\":\"urn:li:skill:11169\"},{\"type\":\"SKILL\",\"name\":{\"locale\":{\"country\":\"US\",\"language\":\"en\"},\"value\":\"Business Innovation\"},\"urn\":\"urn:li:skill:27286\"}]}";

        var test = JsonSerializer.Deserialize<WrappedResponse<IList<Classification>>>(jsond);
        Assert.IsNotNull(test);
        Assert.True(test.elements.Count == 38);

    }

    [Test]
    public async Task Test1()
    {
        var sp = this.services.BuildServiceProvider(true);
        var client = sp.GetService<LinkedInClient>();
        //client.ReadResponseAsString = true;
        var result = await client.GetClassifications("keyword", "business", "US", "en");

        Assert.True(result.Count == 38);
    }
}
