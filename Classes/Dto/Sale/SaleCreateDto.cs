namespace ShoeStore.Dto.Sale
{
    public class SaleCreateDto
    {
        public long? ItemId { get; set; } = null;
        public long? EmployeeId { get; set; } = null;
        public int? Count { get; set; } = 1;
        public bool IsReturned { get; set; } = false;
    }
}
