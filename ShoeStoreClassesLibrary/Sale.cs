using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Sale")]
    public class Sale
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public Item Item { get; set; }

        [ForeignKey("employee_id")]
        public Employee Employee { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("is_returned")]
        public bool IsReturned { get; set; } = false;
    }
}
