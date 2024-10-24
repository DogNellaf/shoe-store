using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Employee")]

    public class Employee
    {
        public long Id { get; set; }
        public Role Role { get; set; }
    }
}
