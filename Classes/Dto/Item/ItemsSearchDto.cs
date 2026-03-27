namespace ShoeStore.Dto.Item
{
    public class ItemsSearchDto
    {
        public Dictionary<string, string[]>? Parameters { get; set; } = null;
        public float? MinPrice { get; set; } = null;
        public float? MaxPrice { get; set; } = null;
        public long? ShopId { get; set; } = null;
    }
}
