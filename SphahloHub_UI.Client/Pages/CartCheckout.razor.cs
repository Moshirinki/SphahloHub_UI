using Microsoft.AspNetCore.Components;
using SphahloHub_UI.Client.Service.Implementation;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Pages
{
    public partial class CartCheckout
    {
        [Inject] private IOrderService _orderService { get; set; } = default!;
        private CartService _cartService { get; set; } = default!;
        [Inject] private NavigationManager nav { get; set; } = default!;

        private async Task Checkout()
        {
            var res = await _orderService.PlaceOrderAsync(_cartService, "Payfast");
            if (res != null)
            {
                _cartService.ClearCart();
                nav.NavigateTo(res.RedirectUrl, forceLoad: true);
            }
        }
    }
}