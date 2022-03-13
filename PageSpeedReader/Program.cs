// See https://aka.ms/new-console-template for more information
using Google.Apis.PagespeedInsights.v5;
using Google.Apis.PagespeedInsights.v5.Data;
using Google.Apis.Services;
using MongoDB.Driver;
using System.Text.Json;

if (args.Length == 0)
{
    throw new ArgumentException("a page url must be provided");
}

string pageUrl = args[0];

if (string.IsNullOrEmpty(pageUrl))
{
    throw new ArgumentException(nameof(pageUrl));
}

Console.WriteLine("Hello, World!");

var pageSpeedService = new PagespeedInsightsService(new BaseClientService.Initializer
{
    ApplicationName = "Page Speed Test",
    ApiKey = "AIzaSyD6evH0lZ7DhD6Ys8c0RGpM4Q6wRTe-xeQ"
});

var pageSpeedRequest = pageSpeedService.Pagespeedapi.Runpagespeed(pageUrl);
var pageSpeedResponse = pageSpeedRequest.Execute();


var mongoClient = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
var db = mongoClient.GetDatabase("PageSpeedMetrics");

var speedCollection = db.GetCollection<PagespeedApiPagespeedResponseV5>("PageSpeedResults");

speedCollection.InsertOne(pageSpeedResponse);

Console.WriteLine("Completed!");
Console.ReadKey();
