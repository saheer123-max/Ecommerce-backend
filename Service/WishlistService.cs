using Microsoft.EntityFrameworkCore;
using WeekFive.DTO;
using WeekFive.Interfaces;
using WeekFive.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeekFive.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _context;

        public WishlistService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToWishlistAsync(int userId, AddToWishlistDto dto)
        {
           
            var existingItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == dto.ProductId);

            if (existingItem != null)
            {
               
                throw new InvalidOperationException("Product already exists in wishlist.");
            }

            var newItem = new Wishlist
            {
                UserId = userId,
                ProductId = dto.ProductId
            };

            _context.Wishlists.Add(newItem);
            await _context.SaveChangesAsync();
        }


        public async Task<List<WishlistItemDto>> GetWishlistAsync(int userId)
        {
            return await _context.Wishlists
                .Where(w => w.UserId == userId)
                .Include(w => w.Product)
                .Select(w => new WishlistItemDto
                {
                    ProductId = w.ProductId,
                    ProductName = w.Product.Name,
                    Price = w.Product.Price,
                    Description = w.Product.Description
                })
                .ToListAsync();
        }

        public async Task RemoveFromWishlistAsync(int userId, int productId)
        {
            var item = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (item == null)
                throw new KeyNotFoundException("Wishlist item not found");

            _context.Wishlists.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}