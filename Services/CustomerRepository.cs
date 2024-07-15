using BiteWebAPI.DBContext;
using BiteWebAPI.Entities;

namespace BiteWebAPI.Services
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly BlogDbContext _pieShopDbContext;

        public CustomerRepository(BlogDbContext pieShopDBContext)
        {
            _pieShopDbContext = pieShopDBContext;
        }

        public IEnumerable<Customer> Customers
        {
            get
            {
                return _pieShopDbContext.Customers;
            }
        }

        public Customer? Get(int id)
        {
            return _pieShopDbContext.Customers.FirstOrDefault(c => c.Id == id);
        }
    }
}
