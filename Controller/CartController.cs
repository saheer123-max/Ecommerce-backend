using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WeekFive.DTO;
using WeekFive.Interfaces;

namespace WeekFive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _cartService.AddToCartAsync(userId, dto);
            return Ok(new { Message = "Item added to cart successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return Ok(cartItems);
        }

        [HttpPatch("adjust")]
        public async Task<IActionResult> AdjustCartItem([FromBody] AdjustCartDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _cartService.AdjustCartItemAsync(userId, dto);
            return Ok(new { Message = "Cart item adjusted successfully." });
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveCartItem(int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _cartService.RemoveCartItemAsync(userId, productId);
            return Ok(new { Message = "Item removed from cart." });
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _cartService.ClearCartAsync(userId);
            return Ok(new { Message = "Cart cleared successfully." });
        }
    }
}