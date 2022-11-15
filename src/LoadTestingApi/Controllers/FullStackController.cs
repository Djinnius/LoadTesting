using LazyCache;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using LoadTestingApi.MappingAbstractions;
using Newtonsoft.Json;
using ProtoBuf;
using Microsoft.VisualBasic;
using System;

namespace LoadTestingApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FullStackController : ControllerBase
{
    private readonly ILogger<FullStackController> _logger;
    private readonly IAppCache _appCache;
    private readonly IAddressBookMapper _addressBookMapper;

    public FullStackController(ILogger<FullStackController> logger, IAppCache appCache, IAddressBookMapper addressBookMapper)
    {
        _logger = logger;
        _appCache = appCache;
        _addressBookMapper = addressBookMapper;
    }

    /// <summary>
    ///     [Optimised] Use: 
    ///     <para>- manual mapping to create a DTO from an entity;</para>
    ///     <para>- a string literal to concatenate;</para>
    ///     <para>- a foreach on a list to iterate;</para>
    ///     <para>- Newtonsoft to serialise.</para>
    /// </summary>
    /// <returns> A json address book. </returns>
    [HttpGet]
    public string Unoptimised()
    {
        var addressBook = _appCache.Get<AddressBookEntity>(CacheKeys.CachedAddressBookEntity);
        var dto = ManuallyMapToAddressBookDto(addressBook);
        var json = JsonConvert.SerializeObject(dto);

        var personString = string.Empty;

        foreach (var person in dto.Persons)
           personString += person.Name;

        dto.PersonNames = personString;

        return json;
    }

    /// <summary>
    ///     [Optimised] Use: 
    ///     <para>- mapster code gen to create a DTO from an entity;</para>
    ///     <para>- a string builder to concatenate;</para>
    ///     <para>- a foreach on a span to iterate;</para>
    ///     <para>- protobuf to serialise.</para>
    /// </summary>
    /// <returns> A streamed address book </returns>
    [HttpGet]
    public string Optimised()
    {
        var addressBook = _appCache.Get<AddressBookEntity>(CacheKeys.CachedAddressBookEntity);
        var dto = _addressBookMapper.Map(addressBook);

        var sb = new StringBuilder();
        var personSpan = CollectionsMarshal.AsSpan(dto.Persons);

        foreach (var person in personSpan)
            sb.Append(person.Name);

        dto.PersonNames = sb.ToString();

        MemoryStream stream = new MemoryStream();
        Serializer.Serialize(stream, dto);
        string stringBase64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);

        return stringBase64;
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
