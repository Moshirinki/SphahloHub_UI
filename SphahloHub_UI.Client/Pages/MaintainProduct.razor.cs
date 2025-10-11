using Microsoft.AspNetCore.Components;
using MudBlazor;
using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Pages
{
    public partial class MaintainProduct
    {
        [Inject] private ICatalogService catalogService { get; set; } = default!;
        [Parameter] public ProductRequest productRequest { get; set; } = new();
        [CascadingParameter]
        IMudDialogInstance MudDialog { get; set; }
        [Inject] private ISnackbar snackbar { get; set; }


        private async Task Save()
        {
            bool success;

            if (productRequest.Id == 0)
            {
                success = await catalogService.CreateProductAsync(productRequest);
            }
            else
            {
                success = await catalogService.UpdateProductAsync(productRequest.Id, productRequest);
            }

            if (success)
            {
                MudDialog.Close(DialogResult.Ok(productRequest));
            }
            else
            {
                snackbar.Add("Failed to save account.", Severity.Error);
            }
        }

        private void Cancel()
        {
            MudDialog.Close(DialogResult.Cancel());
        }
    }
}