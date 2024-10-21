namespace ShoeStore.Models
{
    public class Sale
    {
        public long Id { get; set; }
        public Item Item { get; set; }
        public Employee Employee { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsReturned { get; set; } = false;
    }
}
