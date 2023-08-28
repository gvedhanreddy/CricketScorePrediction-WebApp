using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json;

namespace CricketWebApp.Pages.ViewPlayerstats
{

    // This is a C# class named Top10RunScorersModel which is a PageModel
    /// <summary>
    /// Display the Most Run getter by a player
    /// </summary>
  public class Top10RunScorersModel : PageModel
  {
    // This is a public property named Top10RunScorers which is a List of Player objects
    public List<Player> Top10RunScorers = new();

    // This is an asynchronous method named OnGet which is called when the page is requested
    public async void OnGet()
    {
        // This creates a new HttpClient object
        using (var client = new HttpClient()) 
        {
            // This sets the base address for the HTTP client
            client.BaseAddress = new Uri("https://localhost:7043");

            // This sends an HTTP GET request to the "/Player/top-10-run-scorers" endpoint
            var responseTask = client.GetAsync("/Player/top-10-run-scorers");

            // This waits for the HTTP response to complete
            responseTask.Wait();

            // This gets the result of the HTTP response
            var result = responseTask.Result;

            // This checks if the HTTP response was successful
            if(result.IsSuccessStatusCode)
            {
                // This reads the content of the HTTP response as a string
                var readTask = await result.Content.ReadAsStringAsync();

                // This deserializes the JSON string to a List of Player objects
                Top10RunScorers = JsonConvert.DeserializeObject<List<Player>>(readTask);
            }
        }
    }
  }
}
        //private readonly HttpClient _httpClient;
        //public Top10RunScorersModel(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        //public List<Player> Top10RunScorers { get; set; }
        ////public void OnGet()
        ////{
        ////}
        //public async Task OnGetAsync()
        //{
        //    var response = await _httpClient.GetAsync("https://localhost:7043/Player/top-10-run-scorers");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //        Top10RunScorers = JsonSerializer.Deserialize<List<Player>>(json, options);
        //    }
        //    else
        //    {
        //        Top10RunScorers = new List<Player>();
        //    }
        //}
    

