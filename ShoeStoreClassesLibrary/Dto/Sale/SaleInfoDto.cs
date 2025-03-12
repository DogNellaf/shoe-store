namespace ShoeStore.Dto.Sale
{
    public class SaleInfoDto
    {
        public string? ItemTitle { get; set; } = null;
        public string? EmployeeLogin { get; set; } = null;
        public DateTime? CreatedAt { get; set; } = null;
        public bool IsReturned { get; set; } = false;

        public SaleInfoDto Copy()
        {
            return MemberwiseClone() as SaleInfoDto;
        }
    }
}
