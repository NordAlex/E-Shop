using EShop.Catalog.Domain.Entities;

namespace EShop.Catalog.Application.Common.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        public List<CartItem> GetCartItems(int cartId);
    }
}
