using WeekFive.DTO;

namespace WeekFive.Interfaces
{
    
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto request);
       
        Task<bool> DeleteProductAsync(int id);
        Task<ProductDto> UpdateProductAsync(int id, ProductDto request);

    }
}
