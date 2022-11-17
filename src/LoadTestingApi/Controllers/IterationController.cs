using System.Runtime.InteropServices;
using System.Text;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace LoadTestingApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class IterationController
{
    private readonly ILogger<IterationController> _logger;
    private readonly IAppCache _appCache;

    public IterationController(ILogger<IterationController> logger, IAppCache appCache)
    {
        _logger = logger;
        _appCache = appCache;
    }

    /// <summary>
    ///     [Unoptimised] Use a foreach iterator.
    /// </summary>
    /// <returns> A concatenated string. </returns>
    [HttpGet]
    public string IterateWithForEach()
    {
        // Get the cached strings configured during app startup (program.cs)
        var strings = _appCache.Get<List<string>>(CacheKeys.CachedStrings);
        var sb = new StringBuilder();

        foreach (var str in strings)
            sb.Append(str);

        //_logger.LogWarning("Successfully iterated with foreach.");
        return sb.ToString();
    }

    /// <summary>
    ///     [Optimised] Use a span iterator.
    /// </summary>
    /// <returns> A concatenated string. </returns>
    [HttpGet]
    public string IterateWithSpan()
    {
        // Get the cached strings configured during app startup (program.cs)
        var strings = _appCache.Get<List<string>>(CacheKeys.CachedStrings);
        var spanOfStrings = CollectionsMarshal.AsSpan(strings);
        var sb = new StringBuilder();

        foreach (var str in spanOfStrings)
            sb.Append(str);

        //_logger.LogWarning("Successfully iterated with foreach span.");
        return sb.ToString();
    }
}
