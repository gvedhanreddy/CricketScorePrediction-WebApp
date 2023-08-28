using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CricketWebApp.Pages.PredictiveAnalysis
{
	/// <summary>
	/// Predict the number of wickets that will be taken by the player by giving the player name as input
	/// </summary>
    public class WicketsPredictorModel : PageModel
    {
		HttpClient client;

		//Adding the property of playername
		[BindProperty]
		public string PlayerName { get; set; }


		//Creating the Model
		public WicketsPredictorModel()
		{

			client = new HttpClient();
		}
		// public void OnGet()
		// {
		// }
		//search by player name and get the player predicted wicket stats
		public async void OnPostAsync(string PlayerName)
		{
			using (var client = new HttpClient())
			{

				client.BaseAddress = new Uri("https://localhost:7233");
				var responseTask = client.GetAsync("https://localhost:7043/Player/average-wickets-Taken/" + PlayerName);
				Console.WriteLine(responseTask);

				// waiting for response

				responseTask.Wait();
				//get the response from the API if it is succuessfull
				var result = responseTask.Result;
				
				// checking the result value

				if (result.IsSuccessStatusCode)
				{
					var readTask = await result.Content.ReadAsStringAsync();

					ViewData["Result"] = readTask;
				}

			}

		}
	}
}
