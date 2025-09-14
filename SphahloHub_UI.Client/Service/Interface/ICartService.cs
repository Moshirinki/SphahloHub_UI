using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Implementation;

namespace SphahloHub_UI.Client.Service.Interface
{
    public interface ICartService
    {
        void AddToCart(SphahloResponse sphahlo, int quantity);
        void UpdateQuantity(int sphahloId, int quantity);
        void RemoveFromCart(int sphahloId);
        decimal GetTotal();
        void ClearCart();
    }
}