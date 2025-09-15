using SphahloHub_UI.Client.Domain.DTOs;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CartItemService
    {
        public SphahloResponse Sphahlo { get; set; } = default!;
        public Dictionary<int, bool> IngredientSelections { get; set; } = new();
        public int Quantity { get; set; } = 1;

        public decimal CalculatePrice()
        {
            var total = Sphahlo.BasePrice;
            foreach (var ing in Sphahlo.Ingredients)
            {
                var selected = IngredientSelections[ing.IngredientId];
                if (selected != ing.IncludedByDefault)
                    total += selected ? ing.Ingredient.PriceDelta : -ing.Ingredient.PriceDelta;
            }
            return total * Quantity;
        }
    }
}
