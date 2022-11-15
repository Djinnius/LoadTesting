using System.Text;

namespace LoadTestingApi.Entities;

public sealed class AddressBookEntity
{
    public List<PersonEntity> Persons { get; set; } = new List<PersonEntity>();

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var person in Persons)
            sb.AppendLine($"Person {person}");

        return sb.ToString();
    }
}
