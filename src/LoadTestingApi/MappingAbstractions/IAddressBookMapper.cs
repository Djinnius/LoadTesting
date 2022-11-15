using LoadTestingApi.DependencyInjection;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using Mapster;

namespace LoadTestingApi.MappingAbstractions;

[Mapper]
public interface IAddressBookMapper : ISingletonService
{
    AddressBookDto Map(AddressBookEntity addressBookEntity);
}
