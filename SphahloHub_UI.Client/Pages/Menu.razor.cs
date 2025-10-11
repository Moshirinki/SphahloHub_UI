using Microsoft.AspNetCore.Components;
using MudBlazor;
using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Pages
{
    public partial class Menu : ComponentBase
    {
        private bool _loading = true;
        private List<ProductResponse> _products = new();
        private Dictionary<int, int> _quantities = new();
        [Inject] public ICatalogService _catalogService { get; set; } = default!;
        [Inject] public ICartService _cartService { get; set; } = default!;
        [Inject] private ISnackbar snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            _loading = true;
            try
            {
                _products = await _catalogService.GetActiveProductsAsync() ?? new List<ProductResponse>();

                // Initialize quantities
                foreach (var product in _products)
                {
                    _quantities[product.Id] = 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                snackbar.Add("Error loading products data", Severity.Error);
                _products = new List<ProductResponse>(); // Ensure we always have a valid list
            }
            finally
            {

                _loading = false;
                StateHasChanged();
            }
        }

        private int GetQuantity(int productId)
        {
            return _quantities.TryGetValue(productId, out var quantity) ? quantity : 1;
        }

        private void AddToCart(ProductResponse product, int quantity)
        {
            _cartService.AddToCart(product, quantity);
            snackbar.Add($"{quantity} x {product.Name} added to cart!", Severity.Success);

            // Reset quantity to 1
            _quantities[product.Id] = 1;
        }
    }
}