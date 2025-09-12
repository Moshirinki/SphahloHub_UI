using SphahloHub_UI.Client.Domain.DTOs;
using SphahloHub_UI.Client.Service.Interface;
using System.Net.Http;
using System.Net.Http.Json;
using static SphahloHub_UI.Client.Domain.SphahloDTOs;

namespace SphahloHub_UI.Client.Service.Implementation
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _http;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger)
        {
            _http = httpClient;
            _logger = logger;
        }

        public async Task<List<SphahloResponse>> GetActiveSphahlosAsync()
        {
            try
            {
                var sphahlos = await _http.GetFromJsonAsync<List<SphahloResponse>>("api/sphahlo");
                return sphahlos ?? new List<SphahloResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load available sphahlos.");
                throw new ApplicationException("Failed to available sphahlos. Please try again later.");
            }
        }

        public async Task<SphahloResponse> GetSphahloByIdAsync(int sphahloId)
        {
            try
            {
                var sphahlo = await _http.GetFromJsonAsync<SphahloResponse>($"api/Sphahlo/{sphahloId}");
                return sphahlo ?? new SphahloResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sphahlo with ID:{sphahloId} was not found.");
                throw new ApplicationException($"Sphahlo with ID:{sphahloId} was not found.");
            }
        }

        public async Task<bool> CreateSphahloAsync(SphahloRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Sphahlo", request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Unable to Add New Item.");
                return false;
            }
        }

        public async Task<bool> UpdateSphahloAsync(int sphahloId, SphahloRequest request)
        {
            try
            {
                var sphahlo = await _http.GetFromJsonAsync<SphahloResponse>($"api/Sphahlo/{sphahloId}");
                if (sphahlo == null)
                {
                    _logger.LogError($"Unable to Find Item.");
                    throw new ArgumentException($"Sphahlo with ID {sphahloId} not found");
                }
                else
                {
                    var response = await _http.PutAsJsonAsync($"api/Sphahlo/{sphahloId}", request);
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Unable to Update Item.");
                return false;
            }
        }

        public async Task<bool> ToggleSphahloStatusAsync(int sphahloId)
        {

            try
            {
                var sphahlo = await _http.GetFromJsonAsync<SphahloResponse>($"api/Sphahlo/{sphahloId}");
                if (sphahlo == null)
                {
                    _logger.LogError($"Unable to Find Item.");
                    throw new ArgumentException($"Sphahlo with ID {sphahloId} not found");
                }
                else
                {
                    sphahlo.IsActive = !sphahlo.IsActive;
                    var response = await _http.PutAsJsonAsync($"api/Sphahlo/{sphahloId}", sphahlo);
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Unable to Toggle Item.");
                return false;
            }
        }
    }
}
