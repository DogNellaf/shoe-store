using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("ItemsDiscounts")]
    public class ItemDiscount
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public Item Item { get; set; }

        [ForeignKey("discount_id")]
        public Discount Discount { get; set; }
    }
}
