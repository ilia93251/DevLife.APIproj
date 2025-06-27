using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DevLife.APIproj.Endpoints
{
    public static class GitHubEndpoints
    {
        public static void MapGitHubEndpoints(this WebApplication app)
        {
            
            app.MapGet("/github/login", (IConfiguration config) =>
            {
                var clientId = config["GITHUB_CLIENT_ID"];
                var redirectUri = "http://localhost:5000/github/callback";

                var githubAuthUrl = $"https://github.com/login/oauth/authorize" +
                    $"?client_id={clientId}" +
                    $"&redirect_uri={redirectUri}" +
                    $"&scope=repo";

                return Results.Redirect(githubAuthUrl);
            });

            app.MapGet("/github/callback", async (string code, IConfiguration config) =>
            {
                var clientId = config["GITHUB_CLIENT_ID"];
                var clientSecret = config["GITHUB_CLIENT_SECRET"];

                using var http = new HttpClient();

                var requestData = new Dictionary<string, string>
                {
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "code", code }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "https://github.com/login/oauth/access_token")
                {
                    Content = new FormUrlEncodedContent(requestData)
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await http.SendAsync(request);
                var content = await response.Content.ReadFromJsonAsync<JsonElement>();

                var accessToken = content.GetProperty("access_token").GetString();

                if (string.IsNullOrWhiteSpace(accessToken))
                    return Results.Problem("Couldn't retrieve GitHub access token.");

                return Results.Ok(new { access_token = accessToken });
            });
        }
    }
}
