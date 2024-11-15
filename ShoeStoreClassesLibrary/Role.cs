using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Role")]
    public class Role
    {
        public long Id { get; set; }
        public required string Title { get; set; }
    }
}
