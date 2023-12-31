﻿namespace EShop.Carting.Application.CartItems.Queries.GetCartItems
{
    public class CartItemDto 
    {
        public string Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
