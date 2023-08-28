using CricketAnalysis.Models; // import the namespace for Player class
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json; // import Newtonsoft.Json for JSON deserialization

namespace CricketWebApp.Pages.ViewPlayerstats
{
    /// <summary>
    /// Display the Most Hundered Hit by a player
    /// </summary>
    public class MostHundredsModel : PageModel // define a Razor Page model
    {
        // public void OnGet()
        //{
        //}

        public List<Player> MostHundreds = new(); // define a public property to store player data

        public async void OnGet() // called when the page is requested
        {
            // create a new HttpClient instance
            using (var client = new HttpClient()) 
            {
                // set the base URI
                client.BaseAddress = new Uri("https://localhost:7043");
                // send an HTTP GET request to the server
                var responseTask = client.GetAsync("/Player/players-who-scored-MostHundreds");
                // wait for the response
                responseTask.Wait();
                // get the response
                var result = responseTask.Result;
                 // if the request was successful
                if (result.IsSuccessStatusCode)
                {
                    // read the response body
                    var readTask = await result.Content.ReadAsStringAsync();
                    // deserialize the JSON data into a List<Player> object
                    MostHundreds = JsonConvert.DeserializeObject<List<Player>>(readTask);

                }
            }
        }
    }
}
