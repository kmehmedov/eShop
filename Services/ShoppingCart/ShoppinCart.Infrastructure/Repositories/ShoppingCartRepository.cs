using Microsoft.Extensions.Logging;
using ShoppingCart.Domain.Models.ShoppingCart;
using StackExchange.Redis;
using System.Text.Json;

namespace ShoppingCart.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCartRepository(ILogger<ShoppingCartRepository> logger, ConnectionMultiplexer redis)
        {
            _logger=logger;
            _database=redis.GetDatabase();
            _redis=redis;
        }
        public Task CreateAsync(Domain.Models.ShoppingCart.ShoppingCart item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<Domain.Models.ShoppingCart.ShoppingCart> GetAsync(string id)
        {
            var data = await _database.StringGetAsync(id);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Domain.Models.ShoppingCart.ShoppingCart>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Domain.Models.ShoppingCart.ShoppingCart> UpdateAsync(Domain.Models.ShoppingCart.ShoppingCart item)
        {
            var created = await _database.StringSetAsync(item.BuyerId, JsonSerializer.Serialize(item, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }));

            if (!created)
            {
                _logger.LogInformation("Problem occur persisting the item.");
                return null;
            }

            _logger.LogInformation("Basket item persisted successfully.");

            return await GetAsync(item.BuyerId);
        }

        #region Private members
        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

        private readonly ILogger<ShoppingCartRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        #endregion
    }
}
