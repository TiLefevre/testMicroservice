using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stock.Entities;

public class Stock
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set; }
    
    [BsonElement("ProductId")] 
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    

}