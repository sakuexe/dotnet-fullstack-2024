using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace databased_vs.Models
{
	public class User : IHasId
	{
		[BsonId] // this is the main id of the collection item
		[BsonRepresentation(BsonType.String)] // uses the id as a string, instead of a complex class
		public ObjectId _id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

        private readonly Random _random = new Random();
        private readonly List<string> _names = new List<string>(){
            "Pertti", "Paavo", "Maksimus", "Saku", "Sienimies", "Amogustus",
            "Jeso'n", "Patrik", "Frank", "Matt", "Homer",
        };

        public User(string? name = null, string? email = null, string? password = null)
        {
            _id = ObjectId.GenerateNewId();
            Name = name ?? _names[_random.Next(0, _names.Count)];
            Email = email ?? $"{Name.ToLower()}{_random.Next(100)}@example.com";
            Password = password ?? $"not-safe-{_random.Next(1000, 9999)}";
        }
	}
}
