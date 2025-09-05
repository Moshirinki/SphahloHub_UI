using System.Net.Http.Json;
using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class OrderService
    {
        private readonly HttpClient _http;
        public OrderService(HttpClient http) => _http = http;

        public async Task<CreateOrderRes?> PlaceOrderAsync(CartService cart, string provider)
        {
            var items = cart.Items.Select(ci =>
                new CreateOrderItemReq(
                    ci.Sphahlo.Id,
                    ci.Quantity,
                    ci.Sphahlo.Ingredients.Select(ing =>
                        new CustomiseIngredientReq(
                            ing.IngredientId,
                            ci.IngredientSelections[ing.IngredientId],
                            ing.PriceDelta
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
