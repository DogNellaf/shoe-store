using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    public class ItemIncome
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public Item Item { get; set; } = null!;
        public DateTime Date { get; set; }

        public int Value { get; set; }
    }
}
