using StackExchange.Redis;
using System.Threading.Tasks;

namespace Inveon.Core.Interfaces.DbConfigs
{
    public interface IRedisConfiguration
    {
        IDatabase Database { get; }
        IServer Server { get;  }
        Task FlushDatabaseAsync();

    }
}
