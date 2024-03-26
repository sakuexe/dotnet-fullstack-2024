using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace databased_vs.Models
{
	public class User
	{
		[BsonId] // this is the main id of the collection item
		[BsonRepresentation(BsonType.String)] // uses the id as a string, instead of a complex class
		public ObjectId _id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
