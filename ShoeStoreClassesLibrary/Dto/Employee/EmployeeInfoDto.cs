using ShoeStore.Models;
using System.Text.Json.Serialization;

namespace Library.Dto.Employee
{
    public class EmployeeInfoDto
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("Login")]
        public string Login {  get; set; }

        [JsonPropertyName("Password")]
        public string? Password { get; set; } = null;

        [JsonPropertyName("Role")]
        public string Role {  get; set; }

        public EmployeeInfoDto()
        {
                
        }
        public EmployeeInfoDto(long id, string login, Role role)
        {
            Id = id;
            Login = login;
            Role = role.Title;
        }

        public EmployeeInfoDto Copy()
        {
            return MemberwiseClone() as EmployeeInfoDto;
        }
    }
}
