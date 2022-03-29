using MongoDB.Bson.Serialization.Attributes;

namespace Inveon.Core.Models.Entities
{
    public class Product:BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Barcode")]
        public string Barcode { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("ProductImage")]
        public ProductImage ProductImage { get; set; }
    }
    public class ProductImage
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Alt { get; set; }

    }
}
