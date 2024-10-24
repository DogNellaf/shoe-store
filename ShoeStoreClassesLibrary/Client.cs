using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models
{
    [Table("Client")]
    public class Client
    {
        public long Id { get; set; }
        public string FIO { get; set; }
        public long Phone { get; set; }
    }
}
