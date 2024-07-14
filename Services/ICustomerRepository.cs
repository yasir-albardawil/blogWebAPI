using BiteWebAPI.Entities;

namespace BiteWebAPI.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Customers { get; }

        Customer? Get(int id);
    }
}
