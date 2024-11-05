using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        public long Id { get; set; }

        [ForeignKey("employee_id")]
        public Employee Employee { get; set; }

        [ForeignKey("shop_id")]
        public Shop Shop { get; set; }
        public DateTime Date { get; set; }
    }
}
