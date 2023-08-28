using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CricketWebApp.Pages.ViewPlayerstats
{
    /// <summary>
    /// Display the Most Indivdual Score Hit by a player
    /// </summary>
    public class Top10IndividualScoresModel : PageModel
    {
        // Define a public property to store the top 10 individual scores
        public List<Player> Top10IndividualScore = new();

        // Define the OnGet() method that will be executed when the page is requested using the GET HTTP method
        public async void OnGet()
        {
            // Create an instance of the HttpClient class to make HTTP requests
            using (var client = new HttpClient())
            {
                // Set the base address of the API that provides the data
                client.BaseAddress = new Uri("https://localhost:7043");

                // Make an HTTP GET request to the API endpoint that returns the top 10 players by highest individual score
                var responseTask = client.GetAsync("/Player/top-10-players-by-Highestindividualscore");

                // Wait for the response to be returned
                responseTask.Wait();

                // Get the actual response object from the returned task
                var result = responseTask.Result;

                // If the response indicates success
                if (result.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var readTask = await result.Content.ReadAsStringAsync();

                    // Deserialize the response content into a list of Player objects using the Newtonsoft.Json library
                    Top10IndividualScore = JsonConvert.DeserializeObject<List<Player>>(readTask);
                }
            }
        }
    }
}
