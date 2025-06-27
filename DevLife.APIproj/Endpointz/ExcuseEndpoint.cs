using DevLife.APIproj.Models;
using DevLife.APIproj.Services;

namespace DevLife.APIproj.Endpointz
{
    public static class ExcuseEndpoint
    {
        public static void MapExcuseEndpoints(this WebApplication app)
        {

            app.MapPost("/escape", async (ExcuseRequest request, IConfiguration config) =>
            {
                string apikey = config["OPENAI_API_KEY"];

                if (string.IsNullOrWhiteSpace(apikey))
                    return Results.Problem("apikey is missing :(");

                var excuse = await ExcuseService.ExcuseAsync(request.Type, apikey);

                var response = new ExcuseResponse
                {
                    ExcuseText = excuse,
                    Beliaveability = Random.Shared.Next(60, 100)
                };

                return Results.Ok(response);    

            });
        }

    }
}
