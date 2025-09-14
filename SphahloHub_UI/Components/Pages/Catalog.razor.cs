using Microsoft.AspNetCore.Components;
using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;

namespace SphahloHub_UI.Components.Pages
{
    public partial class Catalog
    {
        [Inject] public ICatalogService _catalogService { get; set; } = default!;
        [Parameter] public SphahloRequest sphahloRequest { get; set; }
        private List<SphahloResponse> sphahlos = new();

        protected override async Task OnInitializedAsync()
        {
            sphahlos = await _catalogService.GetActiveSphahlosAsync() ?? new List<SphahloResponse>();
        }
    }
}