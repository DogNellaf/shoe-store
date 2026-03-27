using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("ItemsProperties")]
    public class ItemProperty
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public required Item Item { get; set; }

        [ForeignKey("property_id")]
        public required Property Property { get; set; }
        public required string Value { get; set; }
    }
}
