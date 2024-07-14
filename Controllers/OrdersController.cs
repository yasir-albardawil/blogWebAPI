using BiteWebAPI.Entities;
using BiteWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BiteWebAPI.Controllers
{
    //[Authorize(Roles = "SuperAdmin, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;
        private readonly HttpClient _client;

        public OrdersController(IOrderRepository orderRepository, IShoppingCart shoppingCart, HttpClient client)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders([FromQuery] string? search, [FromQuery] string? sortOrder)
        {
            var orders = _orderRepository.AllOrders;

            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(o =>
                    o.FirstName.Contains(search) ||
                    o.Email.Contains(search) ||
                    o.Country.Contains(search) ||
                    o.City.Contains(search));
            }

            orders = sortOrder switch
            {
                "name_desc" => orders.OrderByDescending(p => p.FirstName),
                "id_desc" => orders.OrderByDescending(p => p.OrderId),
                _ => orders.OrderBy(p => p.FirstName),
            };

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepository.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _orderRepository.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("Checkout")]
        public async Task<ActionResult<Order>> Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first");
            }

            if (ModelState.IsValid)
            {
                await _orderRepository.CreateAsync(order);
                _shoppingCart.ClearCart();
                return Ok(order);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("CheckoutComplete")]
        public ActionResult CheckoutComplete()
        {
            return Ok("Checkout complete.");
        }
    }
}
