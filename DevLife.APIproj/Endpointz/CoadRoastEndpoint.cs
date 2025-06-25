using Microsoft.Extensions.Diagnostics.HealthChecks;
using DevLife.APIproj.Models;
using DevLife.APIproj.Services;

namespace DevLife.APIproj.Endpointz
{
    public static class CoadRoastEndpoint
    {
        public static void MapCoadRoastEndpoint(this WebApplication app)
        {
            app.MapPost("/api/roast", async (
                CodeRoastRequest request,
                IConfiguration config
            ) =>
            {
                if (string.IsNullOrWhiteSpace(request.Usercode) || string.IsNullOrWhiteSpace(request.Language))
                    return Results.BadRequest("language or code is missing");

                var apikey = config["OPENAI_API_KEY"];
                if (string.IsNullOrEmpty(apikey))
                    return Results.Problem("apikey is missing");

                var result = await CoadRoastService.RoastAsync(request.Usercode, request.Language, apikey);

                return Results.Ok(new { roast = result });
            });
        }
    }
}
