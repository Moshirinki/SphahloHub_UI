using Microsoft.AspNetCore.Components;
using MudBlazor;
using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Pages
{
    public partial class Catalog : ComponentBase
    {
        [Inject] public ICatalogService _catalogService { get; set; } = default!;
        [Parameter] public ProductRequest productRequest { get; set; }
        private List<ProductResponse> products = new();
        private IDialogService _dialogService { get; set; }
        private ISnackbar snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            products = await _catalogService.GetActiveProductsAsync() ?? new List<ProductResponse>();
        }

        //private async Task ShowCreateDialog()
        //{
        //    var parameters = new DialogParameters
        //    {
        //        ["sphahloRequest"] = new SphahloRequest()
        //    };
        //    var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

        //    var dialogTask = _dialogService.ShowAsync<MaintainSphahlo>("Create Item", parameters, options);
        //    var dialog = await dialogTask;
        //    var result = await dialog.Result;

        //    if (!result.Canceled && result.Data is SphahloResponse createdItem)
        //    {
        //        sphahlos.Add(createdItem);
        //        snackbar.Add("Item created successfully.", Severity.Success);
        //    }
        //}

        //private async Task ShowEditDialog(SphahloResponse item)
        //{
        //    var parameters = new DialogParameters
        //    {
        //        ["sphahloRequest"] = new SphahloRequest
        //        {
        //            Name = item.Name,
        //            Description = item.Description,
        //            Price = item.BasePrice,
        //        }
        //    };
        //    var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

        //    var dialogTask = _dialogService.ShowAsync<MaintainSphahlo>("Edit Item", parameters, options);
        //    var dialog = await dialogTask;
        //    var result = await dialog.Result;

        //    if (!result.Canceled && result.Data is SphahloResponse updatedItem)
        //    {
        //        var index = sphahlos.FindIndex(a => a.Id == updatedItem.Id);
        //        if (index >= 0)
        //        {
        //            sphahlos[index] = updatedItem;
        //            snackbar.Add("Item updated successfully.", Severity.Success);
        //        }
        //    }
        //}
        private async Task DeleteItem(int id)
        {

            bool? result = await _dialogService.ShowMessageBox(
                "Confirm Delete",
                "Are you sure you want to delete this item?",
                yesText: "Delete", noText: "Cancel");

            if (result == true)
            {
                var response = await _catalogService.ToggleProductStatusAsync(id);
                if (response)
                {
                    snackbar.Add("Item deleted successfully.", Severity.Success);
                }
                else
                {
                    snackbar.Add("Failed to delete item.", Severity.Error);
                }
            }
        }
    }
}