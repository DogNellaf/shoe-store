using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        public long Id { get; set; }
        public Employee Employee { get; set; }
        public Shop Shop { get; set; }
        public DateTime Date { get; set; }
    }
}
