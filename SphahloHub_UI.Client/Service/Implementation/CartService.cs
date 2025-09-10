using SphahloHub_UI.Client.Service.Interface;
using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly List<CartItem> _items = new();
        public IReadOnlyList<CartItem> CartItems => _items;

        public void AddToCart(SphahloDto sphahlo, Dictionary<int, bool> selections)
            => _items.Add(new CartItem
            {
                Sphahlo = sphahlo,
                IngredientSelections = new Dictionary<int, bool>(selections)
            });

        public void Remove(CartItem item) => _items.Remove(item);
        public decimal GetTotal() => _items.Sum(i => i.CalculatePrice());
        public void Clear()
        {
            _items.Clear();
        }
    }

}
