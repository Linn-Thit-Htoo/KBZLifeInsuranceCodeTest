using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.Shared.Services.CacheServices
{
    public class RedisService
    {
        private readonly IConfiguration _configuration;
        private readonly int _redisExpirationLimit;

        public RedisService(IConfiguration configuration)
        {
            _configuration = configuration;
            _redisExpirationLimit = Convert.ToInt32(_configuration.GetSection("RedisCacheExpirationLimit"));
        }

        public async Task SetAsync<T>(string key, object value)
        {
            try
            {
                var configuration = ConfigurationOptions.Parse("localhost:6379");
                var redisConnection = await ConnectionMultiplexer.ConnectAsync(configuration);

                var redisCache = redisConnection.GetDatabase();
                await redisCache.StringSetAsync(key, value.ToJson(), TimeSpan.FromMinutes(_redisExpirationLimit));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> GetAsync<T>(string key)
        {
            try
            {
                var configuration = ConfigurationOptions.Parse("localhost:6379");
                var redisConnection = await ConnectionMultiplexer.ConnectAsync(configuration);

                var redisCache = redisConnection.GetDatabase();
                string? jsonStr = await redisCache.StringGetAsync(key);
                ArgumentNullException.ThrowIfNullOrEmpty(jsonStr);

                return jsonStr.ToObject<List<T>>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
