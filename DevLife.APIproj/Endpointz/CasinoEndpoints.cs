using DevLife.APIproj.Data;
using DevLife.APIproj.Models;
using System;
using System.Runtime.CompilerServices;
using DevLife.APIproj.Services;
using Microsoft.EntityFrameworkCore;

namespace DevLife.APIproj.Endpointz
{
    public static class CasinoEndpoints
    {

        public static void MapCasinoEndpoints(this WebApplication app)
        {
            app.MapPost("/api/casino/submit", async (
                  CodeGuessRequest request,
                  IConfiguration config,
                  DevLifeDbContext db
                  ) =>
            {
                var apikey = config["OPENAI_API_KEY"];
                if (string.IsNullOrEmpty(apikey)) 
                    return Results.Problem("Missing ApiKey");

                var user = await db.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
                if (user == null) 
                    return Results.NotFound("User not found");

                var challenge = await CodeChallengeService.GenerateChallengeFromAI(request.Stack, apikey);
                if (challenge is null) 
                    return Results.Problem("Chellenged couldnt be generated");

                bool IsCorrect = request.SelectedCode.Trim() == challenge.CorrectCode.Trim();

                if (IsCorrect) user.Coins += request.Bet;
                else user.Coins -= request.Bet;

                await db.SaveChangesAsync();

                return Results.Ok(new
                {
                    success = IsCorrect,
                    newscore = user.Coins
                });
            });
        } 
    
    }
}
