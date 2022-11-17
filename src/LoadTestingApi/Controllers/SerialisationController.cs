using LazyCache;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProtoBuf;

namespace LoadTestingApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SerialisationController : ControllerBase
{
    private readonly ILogger<SerialisationController> _logger;
    private readonly IAppCache _appCache;

    public SerialisationController(ILogger<SerialisationController> logger, IAppCache appCache)
    {
        _logger = logger;
        _appCache = appCache;
    }

    /// <summary>
    ///     [Unoptimised] Use Newtonsoft.
    /// </summary>
    /// <returns> A serialised string. </returns>
    [HttpGet]
    public string SerialiseWithNewtonsoft()
    {
        var addressBook = _appCache.Get<AddressBookDto>(CacheKeys.CachedAddressBookDto);
        var result = JsonConvert.SerializeObject(addressBook);

        //_logger.LogWarning("Successfully serialised with Newtonsoft.");
        return result;
    }

    /// <summary>
    ///     [Optimised] Use Protobuf.
    /// </summary>
    /// <returns> A stream. </returns>
    [HttpGet]
    public Stream SerialiseWithProtobuf()
    {
        var addressBook = _appCache.Get<AddressBookDto>(CacheKeys.CachedAddressBookDto);
        MemoryStream stream = new MemoryStream();
        Serializer.Serialize(stream, addressBook);

        //_logger.LogWarning("Successfully serialised with protobuf.");
        return stream;
    }
}
