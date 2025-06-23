using DevLife.APIproj.DTO; 
using DevLife.APIproj.Data;
using DevLife.APIproj.Models;
using Microsoft.EntityFrameworkCore;

namespace DevLife.APIproj.Endpointz
{
    public  static class AuthEndpoints
    {
       public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {

            //Register Endpoint
            app.MapPost("/register", async (RegisterDto registerDto, DevLifeDbContext db) =>
            {

                if (await db.Users.AnyAsync(u => u.Username == registerDto.Username))
                {
                    return Results.Conflict("Username already exists");
                }

                var user = new User
                {
                    Username = registerDto.Username,
                    Firstname = registerDto.Firstname,
                    Lastname = registerDto.Lastname,
                    BirthDate = registerDto.BirthDate,
                    Level = registerDto.Level,
                    Stack = registerDto.Stack
                };
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return Results.Created($"/users/{user.Id}", user);
            });

            //Login Endpoint
            app.MapPost("/login", async (LoginDto loginDto, DevLifeDbContext db) =>
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
                
                if (user is null)
                {
                    return Results.NotFound("User not found");
                }
                
                return Results.Ok(user);
            });
        }


    }
}
