namespace SphahloHub_UI.Client.Domain.DTOs
{
    public class CartItem
    {
        public SphahloResponse Sphahlo { get; set; } = new();
        public int Quantity { get; set; } = 1;
        public decimal TotalPrice => Sphahlo.BasePrice * Quantity;
    }
}
