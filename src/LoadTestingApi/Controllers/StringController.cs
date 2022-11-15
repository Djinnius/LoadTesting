using System.Text;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace LoadTestingApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class StringController : ControllerBase
{
    private readonly ILogger<StringController> _logger;
    private readonly IAppCache _appCache;

    public StringController(ILogger<StringController> logger, IAppCache appCache)
    {
        _logger = logger;
        _appCache = appCache;
    }

    /// <summary>
    ///     [Unoptimised] Use a string literal '+=' to concatenate.
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
    ///     [Optimised] Use a string builder to concatenate.
    /// </summary>
    /// <returns> A concatenated string. </returns>
    [HttpGet]
    public string ConcatenateByStringBuilder()
    {
        // Get the cached strings configured during app startup (program.cs)
        var strings = _appCache.Get<List<string>>(CacheKeys.CachedStrings);
        var sb = new StringBuilder();

        foreach (var str in strings)
            sb.Append(str);

        _logger.LogInformation("Successfully generated string from String Builder");
        return sb.ToString();
    }
}
