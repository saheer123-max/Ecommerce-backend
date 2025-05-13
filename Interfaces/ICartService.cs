using System.Collections.Generic;
using System.Threading.Tasks;
using WeekFive.DTO;

namespace WeekFive.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, AddToCartDto dto);
        Task<List<CartItemDto>> GetCartItemsAsync(int userId);
        Task AdjustCartItemAsync(int userId, AdjustCartDto dto);
        Task RemoveCartItemAsync(int userId, int productId);
        Task ClearCartAsync(int userId);
    }
}