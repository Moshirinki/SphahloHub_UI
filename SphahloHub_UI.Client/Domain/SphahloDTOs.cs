namespace SphahloHub_UI.Client.Domain
{
    public class SphahloDTOs
    {
        public record IngredientDto(int Id, string Name, decimal PriceDelta, bool IsOptional);
        public record IngredientSelectionDto(int IngredientId, string Name, bool IncludedByDefault, decimal PriceDelta);

        public record SphahloDto(
            int Id,
            string Name,
            string? Description,
            decimal BasePrice,
            IEnumerable<IngredientSelectionDto> Ingredients
        );

        public record CustomiseIngredientReq(int IngredientId, bool Added, decimal PriceDelta);
        public record CreateOrderItemReq(int SphahloId, int Quantity, IEnumerable<CustomiseIngredientReq> Ingredients);
        public record CreateOrderReq(IEnumerable<CreateOrderItemReq> Items, string Provider);
        public record CreateOrderRes(int OrderId, string Provider, string PaymentRef, string RedirectUrl);

    }
}
