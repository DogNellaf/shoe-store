using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Employee")]

    public class Employee
    {
        public long Id { get; set; }

        [ForeignKey("role_id")]
        public required Role Role { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
