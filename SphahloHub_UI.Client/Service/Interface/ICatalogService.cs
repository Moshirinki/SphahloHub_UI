using SphahloHub_UI.Client.Domain.DTOs;
using static SphahloHub_UI.Client.Domain.ProductDTOs;

namespace SphahloHub_UI.Client.Service.Interface
{
    public interface ICatalogService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<List<ProductResponse>> GetActiveProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(int productId);
        Task<bool> CreateProductAsync(ProductRequest request);
        Task<bool> UpdateProductAsync(int productId, ProductRequest request);
        Task<bool> ToggleProductStatusAsync(int productId);
    }
}
