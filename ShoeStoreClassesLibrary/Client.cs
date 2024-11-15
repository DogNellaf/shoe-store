using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Client")]
    public class Client
    {
        public long Id { get; set; }
        public required string FIO { get; set; }
        public required long Phone { get; set; }
    }
}
