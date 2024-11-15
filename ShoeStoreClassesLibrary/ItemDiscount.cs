using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("ItemsDiscounts")]
    public class ItemDiscount
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public required Item Item { get; set; }

        [ForeignKey("discount_id")]
        public required Discount Discount { get; set; }
    }
}
