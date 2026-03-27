namespace ShoeStore.Dto.Item
{
    public class ItemCreateDto
    {
        public long? ShopId { get; set; } = null;
        public string? Article { get; set; } = null;
        public float? Price { get; set; } = null;
        public int? StorageCount { get; set; } = null;
        public Dictionary<string, string>? Properties { get; set; } = null;
    }
}
