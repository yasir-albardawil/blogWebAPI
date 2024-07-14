using BiteWebAPI.Entities;

namespace BiteWebAPI.Services
{
    public interface IOrderRepository
    {
        IEnumerable<Order> AllOrders { get; }
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<Order?> FindAsync(int? id);
    }
}
