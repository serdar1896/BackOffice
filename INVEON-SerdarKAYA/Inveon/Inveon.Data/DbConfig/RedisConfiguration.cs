using Inveon.Core.Interfaces.DbConfigs;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Inveon.Data.DbConfig
{
    public class RedisConfiguration:IRedisConfiguration
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private string configurationString;
        public RedisConfiguration(IConfiguration configuration)
        {           
            CreateRedisConfigurationString(configuration);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationString);
            Database = _connectionMultiplexer.GetDatabase();
        }

        public IDatabase Database { get; }
        public IServer Server=>_connectionMultiplexer.GetServer(configurationString);

        private void CreateRedisConfigurationString(IConfiguration configuration)
        {
            string host = configuration.GetSection("RedisConfiguration:Host").Value;
            string port = configuration.GetSection("RedisConfiguration:Port").Value;
            string password = configuration.GetSection("RedisConfiguration:Password").Value;
            configurationString = $"{host}:{port},password={password}";
        }

        public async Task FlushDatabaseAsync()
        {
           await _connectionMultiplexer.GetServer(configurationString).FlushDatabaseAsync();
        }

    }
}