using Microsoft.Extensions.Options;
using System.Text.Json;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class OrderService : IOrderService
    {
        public OrderService(HttpClient httpClient, ILogger<OrderService> logger, IOptions<AppSettings> settings)
        {
            _httpClient=httpClient;
            _logger=logger;
            _settings=settings;

            _remoteServiceBaseUrl = $"{_settings.Value.GatewayUrl}/o/api/order/";
        }
        public async Task<Order> CreateDraftFromShoppinCartAsync(ShoppingCart shoppingCart)
        {
            var uri = $"{_remoteServiceBaseUrl}draft";
            var model = new OrderItemDraft()
            {
                BuyerId = shoppingCart.BuyerId,
                OrderItems = new()
            };

            model.OrderItems.AddRange(shoppingCart.Items.Select(x => new OrderItem { ProductId = x.ProductId, ProductName = x.ProductName, UnitPrice = x.UnitPrice, UnitQuantity = x.UnitQuantity }));

            var response = await _httpClient.PostAsJsonAsync(uri, model);
            var responseString = await response.Content.ReadAsStringAsync();

            var order = JsonSerializer.Deserialize<Order>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return order;
        }

        public async Task<Order> Create(Order order)
        {
            var uri = $"{_remoteServiceBaseUrl}";

            var response = await _httpClient.PostAsJsonAsync(uri, order);
            var responseString = await response.Content.ReadAsStringAsync();

            var createdOrder = JsonSerializer.Deserialize<Order>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createdOrder;
        }

        public async Task Cancel(int id)
        {
            var uri = $"{_remoteServiceBaseUrl}cancel";

            var response = await _httpClient.PostAsJsonAsync(uri, new { Id = id });

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Order>> GetAll(AppUser user)
        {
            var uri = $"{_remoteServiceBaseUrl}buyer/{user.Id}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return orders;
        }

        #region Private members
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;

        private readonly string _remoteServiceBaseUrl;
        #endregion
    }

    public record OrderItemDraft
    {
        public string BuyerId { get; set; }
public List<OrderItem> OrderItems { get; set; }
    }
}
