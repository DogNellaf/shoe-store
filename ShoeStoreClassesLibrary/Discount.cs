namespace ShoeStore.Models
{
    public class Discount
    {
        public long Id { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public float Value { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
