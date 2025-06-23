using DevLife.APIproj.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DevLife.APIproj.Services
{

    namespace DevLife.APIproj.Services
    {
        public static class CodeChallengeService
        {
            public static async Task<CasinoCodeChallenge?> GenerateChallengeFromAI(string stack, string apiKey)
            {
                var message = $"Give me two code snippets in {stack}. One should be correct and functional. The other should have a common bug. " +
                              "Return the result as JSON with fields: correctCode and buggyCode.";

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                    new { role = "system", content = "You are a code generator for developers." },
                    new { role = "user", content = message }
                }
                };

                var http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await http.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;

                try
                {
                    var doc = JsonDocument.Parse(json);
                    var contentText = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                 
                    if (contentText is null)
                        return null;

                    return JsonSerializer.Deserialize<CasinoCodeChallenge>(contentText);
                }
                catch
                {
                    return null;
                }
            }
        }
    }


}