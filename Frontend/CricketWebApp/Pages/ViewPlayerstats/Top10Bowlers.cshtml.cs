using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CricketWebApp.Pages.ViewPlayerstats
{
    /// <summary>
    /// Display the Most Wickets taken by a player
    /// </summary>
    public class Top10BowlersModel : PageModel
    {
        // public void OnGet()
        //{
        //}

        // List of top 10 bowlers will be stored here
        public List<Player> Top10Bowlers = new();

        // OnGet method is called when the page is loaded
        public async void OnGet()
        {
            // Initialize a new instance of HttpClient
            using (var client = new HttpClient())
            {
                // Set the base address for the API
                client.BaseAddress = new Uri("https://localhost:7043");

                // Send a GET request to retrieve the top 10 wicket takers from the API
                var responseTask = client.GetAsync("/Player/top-10-wicket-takers");
                responseTask.Wait();

                var result = responseTask.Result;

                // If the request is successful, deserialize the JSON response and store it in Top10Bowlers
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    Top10Bowlers = JsonConvert.DeserializeObject<List<Player>>(readTask);

                }
            }
        }
    }
}
