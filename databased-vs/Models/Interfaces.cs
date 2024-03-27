using MongoDB.Bson;

namespace databased_vs.Models;

public interface IHasId
{
    public ObjectId _id { get; set; }
}
