using System.Text;
using LazyCache;
using LoadTestingApi.Dtos;
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
        var addressBook = _appCache.Get<AddressBookDto>(CacheKeys.CachedAddressBookDto);
        var stringResult = string.Empty;

        foreach (var person in addressBook.Persons)
            stringResult += person.Name;

        //_logger.LogWarning("Successfully generated string from String Literals.");
        return stringResult;
    }

    /// <summary>
    ///     [Optimised] Use a string builder to concatenate.
    /// </summary>
    /// <returns> A concatenated string. </returns>
    [HttpGet]
    public string ConcatenateByStringBuilder()
    {
        var addressBook = _appCache.Get<AddressBookDto>(CacheKeys.CachedAddressBookDto);
        var sb = new StringBuilder();

        foreach (var person in addressBook.Persons)
            sb.Append(person.Name);

        //_logger.LogWarning("Successfully generated string from String Builder.");
        return sb.ToString();
    }
}
