using EShop.Carting.Application.Common.Interfaces;
using EShop.Carting.Domain.Entities;
using EShop.Carting.Infrastructure.Repositories.Common;
using LiteDB;

namespace EShop.Carting.Infrastructure.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ILiteDatabase database) : base(database)
        {
        }

        public List<CartItem> GetCartItems(string cartId)
        {
            var cartItems = Query<CartItem>().Where(x => x.CartId == cartId);
            return cartItems.ToList();
        }

        public List<CartItem> GetItems(int itemId) => Query<CartItem>().Where(x => x.ItemId == itemId).ToList();
    }
}
