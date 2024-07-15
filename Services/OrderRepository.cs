
using BiteWebAPI.DBContext;
using BiteWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace BiteWebAPI.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BlogDbContext _pieShopDBContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(BlogDbContext pieShopDBContext, IShoppingCart shoppingCart)
        {
            _pieShopDBContext = pieShopDBContext;
            _shoppingCart = shoppingCart;
        }

        public IEnumerable<Order> AllOrders
        {
            get
            {
                return _pieShopDBContext.Orders;
            }
        }

        public async Task CreateAsync(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Item.Id,
                    Price = shoppingCartItem.Item.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            await _pieShopDBContext.Orders.AddAsync(order);

            await _pieShopDBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _pieShopDBContext.Orders.FindAsync(id);
            if (order != null)
            {
                _pieShopDBContext.Remove(order);
                await _pieShopDBContext.SaveChangesAsync();
            }
        }

        public async Task<Order?> FindAsync(int? id)
        {
            return await _pieShopDBContext.Orders.FindAsync(id);
        }

        public async Task UpdateAsync(Order order)
        {
            _pieShopDBContext.Update(order);
            await _pieShopDBContext.SaveChangesAsync();
        }
    }
}
