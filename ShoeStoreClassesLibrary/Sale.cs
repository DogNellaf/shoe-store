using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Sale")]
    public class Sale
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public required Item Item { get; set; }

        [ForeignKey("employee_id")]
        public required Employee Employee { get; set; }

        [Column("created_at")]
        public required DateTime CreatedAt { get; set; }

        [Column("is_returned")]
        public required bool IsReturned { get; set; } = false;
    }
}
