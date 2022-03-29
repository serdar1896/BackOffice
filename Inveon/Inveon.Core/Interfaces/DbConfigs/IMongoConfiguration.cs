namespace Inveon.Core.Interfaces.DbConfigs
{
    public interface IMongoConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
