namespace ShoeStore.Models
{
    public class Schedule
    {
        public long Id { get; set; }
        public Employee Employee { get; set; }
        public Shop Shop { get; set; }
        public DateTime Date { get; set; }
    }
}
