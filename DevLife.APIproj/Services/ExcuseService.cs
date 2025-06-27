using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace DevLife.APIproj.Services
{
    public class ExcuseService
    {
        public static async Task <string> ExcuseAsync(string Type , string apikey)
        {
            var prompt = $" მოიფიქრე კრეატიული მიზეზი იმისთვის , რომ {Type} შეხვედრაზე არ წასასვლელად," +
                $" შეეცადე ცოტა სარკასტული პასუხიც დააბრუნო";


            var request = new
            {
                model = "gpt-3.5-turbo",
                messege = new[]
                {
                    new { role = "system", content = "კრეატიული და პოზიტიური დამხმარე ხარ" },
                    new { role= "user", content = prompt}
                }

            };

            using var http = new HttpClient();

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apikey);

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("https://api.openai.com/v1/chat/completions", content);


            if (!response.IsSuccessStatusCode) return $"error  {response.StatusCode}";

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

            var root = jsonDoc.RootElement;

            var messages = root
               .GetProperty("choices")[0]
               .GetProperty("message")
               .GetProperty("content")
               .GetString();

            if (string.IsNullOrEmpty(messages)) return "its empty";

            return messages;

        } 
    }
}
