using LoadTestingApi.DependencyInjection;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using Mapster;

namespace LoadTestingApi.MappingAbstractions;

[Mapper]
public interface IPhoneMapper : ISingletonService
{
    PhoneDto Map(PhoneEntity phoneEntity);
}
