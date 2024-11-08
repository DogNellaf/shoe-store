using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Item")]
    public class Item
    {
        [Column("id")]
        public long Id { get; set; }

        [ForeignKey("shop_id")]
        public Shop Shop { get; set; }

        [Column("article")]
        public string Article { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("storage_count")]
        public int StorageCount { get; set; }
    }
}
