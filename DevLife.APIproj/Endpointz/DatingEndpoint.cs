using MongoDB.Driver;
using System.Linq; 

public static class DatingEndpoints
{
    public static void MapDatingEndpoints(this WebApplication app)
    {
        app.MapPost("/api/dating/profile", async (DatingProfile profile, MongoDbContext db) =>
        {
            await db.DatingProfiles.InsertOneAsync(profile);
            return Results.Ok();
        });

        app.MapGet("/api/dating/matches/{userId}", async (string userId, MongoDbContext db) =>
        {
            var me = await db.DatingProfiles.Find(p => p.UserId == userId).FirstOrDefaultAsync();
            if (me == null) return Results.NotFound("Profile not found");

            var matches = await db.DatingProfiles
                .Find(p => p.UserId != userId &&
                           p.Gender == me.Preference &&
                           p.Preference == me.Gender &&
                           p.Stack.Any(s => me.Stack.Contains(s)))
                .ToListAsync();

            return Results.Ok(matches);
        });
    }
}
