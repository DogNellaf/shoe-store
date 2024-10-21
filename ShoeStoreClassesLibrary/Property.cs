namespace ShoeStore.Models
{
    public class Property
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<Item> Items { get; set; }
    }
}
