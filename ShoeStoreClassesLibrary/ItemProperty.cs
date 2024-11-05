using ShoeStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClassesLibrary
{
    [Table("ItemsProperties")]
    public class ItemProperty
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public Item Item { get; set; }

        [ForeignKey("property_id")]
        public Property Property { get; set; }
        public string Value { get; set; }
    }
}
