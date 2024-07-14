using BiteWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiteWebAPI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IItemRepository itemRepository, IShoppingCart shoppingCart)
        {
            _itemRepository = itemRepository;
            _shoppingCart = shoppingCart;
        }
        public ActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            //var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return Ok();
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = _itemRepository.GetItemsAsync().Result.FirstOrDefault(p => p.Id == pieId);

            if (selectedPie != null)
            {
                _shoppingCart.AddToCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _itemRepository.GetItemsAsync().Result.FirstOrDefault(p => p.Id == pieId);

            if (selectedPie != null)
            {
                _shoppingCart.RemoveFromCart(selectedPie);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearShoppingCart()
        {
             if (_shoppingCart.GetShoppingCartItems().Count() > 0)
                _shoppingCart.ClearCart();

            return RedirectToAction("index");
        }
    }
}
