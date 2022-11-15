using System.Text;
using ProtoBuf;

namespace LoadTestingApi.Dtos;

[ProtoContract]
public sealed class AddressBookDto
{
    [ProtoMember(1)]
    public List<PersonDto> Persons { get; set; } = new List<PersonDto>();

    [ProtoMember(2)]
    public string PersonNames { get; set; } = string.Empty;

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var person in Persons)
            sb.AppendLine($"Person {person}");

        return sb.ToString();
    }
}
