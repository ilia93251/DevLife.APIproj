using DevLife.APIproj.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DevLife.APIproj.Services
{
    public static class CodeChallengeService
    {

        public static async Task <CasinoCodeChallenge> GenerateChallengeFromAI(string stack, string apiKey)
        {
            var prompt = $"Give me two code snippets in {stack}." +
                $" One correct, one with a common bug. Return JSON with fields: correctCode and buggyCode.";

            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
        {
            new { role = "system", content = "You are a code generator for developers." },
            new { role = "user", content = prompt}
        }
            };

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var jsonRequest = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("https://api.openai.com/v1/chat/completions", jsonRequest);

            if (!response.IsSuccessStatusCode) return null;

            var jsonResponse = await response.Content.ReadAsStringAsync();

            try
            {
                var content = JsonDocument.Parse(jsonResponse)
                    .RootElement.GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                if (string.IsNullOrWhiteSpace(content)) return null;

                var cleaned = content.Trim('`').Trim();
                return JsonSerializer.Deserialize<CasinoCodeChallenge>(cleaned);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return null;
            }
        }
    }
}
