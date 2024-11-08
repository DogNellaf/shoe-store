using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Property")]
    public class Property
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
