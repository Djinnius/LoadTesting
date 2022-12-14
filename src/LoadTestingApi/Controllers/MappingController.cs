using LazyCache;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using LoadTestingApi.MappingAbstractions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace LoadTestingApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MappingController
{
    private readonly ILogger<MappingController> _logger;
    private readonly IAppCache _appCache;
    private readonly IMapper _mapper;
    private readonly IAddressBookMapper _addressBookMapper;

    public MappingController(ILogger<MappingController> logger, IAppCache appCache, IMapper mapper, IAddressBookMapper addressBookMapper)
    {
        _logger = logger;
        _appCache = appCache;
        _mapper = mapper;
        _addressBookMapper = addressBookMapper;
    }

    /// <summary>
    ///     [Unoptimised] Manually map properties.
    /// </summary>
    /// <returns> The name of the first person in the address book. </returns>
    [HttpGet]
    public string MapManually()
    {
        var addressBook = _appCache.Get<AddressBookEntity>(CacheKeys.CachedAddressBookEntity);
        var result = ManuallyMapToAddressBookDto(addressBook);
        //_logger.LogWarning("Successfully mapped manually.");
        return result.Persons.First().Name;
    }

    /// <summary>
    ///     [Optimised] Use Mapster Autogenerated mappings.
    /// </summary>
    /// <returns> The name of the first person in the address book. </returns>
    [HttpGet]
    public string MapWithMapsterCodeGeneration()
    {
        var addressBook = _appCache.Get<AddressBookEntity>(CacheKeys.CachedAddressBookEntity);
        var result = _addressBookMapper.Map(addressBook);
        //_logger.LogWarning("Successfully mapped with mapster code gen.");
        return result.Persons.First().Name;
    }

    private AddressBookDto ManuallyMapToAddressBookDto(AddressBookEntity addressBookEntity)
    {
        return new AddressBookDto
        {
            Persons = addressBookEntity.Persons.Select(personEntity =>
            {
                return new PersonDto
                {
                    ID = personEntity.ID,
                    Name = personEntity.Name,
                    Age = personEntity.Age,
                    Email = personEntity.Email,
                    Phones = personEntity.Phones.Select(phoneEntity =>
                    {
                        return new PhoneDto
                        {
                            Number = phoneEntity.Number,
                            PhoneType = phoneEntity.PhoneType
                        };
                    }).ToList()
                };
            }).ToList()
        };
    }
}
