using SphahloHub_UI.Client.Domain.DTOs;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CartItemService
    {
        public ProductResponse Product { get; set; } = default!;
        public Dictionary<int, bool> IngredientSelections { get; set; } = new();
        public int Quantity { get; set; } = 1;

        public decimal CalculatePrice()
        {
            var total = Product.BasePrice;
            foreach (var ing in Product.Ingredients)
            {
                var selected = IngredientSelections[ing.IngredientId];
                if (selected != ing.IncludedByDefault)
                    total += selected ? ing.Ingredient.PriceDelta : -ing.Ingredient.PriceDelta;
            }
            return total * Quantity;
        }
    }
}
