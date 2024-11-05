namespace ShoeStoreBackend.Dto
{
    public class EmployeeCreateDto
    {
        public long? RoleId { get; set; } = null;
        public string? Login { get; set; } = null;
        public string? Password { get; set; } = null;
    }
}
