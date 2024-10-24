using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        public long Id { get; set; }
        public Item Item { get; set; }
        public Client Client { get; set; }
        public short? Count { get; set; } = null;
        public DateTime EndAt { get; set; }
        public float? Deposit { get; set; } = 0;
        public List<Sale> Sales { get; set; }
    }
}
