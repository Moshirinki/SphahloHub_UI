using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly List<CartItemService> _items = new();
        public IReadOnlyList<CartItemService> CartItems => _items;

        public event Action? OnCartChanged;

        public void AddToCart(ProductResponse product, int quantity = 1)
        {
            var existingItem = _items.FirstOrDefault(i => i.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItemService { Product = product, Quantity = quantity });
            }

            OnCartChanged?.Invoke();
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.Product.Id == productId);
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

        public void RemoveFromCart(int productId)
        {
            var item = _items.FirstOrDefault(i => i.Product.Id == productId);
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