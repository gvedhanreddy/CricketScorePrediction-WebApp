// Importing required namespaces
using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CricketWebApp.Pages.ViewPlayerstats
{
    /// <summary>
    /// Search player by name get all player stats
    /// </summary>
    public class SearchByNameModel : PageModel
    {
        HttpClient client;
        // Constructor to initialize an instance of HttpClient
        public SearchByNameModel()
        {
            client = new HttpClient();
        }
        // Binding the search string entered by the user
        [BindProperty]
        public string searchString { get; set; }
        // public void OnGet()
        //{
        //}
        // Player object to store the search results
        public Player Modeldata = new Player();
        // Method to handle POST requests for the search form
        public async Task OnPostAsync()
        {
            using (var client = new HttpClient())
            {
                // Setting the base address for the HTTP request
                client.BaseAddress = new Uri("https://localhost:7043");
                // Sending an HTTP GET request to the PlayerController to search for the entered player name
                var responseTask = await client.GetAsync($"/Player/searchbyname?name={searchString}");

                if (responseTask.IsSuccessStatusCode)
                {
                    // Reading the HTTP response message and deserializing the JSON data to a list of Player objects
                    var readTask = await responseTask.Content.ReadAsStringAsync();
                    var playerList = JsonConvert.DeserializeObject<List<Player>>(readTask);
                     // Storing the first player in the search results in the Modeldata object
                    Modeldata = playerList.FirstOrDefault();
                }
            }
        }
    
    }
}
