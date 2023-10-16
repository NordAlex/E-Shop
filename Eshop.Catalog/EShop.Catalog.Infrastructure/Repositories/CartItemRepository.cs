using EShop.Catalog.Application.Common.Interfaces;
using EShop.Catalog.Domain.Entities;
using EShop.Catalog.Infrastructure.Repositories.Common;
using LiteDB;

namespace EShop.Catalog.Infrastructure.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ILiteDatabase database) : base(database)
        {
        }

        public List<CartItem> GetCartItems(int cartId)
        {
            var cartItems = Query<CartItem>().Where(x => x.CartId == cartId);
            return cartItems.ToList();
        }
    }
}
