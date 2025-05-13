using Microsoft.EntityFrameworkCore;
using WeekFive.DTO;
using WeekFive.Interfaces;
using WeekFive.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeekFive.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(int userId, AddToCartDto dto)
        {
            var existingItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
             
                var newItem = new Cart
                {
                    UserId = userId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };
                _context.Carts.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<CartItemDto>> GetCartItemsAsync(int userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .Select(c => new CartItemDto
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                })
                .ToListAsync();
        }

        public async Task AdjustCartItemAsync(int userId, AdjustCartDto dto)
        {
            var existingItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == dto.ProductId);

            if (existingItem == null)
                throw new KeyNotFoundException("Cart item not found.");

            existingItem.Quantity += dto.Delta;

            if (existingItem.Quantity <= 0)
                _context.Carts.Remove(existingItem);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(int userId, int productId)
        {
            var item = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (item == null)
                throw new KeyNotFoundException("Cart item not found.");

            _context.Carts.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var items = await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}