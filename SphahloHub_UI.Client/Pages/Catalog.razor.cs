using Microsoft.AspNetCore.Components;
using MudBlazor;
using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Pages
{
    public partial class Catalog : ComponentBase
    {
        private bool _loading = true;
        [Inject] public ICatalogService _catalogService { get; set; } = default!;
        [Parameter] public ProductRequest productRequest { get; set; }
        private List<ProductResponse> products = new();
        [Inject] private IDialogService _dialogService { get; set; } = default!;
        [Inject] private ISnackbar snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            _loading = true;
            try
            {
                products = await _catalogService.GetAllProductsAsync() ?? new List<ProductResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
                snackbar.Add("Error loading products data", Severity.Error);
                products = new List<ProductResponse>(); // Ensure we always have a valid list
            }
            finally
            {
                _loading = false;
                StateHasChanged();
            }
        }

        private async Task ShowCreateDialog()
        {
            var parameters = new DialogParameters
            {
                ["productRequest"] = new ProductRequest()
            };
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialogTask = _dialogService.ShowAsync<MaintainProduct>("Create Item", parameters, options);
            var dialog = await dialogTask;
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is ProductResponse createdItem)
            {
                products.Add(createdItem);
                snackbar.Add("Item created successfully.", Severity.Success);
                await LoadProducts();
            }
        }

        private async Task ShowEditDialog(ProductResponse item)
        {
            var parameters = new DialogParameters
            {
                ["productRequest"] = new ProductRequest
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    Price = item.BasePrice
                }
            };
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialogTask = _dialogService.ShowAsync<MaintainProduct>("Edit Item", parameters, options);
            var dialog = await dialogTask;
            var result = await dialog.Result;

            if (!result.Canceled && result.Data is ProductResponse updatedItem)
            {
                var index = products.FindIndex(a => a.Id == updatedItem.Id);
                if (index >= 0)
                {
                    products[index] = updatedItem;
                    snackbar.Add("Item updated successfully.", Severity.Success);
                    StateHasChanged();
                }
                else
                {
                    snackbar.Add("Failed to update the item.", Severity.Error);
                }
            }
        }

        private async Task DeleteItem(int id)
        {
            var itemName = products.Where(x => x.Id == id).Select(x => x.Name).First();

            bool? result = await _dialogService.ShowMessageBox(
                "Confirm Delete",
                $"Are you sure you want to delete : {itemName}?",
                yesText: "Delete",
                cancelText: "Cancel");

            if (result == true)
            {
                var response = await _catalogService.ToggleProductStatusAsync(id);
                if (response)
                {
                    snackbar.Add("Item deleted successfully.", Severity.Success);
                    await LoadProducts();
                }
                else
                {
                    snackbar.Add("Failed to delete item.", Severity.Error);
                }
            }
        }
    }
}