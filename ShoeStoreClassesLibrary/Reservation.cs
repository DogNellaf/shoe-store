using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public Item Item { get; set; }

        [ForeignKey("client_id")]
        public Client Client { get; set; }

        [Column("count")]
        public short? Count { get; set; } = null;

        [Column("end_at")]
        public DateTime EndAt { get; set; }

        [Column("deposit")]
        public float? Deposit { get; set; } = 0;
    }
}
