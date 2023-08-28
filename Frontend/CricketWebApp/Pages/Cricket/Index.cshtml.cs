using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CricketWebApp.Pages.Cricket
{
    /// <summary>
    /// Show player details in the Homepage with all the stats
    /// </summary>
    public class IndexModel : PageModel
    {

        public List<Player> Players = new();
        // This method is executed when a GET request is made to the page
        public async void OnGet()
        {
            // Creating a new HTTP client
            using (var client = new HttpClient())
            {
                // Setting the base address of the API
                client.BaseAddress = new Uri("http://localhost:5293");
                // Sending a GET request to the API to retrieve all Players
                var responseTask = client.GetAsync("https://localhost:7043/Player");
                Console.WriteLine(responseTask);
                 // Wait for the response to complete
                responseTask.Wait();
                // Get the response object from the result
                var result = responseTask.Result;
                // Checking if the response was successful
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                     // Deserialize the response content into a list of Player objects
                    Players = JsonConvert.DeserializeObject<List<Player>>(readTask);
                }

            }
        }
    }
    
}
