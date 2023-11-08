using EShop.Carting.Domain.Entities;

namespace EShop.Carting.Application.Common.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        public List<CartItem> GetCartItems(string cartId);
        public List<CartItem> GetItems(int itemId);
    }
}
