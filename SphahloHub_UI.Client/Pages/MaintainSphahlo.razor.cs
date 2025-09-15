using Microsoft.AspNetCore.Components;
using MudBlazor;
using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Client.Pages
{
    public partial class MaintainSphahlo
    {
        [Inject] private ICatalogService catalogService { get; set; } = default!;
        [Parameter] public SphahloRequest sphahloRequest { get; set; } = new();
        [CascadingParameter]
        IMudDialogInstance MudDialog { get; set; }
        [Inject] private ISnackbar snackbar { get; set; }


        private async Task Save()
        {
            bool success;

            if (sphahloRequest.Id == 0)
            {
                success = await catalogService.CreateSphahloAsync(sphahloRequest);
            }
            else
            {
                success = await catalogService.UpdateSphahloAsync(sphahloRequest.Id, sphahloRequest);
            }

            if (success)
            {
                MudDialog.Close(DialogResult.Ok(sphahloRequest));
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