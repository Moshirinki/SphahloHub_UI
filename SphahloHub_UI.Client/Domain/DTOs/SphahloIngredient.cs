namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class SphahloIngredient
    {
        public int SphahloId { get; set; }
        public int IngredientId { get; set; }
        public bool IncludedByDefault { get; set; } = true; // shown pre‑checked
        public Ingredient Ingredient { get; set; } = default!;
    }
}
