using System.Text;
using ProtoBuf;

namespace LoadTestingApi.Dtos;

[ProtoContract]
internal sealed class PersonDto
{
    [ProtoMember(1)]
    public int ID { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; } = string.Empty;

    [ProtoMember(3)]
    public int Age { get; set; }

    [ProtoMember(4)]
    public string Email { get; set; } = string.Empty;

    [ProtoMember(5)]
    public List<PhoneDto> Phones { get; set; } = new List<PhoneDto>();

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"ID: {ID}");
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Age: {Age}");
        sb.AppendLine($"Email: {Email}");

        foreach (var phone in Phones)
            sb.AppendLine($"Phone: {phone}");

        return sb.ToString();
    }
}
