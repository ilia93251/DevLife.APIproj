using Microsoft.AspNetCore.SignalR;

namespace DevLife.APIproj.Models
{
    public class BugChaseHub: Hub 
    {

           public async Task BroadcastNewScore(string Username, int Score)
        {
            await Clients.All.SendAsync("NewScore", Username, Score);
        }
    }
}
