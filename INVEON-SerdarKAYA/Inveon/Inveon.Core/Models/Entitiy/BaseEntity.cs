using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inveon.Core.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
