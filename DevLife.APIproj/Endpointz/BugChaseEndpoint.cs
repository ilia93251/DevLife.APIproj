using System.Runtime.CompilerServices;
using DevLife.APIproj.Models;
using DevLife.APIproj.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace DevLife.APIproj.Endpointz
{
    public static class BugChaseEndpoint
    {

        public static void MapBugChaseEndpoints (this WebApplication app)
        {
            app.MapPost("/bugchase/score", async (
                BugChaseScore incomingScore, 
                DevLifeDbContext db,
                IHubContext<BugChaseHub> hub,
                HttpContext http) =>

            {

                var username = http.Session.GetString("username");
                if (string.IsNullOrEmpty(username))
                    return Results.Unauthorized();

                if (incomingScore.Score <= 0 || string.IsNullOrEmpty(incomingScore.Username))
                    return Results.BadRequest("incorrect username or score");

                db.BugChaseScores.Add(incomingScore);
                await db.SaveChangesAsync();

                await hub.Clients.All.SendAsync("Newscore", incomingScore.Username, incomingScore.Score);

                return Results.Ok(new { message = "Score saved!" });


            });

            app.MapGet("/bugchase/leaderboard", async (DevLifeDbContext db) =>
            {
                var topscorers = await db.BugChaseScores
                .OrderByDescending(s => s.Score)
                .Take(10)
                .ToListAsync();

                return Results.Ok(topscorers);
            });
        }
    }
}
