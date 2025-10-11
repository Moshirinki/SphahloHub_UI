namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class ProductIngredient
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public bool IncludedByDefault { get; set; } = true; // shown pre‑checked
        public Ingredient Ingredient { get; set; } = default!;
    }
}
