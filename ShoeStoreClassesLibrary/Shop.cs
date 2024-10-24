using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Shop")]
    public class Shop
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
    }
}
