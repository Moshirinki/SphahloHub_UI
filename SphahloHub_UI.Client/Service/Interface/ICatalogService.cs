using SphahloHub_UI.Client.Domain.DTOs;
using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Service.Interface
{
    public interface ICatalogService
    {
        Task<List<SphahloResponse>> GetActiveSphahlosAsync();
        Task<SphahloResponse> GetSphahloByIdAsync(int sphahloId);
        Task<bool> CreateSphahloAsync(SphahloRequest request);
        Task<bool> UpdateSphahloAsync(int sphahloId, SphahloRequest request);
        Task<bool> ToggleSphahloStatusAsync(int sphahloId);
    }
}
