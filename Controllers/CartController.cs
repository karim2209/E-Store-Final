using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace ElectronicsStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<IActionResult> AddItem(int electronicId, int quantity = 1, int redirect = 0)
        {
            var cartCount = await _cartRepository.AddItem(electronicId, quantity);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");            
        }
        public async Task<IActionResult> RemoveItem(int electronicId)
        {
            var cartCount = await _cartRepository.RemoveItem(electronicId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepository.GetUserCart();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepository.GetCartItemCount();
            return Ok(cartItem);
        }
        public async Task<IActionResult> Checkout()
        {
            bool isCheckedOut = await _cartRepository.DoCheckout();
            if (!isCheckedOut)
                throw new Exception("Somthing happen in server side");
            return RedirectToAction("Index", "Home");
        }
    }
}
