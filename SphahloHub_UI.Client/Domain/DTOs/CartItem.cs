namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class CartItem
    {
        public ProductResponse Product { get; set; } = new();
        public int Quantity { get; set; } = 1;
        public decimal TotalPrice => Product.BasePrice * Quantity;
    }
}
