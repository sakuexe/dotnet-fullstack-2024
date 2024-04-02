using MongoDB.Driver;
using blazeit.Models;
using MongoDB.Bson;

namespace blazeit.Services;

public class DatabaseManipulator
{
    // connection settings
    private static string? DATABASE_NAME;
    private static string? HOST;
    // the configuration objects
    private static IConfiguration? config;
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

    public static T Save<T>(T record) where T : IHasId
    {
        // use the classname of the passed object as the table name
        var table = typeof(T).Name;
        try
        {
            var mongoCollection = database?.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", record._id);
            // use the upsert option. If the record is not found, it will be inserted
            var usingUpsert = new ReplaceOptions { IsUpsert = true };
            // upsert the record (update-insert)
            mongoCollection?.ReplaceOne(filter, record, usingUpsert);
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue");
            Console.WriteLine(e.Message);
        }
        return record;
    }

    public static T? FindOne<T>(string property, string value)
    {
        var table = typeof(T).Name;
        T? result = default;
        try
        {
            var collection = database?.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq(property, value);
            result = collection.Find(filter).FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue");
            Console.WriteLine(e.Message);
        }
        return result;
    }

    // find many version of the search function above
    public static List<T>? FindMany<T>(string property, string value)
    {
        var table = typeof(T).Name;
        List<T>? result = default;
        try
        {
            var collection = database?.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq(property, value);
            result = collection.Find(filter).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue");
            Console.WriteLine(e.Message);
        }
        return result;
    }

    public static List<T>? FindAll<T>()
    {
        var table = typeof(T).Name;
        List<T>? result = default;
        try
        {
            var collection = database?.GetCollection<T>(table);
            result = collection.Find(new BsonDocument()).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Skill Issue");
            Console.WriteLine(e.Message);
        }
        return result;
    }
}