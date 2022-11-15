using LoadTestingApi.DependencyInjection;
using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using Mapster;

namespace LoadTestingApi.MappingAbstractions;

[Mapper]
public interface IPersonMapper : ISingletonService
{
    PersonDto Map(PersonEntity personEntity);
}
