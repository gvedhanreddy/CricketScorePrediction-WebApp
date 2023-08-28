using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CricketWebApp.Pages.ViewPlayerstats
{
    // This class defines the MostFours Razor Page model
    /// <summary>
    /// Display the Most Fours Hit by a player
    /// </summary>
    public class MostFoursModel : PageModel
    {
        // Public property to hold a list of players with the most fours
        public List<Player> MostFours = new();

        // The OnGet method retrieves data from an API and populates the MostFours property
        public async void OnGet()
        {
            // Create a new HttpClient to send a GET request to the specified URL
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7043");
                var responseTask = client.GetAsync("/Player/top-10-players-who-scored-MostFours");
                responseTask.Wait();

                var result = responseTask.Result;

                // If the request was successful, deserialize the response and assign it to the MostFours property
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    MostFours = JsonConvert.DeserializeObject<List<Player>>(readTask);
                }
            }
        }
    }
}
