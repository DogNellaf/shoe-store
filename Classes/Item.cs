using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Item")]
    public class Item
    {
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("shop_id")]
        public required Shop Shop { get; set; }

        [Column("article")]
        public required string Article { get; set; }

        [Column("price")]
        public required double Price { get; set; }

        [Column("storage_count")]
        public required int StorageCount { get; set; }
    }
}
