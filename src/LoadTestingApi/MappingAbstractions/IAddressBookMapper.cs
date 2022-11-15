using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using Mapster;

namespace LoadTestingApi.MappingAbstractions;

[Mapper]
public interface IAddressBookMapper
{
    AddressBookDto Map(AddressBookEntity addressBookEntity);
}
