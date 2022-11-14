using System.Runtime.InteropServices;
using System.Text;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace LoadTestingApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class StringController
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IAppCache _appCache;

    public StringController(ILogger<WeatherForecastController> logger, IAppCache appCache)
    {
        _logger = logger;
        _appCache = appCache;
    }

    /// <summary>
    ///     [Unoptimised] Use a foreach iterator and '+=' to concatenate.
    /// </summary>
    /// <returns> A concatenated string. </returns>
    [HttpGet]
    public string ConcatenateByStringLiteral()
    {
        // Get the cached strings configured during app startup (program.cs)
        var strings = _appCache.Get<List<string>>(CacheKeys.CachedStrings);
        var stringResult = string.Empty;

        foreach (var str in strings)
            stringResult += str;

        _logger.LogInformation("Successfully generated string from String Literals");
        return stringResult;
    }

    /// <summary>
    ///     [Optimised] Use a span iterator and string builder to concatenate.
    /// </summary>
    /// <returns> A concatenated string. </returns>
    [HttpGet]
    public string ConcatenateByStringBuilder()
    {
        // Get the cached strings configured during app startup (program.cs)
        var strings = _appCache.Get<List<string>>(CacheKeys.CachedStrings);
        var spanOfStrings = CollectionsMarshal.AsSpan(strings);
        var sb = new StringBuilder();

        foreach (var str in spanOfStrings)
            sb.Append(str);

        _logger.LogInformation("Successfully generated string from String Builder");
        return sb.ToString();
    }
}
