namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PriceDelta { get; set; } // + for add, - for remove discount if needed
        public bool IsOptional { get; set; } = true; // false = core ingredient
        public bool IsActive { get; set; } = true;
        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
    }
}
