using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Employee")]

    public class Employee
    {
        public long Id { get; set; }

        [ForeignKey("role_id")]
        public Role Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
