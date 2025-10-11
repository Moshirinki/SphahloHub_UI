using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Implementation;

namespace SphahloHub_UI.Client.Service.Interface
{
    public interface ICartService
    {
        void AddToCart(ProductResponse product, int quantity);
        void UpdateQuantity(int productId, int quantity);
        void RemoveFromCart(int productId);
        decimal GetTotal();
        void ClearCart();
    }
}