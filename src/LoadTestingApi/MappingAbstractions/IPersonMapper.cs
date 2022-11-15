using LoadTestingApi.Dtos;
using LoadTestingApi.Entities;
using Mapster;

namespace LoadTestingApi.MappingAbstractions;

[Mapper]
public interface IPersonMapper
{
    PersonDto Map(PersonEntity personEntity);
}
