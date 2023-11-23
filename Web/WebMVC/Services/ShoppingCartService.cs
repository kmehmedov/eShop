using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using WebMVC.ViewModels;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebMVC.ViewModels.ShoppingCartViewModels;

namespace WebMVC.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public ShoppingCartService(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<ShoppingCartService> logger)
        {
            _apiClient = httpClient;
            _settings = settings;
            _logger = logger;

            _purchaseUrl =  $"{_settings.Value.GatewayUrl}/sc/api/";
        }
        public async Task AddItemToCartAsync(AppUser user, int productId)
        {
            var uri = $"{_purchaseUrl}shoppingcart/items";

            var newItem = new
            {
                CatalogItemId = productId,
                BasketId = user.Id,
                Quantity = 1
            };

            var basketContent = new StringContent(JsonSerializer.Serialize(newItem), Encoding.UTF8, "application/json");

            await _apiClient.PostAsync(uri, basketContent);
        }

        public async Task<ShoppingCart> GetCartAsync(AppUser user)
        {
            var uri = $"{_purchaseUrl}shoppingcart/{user.Id}";
            
            var responseString = await _apiClient.GetStringAsync(uri);

            var shoppingCart = JsonSerializer.Deserialize<ShoppingCart>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return shoppingCart;
        }

        public async Task UpdateCartAsync(ShoppingCart shoppinCart)
        {
            var uri = $"{_purchaseUrl}shoppingcart";

            await _apiClient.PostAsJsonAsync(uri, shoppinCart);
        }

        public async Task<ShoppingCart> SetQuantitiesAsync(AppUser user, Dictionary<string, int> quantities)
        {
            var shoppinCart = await GetCartAsync(user);

            foreach (var quantity in quantities)
            {
                var basketItem = shoppinCart.Items.SingleOrDefault(bitem => bitem.Id == quantity.Key);
                basketItem.UnitQuantity = quantity.Value;
            }

            await UpdateCartAsync(shoppinCart);

            return shoppinCart;
        }

        public async Task<ShoppingCart> RemoveItemFromCartAsync(AppUser user, string itemId)
        {
            var shoppinCart = await GetCartAsync(user);

            shoppinCart.Items.RemoveAll(x => x.Id == itemId);

            await UpdateCartAsync(shoppinCart);

            return shoppinCart;
        }

        #region Private members
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly ILogger<ShoppingCartService> _logger;
        private readonly string _purchaseUrl;
        #endregion
    }
}
