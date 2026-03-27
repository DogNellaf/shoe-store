namespace ShoeStore.Dto.Discount
{
    public class DiscountCreateDto
    {
        public DateTime? StartAt { get; set; } = null;
        public DateTime? EndAt { get; set; } = null;
        public double? Value { get; set; } = null;
    }
}
