using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DatingProfile
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";

    public string UserId { get; set; } = "";
    public string Gender { get; set; } = ""; 
    public string Preference { get; set; } = ""; 
    public string Bio { get; set; } = "";
    public List<string> Stack { get; set; } = new();
}
