using Microsoft.AspNetCore.Components;
using MudBlazor;
using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Pages
{
    public partial class Menu : ComponentBase
    {
        private bool _loading = true;
        private List<SphahloResponse> _sphahlos = new();
        private Dictionary<int, int> _quantities = new();
        [Inject] public ICatalogService _catalogService { get; set; } = default!;
        [Inject] public ICartService _cartService { get; set; } = default!;
        [Inject] private ISnackbar snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadSphahlos();
        }

        private async Task LoadSphahlos()
        {
            _loading = true;
            try
            {
                _sphahlos = await _catalogService.GetActiveSphahlosAsync() ?? new List<SphahloResponse>();

                // Initialize quantities
                foreach (var sphahlo in _sphahlos)
                {
                    _quantities[sphahlo.Id] = 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sphahlos: {ex.Message}");
                snackbar.Add("Error loading spahhlos data", Severity.Error);
                _sphahlos = new List<SphahloResponse>(); // Ensure we always have a valid list
            }
            finally
            {

                _loading = false;
                StateHasChanged();
            }
        }

        private int GetQuantity(int sphahloId)
        {
            return _quantities.TryGetValue(sphahloId, out var quantity) ? quantity : 1;
        }

        private void AddToCart(SphahloResponse sphahlo, int quantity)
        {
            _cartService.AddToCart(sphahlo, quantity);
            snackbar.Add($"{quantity} x {sphahlo.Name} added to cart!", Severity.Success);

            // Reset quantity to 1
            _quantities[sphahlo.Id] = 1;
        }
    }
}