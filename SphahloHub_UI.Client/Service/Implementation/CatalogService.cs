using SphahloHub_UI.Client.Service.Interface;
using System.Net.Http.Json;
using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _http;
        public CatalogService(HttpClient http) => _http = http;

        public async Task<List<SphahloDto>> GetSphahlosAsync()
            => await _http.GetFromJsonAsync<List<SphahloDto>>("api/sphahlo") ?? new();

        public async Task<SphahloDto?> GetSphahloAsync(int id)
            => await _http.GetFromJsonAsync<SphahloDto>($"api/sphahlo/{id}");
    }

}
