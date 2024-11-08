namespace ShoeStore.Dto.Discount
{
    public class DiscountAttachDto
    {
        public long? DiscountId { get; set; } = null;
        public long[]? ItemIds { get; set; } = null;
    }
}
