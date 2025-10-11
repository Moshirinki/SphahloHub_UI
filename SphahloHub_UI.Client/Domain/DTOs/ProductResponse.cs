namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProductIngredient> Ingredients { get; set; } = new List<ProductIngredient>();
        public string? ImageUrl { get; set; } = "/images/default-sphahlo.png";
    }
}
