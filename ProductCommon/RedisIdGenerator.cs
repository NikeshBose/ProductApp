using StackExchange.Redis;

namespace ProductCommon
{
    public class RedisIdGenerator
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisIdGenerator(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
        }

        public async Task<int> GenerateUniqueProductIdAsync()
        {
            var newId = await _db.StringIncrementAsync("ProductIdSequence");
            return (int)newId;
        }
    }
}
