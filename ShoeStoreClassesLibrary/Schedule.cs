using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        public long Id { get; set; }

        [ForeignKey("employee_id")]
        public required Employee Employee { get; set; }

        [ForeignKey("shop_id")]
        public required Shop Shop { get; set; }
        public required DateTime Date { get; set; }
    }
}
