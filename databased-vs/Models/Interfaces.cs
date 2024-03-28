using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace databased_vs.Models;

public interface IHasId
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public ObjectId _id { get; set; }
}
