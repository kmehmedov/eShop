using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class CatalogService : ICatalogService
    {
        public CatalogService(HttpClient httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
        {
            _httpClient=httpClient;
            _logger=logger;
            _settings=settings;

            _remoteServiceBaseUrl = $"{_settings.Value.GatewayUrl}/c/api/catalog/";
        }
        public Task<IEnumerable<CatalogBrand>> GetBrands()
        {
            throw new NotImplementedException();
        }

        public async Task<CatalogItem> GetCatalogItemById(int id)
        {
            var uri = $"{_remoteServiceBaseUrl}catalogitems/{id}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var catalogItem = JsonSerializer.Deserialize<CatalogItem>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return catalogItem;
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItems(int? brand)
        {
            var filterQs = "";

            if (brand.HasValue)
            {
                var brandQs = (brand.HasValue) ? brand.Value.ToString() : string.Empty;
                filterQs = $"/brand/{brandQs}";
            }
            else
            {
                filterQs = string.Empty;
            }

            var uri = $"{_remoteServiceBaseUrl}catalogitems{filterQs}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var catalogItems = JsonSerializer.Deserialize<IEnumerable<CatalogItem>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return catalogItems;
        }

        #region Private members
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogService> _logger;

        private readonly string _remoteServiceBaseUrl;
        #endregion
    }
}
