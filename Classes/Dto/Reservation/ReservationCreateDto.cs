namespace ShoeStore.Dto.Reservation
{
    public class ReservationCreateDto
    {
        public long? ItemId { get; set; } = null;
        public long? Phone { get; set; } = null;
        public string? FIO { get; set; } = null;
        public short? Count { get; set; } = null;
        public DateTime? EndAt { get; set; } = null;
        public float? Deposit { get; set; } = 0;
    }
}
