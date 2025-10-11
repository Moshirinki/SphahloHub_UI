using SphahloHub_UI.Client.Service.Implementation;
using static SphahloHub_UI.Client.Domain.ProductDTOs;

namespace SphahloHub_UI.Client.Service.Interface
{
    public interface IOrderService
    {
        Task<CreateOrderRes?> PlaceOrderAsync(CartService cart, string provider);
    }
}