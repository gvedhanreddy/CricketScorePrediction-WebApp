using CricketAnalysis.Models; // This namespace contains the Player class
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json; // This namespace is used to deserialize JSON

namespace CricketWebApp.Pages.ViewPlayerstats
{
    /// <summary>
    /// Display the Most Fifties Hit by a player
    /// </summary>
    public class MostFiftyModel : PageModel
    {
        // This public property will hold a list of players who scored the most fifties
        public List<Player> MostFifty = new();

        // This method is called when the page is loaded
        public async void OnGet()
        {
            // Create a new HttpClient instance
            using (var client = new HttpClient())
            {
                // Set the base address of the API endpoint
                client.BaseAddress = new Uri("https://localhost:7043");
                
                // Send a GET request to the specified URL
                var responseTask = client.GetAsync("/Player/players-who-scored-MostFifty");
                responseTask.Wait();

                // Get the response
                var result = responseTask.Result;
                
                // If the response is successful
                if (result.IsSuccessStatusCode)
                {
                    // Read the response content as a JSON string
                    var readTask = await result.Content.ReadAsStringAsync();
                    
                    // Deserialize the JSON string into a list of Player objects
                    MostFifty = JsonConvert.DeserializeObject<List<Player>>(readTask);
                }
            }
        }
    }
}
