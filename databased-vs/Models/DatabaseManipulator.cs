using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.Xml;

namespace databased_vs.Models
{
	public class DatabaseManipulator
	{
		private static string? DATABASE_NAME;
		private static string? HOST;
		private static IConfiguration config;
		// needed for connection
		private static MongoServerAddress? address;
		private static MongoClientSettings? clientSettings;
		private static MongoClient? client;
		private static IMongoDatabase? database;

		public static void Initialize(IConfiguration configuration)
		{
			config = configuration;
			// kinda like using a .env file, but instead we search from appsettings.json
			var sections = config.GetSection("ConnectionStrings");
			DATABASE_NAME = sections.GetValue<string>("DatabaseName");
			HOST = sections.GetValue<string>("MongoConnection");

			// initializing the database settings for the client
			address = new MongoServerAddress(HOST);
			clientSettings = new MongoClientSettings() { Server = address };
			client = new MongoClient(clientSettings);

			// get the database from the client
			database = client.GetDatabase(DATABASE_NAME);
		}

		public static T Save<T>(string table, T record)
		{
			try
			{
				var mongoCollection = database?.GetCollection<T>(table);
				mongoCollection?.InsertOne(record);
				// tehtävänanto: tallenna funktio pelkällä yhdellä objekti argumentilla.
				// niin että se osaaa päivittää ja tallentaa uusia (Insert/Replace)
			} catch
			{
				Console.WriteLine("You fucked up lmao");
			}
			return record;
		}

		public static T GetByObjectId<T>(string table, ObjectId id)
		{
			var collection = database.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("_id", id);
			var result = collection.Find(filter).FirstOrDefault();
			return result;
		}

		public static List<T> GetAll<T>(string table)
		{
			var collection = database.GetCollection<T>(table);
			// new bsondocument sets the find to everything
			return collection.Find(new BsonDocument()).ToList();
		}
	}
}
