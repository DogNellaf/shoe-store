using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        public long Id { get; set; }

        [ForeignKey("item_id")]
        public required Item Item { get; set; }

        [ForeignKey("client_id")]
        public required Client Client { get; set; }

        [Column("count")]
        public required short? Count { get; set; } = null;

        [Column("end_at")]
        public DateTime EndAt { get; set; }

        [Column("deposit")]
        public required float? Deposit { get; set; } = 0;
    }
}
