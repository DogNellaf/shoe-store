using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Sale")]
    public class Sale
    {
        public long Id { get; set; }
        public Item Item { get; set; }
        public Employee Employee { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsReturned { get; set; } = false;
    }
}
