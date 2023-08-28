using CricketAnalysis.Models;  // This imports the Player class from the CricketAnalysis.Models namespace
using Microsoft.AspNetCore.Mvc;  // This imports the required namespaces for Razor Pages
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;  // This imports the Newtonsoft.Json library for JSON deserialization

namespace CricketWebApp.Pages.ViewPlayerstats
{
    // This defines the Top10StrikeRateModel class which inherits from PageModel
    /// <summary>
    /// Display the Most Strikerate of a player
    /// </summary>
    public class Top10StrikeRateModel : PageModel
    {
        //public void OnGet()
        //{
        //}
        // This defines a public property named Top10Strikerate which is a List of Player objects
        public List<Player> Top10Strikerate = new();
        // This is an asynchronous method called when the page is requested
        public async void OnGet()
        {
            // Creates a new HttpClient object
            using (var client = new HttpClient())
            {
                //Sets the base address for the HTTP client
                client.BaseAddress = new Uri("https://localhost:7043");
                // This sends an HTTP GET request to the "/Player/top-10-players-by-strike-rate" endpoint
                var responseTask = client.GetAsync("/Player/top-10-players-by-strike-rate");
                // This waits for the HTTP response to complete
                responseTask.Wait();

                // This gets the result of the HTTP response
                var result = responseTask.Result;
                // This checks if the HTTP response was successful
                if (result.IsSuccessStatusCode)
                {
                    // This reads the content of the HTTP response as a string
                    var readTask = await result.Content.ReadAsStringAsync();
                    // This deserializes the JSON string to a List of Player objects
                    Top10Strikerate = JsonConvert.DeserializeObject<List<Player>>(readTask);

                }
            }
        }
    }
}
