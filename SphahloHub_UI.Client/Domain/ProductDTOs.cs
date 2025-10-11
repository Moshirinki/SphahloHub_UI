namespace SphahloHub_UI.Client.Domain
{
    public class ProductDTOs
    {
        public record IngredientDto(int Id, string Name, decimal PriceDelta, bool IsOptional);
        public record IngredientSelectionDto(int IngredientId, string Name, bool IncludedByDefault, decimal PriceDelta);

        public record ProductDto(
            int Id,
            string Name,
            string? Description,
            decimal BasePrice,
            bool IsActive,
            IEnumerable<IngredientSelectionDto> Ingredients
        );

        public record CustomiseIngredientReq(int IngredientId, bool Added, decimal PriceDelta);
        public record CreateOrderItemReq(int productId, int Quantity, IEnumerable<CustomiseIngredientReq> Ingredients);
        public record CreateOrderReq(IEnumerable<CreateOrderItemReq> Items, string Provider);
        public record CreateOrderRes(int OrderId, string Provider, string PaymentRef, string RedirectUrl);

    }
}
