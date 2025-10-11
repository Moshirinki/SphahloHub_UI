using SphahloHub_UI.Client.Service.Interface;
using System.Net.Http.Json;
using static SphahloHub_UI.Client.Domain.ProductDTOs;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        public OrderService(HttpClient http) => _http = http;

        public async Task<CreateOrderRes?> PlaceOrderAsync(CartService cart, string provider)
        {
            var items = cart.CartItems.Select(ci =>
                new CreateOrderItemReq(
                    ci.Product.Id,
                    ci.Quantity,
                    ci.Product.Ingredients.Select(ing =>
                        new CustomiseIngredientReq(
                            ing.IngredientId,
                            ci.IngredientSelections[ing.IngredientId],
                            ing.Ingredient.PriceDelta
                        )
                    )
                )
            );

            var req = new CreateOrderReq(items, provider);

            var response = await _http.PostAsJsonAsync("api/orders", req);
            return await response.Content.ReadFromJsonAsync<CreateOrderRes>();
        }
    }

}
