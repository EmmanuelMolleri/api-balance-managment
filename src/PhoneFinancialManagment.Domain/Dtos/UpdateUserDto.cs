using System.Text.Json.Serialization;

namespace PhoneFinancialManagment.Domain.Dtos;

public class UpdateUserDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
}