using SphahloHub_UI.Client.Service.Implementation;
using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Service.Interface
{
    public interface ICartService
    {
        void AddToCart(SphahloDto sphahlo, Dictionary<int, bool> selections);
        void Remove(CartItem item);
        decimal GetTotal();
        void Clear();
    }
}