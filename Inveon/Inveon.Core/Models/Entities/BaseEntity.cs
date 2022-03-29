using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inveon.Core.Models.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
