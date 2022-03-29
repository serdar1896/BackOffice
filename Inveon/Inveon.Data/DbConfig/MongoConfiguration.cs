using Inveon.Core.Interfaces.DbConfigs;

namespace Inveon.Data.DbConfig
{
    public class MongoConfiguration:IMongoConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
