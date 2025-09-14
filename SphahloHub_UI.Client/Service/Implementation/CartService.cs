using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly List<CartItemService> _items = new();
        public IReadOnlyList<CartItemService> CartItems => _items;

        public event Action? OnCartChanged;

        public void AddToCart(SphahloResponse sphahlo, int quantity = 1)
        {
            var existingItem = _items.FirstOrDefault(i => i.Sphahlo.Id == sphahlo.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItemService { Sphahlo = sphahlo, Quantity = quantity });
            }

            OnCartChanged?.Invoke();
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.Sphahlo.Id == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    _items.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }

                OnCartChanged?.Invoke();
            }
        }

        public void RemoveFromCart(int sphahloId)
        {
            var item = _items.FirstOrDefault(i => i.Sphahlo.Id == sphahloId);
            if (item != null)
            {
                _items.Remove(item);
                OnCartChanged?.Invoke();
            }
        }
        public decimal GetTotal() => _items.Sum(i => i.CalculatePrice());
        public void ClearCart()
        {
            _items.Clear();
            OnCartChanged?.Invoke();
        }
    }

}