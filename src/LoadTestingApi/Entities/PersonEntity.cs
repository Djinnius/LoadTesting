using System.Text;

namespace LoadTestingApi.Entities;

public sealed class PersonEntity
{
    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Email { get; set; } = string.Empty;

    public List<PhoneEntity> Phones { get; set; } = new List<PhoneEntity>();

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
