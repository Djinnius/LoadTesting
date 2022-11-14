using System.Text;
using ProtoBuf;

namespace LoadTestingApi.Dtos;

[ProtoContract]
internal sealed class AddressBookDto
{
    [ProtoMember(1)]
    public List<PersonDto> Persons { get; set; } = new List<PersonDto>();

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var person in Persons)
            sb.AppendLine($"Person {person}");

        return sb.ToString();
    }
}
