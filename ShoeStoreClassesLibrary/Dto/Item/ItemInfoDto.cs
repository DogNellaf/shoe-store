namespace ShoeStore.Dto.Item
{
    public class ItemInfoDto
    {
        public long Id { get; set; }
        public long ShopId { get; set; }
        public string? Article { get; set; }
        public double Price { get; set; }
        public int StorageCount { get; set; }

        public ItemInfoDto(Models.Item item)
        {
            Id = item.Id;
            ShopId = item.Shop.Id;
            Article = item.Article;
            Price = item.Price;
            StorageCount = item.StorageCount;
        }
    }
}
