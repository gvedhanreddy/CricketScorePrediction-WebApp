using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CricketWebApp.Pages.PredictiveAnalysis
{
    /// <summary>
    /// Calculate the boundary percentage for the player to get insights on his 4s and 6s hitting abilities
    /// </summary>
    public class BoundaryPercentageModel : PageModel
    {
        HttpClient client;

        //Add property to the player
        [BindProperty]
        public string PlayerName { get; set; }

        //property to display the Boundarypercentage Data model
        [BindProperty]
        public List<BounderyPercentageDataModel> DataModel { get; set; }

        //adding Model to display the boundary percentage
        public BoundaryPercentageModel()
        {

            client = new HttpClient();
        }
        public void OnGet()
        {
        }
        //search by player name and get the player boundary hitting stats
        public async void OnPostAsync(string PlayerName)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7233");

                var responseTask = client.GetAsync("https://localhost:7043/Player/BoundaryPercentageByName?search="+PlayerName);
                Console.WriteLine(responseTask);

                // waiting for response

                responseTask.Wait();

                var result = responseTask.Result;

                // checking the result value

                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                     DataModel = JsonConvert.DeserializeObject<List<BounderyPercentageDataModel>>(readTask);

                }

            }
        
        }
    }

    //Property to display the boundary percentage
    public class BounderyPercentageDataModel
    {
        // defining data variables
        public int playerId { get; set; }       
        public string PlayerName { get; set;}
        public double boundaryPercentage { get; set;}
    }
        


}
