using Microsoft.AspNetCore.Components;
using SphahloHub_UI.Client.Service.Interface;
using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Pages
{
    public partial class SphahloDetails : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        private SphahloDto? sphahlo;
        private Dictionary<int, bool> selections = new();
        [Inject]private ICartService _cartService { get; set; } = default!;
        [Inject]private ICatalogService _catalogService { get; set; } = default!;
        [Inject] private NavigationManager nav { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            sphahlo = await _catalogService.GetSphahloAsync(Id);
            if (sphahlo != null)
                selections = sphahlo.Ingredients.ToDictionary(i => i.IngredientId, i => i.IncludedByDefault);
        }

        private decimal CalcPrice()
        {
            if (sphahlo == null) return 0;
            var total = sphahlo.BasePrice;
            foreach (var ing in sphahlo.Ingredients)
            {
                var sel = selections[ing.IngredientId];
                if (sel != ing.IncludedByDefault)
                    total += sel ? ing.PriceDelta : -ing.PriceDelta;
            }
            return total;
        }

        private void Add()
        {
            if (sphahlo != null)
            {
                _cartService.AddToCart(sphahlo, selections);
                nav.NavigateTo("/cart");
            }
        }
    }
}