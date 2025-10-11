using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;
using System.Net.Http.Json;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _http;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger)
        {
            _http = httpClient;
            _logger = logger;
        }

        public async Task<List<ProductResponse>> GetActiveProductsAsync()
        {
            try
            {
                var products = await _http.GetFromJsonAsync<List<ProductResponse>>("api/product");                
                return products
                    .Where(x => x.IsActive == true)
                    .OrderBy(x => x.BasePrice)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load available products.");
                throw new ApplicationException("Failed to retrieve available products. Please try again later.");
            }
        }

        public async Task<ProductResponse> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _http.GetFromJsonAsync<ProductResponse>($"api/Product/{productId}");
                return product ?? new ProductResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Product with ID:{productId} was not found.");
                throw new ApplicationException($"Product with ID:{productId} was not found.");
            }
        }

        public async Task<bool> CreateProductAsync(ProductRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Product", request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Unable to Add New Item.");
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(int productId, ProductRequest request)
        {
            try
            {
                var product = await _http.GetFromJsonAsync<ProductResponse>($"api/Product/{productId}");
                if (product == null)
                {
                    _logger.LogError($"Unable to Find Item.");
                    throw new ArgumentException($"Product with ID {productId} not found");
                }
                else
                {
                    var response = await _http.PutAsJsonAsync($"api/Product/{productId}", request);
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Unable to Update Item.");
                return false;
            }
        }

        public async Task<bool> ToggleProductStatusAsync(int productId)
        {

            try
            {
                var product = await _http.GetFromJsonAsync<ProductResponse>($"api/Product/{productId}");
                if (product == null)
                {
                    _logger.LogError($"Unable to Find Item.");
                    throw new ArgumentException($"Product with ID {productId} not found");
                }
                else
                {
                    product.IsActive = !product.IsActive;
                    var response = await _http.PutAsJsonAsync($"api/Product/{productId}", product);
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Unable to Toggle Item.");
                return false;
            }
        }
    }
}
