using BiteWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public ActionResult GetCustomers()
        {
            var customers = _customerRepository.Customers;
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult GetCustomer(int id)
        {
            var customer = _customerRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
