using LazyCache;
using LoadTestingApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProtoBuf;

namespace LoadTestingApi.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IAppCache _appCache;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IAppCache appCache)
    {
        _logger = logger;
        _appCache = appCache;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    //public IEnumerable<WeatherForecast> GetSlow(int count)
    //{
        
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}

    //private async Task DoNormalAmountOfWork()
    //{
    //    // await rather than return task (10 to 30 ns cost)
    //    await NormalAmountOfWork();
    //}

    //private async Task NormalAmountOfWork()
    //{
    //    // Get the cached strings configuraed during app startup (program.cs)
    //    var strings = _appCache.Get<List<string>>(CacheKeys.CachedStrings);
    //    var stringResult = string.Empty;

    //    // Based on string benchmarks and iterator benchmarks
    //    foreach (var str in strings)
    //        stringResult += str;

    //    // Based on serialisation benchmarks
    //    var addressBook = _appCache.Get<AddressBookDto>(CacheKeys.CachedAddressBook);
    //    JsonConvert.SerializeObject(addressBook);
    //}
}
