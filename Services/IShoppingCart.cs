using BiteWebAPI.Entities;

namespace BiteWebAPI.Services
{

    public interface IShoppingCart
    {
        void AddToCart(Item pie);
        int RemoveFromCart(Item pie);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
