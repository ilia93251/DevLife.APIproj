using System.Net.Http.Headers;
using System.Text.Json;

namespace DevLife.APIproj.Services
{
    public class CoadRoastService
    {

        public static async Task<string>RoastAsync(string Language, string Usercode, string apikey)
        {

            var prompt = $" შენ ახლა ხარ კოდის ექსპერტი. შეაფასე ეს კოდი :{Usercode},კოდის ენა: {Language} და სარკასტული პასუხი დააბრუნე," +
                $"იუმორი ჩაურთე. აი პასუხების მაგალითს მოგცემ: როუსტინგი თუ ცუდია: " +
                $"ეს კოდი ისე ცუდია, კომპილატორმა დეპრესია დაიწყოr შექება თუ კარგია: ბრავო! ამ კოდს ჩემი ბებიაც დაწერდა," +
                $" მაგრამ მაინც კარგია ";

            var request = new
            {
                model = "gpt-3.5-turbo",
                messege = new[]
                {
                    new { role = "system", content = "შენ ხარ პროფესიონალი დეველოპერი, რომელიც ქართულად" +
                    "სახალისოდ აფასებს კოდს" },
                    new { role = "user", content = prompt }
                }

            };

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apikey);

            var content = new StringContent(JsonSerializer.Serialize(request));
            var response = await http

        } 
    }
}
