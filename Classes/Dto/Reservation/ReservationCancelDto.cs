namespace ShoeStore.Dto.Reservation
{
    public class ReservationCancelDto
    {
        public string? FIO { get; set; } = null;
        public long? Phone { get; set; } = null;
        public long? ItemId { get; set; } = null;
    }
}
