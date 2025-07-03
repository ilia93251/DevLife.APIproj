using DevLife.APIproj.Data;
using DevLife.APIproj.DTO; 
using DevLife.APIproj.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

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

                var birthDateUtc = DateTime.SpecifyKind(registerDto.BirthDate, DateTimeKind.Utc);
                var user = new User
                {
                    Username = registerDto.Username,
                    Firstname = registerDto.Firstname,
                    Lastname = registerDto.Lastname,
                    BirthDate = birthDateUtc,
                    Level = registerDto.Level,
                    Stack = registerDto.Stack,
                    ZodiacSign = ZodiacHelper.GetZodiacSign(registerDto.BirthDate)
                };
                try
                {
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error saving user: {ex.InnerException?.Message ?? ex.Message}");
                }

                return Results.Created($"/users/{user.Id}", user);
            });

            //Login Endpoint
            app.MapPost("/login", async (LoginDto loginDto, DevLifeDbContext db, HttpContext http) =>
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
                
                if (user is null)
                {
                    return Results.NotFound("User not found");
                }

                http.Session.SetString("username", user.Username);

                return Results.Ok(user);
            });
        }


    }
}
