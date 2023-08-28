
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApp.Pages.PredictiveAnalysis
{
    /// <summary>
    /// Predict the number of runs scored by the player by giving the player name as input
    /// </summary>
    public class AverageRunsScoredModel : PageModel
    {
        /// setting up the client
        HttpClient client;

        // adding property to display player results
        public string PlayerName { get; set; }

        // adding property to display player results
        [BindProperty]
        public string AverageRunsScored { get; set; }


        /// Creating object of HttpClient
        public AverageRunsScoredModel()
        {

            client = new HttpClient();
        }

        // public async Task OnGetAsync()
        // {
           
        // }

        /// Posting the results of the predictive analysis
        public async void OnPostAsync(string PlayerName)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7233");

                /// Get response by searching the player name from the database
                var responseTask = client.GetAsync("https://localhost:7043/Player/average-run-scored/" + PlayerName);
                Console.WriteLine(responseTask);
                responseTask.Wait();

                var result = responseTask.Result;
                //if response is succcusesfull display it
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();

                    ViewData["Result"]= readTask; 
                }

            }

        }
    }
}
