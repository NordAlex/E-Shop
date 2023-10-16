using EShop.Catalog.Domain.Common;

namespace EShop.Catalog.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
