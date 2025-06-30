using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MONGODB_URL"]);
        _database = client.GetDatabase("devlife");
    }

    public IMongoCollection<DatingProfile> DatingProfiles =>
        _database.GetCollection<DatingProfile>("dating_profiles");
}
 