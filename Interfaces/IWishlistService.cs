using System.Collections.Generic;
using System.Threading.Tasks;
using WeekFive.DTO;

namespace WeekFive.Interfaces
{
    public interface IWishlistService
    {
        Task AddToWishlistAsync(int userId, AddToWishlistDto dto);
        Task<List<WishlistItemDto>> GetWishlistAsync(int userId);
        Task RemoveFromWishlistAsync(int userId, int productId);
    }
}