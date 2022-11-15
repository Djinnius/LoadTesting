using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using Mapster;

namespace LoadTestingApi.MappingAbstractions;

[Mapper]
public interface IPhoneMapper
{
    PhoneDto Map(PhoneEntity phoneEntity);
}
