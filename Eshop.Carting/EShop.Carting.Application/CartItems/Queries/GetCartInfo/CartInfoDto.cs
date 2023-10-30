namespace EShop.Carting.Application.CartItems.Queries.GetCartInfo
{
    public class CartInfoDto
    {
        public string Id { get; set; }
        public List<CartInfoItemDto> Items { get; set; }
    }

    public class CartInfoItemDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
