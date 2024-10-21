namespace ShoeStore.Models
{
    public class Item
    {
        public long Id { get; set; }
        public Shop Shop { get; set; }
        public string Article { get; set; }
        public float Price { get; set; }
        public int StorageCount { get; set; }
        public List<Property> Properties { get; set; }
    }
}
