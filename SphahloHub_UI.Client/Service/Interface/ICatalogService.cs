using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Service.Interface
{
    public interface ICatalogService
    {
        Task<List<SphahloDto>> GetSphahlosAsync();
        Task<SphahloDto?> GetSphahloAsync(int id);
    }
}
