using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Property")]
    public class Property
    {
        public long Id { get; set; }
        public required string Title { get; set; }
    }
}
